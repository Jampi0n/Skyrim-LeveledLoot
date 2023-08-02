using Mutagen.Bethesda.Plugins;
using System;
using System.Collections.Generic;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.FormKeys.SkyrimLE;
using Mutagen.Bethesda.Plugins.Records;
using Noggog;
using System.Linq;

using Form = Mutagen.Bethesda.Plugins.IFormLink<Mutagen.Bethesda.Plugins.Records.IMajorRecordGetter>;


namespace LeveledLoot {
    class Enchanter {

        static readonly string prefix = "JLL_";
        static readonly int NUM_CHILDREN = 6;
        static readonly int MAX_DEPTH = 2;
        static readonly int MAX_LEAVES = (int)Math.Pow(NUM_CHILDREN, MAX_DEPTH);

        static readonly ItemMaterial ENCH_1 = new("1", 80, 20, 0, 40, LootRQ.Special);
        static readonly ItemMaterial ENCH_2 = new("2", 20, 20, 0, 80, LootRQ.Special);
        static readonly ItemMaterial ENCH_3 = new("3", 0, 20, 11, 120, LootRQ.Special);
        static readonly ItemMaterial ENCH_4 = new("4", 0, 20, 24, 160, LootRQ.Special);
        static readonly ItemMaterial ENCH_5 = new("5", 0, 13, 37, 200, LootRQ.Special);
        static readonly ItemMaterial ENCH_6 = new("6", 0, 7, 50, 240, LootRQ.Special);

        static readonly List<ItemMaterial> EnchTiers = new() {
            ENCH_1,
            ENCH_2,
            ENCH_3,
            ENCH_4,
            ENCH_5,
            ENCH_6
        };


        static Dictionary<ItemType, Dictionary<int, Dictionary<IFormLink<IEffectRecordGetter>, Tuple<int, string, string>>>> enchantmentDict = new();
        static Dictionary<Tuple<FormKey, int>, Form> enchantedItems = new();


        public static void Reset() {
            enchantmentDict.Clear();
            enchantedItems.Clear();
            foreach(var material in ItemMaterial.ALL) {
                material.enchListMap.Clear();
            }
        }

        static Form EnchantArmor(IArmorGetter armorGetter, ItemType itemType, int enchantTier, string name) {
            var key = new Tuple<FormKey, int>(armorGetter.FormKey, -enchantTier);
            if(!enchantedItems.ContainsKey(key)) {
                var leveledList = Program.State!.PatchMod.LeveledItems.AddNew();
                leveledList.EditorID = prefix + name + "_LItemArmor_EnchTier" + enchantTier + "_" + armorGetter.EditorID;

                if(!enchantmentDict.ContainsKey(itemType)) {
                    throw new Exception("No enchantments for item type.");
                }
                var dict = enchantmentDict[itemType];
                foreach(var enchTuple in dict[enchantTier]) {
                    LeveledItemEntry entry = new();
                    entry.Data ??= new LeveledItemEntryData();
                    leveledList.ChanceNone = 0;
                    entry.Data.Count = 1;
                    entry.Data.Level = 1;

                    var armor = Program.State!.PatchMod.Armors.AddNew();
                    var armorName = armorGetter.Name == null ? "" : armorGetter.Name.String;
                    armor.DeepCopyIn(armorGetter);
                    armor.ObjectEffect.SetTo(enchTuple.Key);
                    armor.EnchantmentAmount = (ushort)enchTuple.Value.Item1;
                    armor.Name = enchTuple.Value.Item2.Replace("$NAME$", armorName);
                    armor.EditorID += "_" + enchTuple.Value.Item3;
                    entry.Data.Reference.SetTo(armor.ToLink());

                    if(leveledList.Entries == null) {
                        leveledList.Entries = new Noggog.ExtendedList<LeveledItemEntry>();
                    }
                    leveledList.Entries!.Add(entry);
                }
                enchantedItems[key] = leveledList.ToLink();
            }
            return enchantedItems[key];
        }

        static Form EnchantWeapon(IWeaponGetter weaponGetter, ItemType itemType, int enchantTier, string name) {
            var key = new Tuple<FormKey, int>(weaponGetter.FormKey, -enchantTier);
            if(!enchantedItems.ContainsKey(key)) {
                var leveledList = Program.State!.PatchMod.LeveledItems.AddNew();
                leveledList.EditorID = prefix + name + "_LItemWeapon_EnchTier" + enchantTier + "_" + weaponGetter.EditorID;

                if(!enchantmentDict.ContainsKey(itemType)) {
                    throw new Exception("No enchantments for item type.");
                }
                var dict = enchantmentDict[itemType];
                foreach(var enchTuple in dict[enchantTier]) {
                    LeveledItemEntry entry = new();
                    entry.Data ??= new LeveledItemEntryData();
                    leveledList.ChanceNone = 0;
                    entry.Data.Count = 1;
                    entry.Data.Level = 1;

                    var weapon = Program.State!.PatchMod.Weapons.AddNew();
                    var weaponName = weaponGetter.Name == null ? "" : weaponGetter.Name.String;
                    weapon.DeepCopyIn(weaponGetter);
                    weapon.ObjectEffect.SetTo(enchTuple.Key);
                    weapon.EnchantmentAmount = (ushort)enchTuple.Value.Item1;
                    weapon.Name = enchTuple.Value.Item2.Replace("$NAME$", weaponName);
                    weapon.EditorID += "_" + enchTuple.Value.Item3;
                    entry.Data.Reference.SetTo(weapon.ToLink());

                    leveledList.Entries ??= new Noggog.ExtendedList<LeveledItemEntry>();
                    leveledList.Entries!.Add(entry);
                }
                enchantedItems[key] = leveledList.ToLink();
            }
            return enchantedItems[key];
        }

        public static Form Enchant(Form itemLink, int lootLevel, string name) {
            var key = new Tuple<FormKey, int>(itemLink.FormKey, lootLevel);
            if(!enchantedItems.ContainsKey(key)) {



                if(itemLink.TryResolve(Program.State.LinkCache, out var item)) {
                    ItemType? itemType = null;
                    IArmorGetter? armorGetter = null;
                    IWeaponGetter? weaponGetter = null;
                    if(item is IArmorGetter a) {
                        armorGetter = a;

                        if(armorGetter.BodyTemplate!.ArmorType == ArmorType.HeavyArmor) {
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
                        } else if(armorGetter.BodyTemplate!.ArmorType == ArmorType.LightArmor) {
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
                        weaponGetter = w;

                        if(weaponGetter.HasKeyword(Skyrim.Keyword.WeapTypeBattleaxe)) {
                            itemType = ItemType.Battleaxe;
                        } else if(weaponGetter.HasKeyword(Skyrim.Keyword.WeapTypeBow)) {
                            itemType = ItemType.Bow;
                        } else if(weaponGetter.HasKeyword(Skyrim.Keyword.WeapTypeDagger)) {
                            itemType = ItemType.Dagger;
                        } else if(weaponGetter.HasKeyword(Skyrim.Keyword.WeapTypeGreatsword)) {
                            itemType = ItemType.Greatsword;
                        } else if(weaponGetter.HasKeyword(Skyrim.Keyword.WeapTypeMace)) {
                            itemType = ItemType.Mace;
                        } else if(weaponGetter.HasKeyword(Skyrim.Keyword.WeapTypeSword)) {
                            itemType = ItemType.Sword;
                        } else if(weaponGetter.HasKeyword(Skyrim.Keyword.WeapTypeWarAxe)) {
                            itemType = ItemType.Waraxe;
                        } else if(weaponGetter.HasKeyword(Skyrim.Keyword.WeapTypeWarhammer)) {
                            itemType = ItemType.Warhammer;
                        }
                    }
                    if(armorGetter == null && weaponGetter == null) {
                        throw new Exception("Item must be armor or weapon.");
                    }
                    if(itemType == null) {
                        throw new Exception("ItemType is null.");
                    }

                    double totalChance = 0;
                    List<double> itemChancesDouble = new();
                    List<int> itemChancesInt = new();
                    List<Form> newItemList = new();
                    string listName = prefix + name + "_EnchTierSelection_Lvl" + lootLevel + "_";
                    if(armorGetter != null) {
                        listName += armorGetter.EditorID;
                    } else {
                        listName += weaponGetter!.EditorID;
                    }

                    for(int t = 0; t < EnchTiers.Count; t++) {
                        if(armorGetter != null) {
                            newItemList.Add(EnchantArmor(armorGetter, itemType.Value, t + 1, name));
                        } else {
                            newItemList.Add(EnchantWeapon(weaponGetter!, itemType.Value, t + 1, name));
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

                    var chanceList = ChanceList.GetChanceList(newItemList.ToArray(), itemChancesIntBetter.ToArray())!;
                    var tree = RandomTree.GetRandomTree(chanceList, listName);

                    enchantedItems[key] = tree.linkedItem;
                }

            }
            return enchantedItems[key];
        }

        public static void RegisterArmorEnchantments(ItemType itemType, IFormLink<ISkyrimMajorRecordGetter> itemLink, IFormLink<ILeveledItemGetter> enchantmentLists, int tier) {
            var item = itemLink.TryResolve(Program.State.LinkCache);
            string itemName = "";
            if(item != null) {
                if(item is IArmorGetter armor) {
                    itemName = armor.Name!.String!;
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
            if(enchantmentLists.TryResolve(Program.State.LinkCache, out var enchList)) {
                foreach(var entry in enchList.Entries!) {
                    if(entry.Data!.Reference.TryResolve(Program.State.LinkCache, out var enchantedItem)) {
                        string enchantedItemName = "";
                        IFormLink<IEffectRecordGetter> ench;
                        int enchAmount = 0;
                        if(enchantedItem is IArmorGetter armor) {
                            enchantedItemName = armor.Name!.String!;
                            ench = armor.ObjectEffect.AsSetter();
                            enchAmount = armor.EnchantmentAmount.GetValueOrDefault(0);
                        } else {
                            throw new Exception("Must be armor.");
                        }
                        if(!enchantedItemName.Contains(itemName)) {
                            throw new Exception("Enchanted item name must contain base item name as substring.");
                        }
                        if(ench.IsNull) {
                            throw new Exception("Enchanted item has no enchantment.");
                        }
                        if(ench.TryResolve(Program.State.LinkCache, out var effectRecord)) {
                            dict[tier][ench] = new Tuple<int, string, string>(enchAmount, enchantedItemName.Replace(itemName, "$NAME$"), effectRecord.EditorID!);
                        }
                    }
                }
            }
        }

        public static void RegisterWeaponEnchantments(ItemType itemType, IFormLink<ISkyrimMajorRecordGetter> itemLink, IFormLink<ILeveledItemGetter> enchantmentLists, int startingTier) {
            var item = itemLink.TryResolve(Program.State.LinkCache);
            string itemName = "";
            if(item != null) {
                if(item is IWeaponGetter weapon) {
                    itemName = weapon.Name!.String!;
                }
            }
            if(itemName == "") {
                throw new Exception("Item has no name.");
            }
            if(!enchantmentDict.ContainsKey(itemType)) {
                enchantmentDict.Add(itemType, new Dictionary<int, Dictionary<IFormLink<IEffectRecordGetter>, Tuple<int, string, string>>>());
            }
            var dict = enchantmentDict[itemType];
            if(enchantmentLists.TryResolve(Program.State.LinkCache, out var enchList)) {
                foreach(var entry in enchList.Entries!) {
                    if(entry.Data!.Reference.TryResolve(Program.State.LinkCache, out var subListForm)) {
                        if(subListForm is ILeveledItemGetter subList) {
                            int i = startingTier;
                            if(subList.Entries != null) {
                                foreach(var subEntry in subList.Entries) {
                                    if(!dict.ContainsKey(i)) {
                                        dict.Add(i, new Dictionary<IFormLink<IEffectRecordGetter>, Tuple<int, string, string>>());
                                    }
                                    if(subEntry.Data!.Reference.TryResolve(Program.State.LinkCache, out var enchantedItem)) {
                                        string enchantedItemName = "";
                                        IFormLink<IEffectRecordGetter> ench;
                                        int enchAmount = 0;
                                        if(enchantedItem is IWeaponGetter weapon) {
                                            enchantedItemName = weapon.Name!.String!;
                                            ench = weapon.ObjectEffect.AsSetter();
                                            enchAmount = weapon.EnchantmentAmount.GetValueOrDefault(0);
                                        } else {
                                            throw new Exception("Must be weapon.");
                                        }
                                        if(!enchantedItemName.Contains(itemName)) {
                                            throw new Exception("Enchanted item name must contain base item name as substring.");
                                        }
                                        if(ench.IsNull) {
                                            throw new Exception("Enchanted item has no enchantment.");
                                        }
                                        if(ench.TryResolve(Program.State.LinkCache, out var effectRecord)) {
                                            dict[i][ench] = new Tuple<int, string, string>(enchAmount, enchantedItemName.Replace(itemName, "$NAME$"), effectRecord.EditorID!);
                                        }
                                    }
                                    i++;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void RegisterWeaponEnchantmentManual(IFormLink<IWeaponGetter> itemLink, IFormLink<IWeaponGetter> enchantedItemLink, int tier, params ItemType[] itemTypes) {
            if(itemLink.TryResolve(Program.State.LinkCache, out var weapon)) {
                if(enchantedItemLink.TryResolve(Program.State.LinkCache, out var enchantedWeapon)) {
                    var itemName = weapon.Name!.String!;
                    var enchantedItemName = enchantedWeapon.Name!.String!;
                    var ench = enchantedWeapon.ObjectEffect.AsSetter();
                    var enchEditorID = "";
                    var enchAmount = enchantedWeapon.EnchantmentAmount.GetValueOrDefault(0);
                    if(ench.TryResolve(Program.State.LinkCache, out var effectRecord)) {
                        enchEditorID = effectRecord.EditorID!;
                    }
                    foreach(var itemType in itemTypes) {
                        if(!enchantmentDict.ContainsKey(itemType)) {
                            enchantmentDict.Add(itemType, new Dictionary<int, Dictionary<IFormLink<IEffectRecordGetter>, Tuple<int, string, string>>>());
                        }
                        var dict = enchantmentDict[itemType];
                        if(!dict.ContainsKey(tier)) {
                            dict.Add(tier, new Dictionary<IFormLink<IEffectRecordGetter>, Tuple<int, string, string>>());
                        }
                        dict[tier][ench] = new Tuple<int, string, string>(enchAmount, enchantedItemName.Replace(itemName, "$NAME$"), enchEditorID);
                    }
                }
            }
        }
    }
}
