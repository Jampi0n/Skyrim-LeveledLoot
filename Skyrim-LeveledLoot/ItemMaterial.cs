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
using System.Collections.ObjectModel;

namespace LeveledLoot {

    enum LootRQ {
        Rare,
        DLC2,
        NoEnch,
        Ench,
        Special
    }

    enum ItemType {
        Helmet,
        Cuirass,
        Gauntlets,
        Boots,
        Shield,
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
        Arrow6,
        Arrow12,
        SoulGemFilled,
        SoulGemEmpty
    }

    class ItemTypeConfig {

        private static readonly Dictionary<Enum, HashSet<Enum>> childItemTypes = new();
        private static readonly Dictionary<Enum, HashSet<Enum>> parentItemTypes = new();

        public static void AddChildItemType(Enum parent, Enum child) {
            if(!childItemTypes.ContainsKey(parent)) {
                childItemTypes.Add(parent, new HashSet<Enum>());
            }
            childItemTypes[parent].Add(child);
            if(!parentItemTypes.ContainsKey(child)) {
                parentItemTypes.Add(child, new HashSet<Enum>());
            }
            parentItemTypes[child].Add(parent);
        }

        public static HashSet<Enum> GetAllChildItemTypes(Enum parent) {
            if(!childItemTypes.ContainsKey(parent)) {
                return new HashSet<Enum>() { parent };
            }
            var result = new HashSet<Enum>() { parent };
            foreach(var child in childItemTypes[parent]) {
                foreach(var i in GetAllChildItemTypes(child)) {
                    result.Add(i);
                }
            }
            return result;
        }

        public static HashSet<Enum> GetAllParentItemTypes(Enum child) {
            if(!parentItemTypes.ContainsKey(child)) {
                return new HashSet<Enum>() { child };
            }
            var result = new HashSet<Enum>() { child };
            foreach(var parent in parentItemTypes[child]) {
                foreach(var i in GetAllParentItemTypes(parent)) {
                    result.Add(i);
                }
            }
            return result;
        }

        public static void Config() {
            AddChildItemType(ItemType.Helmet, ItemType.HeavyHelmet);
            AddChildItemType(ItemType.Helmet, ItemType.LightHelmet);
            AddChildItemType(ItemType.Cuirass, ItemType.HeavyCuirass);
            AddChildItemType(ItemType.Cuirass, ItemType.LightCuirass);
            AddChildItemType(ItemType.Gauntlets, ItemType.HeavyGauntlets);
            AddChildItemType(ItemType.Gauntlets, ItemType.LightGauntlets);
            AddChildItemType(ItemType.Boots, ItemType.HeavyBoots);
            AddChildItemType(ItemType.Boots, ItemType.LightBoots);
            AddChildItemType(ItemType.Shield, ItemType.HeavyShield);
            AddChildItemType(ItemType.Shield, ItemType.LightShield);
        }
    }


    class ItemVariant {
        public readonly Form item;
        public readonly int weight;
        public readonly short count;
        public readonly byte chanceNone;
        public ItemVariant(Form item, int weight, short count, byte chanceNone) {
            this.item = item;
            this.weight = weight;
            this.count = count;
            this.chanceNone = chanceNone;
        }
    }

    class ItemMaterial {
        public double startChance;
        public double endChance;
        public int firstLevel;
        public int lastLevel;

        public static LinkedList<ItemMaterial> ALL = new();
        public string name;
        public Dictionary<Enum, LinkedList<ItemVariant>> itemMap = new();
        public Dictionary<Enum, Form> listMap = new();
        public Dictionary<Tuple<Enum, int>, Form> enchListMap = new();
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

        public void Arrow75(Form? baseArrowList) {
            AddItemCount(ItemType.Arrow, 1, 0, baseArrowList);
            AddItemCount(ItemType.Arrow6, 6, 0, baseArrowList);
            AddItemCount(ItemType.Arrow12, 12, 0, baseArrowList);
        }

        public void AddItem(Enum itemType, Form? item, int weight, short count = 1, byte chanceNone = 0) {
            if(item == null) {
                return;
            }

            HashSet<Enum> itemTypeSet = ItemTypeConfig.GetAllParentItemTypes(itemType);
            foreach(var i in itemTypeSet) {
                if(!itemMap.ContainsKey(i)) {
                    itemMap.Add(i, new LinkedList<ItemVariant>());
                }
                itemMap[i].AddLast(new ItemVariant(item, weight, count, chanceNone));
            }
        }
        public void AddItem(Enum itemType, params Form?[] items) {
            foreach(Form? item in items) {
                AddItem(itemType, item, 1);
            }
        }

        public void AddItemCount(Enum itemType, short count, byte chanceNone, params Form?[] items) {
            foreach(Form? item in items) {
                AddItem(itemType, item, 1, count, chanceNone);
            }
        }

        public void AddItemEnch(Enum itemType, params Form?[] items) {
            int weight = items.Length;
            foreach(Form? item in items) {
                AddItem(itemType, item, weight);
                weight--;
            }
        }

        public Form? GetFirst(Enum itemType) {
            if(!itemMap.ContainsKey(itemType)) {
                return null;
            }
            return itemMap[itemType].First!.Value.item;
        }

        public Form? GetItem(Enum itemType, bool enchant, int level, string name) {
            if(!itemMap.ContainsKey(itemType)) {
                return null;
            }
            if(enchant) {
                var key = new Tuple<Enum, int>(itemType, level);
                if(!enchListMap.ContainsKey(key)) {

                    LeveledItem leveledList;
                    Form listLink;
                    bool variants = true;
                    if(itemMap[itemType].Count == 1) {
                        var itemVariant = itemMap[itemType].First!.Value;
                        if(itemVariant.count == 1 && itemVariant.chanceNone == 0) {
                            variants = false;
                        }
                    }
                    if(variants) {
                        leveledList = Program.State!.PatchMod.LeveledItems.AddNew();
                        leveledList.EditorID = LeveledList.prefix + name + "_EnchVariantSelection_Lvl" + level;
                        for(int i = 0; i < itemMap[itemType].Count; ++i) {
                            var itemVariant = itemMap[itemType].ElementAt(i);
                            for(int j = 0; j < itemVariant.weight; ++j) {
                                LeveledItemEntry entry = new();
                                entry.Data ??= new LeveledItemEntryData();
                                leveledList.ChanceNone = Math.Max(itemVariant.chanceNone, leveledList.ChanceNone);
                                entry.Data.Count = itemVariant.count;
                                entry.Data.Level = 1;
                                entry.Data.Reference.SetTo( Enchanter.Enchant(itemVariant.item, level, name).FormKey);
                                leveledList.Entries ??= new Noggog.ExtendedList<LeveledItemEntry>();
                                leveledList.Entries!.Add(entry);
                            }
                        }
                        listLink = leveledList.ToLink();
                    } else {
                        listLink = Enchanter.Enchant(itemMap[itemType].First!.Value.item, level, name);
                    }
                    enchListMap.Add(key, listLink);
                }
                return enchListMap[key];
            } else {
                if(itemMap[itemType].Count == 1) {
                    var itemVariant = itemMap[itemType].First!.Value;
                    if(itemVariant.count == 1 && itemVariant.chanceNone == 0) {
                        return itemMap[itemType].First!.Value.item;
                    }
                }
                if(!listMap.ContainsKey(itemType)) {
                    LeveledItem leveledList = Program.State!.PatchMod.LeveledItems.AddNew();
                    leveledList.EditorID = LeveledList.prefix + name + "_" + itemType.ToString() + "_Variants";
                    for(int i = 0; i < itemMap[itemType].Count; ++i) {
                        var itemVariant = itemMap[itemType].ElementAt(i);
                        for(int j = 0; j < itemVariant.weight; ++j) {
                            LeveledItemEntry entry = new();
                            entry.Data ??= new LeveledItemEntryData();
                            leveledList.ChanceNone = Math.Max(itemVariant.chanceNone, leveledList.ChanceNone);
                            entry.Data.Count = itemVariant.count;
                            entry.Data.Level = 1;
                            entry.Data.Reference = (Mutagen.Bethesda.Plugins.IFormLink<IItemGetter>)itemVariant.item;
                            leveledList.Entries ??= new Noggog.ExtendedList<LeveledItemEntry>();
                            leveledList.Entries!.Add(entry);
                        }
                    }
                    listMap.Add(itemType, leveledList.ToLink());
                }
                return listMap[itemType];
            }
        }
    }
}
