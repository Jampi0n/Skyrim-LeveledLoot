using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Form = Mutagen.Bethesda.Plugins.IFormLink<Mutagen.Bethesda.Plugins.Records.IMajorRecordGetter>;


namespace LeveledLoot {
    class ChanceList {
        ChanceList prev;
        ChanceList next;
        public LeveledListEntry item;
        public int chance;
        public int size;
        public int totalChance;
        public string name;

        public static ChanceList Create(LeveledListEntry item, int chance, ChanceList? before) {
            var list = new ChanceList(item, chance, before);
            return list.Update(chance);
        }

        public static ChanceList? GetChanceList(LeveledListEntry[] itemList, int[] chanceList) {
            int div = CustomMath.GcdList(chanceList.ToList());
            for(int i = 0; i < chanceList.Length; ++i) {
                chanceList[i] /= div;
            }
            ChanceList? newChanceList = null;
            for(int i = 0; i < itemList.Length; ++i) {
                if(chanceList[i] > 0) {
                    newChanceList = Create(itemList[i], chanceList[i], newChanceList);
                }
            }
            if(newChanceList == null) {
                Console.WriteLine("newChanceList == null");
            }
            return newChanceList;
        }

        static int counter = 0;
        public ChanceList(LeveledListEntry item, int chance, ChanceList? before) {
            name = "CL" + counter;
            counter++;
            this.item = item;
            this.totalChance = 0;
            if(before == null) {
                next = this;
                prev = this;
                size = 1;
                totalChance = chance;
            } else {
                size = before.size + 1;
                ChanceList p = before.prev;
                before.prev = this;
                next = before;
                prev = p;
                p.next = this;
            }
        }

        public ChanceList Update(int chance) {
            this.chance = chance;
            if(chance == 0) {
                next.size = size;
                return next;
            }
            if(size == 1) {
                totalChance = chance;
            }
            if(size == 2) {
                if(next.chance > chance) {
                    next.size = size;
                    next.totalChance = chance + next.chance;
                    return next;
                } else {
                    totalChance = chance + next.chance;
                    return this;
                }
            }
            ChanceList best = next.chance > chance ? next : this;
            ChanceList originalNext = next;
            int j = 0;
            bool loop = false;
            while(next.chance > chance) {
                if(next == best) {
                    if(loop) {
                        break;
                    } else {
                        loop = true;
                    }
                }
                j++;
                ChanceList n = next;
                ChanceList p = prev;
                ChanceList nn = n.next;
                p.next = n;
                n.prev = p;
                n.next = this;
                prev = n;
                next = nn;
                nn.prev = this;
            }
            best.size = size;
            best.totalChance = chance + originalNext.totalChance;
            return best;
        }

        public ChanceList[] ToArray() {
            ChanceList start = this;
            ChanceList current = this;
            bool loop = false;
            var array = new ChanceList[size];
            int j = 0;
            while(true) {
                j++;
                if(current == start) {
                    if(loop == true) {
                        return array;
                    }
                    loop = true;
                }
                ChanceList tmp = current;
                current = current.next;
                array[j - 1] = tmp;
            }
        }

        public string Hash() {
            
            string str = "";
            foreach(ChanceList chanceElement in this.ToArray()) {
                str += "C=" + chanceElement.chance + "_I=" + chanceElement.item.itemLink + "x" + chanceElement.item.count + ",";
            }
            return str;
        }

        public override string ToString() {
            string str = "";
            foreach(ChanceList chanceElement in this.ToArray()) {
                var item = chanceElement.item.itemLink.TryResolve(Program.State.LinkCache);
                str += "" + chanceElement.chance + ":" + item!.EditorID + ", ";
            }
            return str;
        }
    }

}
