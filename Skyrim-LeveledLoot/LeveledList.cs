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

using Form = Mutagen.Bethesda.Plugins.IFormLink<Mutagen.Bethesda.Plugins.Records.IMajorRecordCommonGetter>;

namespace LeveledLoot {
    class LeveledList {

        static string prefix = "JLL_";
        public static int NUM_CHILDREN = 10;
        static int MAX_DEPTH = 2;
        static int MAX_LEAVES = (int)Math.Pow(NUM_CHILDREN, MAX_DEPTH);
        static int[] LEVEL_LIST = new int[] { 1, 4, 7, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 70, 80, 90, 100 };
        static Quest? mainQuest;
        static FormList? lockedList;

        public static Form CreateSubList(Enum itemType, int level, string name, IEnumerable<ItemMaterial> materials, params LootRQ[] requirements) {
            double totalChance = 0;
            LinkedList<double> itemChancesDouble = new();
            LinkedList<int> itemChancesInt = new();
            LinkedList<Form> newItemList = new();
            foreach(ItemMaterial itemMaterial in materials) {
                HashSet<LootRQ> commonRequirements = itemMaterial.requirements.Intersect(requirements).ToHashSet();
                if(!commonRequirements.SetEquals(itemMaterial.requirements)) {
                    continue;
                }
                Form? f = itemMaterial.GetItem(itemType);
                if(f == null) {
                    continue;
                }
                newItemList.AddLast(f);

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
                double chance =  y * (itemMaterial.endChance - itemMaterial.startChance) + itemMaterial.startChance;
                totalChance += chance;
                itemChancesDouble.AddLast(chance);
            }
            for(int i = 0; i < itemChancesDouble.Count; ++i) {
                itemChancesInt.AddLast((int)(itemChancesDouble.ElementAt(i) * MAX_LEAVES / totalChance));
            }

            var chanceList = ChanceList.getChanceList(newItemList.ToArray(), itemChancesInt.ToArray())!;
            var tree = RandomTree.GetRandomTree(chanceList, name + "_Lvl" + level);
            return tree.linkedItem;
        }

        public static LeveledItem CreateList(Enum itemType, string name, int levelFactor, IEnumerable<ItemMaterial> materials, params LootRQ[] requirements) {
            LeveledItem leveledList = Program.state!.PatchMod.LeveledItems.AddNew();
            leveledList.EditorID = name;
            leveledList.Flags |= LeveledItem.Flag.CalculateForEachItemInCount;
            foreach(int level in LEVEL_LIST) {
                Form f = CreateSubList(itemType, level * levelFactor, name, materials, requirements);
                LeveledItemEntry entry = new();
                if(entry.Data == null) {
                    entry.Data = new LeveledItemEntryData();
                }
                entry.Data.Count = 1;
                entry.Data.Level = (short)level;
                entry.Data.Reference.SetTo(f.FormKey);
                if(leveledList.Entries == null) {
                    leveledList.Entries = new Noggog.ExtendedList<LeveledItemEntry>();
                }
                leveledList.Entries!.Add(entry);
            }
            return leveledList;
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

        public static void LinkList(FormLink<ILeveledItemGetter> vanillaList, int levelFactor, ItemType itemType, params LootRQ[] requirements) {
            LinkList(vanillaList, levelFactor, itemType, ItemMaterial.ALL, requirements);
        }

        public static void LinkList(FormLink<ILeveledItemGetter> oldList, params Form[] newList) {
            LeveledItem leveledList = Program.state!.PatchMod.LeveledItems.GetOrAddAsOverride(oldList, Program.state.LinkCache);
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

        public static void LockLists(params FormLink<ILeveledItemGetter>[] lists) {
            foreach(var list in lists) {
                lockedList!.Items.Add(list);
            }
        }

        public static void CreateQuest() {
            mainQuest = Program.state!.PatchMod.Quests.AddNew();
            lockedList = Program.state.PatchMod.FormLists.AddNew();
            lockedList.EditorID = prefix + "LockedList";
            mainQuest.EditorID = prefix + "Main";
            mainQuest.Name = mainQuest.EditorID;
            mainQuest.Flags = Quest.Flag.StartGameEnabled | Quest.Flag.RunOnce;
            var playerAlias = new QuestAlias {
                ID = 0,
                Name = prefix + "Main_PlayerAlias",
                Flags = null,
            };
            playerAlias.ForcedReference.SetTo(Constants.Player.FormKey);
            mainQuest.Aliases.Add(playerAlias);
            mainQuest.VirtualMachineAdapter ??= new QuestAdapter();

            var mainScript = new ScriptEntry {
                Name = prefix + "Main",
            };
            var formListProperty = new ScriptObjectProperty {
                Name = prefix + "LockedList",
                Flags = ScriptProperty.Flag.Edited,
            };
            formListProperty.Object.SetTo(lockedList);
            mainScript.Properties.Add(formListProperty);
            mainQuest.VirtualMachineAdapter.Scripts.Add(mainScript);
            var playerAliasFragment = new QuestFragmentAlias {
                Property = new ScriptObjectProperty {
                    Alias = 0
                }
            };
            playerAliasFragment.Property.Object.SetTo(mainQuest);
            var playerAliasScript = new ScriptEntry {
                Flags = ScriptEntry.Flag.Local,
                Name = prefix + "Main_OnLoadGame",
            };
            var mainQuestProperty = new ScriptObjectProperty {
                Name = "main",
                Flags = ScriptProperty.Flag.Edited,
            };
            mainQuestProperty.Object.SetTo(mainQuest);
            playerAliasScript.Properties.Add(mainQuestProperty);
            playerAliasFragment.Scripts.Add(playerAliasScript);

            mainQuest.VirtualMachineAdapter.Aliases.Add(playerAliasFragment);
        }
    }
}
