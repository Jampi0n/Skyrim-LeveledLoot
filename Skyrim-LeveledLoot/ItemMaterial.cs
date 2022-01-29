using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Plugins;

using Form = Mutagen.Bethesda.Plugins.IFormLink<Mutagen.Bethesda.Plugins.Records.IMajorRecordGetter>;

namespace LeveledLoot {

    enum LootRQ {
        Rare,
        DLC2,
        NoEnch,
        Ench,
        Special
    }

    enum ItemType {
        HeavyHelmet,
        HeavyCuirass,
        HeavyGauntlets,
        HeavyBoots,
        HeavyShield,
        LightHelmet,
        LightCuirass,
        LightGauntlets,
        LightBoots,
        LightShield,
        Sword,
        Waraxe,
        Mace,
        Dagger,
        Greatsword,
        Battleaxe,
        Warhammer,
        Bow,
        SetNoHelmet,
        SetWithHelmet,
        Arrow,
    }

    class ItemVariant {
        public readonly Form item;
        public readonly int weight;
        public ItemVariant(Form item, int weight) {
            this.item = item;
            this.weight = weight;
        }
    }

    class ItemMaterial {
        public double startChance;
        public double endChance;
        public int firstLevel;
        public int lastLevel;

        public static LinkedList<ItemMaterial> ALL = new();
        string name;
        public Dictionary<Enum, LinkedList<ItemVariant>> itemMap = new();
        public Dictionary<Enum, Form> listMap = new();
        public HashSet<LootRQ> requirements;

        public ItemMaterial(string name, double startChance, double endChance, int firstLevel, int lastLevel, params LootRQ[] requirements) {
            this.name = name;
            this.startChance = startChance;
            this.endChance = endChance;
            this.firstLevel = firstLevel;
            this.lastLevel = lastLevel;
            this.requirements = requirements.ToHashSet();
            ALL.AddLast(this);
        }

        public void DefaultHeavyArmor(Form? helmet, Form? cuirass, Form? gauntlets, Form? boots, Form? shield) {
            AddItem(ItemType.HeavyHelmet, helmet);
            AddItem(ItemType.HeavyCuirass, cuirass);
            AddItem(ItemType.HeavyGauntlets, gauntlets);
            AddItem(ItemType.HeavyBoots, boots);
            AddItem(ItemType.HeavyShield, shield);
        }

        public void DefaultLightArmor(Form? helmet, Form? cuirass, Form? gauntlets, Form? boots, Form? shield) {
            AddItem(ItemType.LightHelmet, helmet);
            AddItem(ItemType.LightCuirass, cuirass);
            AddItem(ItemType.LightGauntlets, gauntlets);
            AddItem(ItemType.LightBoots, boots);
            AddItem(ItemType.LightShield, shield);
        }

        public void DefaultWeapon(Form? sword, Form? waraxe, Form? mace, Form? dagger, Form? greatsword, Form? battleaxe, Form? warhammer, Form? bow) {
            AddItem(ItemType.Sword, sword);
            AddItem(ItemType.Waraxe, waraxe);
            AddItem(ItemType.Mace, mace);
            AddItem(ItemType.Dagger, dagger);
            AddItem(ItemType.Greatsword, greatsword);
            AddItem(ItemType.Battleaxe, battleaxe);
            AddItem(ItemType.Warhammer, warhammer);
            AddItem(ItemType.Bow, bow);
        }

        public void AddItem(Enum itemType, Form? item, int weight) {
            if(item == null) {
                return;
            }
            if(!itemMap.ContainsKey(itemType)) {
                itemMap.Add(itemType, new LinkedList<ItemVariant>());
            }
            itemMap[itemType].AddLast(new ItemVariant(item, weight));
        }
        public void AddItem(Enum itemType, params Form?[] items) {
            foreach(Form? item in items) {
                AddItem(itemType, item, 1);
            }
        }

        public Form? GetItem(Enum itemType) {
            if(!itemMap.ContainsKey(itemType)) {
                return null;
            }
            if(itemMap[itemType].Count == 1) {
                return itemMap[itemType].First!.Value.item;
            } else {
                if(!listMap.ContainsKey(itemType)) {
                    LeveledItem leveledList = Program.state!.PatchMod.LeveledItems.AddNew();
                    leveledList.EditorID = name + "_" + itemType.ToString() + "_Variants";
                    for(int i = 0; i < itemMap[itemType].Count; ++i) {
                        for(int j = 0; j < itemMap[itemType].ElementAt(i).weight; ++j) {
                            LeveledItemEntry entry = new();
                            if(entry.Data == null) {
                                entry.Data = new LeveledItemEntryData();
                            }
                            entry.Data.Count = 1;
                            entry.Data.Level = 1;
                            entry.Data.Reference = (Mutagen.Bethesda.Plugins.IFormLink<IItemGetter>)itemMap[itemType].ElementAt(i).item;
                            if(leveledList.Entries == null) {
                                leveledList.Entries = new Noggog.ExtendedList<LeveledItemEntry>();
                            }
                            leveledList.Entries!.Add(entry);
                        }
                    }
                    listMap.Add(itemType, leveledList.AsLink());
                }
                return listMap[itemType];
            }
        }

    }
}
