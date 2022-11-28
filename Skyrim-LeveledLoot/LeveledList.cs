using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.FormKeys.SkyrimLE;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;

using Form = Mutagen.Bethesda.Plugins.IFormLink<Mutagen.Bethesda.Plugins.Records.IMajorRecordGetter>;

namespace LeveledLoot {
    class LeveledList {

        static string prefix = "JLL_";
        public static int NUM_CHILDREN = 10;
        public static int FACTOR_JUNK = 1;
        public static int FACTOR_COMMON = 2;
        public static int FACTOR_RARE = 3;
        public static int FACTOR_BEST = 4;
        static int MAX_DEPTH = 3;
        static int MAX_LEAVES = (int)Math.Pow(NUM_CHILDREN, MAX_DEPTH);
        static int[] LEVEL_LIST = new int[] { 1, 4, 7, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 70, 80, 90, 100 };
        static List<FormLink<ILeveledItemGetter>> lockedLists = new();
        static LeveledItem? dummyList;

        static HashSet<IFormLinkGetter<ILeveledItemGetter>> adjustedWeaponEnchLists = new();
        public static void AdjustWeaponEnchList(IFormLinkGetter<ILeveledItemGetter> vanillaList) {
            if(adjustedWeaponEnchLists.Contains(vanillaList)) {
                return;
            }
            adjustedWeaponEnchLists.Add(vanillaList);
            var leveledItemGetter = vanillaList.Resolve(Program.state.LinkCache);
            var entries = leveledItemGetter.Entries;
            if(entries == null || entries.Count() == 0) {
                return;
            }
            var first = entries.First().Data!.Reference.Resolve(Program.state.LinkCache);
            if(first is ILeveledItemGetter) {
                foreach(var entry in entries) {
                    var entryRef = entry.Data!.Reference.Resolve(Program.state.LinkCache);
                    if(entryRef is ILeveledItemGetter entryLeveledItem) {
                        AdjustWeaponEnchList(entryLeveledItem.ToLink());
                    }
                }
            } else if(first is IWeaponGetter) {
                var editList = Program.state.PatchMod.LeveledItems.GetOrAddAsOverride(leveledItemGetter);
                editList.Flags = LeveledItem.Flag.CalculateFromAllLevelsLessThanOrEqualPlayer;
                var enchList = new List<IFormLink<IWeaponGetter>>();
                foreach(var entry in editList.Entries!) {
                    var entryRef = entry.Data!.Reference.Resolve(Program.state.LinkCache);
                    if(entryRef is IWeaponGetter entryWeapon) {
                        enchList.Add(entryWeapon.ToLink());
                    } else {
                        throw new Exception("Must only contain weapons!");
                    }
                }
                editList.Entries = new Noggog.ExtendedList<LeveledItemEntry>();
                int weight = enchList.Count;
                foreach(var weapon in enchList) {
                    for(int i = 0; i < weight; ++i) {
                        editList.Entries.Add(new LeveledItemEntry() {
                            Data = new LeveledItemEntryData() {
                                Count = 1,
                                Level = 1,
                                Reference = weapon
                            }
                        });
                    }
                    weight--;
                }
            }
        }

        public static Form CreateSubList(Enum itemType, int level, string name, IEnumerable<ItemMaterial> materials, params LootRQ[] requirements) {
            double totalChance = 0;
            List<double> itemChancesDouble = new();
            List<int> itemChancesInt = new();
            List<Form> newItemList = new();
            foreach(ItemMaterial itemMaterial in materials) {
                HashSet<LootRQ> commonRequirements = itemMaterial.requirements.Intersect(requirements).ToHashSet();
                if(!commonRequirements.SetEquals(itemMaterial.requirements)) {
                    continue;
                }
                Form? f = itemMaterial.GetItem(itemType);
                if(f == null) {
                    continue;
                }
                newItemList.Add(f);

                // linear weight x between 0 and 1 depending on player level and first and last level of the item
                // 0 -> start chance
                // 1 -> end chance
                double x = Math.Min(1.0, Math.Max(0.0, (1.0 * level - itemMaterial.firstLevel) / (itemMaterial.lastLevel - itemMaterial.firstLevel)));

                // polynomial weight y:
                // |                 ..|
                // |                .  |
                // |              ..   |
                // |        ......     |
                // |........           |

                // This is low for the longest time, meaning that high chances are only achieved once the player level is close to last level
                double y = (4.0 / 3.0 * Math.Pow(x, 2) - 1.0 / 3.0 * Math.Pow(x, 8));
                double chance = y * (itemMaterial.endChance - itemMaterial.startChance) + itemMaterial.startChance;
                totalChance += chance;
                itemChancesDouble.Add(chance);
            }

            var itemChancesIntBetter = CustomMath.ApproximateProbabilities(itemChancesDouble, MAX_LEAVES, totalChance);

            for(int i = 0; i < itemChancesDouble.Count; ++i) {
                itemChancesInt.Add((int)(itemChancesDouble.ElementAt(i) * MAX_LEAVES / totalChance));
            }

            var chanceList = ChanceList.getChanceList(newItemList.ToArray(), itemChancesIntBetter.ToArray())!;
            var tree = RandomTree.GetRandomTree(chanceList, name + "_Lvl" + level);
            return tree.linkedItem;
        }

        public static LeveledItem CreateListCount(Enum itemType, string name, short count, int levelFactor, IEnumerable<ItemMaterial> materials, params LootRQ[] requirements) {
            LeveledItem leveledList = Program.state!.PatchMod.LeveledItems.AddNew();
            leveledList.EditorID = name;
            leveledList.Flags |= LeveledItem.Flag.CalculateForEachItemInCount;
            foreach(int level in LEVEL_LIST) {
                Form f = CreateSubList(itemType, level * levelFactor, name, materials, requirements);
                LeveledItemEntry entry = new();
                if(entry.Data == null) {
                    entry.Data = new LeveledItemEntryData();
                }
                entry.Data.Count = count;
                entry.Data.Level = (short)level;
                entry.Data.Reference.SetTo(f.FormKey);
                if(leveledList.Entries == null) {
                    leveledList.Entries = new Noggog.ExtendedList<LeveledItemEntry>();
                }
                leveledList.Entries!.Add(entry);
            }
            return leveledList;
        }

        public static LeveledItem CreateList(Enum itemType, string name, int levelFactor, IEnumerable<ItemMaterial> materials, params LootRQ[] requirements) {
            return CreateListCount(itemType, name, 1, levelFactor, materials, requirements);
        }

        public static void LinkList(FormLink<ILeveledItemGetter> vanillaList, int levelFactor, Enum itemType, IEnumerable<ItemMaterial> materials, params LootRQ[] requirements) {
            LeveledItem leveledList = Program.state!.PatchMod.LeveledItems.GetOrAddAsOverride(vanillaList, Program.state.LinkCache);
            leveledList.Entries = new Noggog.ExtendedList<LeveledItemEntry>();
            LeveledItemEntry entry = new();
            if(entry.Data == null) {
                entry.Data = new LeveledItemEntryData();
            }
            entry.Data.Count = 1;
            entry.Data.Level = 1;
            leveledList.Flags |= LeveledItem.Flag.CalculateForEachItemInCount;
            string name = leveledList.EditorID + "_LevelList";
            entry.Data.Reference.SetTo(CreateList(itemType, name, levelFactor, materials, requirements).FormKey);
            leveledList.Entries!.Add(entry);
        }

        public static void LinkListCount(FormLink<ILeveledItemGetter> vanillaList, short count, int levelFactor, Enum itemType, IEnumerable<ItemMaterial> materials, params LootRQ[] requirements) {
            LeveledItem leveledList = Program.state!.PatchMod.LeveledItems.GetOrAddAsOverride(vanillaList, Program.state.LinkCache);
            leveledList.Entries = new Noggog.ExtendedList<LeveledItemEntry>();
            LeveledItemEntry entry = new();
            if(entry.Data == null) {
                entry.Data = new LeveledItemEntryData();
            }
            entry.Data.Count = count;
            entry.Data.Level = 1;
            leveledList.Flags |= LeveledItem.Flag.CalculateForEachItemInCount;
            string name = leveledList.EditorID + "_LevelList";
            entry.Data.Reference.SetTo(CreateList(itemType, name, levelFactor, materials, requirements).FormKey);
            leveledList.Entries!.Add(entry);
        }

        public static void LinkList(FormLink<ILeveledItemGetter> vanillaList, int levelFactor, ItemType itemType, params LootRQ[] requirements) {
            LinkList(vanillaList, levelFactor, itemType, ItemMaterial.ALL, requirements);
        }

        public static void LinkListCount(FormLink<ILeveledItemGetter> vanillaList, short count, int levelFactor, ItemType itemType, params LootRQ[] requirements) {
            LinkListCount(vanillaList, count, levelFactor, itemType, ItemMaterial.ALL, requirements);
        }

        public static void LinkList(FormLink<ILeveledItemGetter> oldList, params Form[] newList) {
            LeveledItem leveledList = Program.state!.PatchMod.LeveledItems.GetOrAddAsOverride(oldList, Program.state.LinkCache);
            leveledList.Flags = LeveledItem.Flag.CalculateForEachItemInCount;
            leveledList.Entries = new Noggog.ExtendedList<LeveledItemEntry>();
            foreach(var list in newList) {
                LeveledItemEntry entry = new();
                if(entry.Data == null) {
                    entry.Data = new LeveledItemEntryData();
                }
                entry.Data.Count = 1;
                entry.Data.Level = 1;
                entry.Data.Reference.SetTo(list.FormKey);
                leveledList.Entries!.Add(entry);
            }
        }

        public static void LinkListCount(FormLink<ILeveledItemGetter> oldList, short count, params Form[] newList) {
            LeveledItem leveledList = Program.state!.PatchMod.LeveledItems.GetOrAddAsOverride(oldList, Program.state.LinkCache);
            leveledList.Flags = LeveledItem.Flag.CalculateForEachItemInCount;
            leveledList.Entries = new Noggog.ExtendedList<LeveledItemEntry>();
            foreach(var list in newList) {
                LeveledItemEntry entry = new();
                if(entry.Data == null) {
                    entry.Data = new LeveledItemEntryData();
                }
                entry.Data.Count = count;
                entry.Data.Level = 1;
                entry.Data.Reference.SetTo(list.FormKey);
                leveledList.Entries!.Add(entry);
            }
        }

        public static void LockLists(params FormLink<ILeveledItemGetter>[] lists) {
            foreach(var list in lists) {
                lockedLists.Add(list);
            }
        }

        public static void InitializePatch() {
            dummyList = Program.state.PatchMod.LeveledItems.AddNew();
            dummyList.Entries = new Noggog.ExtendedList<LeveledItemEntry>();
        }

        public static void FinalizePatch() {
            var dlc2Init = Program.state.PatchMod.Quests.GetOrAddAsOverride(Dragonborn.Quest.DLC2Init, Program.state.LinkCache);
            var script = dlc2Init.VirtualMachineAdapter!.Scripts.Find((entry) => entry.Name == "DLC2_QF_DLC2_MQ04_02016E02")!;
            foreach(var list in lockedLists) {
                foreach(var property in script.Properties) {
                    if(property is ScriptObjectProperty oProperty) {
                        if(oProperty.Object.FormKey.Equals(list.FormKey)) {
                            oProperty.Object.SetTo(dummyList);
                        }
                    }
                }
            }
        }
    }
}
