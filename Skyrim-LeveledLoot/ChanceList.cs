using Mutagen.Bethesda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.FormKeys.SkyrimLE;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;

using Form = Mutagen.Bethesda.Plugins.IFormLink<Mutagen.Bethesda.Plugins.Records.IMajorRecordGetter>;


namespace LeveledLoot {
    static class ChanceList {
        private static readonly Dictionary<string, LeveledListEntry> cache = new();

        public static LeveledListEntry GetChanceList(string name, LeveledListEntry[] itemList, int[] chanceList, ref int counter) {
            int div = CustomMath.GcdList(chanceList.ToList());
            if (itemList.Length != chanceList.Length) {
                throw new Exception("itemList and chanceList must have the same length");
            }
            if (itemList.Length == 1) {
                return itemList[0];
            } else {
                var key = "CL";

                // deterministic order
                Array.Sort(itemList, chanceList, Comparer<LeveledListEntry>.Create((a, b) => {
                    return StringComparer.InvariantCulture.Compare(a.itemLink.FormKey.ToString(), b.itemLink.FormKey.ToString());
                }));

                for (var i = 0; i < itemList.Length; i++) {
                    key += "P[" + itemList[i].count + "x" + itemList[i].itemLink.FormKey + "]=" + chanceList[i] + ",";
                }

                if(!cache.ContainsKey(key)) {
                    counter++;
                    LeveledItem leveledList = Program.State.PatchMod.LeveledItems.AddNew();
                    leveledList.Flags = LeveledItem.Flag.CalculateFromAllLevelsLessThanOrEqualPlayer | LeveledItem.Flag.CalculateForEachItemInCount;
                    leveledList.EditorID = name;
                    leveledList.Entries ??= new Noggog.ExtendedList<LeveledItemEntry>();
                    for (var i = 0; i < itemList.Length; i++) {
                        for (var j = 0; j < chanceList[i]; j++) {
                            LeveledItemEntry entry = new();
                            entry.Data ??= new LeveledItemEntryData();
                            entry.Data.Count = (short)itemList[i].count;
                            entry.Data.Level = 1;
                            entry.Data.Reference.SetTo(itemList[i].itemLink.FormKey);
                            leveledList.Entries!.Add(entry);
                        }
                    }
                    cache.Add(key, new LeveledListEntry(leveledList.ToLink(), 1));
                }
                return cache[key];
            }
        }
    }
}
