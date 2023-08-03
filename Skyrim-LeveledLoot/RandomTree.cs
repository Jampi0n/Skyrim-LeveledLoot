using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.Skyrim;

using Form = Mutagen.Bethesda.Plugins.IFormLink<Mutagen.Bethesda.Plugins.Records.IMajorRecordGetter>;

namespace LeveledLoot {
    class RandomTree {


        public ChanceList chanceList;
        public readonly LinkedList<RandomTree> children = new();
        public Form linkedItem;
        public string name;
        public bool isLeaf;

        static readonly Dictionary<string, RandomTree> treeMap = new();

        public static RandomTree GetRandomTree(ChanceList chanceList, string newName, ref int counter) {
            string key = chanceList.Hash();
            if(treeMap.ContainsKey(key)) {
                return treeMap[key];
            } else {
                return new RandomTree(chanceList, newName, ref counter);
            }
        }

        private RandomTree(ChanceList chanceList, string name, ref int counter) {
            string key = chanceList.Hash();
            treeMap.Add(key, this);
            this.name = name;
            this.chanceList = chanceList;
            int totalChance = this.chanceList.totalChance;
            int splitSize = (int)Math.Ceiling(1.0 * totalChance / LeveledList.NUM_CHILDREN);
            isLeaf = this.chanceList.size == 1;
            if(!isLeaf) {
                while(true) {
                    if(this.chanceList.chance == 0) {
                        break;
                    }
                    int currentSplitSize = 0;
                    LinkedList<Form> newItemList = new();
                    LinkedList<int> newChanceList = new();
                    while(this.chanceList.chance > 0 && currentSplitSize < splitSize) {
                        int take = Math.Min(splitSize - currentSplitSize, this.chanceList.chance);
                        currentSplitSize += take;
                        newItemList.AddLast(this.chanceList.item);
                        newChanceList.AddLast(take);
                        this.chanceList = this.chanceList.Update(this.chanceList.chance - take);
                    }

                    string newName = name + "_" + children.Count;
                    ChanceList newChanceList2 = ChanceList.GetChanceList(newItemList.ToArray(), newChanceList.ToArray())!;
                    RandomTree child = newChanceList2.Hash() == key ? new RandomTree(newChanceList2, newName, ref counter) : RandomTree.GetRandomTree(newChanceList2, newName, ref counter);
                    children.AddLast(child);
                }
                counter++;
                LeveledItem leveledList = Program.State!.PatchMod.LeveledItems.AddNew();
                leveledList.EditorID = name;
                for(int i = 0; i < children.Count; ++i) {
                    LeveledItemEntry entry = new();
                    entry.Data ??= new LeveledItemEntryData();
                    entry.Data.Count = 1;
                    entry.Data.Level = 1;
                    entry.Data.Reference = (Mutagen.Bethesda.Plugins.IFormLink<IItemGetter>)children.ElementAt(i).linkedItem;
                    leveledList.Entries ??= new Noggog.ExtendedList<LeveledItemEntry>();
                    leveledList.Entries!.Add(entry);
                }
                linkedItem = leveledList.ToLink();
            } else {
                linkedItem = this.chanceList.item;
            }
        }
    }
}
