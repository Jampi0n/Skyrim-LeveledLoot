using Mutagen.Bethesda.Plugins;
using System;
using System.Collections.Generic;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Plugins.Records;
using Noggog;
using System.Linq;
using System.Text.RegularExpressions;

using Form = Mutagen.Bethesda.Plugins.IFormLink<Mutagen.Bethesda.Plugins.Records.IMajorRecordGetter>;

namespace LeveledLoot {
    class EnchantmentEntry {
        public readonly ushort enchAmount;
        public readonly string enchantedItemName = "";
        public string EnchantmentEditorID => enchantment.EditorID!;
        public readonly IEffectRecordGetter enchantment;

        public EnchantmentEntry(IEffectRecordGetter enchantment, ushort enchAmount, string enchantedItemName) {
            this.enchantment = enchantment;
            this.enchAmount = enchAmount;
            this.enchantedItemName = enchantedItemName;
        }
    }
    class Enchanter {

        static readonly string prefix = "JLL_";

        private static ItemMaterial? ENCH_1;
        private static ItemMaterial? ENCH_1X2;
        private static ItemMaterial? ENCH_2;
        private static ItemMaterial? ENCH_2X2;
        private static ItemMaterial? ENCH_3;
        private static ItemMaterial? ENCH_3X2;
        private static ItemMaterial? ENCH_4;
        private static ItemMaterial? ENCH_4X2;
        private static ItemMaterial? ENCH_5;
        private static ItemMaterial? ENCH_5X2;
        private static ItemMaterial? ENCH_6;
        private static ItemMaterial? ENCH_6X2;

        static readonly List<ItemMaterial> EnchTiers = new();
        public static int numTiers = 0;

        static Dictionary<Tuple<double, int>, List<ItemMaterial>> enchTierCache = new();

        public static List<ItemMaterial> GetEnchTiers(double enchTier) {
            if (enchTier < 0) {
                return EnchTiers;
            }
            var key = new Tuple<double, int>(enchTier, numTiers);
            if (!enchTierCache.ContainsKey(key)) {

                double from = enchTier - 0.5 * (numTiers - 1);
                double to = enchTier + 0.5 * (numTiers - 1);
                var tooLow =  1 - from;
                
                if(tooLow > 0) {
                    from += tooLow;
                    to += tooLow;
                }
                var tooHigh = to - 6;
                if (tooHigh > 0) {
                    from -= tooHigh;
                    to -= tooHigh;
                }


                double fromDiff = Math.Abs(Math.Round(from, MidpointRounding.AwayFromZero) - from);
                double toDiff = Math.Abs(Math.Round(to, MidpointRounding.AwayFromZero) - to);
                int fromInt = 0;
                int toInt = 0;
                if (fromDiff <= toDiff) {
                    fromInt = (int)Math.Round(from, MidpointRounding.AwayFromZero);
                    toInt = fromInt + numTiers - 1;
                } else {
                    toInt = (int)Math.Round(to, MidpointRounding.AwayFromZero);
                    fromInt = toInt - numTiers + 1;
                }
                fromInt = Math.Clamp(fromInt, 1, 6);
                toInt = Math.Clamp(toInt, 1, 6);

                var list = new List<ItemMaterial>();
                for (int i = fromInt - 1; i < toInt; i++) {
                    list.Add(EnchTiers[i]);
                    list.Add(EnchTiers[i + 6]);
                }
                enchTierCache[key] = list;
            }
            return enchTierCache[key];
        }

        static readonly Dictionary<ItemType, Dictionary<int, Dictionary<IFormLink<IEffectRecordGetter>, EnchantmentEntry>>> enchantmentDict = new();
        static readonly Dictionary<Tuple<ItemMaterial, ItemType, int>, Form> enchantedVariants = new();

        public static void Reset() {
            enchantmentDict.Clear();
            enchantedVariants.Clear();
            foreach (var material in ItemMaterial.ALL) {
                material.enchListMap.Clear();
            }
            ENCH_1 = new("1", Program.Settings.enchantmentLootTable.TIER_1, -1);
            ENCH_1X2 = new("1x2", Program.Settings.enchantmentLootTable.TIER_1x2, -7);
            ENCH_2 = new("2", Program.Settings.enchantmentLootTable.TIER_2, -2);
            ENCH_2X2 = new("2x2", Program.Settings.enchantmentLootTable.TIER_2x2, -8);
            ENCH_3 = new("3", Program.Settings.enchantmentLootTable.TIER_3, -3);
            ENCH_3X2 = new("3x2", Program.Settings.enchantmentLootTable.TIER_3x2, -9);
            ENCH_4 = new("4", Program.Settings.enchantmentLootTable.TIER_4, -4);
            ENCH_4X2 = new("4x2", Program.Settings.enchantmentLootTable.TIER_4x2, -10);
            ENCH_5 = new("5", Program.Settings.enchantmentLootTable.TIER_5, -5);
            ENCH_5X2 = new("5x2", Program.Settings.enchantmentLootTable.TIER_5x2, -11);
            ENCH_6 = new("6", Program.Settings.enchantmentLootTable.TIER_6, -6);
            ENCH_6X2 = new("6x2", Program.Settings.enchantmentLootTable.TIER_6x2, -12);

            EnchTiers.AddRange(new List<ItemMaterial>() {
                ENCH_1,
                ENCH_2,
                ENCH_3,
                ENCH_4,
                ENCH_5,
                ENCH_6,
                ENCH_1X2,
                ENCH_2X2,
                ENCH_3X2,
                ENCH_4X2,
                ENCH_5X2,
                ENCH_6X2
            });
        }

        static Form EnchantArmor(IArmorGetter itemGetter, IEffectRecordGetter ench, EnchantmentEntry enchantmentEntry) {
            Statistics.instance.enchantedArmor++;
            var itemCopy = Program.State!.PatchMod.Armors.AddNew();
            var itemName = itemGetter.Name == null ? "" : itemGetter.Name.String;
            itemCopy.DeepCopyIn(itemGetter);
            itemCopy.ObjectEffect.SetTo(ench);
            itemCopy.EnchantmentAmount = enchantmentEntry.enchAmount;
            itemCopy.Name = enchantmentEntry.enchantedItemName.Replace("$NAME$", itemName);
            itemCopy.EditorID += "_" + enchantmentEntry.EnchantmentEditorID;
            return itemCopy.ToLink();
        }

        static Form EnchantWeapon(IWeaponGetter itemGetter, IEffectRecordGetter ench, EnchantmentEntry enchantmentEntry) {
            Statistics.instance.enchantedWeapons++;
            var itemCopy = Program.State!.PatchMod.Weapons.AddNew();
            var itemName = itemGetter.Name == null ? "" : itemGetter.Name.String;
            itemCopy.DeepCopyIn(itemGetter);
            itemCopy.ObjectEffect.SetTo(ench);
            itemCopy.EnchantmentAmount = enchantmentEntry.enchAmount;
            itemCopy.Name = enchantmentEntry.enchantedItemName.Replace("$NAME$", itemName);
            itemCopy.EditorID += "_" + enchantmentEntry.EnchantmentEditorID;
            return itemCopy.ToLink();
        }

        static Form? EnchantVariant(ItemMaterial itemMaterial, ItemType itemType, EnchantmentEntry enchantmentEntry, string name) {
            var variantList = itemMaterial.itemMap[itemType];
            var count = variantList.Count;
            var n = (int)(count * ItemMaterial.maxVariantFraction);
            if (ItemMaterial.maxVariants > 0) {
                n = Math.Min(n, ItemMaterial.maxVariants);
            }
            var ench = enchantmentEntry.enchantment;
            if (n > 1) {
                var order = CustomMath.GetRandomOrder(count);
                Statistics.instance.variantSelectionLists++;
                var leveledList = Program.State!.PatchMod.LeveledItems.AddNew();
                leveledList.EditorID = prefix + name + "_" + enchantmentEntry.EnchantmentEditorID + "_Variants";
                for (int i = 0; i < n; i++) {
                    var index = order[i];
                    var toEnchant = itemMaterial.itemMap[itemType].ElementAt(index).item;
                    LeveledItemEntry entry = new();
                    entry.Data ??= new LeveledItemEntryData();
                    leveledList.ChanceNone = 0;
                    entry.Data.Count = 1;
                    entry.Data.Level = 1;
                    Form? enchanted;
                    if (toEnchant is IWeaponGetter weaponGetter) {
                        enchanted = EnchantWeapon(weaponGetter, ench, enchantmentEntry);
                    } else if (toEnchant is IArmorGetter armorGetter) {
                        enchanted = EnchantArmor(armorGetter, ench, enchantmentEntry);
                    } else {
                        throw new Exception("Must be armor or weapon");
                    }
                    entry.Data.Reference.SetTo(enchanted.FormKey);

                    if (leveledList.Entries == null) {
                        leveledList.Entries = new Noggog.ExtendedList<LeveledItemEntry>();
                    }
                    leveledList.Entries!.Add(entry);

                }
                return leveledList.ToLink();

            } else {
                var toEnchant = itemMaterial.itemMap[itemType].ElementAt(CustomMath.GetRandomInt(0, count)).item;
                if (toEnchant is IWeaponGetter weaponGetter) {
                    return EnchantWeapon(weaponGetter, ench, enchantmentEntry);
                } else if (toEnchant is IArmorGetter armorGetter) {
                    return EnchantArmor(armorGetter, ench, enchantmentEntry);
                }
            }

            return null;
        }

        static Form? EnchantTier(ItemMaterial itemMaterial, ItemType itemType, int enchantTier, string name) {
            var key = new Tuple<ItemMaterial, ItemType, int>(itemMaterial, itemType, -enchantTier);
            if (!enchantedVariants.ContainsKey(key)) {
                if (!enchantmentDict.ContainsKey(itemType)) {
                    throw new Exception("No enchantments for item type.");
                }
                var dict = enchantmentDict[itemType];
                if (!dict.ContainsKey(enchantTier)) {
                    return null;
                }
                var numEnchantments = dict[enchantTier].Count();
                LeveledItem? leveledList = null;
                if (numEnchantments > 1) {
                    Statistics.instance.enchSelectionLists++;
                    leveledList = Program.State!.PatchMod.LeveledItems.AddNew();
                    leveledList.EditorID = prefix + name + "_LItem_EnchTier" + enchantTier;

                    foreach (var enchTuple in dict[enchantTier]) {
                        LeveledItemEntry entry = new();
                        entry.Data ??= new LeveledItemEntryData();
                        leveledList.ChanceNone = 0;
                        entry.Data.Count = 1;
                        entry.Data.Level = 1;
                        var enchanted = EnchantVariant(itemMaterial, itemType, enchTuple.Value, name);
                        if (enchanted == null) {
                            continue;
                        }
                        entry.Data.Reference.SetTo(enchanted.FormKey);

                        if (leveledList.Entries == null) {
                            leveledList.Entries = new Noggog.ExtendedList<LeveledItemEntry>();
                        }
                        leveledList.Entries!.Add(entry);
                    }
                    enchantedVariants[key] = leveledList.ToLink();
                } else if (numEnchantments == 1) {
                    var enchTuple = dict[enchantTier].First();
                    var enchVariants = EnchantVariant(itemMaterial, itemType, enchTuple.Value, name);
                    if (enchVariants == null) {
                        throw new Exception("Could not create enchantments");
                    }
                    enchantedVariants[key] = enchVariants;
                } else {
                    throw new Exception("No enchantment available.");
                }

            }
            return enchantedVariants[key];
        }

        public static Form Enchant(ItemMaterial itemMaterial, ItemType itemType, int lootLevel, string name) {
            var key = new Tuple<ItemMaterial, ItemType, int>(itemMaterial, itemType, lootLevel);
            if (!enchantedVariants.ContainsKey(key)) {




                double totalChance = 0;
                List<double> itemChancesDouble = new();
                List<LeveledListEntry> newItemList = new();
                string listName = prefix + name + "_EnchTierSelection_Lvl" + lootLevel;

                for (int t = 0; t < itemMaterial.enchTierList.Count; t++) {
                    var enchTier = itemMaterial.enchTierList[t];
                    // linear weight x between 0 and 1 depending on player level and first and last level of the item
                    // 0 -> start chance
                    // 1 -> end chance
                    double x = Math.Min(1.0, Math.Max(0.0, (1.0 * lootLevel - enchTier.firstLevel) / (enchTier.lastLevel - enchTier.firstLevel)));

                    double chance = x * (enchTier.endChance - enchTier.startChance) + enchTier.startChance;
                    if (chance <= 0) {
                        continue;
                    }

                    Form? enchantedItem = EnchantTier(itemMaterial, itemType, (int) -enchTier.enchantTier, name);

                    if (enchantedItem == null) {
                        continue;
                    }
                    newItemList.Add(new LeveledListEntry(enchantedItem, 1));

                    totalChance += chance;
                    itemChancesDouble.Add(chance);
                }

                var itemChancesIntBetter = CustomMath.ApproximateProbabilities2(itemChancesDouble);

                var chanceList = ChanceList.GetChanceList(newItemList.ToArray(), itemChancesIntBetter.ToArray())!;
                var tree = RandomTree.GetRandomTree(chanceList, listName, ref Statistics.instance.enchTierSelectionLists);

                enchantedVariants[key] = tree.linkedItem.itemLink;


            }
            return enchantedVariants[key];
        }

        public static void RegisterArmorEnchantments(ItemType itemType, IFormLink<ISkyrimMajorRecordGetter> itemLink, IFormLink<ILeveledItemGetter> enchantmentLists, int tier) {
            var item = itemLink.TryResolve(Program.State.LinkCache);
            string itemName = "";
            if (item != null) {
                if (item is IArmorGetter armor) {
                    itemName = armor.Name!.String!;
                }
            }
            if (itemName == "") {
                throw new Exception("Item has no name.");
            }
            if (!enchantmentDict.ContainsKey(itemType)) {
                enchantmentDict.Add(itemType, new Dictionary<int, Dictionary<IFormLink<IEffectRecordGetter>, EnchantmentEntry>>());
            }
            var dict = enchantmentDict[itemType];
            if (!dict.ContainsKey(tier)) {
                dict.Add(tier, new Dictionary<IFormLink<IEffectRecordGetter>, EnchantmentEntry>());
            }
            if (enchantmentLists.TryResolve(Program.State.LinkCache, out var enchList)) {
                foreach (var entry in enchList.Entries!) {
                    if (entry.Data!.Reference.TryResolve(Program.State.LinkCache, out var enchantedItem)) {
                        string enchantedItemName = "";
                        IFormLink<IEffectRecordGetter> ench;
                        ushort enchAmount = 0;
                        if (enchantedItem is IArmorGetter armor) {
                            enchantedItemName = armor.Name!.String!;
                            ench = armor.ObjectEffect.AsSetter();
                            enchAmount = armor.EnchantmentAmount.GetValueOrDefault(0);
                        } else {
                            throw new Exception("Must be armor.");
                        }
                        if (!enchantedItemName.Contains(itemName)) {
                            throw new Exception("Enchanted item name must contain base item name as substring.");
                        }
                        if (ench.IsNull) {
                            throw new Exception("Enchanted item has no enchantment.");
                        }
                        if (ench.TryResolve(Program.State.LinkCache, out var effectRecord)) {
                            dict[tier][ench] = new EnchantmentEntry(effectRecord, enchAmount, enchantedItemName.Replace(itemName, "$NAME$"));
                        }
                    }
                }
            }
        }

        public static void RegisterWeaponEnchantments(ItemType itemType, IFormLink<ISkyrimMajorRecordGetter> itemLink, IFormLink<ILeveledItemGetter> enchantmentLists, int startingTier) {
            var item = itemLink.TryResolve(Program.State.LinkCache);
            string itemName = "";
            if (item != null) {
                if (item is IWeaponGetter weapon) {
                    itemName = weapon.Name!.String!;
                }
            }
            if (itemName == "") {
                throw new Exception("Item has no name.");
            }
            if (!enchantmentDict.ContainsKey(itemType)) {
                enchantmentDict.Add(itemType, new Dictionary<int, Dictionary<IFormLink<IEffectRecordGetter>, EnchantmentEntry>>());
            }
            var dict = enchantmentDict[itemType];
            if (enchantmentLists.TryResolve(Program.State.LinkCache, out var enchList)) {
                foreach (var entry in enchList.Entries!) {
                    if (entry.Data!.Reference.TryResolve(Program.State.LinkCache, out var subListForm)) {
                        if (subListForm is ILeveledItemGetter subList) {
                            int i = startingTier;
                            if (subList.Entries != null) {
                                foreach (var subEntry in subList.Entries) {
                                    if (!dict.ContainsKey(i)) {
                                        dict.Add(i, new Dictionary<IFormLink<IEffectRecordGetter>, EnchantmentEntry>());
                                    }
                                    if (subEntry.Data!.Reference.TryResolve(Program.State.LinkCache, out var enchantedItem)) {
                                        string enchantedItemName = "";
                                        IFormLink<IEffectRecordGetter> ench;
                                        ushort enchAmount = 0;
                                        if (enchantedItem is IWeaponGetter weapon) {
                                            enchantedItemName = weapon.Name!.String!;
                                            ench = weapon.ObjectEffect.AsSetter();
                                            enchAmount = weapon.EnchantmentAmount.GetValueOrDefault(0);
                                        } else {
                                            throw new Exception("Must be weapon.");
                                        }
                                        if (!enchantedItemName.Contains(itemName)) {
                                            throw new Exception("Enchanted item name must contain base item name as substring.");
                                        }
                                        if (ench.IsNull) {
                                            throw new Exception("Enchanted item has no enchantment.");
                                        }
                                        if (ench.TryResolve(Program.State.LinkCache, out var effectRecord)) {
                                            dict[i][ench] = new EnchantmentEntry(effectRecord, enchAmount, enchantedItemName.Replace(itemName, "$NAME$"));
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

        public static void RegisterJewelryEnchantments(ItemType itemType, IFormLink<ILeveledItemGetter> enchantmentLists, params string[] itemNames) {
            if (!enchantmentDict.ContainsKey(itemType)) {
                enchantmentDict.Add(itemType, new Dictionary<int, Dictionary<IFormLink<IEffectRecordGetter>, EnchantmentEntry>>());
            }
            var dict = enchantmentDict[itemType];
            if (enchantmentLists.TryResolve(Program.State.LinkCache, out var enchList)) {
                var armorList = new List<Tuple<int, IArmorGetter>>();
                foreach (var entry in enchList.Entries!) {

                    if (entry.Data!.Reference.TryResolve(Program.State.LinkCache, out var entryLink)) {
                        if (entryLink is ILeveledItemGetter leveldItem) {
                            RegisterJewelryEnchantments(itemType, leveldItem.ToLink(), itemNames);
                        } else if (entryLink is IArmorGetter armor) {
                            armorList.Add(new Tuple<int, IArmorGetter>(entry.Data.Level, armor));
                        }
                    }
                }
                armorList.Sort((a, b) => {
                    return a.Item1 - b.Item1;
                });

                int i = 1;
                foreach (var pair in armorList) {
                    var armor = pair.Item2;
                    string enchantedItemName = armor.Name!.String!;
                    IFormLink<IEffectRecordGetter> ench = armor.ObjectEffect.AsSetter();
                    var enchAmount = armor.EnchantmentAmount.GetValueOrDefault(0);
                    if (ench.IsNull) {
                        throw new Exception("Enchanted item has no enchantment.");
                    }
                    if (ench.TryResolve(Program.State.LinkCache, out var effectRecord)) {
                        var editorID = effectRecord.EditorID!;

                        int tier = -1;
                        var match = Regex.Match(editorID, @"\d+$", RegexOptions.RightToLeft);
                        if (match.Success) {
                            tier = int.Parse(match.Value);
                        }
                        if (tier < 1 || tier > 6) {
                            tier = i;
                            i++;
                        }

                        var name = enchantedItemName;
                        foreach (var itemName in itemNames) {
                            name = name.Replace(itemName, "$NAME$");
                        }
                        if (!dict.ContainsKey(tier)) {
                            dict.Add(tier, new Dictionary<IFormLink<IEffectRecordGetter>, EnchantmentEntry>());
                        }
                        dict[tier][ench] = new EnchantmentEntry(effectRecord, enchAmount, name);
                    }

                }
            }
        }

        public static void RegisterWeaponEnchantmentManual(IFormLink<IWeaponGetter> itemLink, IFormLink<IWeaponGetter> enchantedItemLink, int tier, params ItemType[] itemTypes) {
            if (itemLink.TryResolve(Program.State.LinkCache, out var weapon)) {
                if (enchantedItemLink.TryResolve(Program.State.LinkCache, out var enchantedWeapon)) {
                    var itemName = weapon.Name!.String!;
                    var enchantedItemName = enchantedWeapon.Name!.String!;
                    var ench = enchantedWeapon.ObjectEffect.AsSetter();

                    var enchAmount = enchantedWeapon.EnchantmentAmount.GetValueOrDefault(0);
                    if (ench.TryResolve(Program.State.LinkCache, out var effectRecord)) {
                        foreach (var itemType in itemTypes) {
                            if (!enchantmentDict.ContainsKey(itemType)) {
                                enchantmentDict.Add(itemType, new Dictionary<int, Dictionary<IFormLink<IEffectRecordGetter>, EnchantmentEntry>>());
                            }
                            var dict = enchantmentDict[itemType];
                            if (!dict.ContainsKey(tier)) {
                                dict.Add(tier, new Dictionary<IFormLink<IEffectRecordGetter>, EnchantmentEntry>());
                            }
                            dict[tier][ench] = new EnchantmentEntry(effectRecord, enchAmount, enchantedItemName.Replace(itemName, "$NAME$"));
                        }
                    }

                }
            }
        }

        public static string CombineNames(string a, string b) {
            if (a.StartsWith("$NAME$ of") && b.StartsWith("$NAME$ of")) {
                var aSplit = a.Split(" ");
                var bSplit = b.Split(" ");
                // $NAME$ of ADJCETIVE ENCHANTMENT
                // remove adjective if it is equal for both enchantments
                // only treat as ADJCETIVE, if it starts with capital letter to avoid words like "the"
                var minLength = Math.Min(aSplit.Length, bSplit.Length);
                var name = a + " and";
                bool doNameMerging = true;
                for (int i = 2; i < minLength; i++) {
                    if (doNameMerging && aSplit[i] == bSplit[i] && aSplit[i].Substring(0, 1).ToUpper() == aSplit[i].Substring(0, 1)) {
                        continue;
                    }
                    if (aSplit[i] != bSplit[i] && aSplit[i].Substring(0, 1).ToUpper() == aSplit[i].Substring(0, 1)) {
                        doNameMerging = false;
                    }
                    name += " " + bSplit[i];
                }
                for (int i = minLength; i < bSplit.Length; i++) {
                    name += " " + bSplit[i];
                }
                return name;
            }
            if (b.StartsWith("$NAME$ of")) {
                return b.Replace("$NAME$", a);
            }
            return a.Replace("$NAME$", b);
        }

        public static void PostProcessEnchantments(List<List<ItemType>> itemTypeHierarchy) {
            var itemTypes = new List<ItemType>();
            foreach (var category in itemTypeHierarchy) {
                foreach (var itemType in category) {
                    itemTypes.Add(itemType);
                }
            }


            foreach (var itemType in itemTypes) {
                for (int tier = 1; tier <= 6; tier++) {
                    if (!enchantmentDict[itemType].ContainsKey(tier)) {
                        continue;
                    }
                    var tierSingle = enchantmentDict[itemType][tier];

                    if (!enchantmentDict[itemType].ContainsKey(tier + 6)) {
                        enchantmentDict[itemType].Add(tier + 6, new Dictionary<IFormLink<IEffectRecordGetter>, EnchantmentEntry>());
                    }
                    var tierDouble = enchantmentDict[itemType][tier + 6];
                    var keys = tierSingle.Keys.ToArray();
                    for (int i = 0; i < keys.Length; i++) {
                        if (keys[i].TryResolve(Program.State.LinkCache, out var effectRecord1)) {
                            if (effectRecord1 is IObjectEffectGetter ench1) {
                                for (int j = i + 1; j < keys.Length; j++) {

                                    if (keys[j].TryResolve(Program.State.LinkCache, out var effectRecord2)) {
                                        if (effectRecord2 is IObjectEffectGetter ench2) {
                                            if (ench1.CastType != ench2.CastType || ench1.TargetType != ench2.TargetType || ench1.EnchantType != ench2.EnchantType) {
                                                continue;
                                            }
                                            var enchCombined = Program.State.PatchMod.ObjectEffects.AddNew();
                                            enchCombined.DeepCopyIn(ench1);
                                            foreach (var effect2 in ench2.Effects) {
                                                enchCombined.Effects.Add(effect2.DeepCopy());
                                            }
                                            enchCombined.EditorID = prefix + ench1.EditorID + "_" + ench2.EditorID;
                                            enchCombined.Name += " & " + ench2.Name;
                                            var v1 = tierSingle[keys[i]];
                                            var v2 = tierSingle[keys[j]];
                                            tierDouble.Add(enchCombined.ToLink(), new EnchantmentEntry(enchCombined, (ushort)(v1.enchAmount + v2.enchAmount), CombineNames(v1.enchantedItemName, v2.enchantedItemName)));
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
