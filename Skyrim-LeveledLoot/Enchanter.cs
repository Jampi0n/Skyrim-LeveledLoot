using Mutagen.Bethesda.Plugins;
using System;
using System.Collections.Generic;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.FormKeys.SkyrimLE;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using Noggog;

using Form = Mutagen.Bethesda.Plugins.IFormLink<Mutagen.Bethesda.Plugins.Records.IMajorRecordGetter>;
using static Mutagen.Bethesda.Skyrim.Furniture;
using System.Linq;
using System.Xml.Linq;
using DynamicData;

namespace LeveledLoot {
    class Enchanter {

        static string prefix = "JLL_";
        public static int NUM_CHILDREN = 10;
        static int MAX_DEPTH = 2;
        static int MAX_LEAVES = (int)Math.Pow(NUM_CHILDREN, MAX_DEPTH);
        static int[] LEVEL_LIST = new int[] { 1, 4, 7, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 70, 80, 90, 100 };

        public static ItemMaterial ENCH_1 = new("1", 80, 20, 0, 40, LootRQ.Special);
        public static ItemMaterial ENCH_2 = new("2", 20, 20, 0, 80, LootRQ.Special);
        public static ItemMaterial ENCH_3 = new("3", 0, 20, 11, 120, LootRQ.Special);
        public static ItemMaterial ENCH_4 = new("4", 0, 20, 24, 160, LootRQ.Special);
        public static ItemMaterial ENCH_5 = new("5", 0, 13, 37, 200, LootRQ.Special);
        public static ItemMaterial ENCH_6 = new("6", 0, 7, 50, 240, LootRQ.Special);

        public static List<ItemMaterial> EnchTiers = new() {
            ENCH_1,
            ENCH_2,
            ENCH_3,
            ENCH_4,
            ENCH_5,
            ENCH_6
        };


        static Dictionary<ItemType, Dictionary<int, Dictionary<IFormLink<IEffectRecordGetter>, Tuple<int, string, string>>>> enchantmentDict = new();
        static Dictionary<Tuple<FormKey, int>, Form> enchantedItems = new();


        static Form EnchantArmor(IArmorGetter armorGetter, ItemType itemType, int enchantTier) {
            var key = new Tuple<FormKey, int>(armorGetter.FormKey, -enchantTier);
            if(!enchantedItems.ContainsKey(key)) {
                var leveledList = Program.state!.PatchMod.LeveledItems.AddNew();
                leveledList.EditorID = prefix + "LItemArmor_EnchTier" + enchantTier + "_" + armorGetter.EditorID;

                if(!enchantmentDict.ContainsKey(itemType)) {
                    throw new Exception("No enchantments for item type.");
                }
                var dict = enchantmentDict[itemType];
                foreach(var enchTuple in dict[enchantTier]) {
                    LeveledItemEntry entry = new();
                    if(entry.Data == null) {
                        entry.Data = new LeveledItemEntryData();
                    }
                    leveledList.ChanceNone = 0;
                    entry.Data.Count = 1;
                    entry.Data.Level = 1;

                    var armor = Program.state!.PatchMod.Armors.AddNew();
                    armor.DeepCopyIn(armorGetter);
                    armor.ObjectEffect.SetTo(enchTuple.Key);
                    armor.EnchantmentAmount = (ushort)enchTuple.Value.Item1;
                    armor.Name = enchTuple.Value.Item2.Replace("$NAME$", armorGetter.Name.String);
                    armor.EditorID += "_" + enchTuple.Value.Item3;
                    entry.Data.Reference.SetTo(armor.AsLink());

                    if(leveledList.Entries == null) {
                        leveledList.Entries = new Noggog.ExtendedList<LeveledItemEntry>();
                    }
                    leveledList.Entries!.Add(entry);
                }
                enchantedItems[key] = leveledList.ToLink();
            }
            return enchantedItems[key];
        }

        static Form EnchantWeapon(IWeaponGetter weaponGetter, ItemType itemType, int enchantTier) {
            var key = new Tuple<FormKey, int>(weaponGetter.FormKey, -enchantTier);
            if(!enchantedItems.ContainsKey(key)) {
                var leveledList = Program.state!.PatchMod.LeveledItems.AddNew();
                leveledList.EditorID = prefix + "LItemWeapon_EnchTier" + (1 + enchantTier) + "_" + weaponGetter.EditorID;

                if(!enchantmentDict.ContainsKey(itemType)) {
                    throw new Exception("No enchantments for item type.");
                }
                var dict = enchantmentDict[itemType];
                foreach(var enchTuple in dict[enchantTier]) {
                    LeveledItemEntry entry = new();
                    if(entry.Data == null) {
                        entry.Data = new LeveledItemEntryData();
                    }
                    leveledList.ChanceNone = 0;
                    entry.Data.Count = 1;
                    entry.Data.Level = 1;

                    var weapon = Program.state!.PatchMod.Weapons.AddNew();
                    weapon.DeepCopyIn(weaponGetter);
                    weapon.ObjectEffect.SetTo(enchTuple.Key);
                    weapon.EnchantmentAmount = (ushort)enchTuple.Value.Item1;
                    weapon.Name = enchTuple.Value.Item2.Replace("$NAME$", weaponGetter.Name.String);
                    weapon.EditorID += enchTuple.Value.Item2.Replace("$NAME$", weaponGetter.Name.String).Replace(" ", "") + "_" + enchantTier;
                    entry.Data.Reference.SetTo(weapon.AsLink());

                    if(leveledList.Entries == null) {
                        leveledList.Entries = new Noggog.ExtendedList<LeveledItemEntry>();
                    }
                    leveledList.Entries!.Add(entry);
                }
                enchantedItems[key] = leveledList.ToLink();
            }
            return enchantedItems[key];
        }

        public static Form Enchant(Form itemLink, int lootLevel) {
            var key = new Tuple<FormKey, int>(itemLink.FormKey, lootLevel);
            if(!enchantedItems.ContainsKey(key)) {



                if(itemLink.TryResolve(Program.state.LinkCache, out var item)) {
                    ItemType? itemType = null;
                    IArmorGetter armorGetter = null;
                    IWeaponGetter weaponGetter = null;
                    if(item is IArmorGetter a) {
                        armorGetter = a;

                        if(armorGetter.BodyTemplate.ArmorType == ArmorType.HeavyArmor) {
                            if(armorGetter.HasKeyword(Skyrim.Keyword.ArmorHelmet)) {
                                itemType = ItemType.HeavyHelmet;
                            } else if(armorGetter.HasKeyword(Skyrim.Keyword.ArmorCuirass)) {
                                itemType = ItemType.HeavyCuirass;
                            } else if(armorGetter.HasKeyword(Skyrim.Keyword.ArmorGauntlets)) {
                                itemType = ItemType.HeavyGauntlets;
                            } else if(armorGetter.HasKeyword(Skyrim.Keyword.ArmorBoots)) {
                                itemType = ItemType.HeavyBoots;
                            } else if(armorGetter.HasKeyword(Skyrim.Keyword.ArmorShield)) {
                                itemType = ItemType.HeavyShield;
                            }
                        } else if(armorGetter.BodyTemplate.ArmorType == ArmorType.LightArmor) {
                            if(armorGetter.HasKeyword(Skyrim.Keyword.ArmorHelmet)) {
                                itemType = ItemType.LightHelmet;
                            } else if(armorGetter.HasKeyword(Skyrim.Keyword.ArmorCuirass)) {
                                itemType = ItemType.LightCuirass;
                            } else if(armorGetter.HasKeyword(Skyrim.Keyword.ArmorGauntlets)) {
                                itemType = ItemType.LightGauntlets;
                            } else if(armorGetter.HasKeyword(Skyrim.Keyword.ArmorBoots)) {
                                itemType = ItemType.LightBoots;
                            } else if(armorGetter.HasKeyword(Skyrim.Keyword.ArmorShield)) {
                                itemType = ItemType.LightShield;
                            }
                        }
                    } else if(item is IWeaponGetter w) {

                    } else {
                        throw new Exception("Item must be armor or weapon.");
                    }

                    double totalChance = 0;
                    List<double> itemChancesDouble = new();
                    List<int> itemChancesInt = new();
                    List<Form> newItemList = new();
                    string name = prefix + "EnchTierSelection_Lvl" + lootLevel + "_";
                    if(armorGetter != null) {
                        name += armorGetter.EditorID;
                    } else {
                        name += weaponGetter.EditorID;
                    }

                    for(int t = 0; t < EnchTiers.Count; t++) {
                        if(armorGetter != null) {
                            newItemList.Add(EnchantArmor(armorGetter, itemType.Value, t + 1));
                        } else {
                            newItemList.Add(EnchantWeapon(weaponGetter, itemType.Value, t + 1));
                        }
                        var itemMaterial = EnchTiers[t];
                        // linear weight x between 0 and 1 depending on player level and first and last level of the item
                        // 0 -> start chance
                        // 1 -> end chance
                        double x = Math.Min(1.0, Math.Max(0.0, (1.0 * lootLevel - itemMaterial.firstLevel) / (itemMaterial.lastLevel - itemMaterial.firstLevel)));

                        double chance = x * (itemMaterial.endChance - itemMaterial.startChance) + itemMaterial.startChance;
                        totalChance += chance;
                        itemChancesDouble.Add(chance);
                    }

                    var itemChancesIntBetter = CustomMath.ApproximateProbabilities(itemChancesDouble, MAX_LEAVES, totalChance);

                    for(int i = 0; i < itemChancesDouble.Count; ++i) {
                        itemChancesInt.Add((int)(itemChancesDouble.ElementAt(i) * MAX_LEAVES / totalChance));
                    }

                    var chanceList = ChanceList.getChanceList(newItemList.ToArray(), itemChancesIntBetter.ToArray())!;
                    var tree = RandomTree.GetRandomTree(chanceList, name);

                    enchantedItems[key] = tree.linkedItem;
                }

            }
            return enchantedItems[key];
        }

        public static void RegisterEnchantments(ItemType itemType, IFormLink<ISkyrimMajorRecordGetter> itemLink, IFormLink<ILeveledItemGetter> enchantmentLists, int tier) {
            var item = itemLink.TryResolve(Program.state.LinkCache);
            var itemName = "";
            if(item != null) {
                if(item is IArmorGetter armor) {
                    itemName = armor.Name.String;
                } else if(item is IWeaponGetter weapon) {
                    itemName = weapon.Name.String;
                }
            }
            if(itemName == "") {
                throw new Exception("Item has no name.");
            }
            if(!enchantmentDict.ContainsKey(itemType)) {
                enchantmentDict.Add(itemType, new Dictionary<int, Dictionary<IFormLink<IEffectRecordGetter>, Tuple<int, string, string>>>());
            }
            var dict = enchantmentDict[itemType];
            if(!dict.ContainsKey(tier)) {
                dict.Add(tier, new Dictionary<IFormLink<IEffectRecordGetter>, Tuple<int, string, string>>());
            }
            if(enchantmentLists.TryResolve(Program.state.LinkCache, out var enchList)) {
                foreach(var entry in enchList.Entries) {
                    if(entry.Data.Reference.TryResolve(Program.state.LinkCache, out var enchantedItem)) {
                        var enchantedItemName = "";
                        IFormLink<IEffectRecordGetter> ench;
                        int enchAmount = 0;
                        if(enchantedItem is IArmorGetter armor) {
                            enchantedItemName = armor.Name.String;
                            ench = armor.ObjectEffect.AsSetter();
                            enchAmount = armor.EnchantmentAmount.GetValueOrDefault(0);
                        } else if(enchantedItem is IWeaponGetter weapon) {
                            enchantedItemName = weapon.Name.String;
                            ench = weapon.ObjectEffect.AsSetter();
                            enchAmount = weapon.EnchantmentAmount.GetValueOrDefault(0);
                        } else {
                            throw new Exception("Enchanted item has unexpected form type.");
                        }
                        if(!enchantedItemName.Contains(itemName)) {
                            throw new Exception("Enchanted item name must contain base item name as substring.");
                        }
                        if(ench.IsNull == null) {
                            throw new Exception("Enchanted item has no enchantment.");
                        }
                        if(ench.TryResolve(Program.state.LinkCache, out var effectRecord)) {
                            dict[tier][ench] = new Tuple<int, string, string>(enchAmount, enchantedItemName.Replace(itemName, "$NAME$"), effectRecord.EditorID);
                        }
                    }
                }
            }
        }
    }
}
