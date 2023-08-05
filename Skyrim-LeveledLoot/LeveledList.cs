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

        public static readonly string prefix = "JLL_";
        public static readonly int NUM_CHILDREN = 200;
        public static readonly int FACTOR_JUNK = 1;
        public static readonly int FACTOR_COMMON = 2;
        public static readonly int FACTOR_RARE = 3;
        public static readonly int FACTOR_BEST = 4;
        static readonly int MAX_DEPTH = 1;
        static readonly int MAX_LEAVES = (int)Math.Pow(NUM_CHILDREN, MAX_DEPTH);
        static readonly int[] LEVEL_LIST = new int[] { 1, 5, 10, 15, 21, 27, 34, 42, 50, 60, 70, 80 };
        static readonly List<FormLink<ILeveledItemGetter>> lockedLists = new();
        static LeveledItem? dummyList;

        public static Form CreateSubList(Enum itemType, int level, string name, IEnumerable<ItemMaterial> materials, bool enchant, params LootRQ[] requirements) {
            double totalChance = 0;
            List<double> itemChancesDouble = new();
            List<int> itemChancesInt = new();
            List<Form> newItemList = new();

            foreach(ItemMaterial itemMaterial in materials) {
                var commonRequirements = itemMaterial.requirements.Intersect(requirements).ToHashSet();
                if(!commonRequirements.SetEquals(itemMaterial.requirements)) {
                    continue;
                }
                Form? f = itemMaterial.GetItem(itemType, enchant, level, name + "_" + itemMaterial.name);
                if(f == null) {
                    continue;
                }
                newItemList.Add(f);

                // linear weight x between 0 and 1 depending on player level and first and last level of the item
                // 0 -> start chance
                // 1 -> end chance
                double x = Math.Min(1.0, Math.Max(0.0, (1.0 * level - itemMaterial.firstLevel) / (itemMaterial.lastLevel - itemMaterial.firstLevel)));

                double chance = x * (itemMaterial.endChance - itemMaterial.startChance) + itemMaterial.startChance;
                totalChance += chance;
                itemChancesDouble.Add(chance);
            }

            var itemChancesIntBetter = CustomMath.ApproximateProbabilities(itemChancesDouble, MAX_LEAVES, totalChance);

            for(int i = 0; i < itemChancesDouble.Count; ++i) {
                itemChancesInt.Add((int)(itemChancesDouble.ElementAt(i) * MAX_LEAVES / totalChance));
            }

            Statistics.instance.materialSelectionLists++;
            var chanceList = ChanceList.GetChanceList(newItemList.ToArray(), itemChancesIntBetter.ToArray())!;
            var tree = RandomTree.GetRandomTree(chanceList, prefix + name + "_Lvl" + level, ref Statistics.instance.materialSelectionLists);
            return tree.linkedItem;
        }

        public static LeveledItem CreateListCount(Enum itemType, string name, short count, int levelFactor, IEnumerable<ItemMaterial> materials, bool enchant, params LootRQ[] requirements) {
            Statistics.instance.levelSelectionLists++;
            LeveledItem leveledList = Program.State!.PatchMod.LeveledItems.AddNew();
            leveledList.EditorID = prefix + name;
            leveledList.Flags = LeveledItem.Flag.CalculateForEachItemInCount | LeveledItem.Flag.SpecialLoot;
            foreach(int level in LEVEL_LIST) {
                Form f = CreateSubList(itemType, level * levelFactor, name, materials, enchant, requirements);
                LeveledItemEntry entry = new();
                entry.Data ??= new LeveledItemEntryData();
                entry.Data.Count = count;
                entry.Data.Level = (short)level;
                entry.Data.Reference.SetTo(f.FormKey);
                leveledList.Entries ??= new Noggog.ExtendedList<LeveledItemEntry>();
                leveledList.Entries!.Add(entry);
            }
            return leveledList;
        }

        public static LeveledItem CreateListEnchanted(Enum itemType, string name, int levelFactor, IEnumerable<ItemMaterial> materials, params LootRQ[] requirements) {
            return CreateListCount(itemType, name, 1, levelFactor, materials, true, requirements);
        }

        public static LeveledItem CreateList(Enum itemType, string name, int levelFactor, IEnumerable<ItemMaterial> materials, params LootRQ[] requirements) {
            return CreateListCount(itemType, name, 1, levelFactor, materials, false, requirements);
        }


        public static void LinkListCount(FormLink<ILeveledItemGetter> vanillaList, short count, int levelFactor, Enum itemType, IEnumerable<ItemMaterial> materials, bool enchant, params LootRQ[] requirements) {
            LeveledItem leveledList = Program.State!.PatchMod.LeveledItems.GetOrAddAsOverride(vanillaList, Program.State.LinkCache);
            leveledList.Entries = new Noggog.ExtendedList<LeveledItemEntry>();
            LeveledItemEntry entry = new();
            entry.Data ??= new LeveledItemEntryData();
            entry.Data.Count = count;
            entry.Data.Level = 1;
            leveledList.Flags |= LeveledItem.Flag.CalculateForEachItemInCount;
            string name = leveledList.EditorID + "_LevelList";
            var list = enchant ? CreateListEnchanted(itemType, name, levelFactor, materials, requirements) : CreateList(itemType, name, levelFactor, materials, requirements);
            entry.Data.Reference.SetTo(list.FormKey);
            leveledList.Entries!.Add(entry);
        }

        public static void LinkListEnchanted(FormLink<ILeveledItemGetter> vanillaList, int levelFactor, Enum itemType, IEnumerable<ItemMaterial> materials, params LootRQ[] requirements) {
            LinkListCount(vanillaList, 1, levelFactor, itemType, materials, true, requirements);
        }

        public static void LinkList(FormLink<ILeveledItemGetter> vanillaList, int levelFactor, Enum itemType, IEnumerable<ItemMaterial> materials, params LootRQ[] requirements) {
            LinkListCount(vanillaList, 1, levelFactor, itemType, materials, false, requirements);
        }

        public static void LinkList(FormLink<ILeveledItemGetter> oldList, params Form[] newList) {
            LeveledItem leveledList = Program.State!.PatchMod.LeveledItems.GetOrAddAsOverride(oldList, Program.State.LinkCache);
            leveledList.Flags = LeveledItem.Flag.CalculateForEachItemInCount;
            leveledList.Entries = new Noggog.ExtendedList<LeveledItemEntry>();
            foreach(var list in newList) {
                LeveledItemEntry entry = new();
                entry.Data ??= new LeveledItemEntryData();
                entry.Data.Count = 1;
                entry.Data.Level = 1;
                entry.Data.Reference.SetTo(list.FormKey);
                leveledList.Entries!.Add(entry);
            }
        }

        public static void LinkListCount(FormLink<ILeveledItemGetter> oldList, short count, params Form[] newList) {
            LeveledItem leveledList = Program.State!.PatchMod.LeveledItems.GetOrAddAsOverride(oldList, Program.State.LinkCache);
            leveledList.Flags = LeveledItem.Flag.CalculateForEachItemInCount;
            leveledList.Entries = new Noggog.ExtendedList<LeveledItemEntry>();
            foreach(var list in newList) {
                LeveledItemEntry entry = new();
                entry.Data ??= new LeveledItemEntryData();
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
            dummyList = Program.State.PatchMod.LeveledItems.AddNew();
            dummyList.Entries = new Noggog.ExtendedList<LeveledItemEntry>();
            dummyList.EditorID = prefix + "THIS_LIST_ABSORBS_RUNTIME_LIST_CHANGES_FROM_DLC2";
        }

        public static void FinalizePatch() {
            var dlc2Init = Program.State.PatchMod.Quests.GetOrAddAsOverride(Dragonborn.Quest.DLC2Init, Program.State.LinkCache);
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
