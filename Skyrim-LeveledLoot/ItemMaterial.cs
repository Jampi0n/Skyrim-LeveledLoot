using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Plugins;
using System.Collections.ObjectModel;
using Mutagen.Bethesda.FormKeys.SkyrimLE;
using Mutagen.Bethesda.Plugins.Aspects;
using Mutagen.Bethesda.Plugins.Records;

using Form = Mutagen.Bethesda.Plugins.IFormLink<Mutagen.Bethesda.Plugins.Records.IMajorRecordGetter>;


namespace LeveledLoot {

    enum LootRQ {
        Rare,
        DLC2,
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
        Arrow6,
        Arrow12,
        Ring,
        Necklace,
        Circlet
    }

    class ItemTypeConfig {

        public static ItemType? GetItemTypeFromKeywords(IMajorRecordGetter item) {
            if (item is IArmorGetter armorGetter) {
                if (armorGetter.BodyTemplate!.ArmorType == ArmorType.HeavyArmor) {
                    if (armorGetter.HasKeyword(Skyrim.Keyword.ArmorHelmet)) {
                        return ItemType.HeavyHelmet;
                    } else if (armorGetter.HasKeyword(Skyrim.Keyword.ArmorCuirass)) {
                        return ItemType.HeavyCuirass;
                    } else if (armorGetter.HasKeyword(Skyrim.Keyword.ArmorGauntlets)) {
                        return ItemType.HeavyGauntlets;
                    } else if (armorGetter.HasKeyword(Skyrim.Keyword.ArmorBoots)) {
                        return ItemType.HeavyBoots;
                    } else if (armorGetter.HasKeyword(Skyrim.Keyword.ArmorShield)) {
                        return ItemType.HeavyShield;
                    }
                } else if (armorGetter.BodyTemplate!.ArmorType == ArmorType.LightArmor) {
                    if (armorGetter.HasKeyword(Skyrim.Keyword.ArmorHelmet)) {
                        return ItemType.LightHelmet;
                    } else if (armorGetter.HasKeyword(Skyrim.Keyword.ArmorCuirass)) {
                        return ItemType.LightCuirass;
                    } else if (armorGetter.HasKeyword(Skyrim.Keyword.ArmorGauntlets)) {
                        return ItemType.LightGauntlets;
                    } else if (armorGetter.HasKeyword(Skyrim.Keyword.ArmorBoots)) {
                        return ItemType.LightBoots;
                    } else if (armorGetter.HasKeyword(Skyrim.Keyword.ArmorShield)) {
                        return ItemType.LightShield;
                    }
                } else {
                    if (armorGetter.HasKeyword(Skyrim.Keyword.ClothingRing)) {
                        return ItemType.Ring;
                    } else if (armorGetter.HasKeyword(Skyrim.Keyword.ClothingNecklace)) {
                        return ItemType.Necklace;
                    } else if (armorGetter.HasKeyword(Skyrim.Keyword.ClothingCirclet)) {
                        return ItemType.Circlet;
                    }
                }
            } else if (item is IWeaponGetter weaponGetter) {
                if (weaponGetter.HasKeyword(Skyrim.Keyword.WeapTypeBattleaxe)) {
                    return ItemType.Battleaxe;
                } else if (weaponGetter.HasKeyword(Skyrim.Keyword.WeapTypeBow)) {
                    return ItemType.Bow;
                } else if (weaponGetter.HasKeyword(Skyrim.Keyword.WeapTypeDagger)) {
                    return ItemType.Dagger;
                } else if (weaponGetter.HasKeyword(Skyrim.Keyword.WeapTypeGreatsword)) {
                    return ItemType.Greatsword;
                } else if (weaponGetter.HasKeyword(Skyrim.Keyword.WeapTypeMace)) {
                    return ItemType.Mace;
                } else if (weaponGetter.HasKeyword(Skyrim.Keyword.WeapTypeSword)) {
                    return ItemType.Sword;
                } else if (weaponGetter.HasKeyword(Skyrim.Keyword.WeapTypeWarAxe)) {
                    return ItemType.Waraxe;
                } else if (weaponGetter.HasKeyword(Skyrim.Keyword.WeapTypeWarhammer)) {
                    return ItemType.Warhammer;
                }
            }
            return null;
        }
    }

    class LeveledListEntry {
        public readonly Form? itemLink;
        public readonly int count;
        public LeveledListEntry(Form? itemLink, int count) {
            this.itemLink = itemLink;
            this.count = count;
        }
    }

    class ItemVariant {
        public readonly IMajorRecordGetter item;
        public readonly Form itemLink;
        public readonly int weight;
        public readonly short count;
        public readonly byte chanceNone;
        public ItemVariant(Form itemLink, int weight, short count, byte chanceNone) {
            this.itemLink = itemLink;
            this.weight = weight;
            this.count = count;
            this.chanceNone = chanceNone;
            item = itemLink.Resolve(Program.State.LinkCache);
        }
    }

    class ItemMaterial {
        public double startChance;
        public double endChance;
        public int firstLevel;
        public int lastLevel;

        public double enchantTier;

        public static int maxVariants = -1;
        public static double maxVariantFraction = 1.0;
        public static LinkedList<ItemMaterial> ALL = new();
        public string name;
        public Dictionary<Enum, LinkedList<ItemVariant>> itemMap = new();
        public Dictionary<Enum, LeveledListEntry> listMap = new();
        public Dictionary<Tuple<Enum, int>, LeveledListEntry> enchListMap = new();
        public HashSet<LootRQ> requirements;
        public readonly List<ItemMaterial> enchTierList;

        public ItemMaterial(string name, LootEntry lootEntry, double enchantTier, params LootRQ[] requirements) {
            this.name = name;
            startChance = lootEntry.startChance;
            endChance = lootEntry.endChance;
            firstLevel = lootEntry.startLevel;
            lastLevel = lootEntry.endLevel;
            this.enchantTier = enchantTier;
            this.enchTierList = Enchanter.GetEnchTiers(this.enchantTier);
            this.requirements = requirements.ToHashSet();
            ALL.AddLast(this);
        }
        public ItemMaterial(string name, LootEntry lootEntry, params LootRQ[] requirements) {
            this.name = name;
            startChance = lootEntry.startChance;
            endChance = lootEntry.endChance;
            firstLevel = lootEntry.startLevel;
            lastLevel = lootEntry.endLevel;
            this.enchantTier = -1;
            this.enchTierList = Enchanter.GetEnchTiers(this.enchantTier);
            this.requirements = requirements.ToHashSet();
            ALL.AddLast(this);
        }

        public ItemMaterial(string name, double startChance, double endChance, int firstLevel, int lastLevel, params LootRQ[] requirements) {
            this.name = name;
            this.startChance = startChance;
            this.endChance = endChance;
            this.firstLevel = firstLevel;
            this.lastLevel = lastLevel;
            this.enchantTier = -1;
            this.enchTierList = Enchanter.GetEnchTiers(this.enchantTier);
            this.requirements = requirements.ToHashSet();
            ALL.AddLast(this);
        }

        public override string ToString() {
            return name + "(" + startChance + ", " + endChance + ", " + firstLevel + ", " + lastLevel + ")";
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
            if (item == null) {
                return;
            }

            if (!itemMap.ContainsKey(itemType)) {
                itemMap.Add(itemType, new LinkedList<ItemVariant>());
            }
            itemMap[itemType].AddLast(new ItemVariant(item, weight, count, chanceNone));

        }
        public void AddItem(Enum itemType, params Form?[] items) {
            foreach (Form? item in items) {
                AddItem(itemType, item, 1);
            }
        }

        public void AddItemCount(Enum itemType, short count, byte chanceNone, params Form?[] items) {
            foreach (Form? item in items) {
                AddItem(itemType, item, 1, count, chanceNone);
            }
        }

        public Form? GetFirst(Enum itemType) {
            if (!itemMap.ContainsKey(itemType)) {
                return null;
            }
            return itemMap[itemType].First!.Value.itemLink;
        }

        public LeveledListEntry? GetItem(Enum itemType, bool enchant, int level, string name) {
            if (!itemMap.ContainsKey(itemType)) {
                return null;
            }
            if (enchant) {
                var key = new Tuple<Enum, int>(itemType, level);
                if (!enchListMap.ContainsKey(key)) {
                    var listLink = Enchanter.Enchant(this, (ItemType)itemType, level, name);
                    enchListMap.Add(key, new LeveledListEntry(listLink, 1));
                }
                return enchListMap[key];
            } else {

                if (!listMap.ContainsKey(itemType)) {
                    if (itemMap[itemType].Count == 1 && itemMap[itemType].First!.Value.chanceNone == 0) {
                        var itemVariant = itemMap[itemType].First!.Value;
                        listMap.Add(itemType, new LeveledListEntry(itemVariant.itemLink, itemVariant.count));
                    } else {

                        Statistics.instance.variantSelectionLists++;
                        LeveledItem leveledList = Program.State!.PatchMod.LeveledItems.AddNew();
                        leveledList.EditorID = LeveledList.prefix + name + "_" + itemType.ToString() + "_Variants";
                        for (int i = 0; i < itemMap[itemType].Count; ++i) {
                            var itemVariant = itemMap[itemType].ElementAt(i);
                            for (int j = 0; j < itemVariant.weight; ++j) {
                                LeveledItemEntry entry = new();
                                entry.Data ??= new LeveledItemEntryData();
                                leveledList.ChanceNone = Math.Max(itemVariant.chanceNone, leveledList.ChanceNone);
                                entry.Data.Count = itemVariant.count;
                                entry.Data.Level = 1;
                                entry.Data.Reference = (IFormLink<IItemGetter>)itemVariant.itemLink;
                                leveledList.Entries ??= new Noggog.ExtendedList<LeveledItemEntry>();
                                leveledList.Entries!.Add(entry);
                            }
                        }
                        listMap.Add(itemType, new LeveledListEntry(leveledList.ToLink(), 1));
                    }
                }
                return listMap[itemType];
            }
        }
    }
}
