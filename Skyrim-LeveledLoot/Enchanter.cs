using Mutagen.Bethesda.Plugins;
using System;
using System.Collections.Generic;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.FormKeys.SkyrimLE;
using Mutagen.Bethesda.Plugins.Records;
using Noggog;
using System.Linq;
using System.Text.RegularExpressions;

using Form = Mutagen.Bethesda.Plugins.IFormLink<Mutagen.Bethesda.Plugins.Records.IMajorRecordGetter>;
using static Mutagen.Bethesda.Skyrim.Furniture;


namespace LeveledLoot
{
    record EnchantmentEntry
    {
        public readonly ushort enchAmount;
        public readonly string enchantedItemName = "";
        public readonly string enchantmentEditorID = "";

        public EnchantmentEntry(ushort enchAmount, string enchantedItemName, string enchantmentEditorID)
        {
            this.enchAmount = enchAmount;
            this.enchantedItemName = enchantedItemName;
            this.enchantmentEditorID = enchantmentEditorID;
        }
    }
    class Enchanter
    {

        static readonly string prefix = "JLL_";

        private static ItemMaterial? ENCH_1   ;
        private static ItemMaterial? ENCH_1X2 ;
        private static ItemMaterial? ENCH_2   ;
        private static  ItemMaterial? ENCH_2X2 ;
        private static  ItemMaterial? ENCH_3   ;
        private static  ItemMaterial? ENCH_3X2 ;
        private static  ItemMaterial? ENCH_4   ;
        private static  ItemMaterial? ENCH_4X2 ;
        private static  ItemMaterial? ENCH_5   ;
        private static  ItemMaterial? ENCH_5X2 ;
        private static  ItemMaterial? ENCH_6   ;
        private static  ItemMaterial? ENCH_6X2 ;

        static readonly List<ItemMaterial> EnchTiers = new();

        static Dictionary<ItemType, Dictionary<int, Dictionary<IFormLink<IEffectRecordGetter>, EnchantmentEntry>>> enchantmentDict = new();
        static Dictionary<Tuple<FormKey, int>, Form> enchantedItems = new();

        public static void Reset(double doubleChance) {
            enchantmentDict.Clear();
            enchantedItems.Clear();
            foreach(var material in ItemMaterial.ALL) {
                material.enchListMap.Clear();
            }
            ENCH_1 = new("1", 80, 24, 0, 40);
            ENCH_1X2 = new("1x2", 80 * doubleChance, 24 * doubleChance, 0, 40);
            ENCH_2 = new("2", 20, 28, 0, 80);
            ENCH_2X2 = new("2x2", 20 * doubleChance, 28 * doubleChance, 0, 80);
            ENCH_3 = new("3", 0, 24, 11, 120);
            ENCH_3X2 = new("3x2", 0, 24 * doubleChance, 11, 120);
            ENCH_4 = new("4", 0, 20, 24, 160);
            ENCH_4X2 = new("3x2", 0, 20 * doubleChance, 24, 160);
            ENCH_5 = new("5", 0, 16, 37, 200);
            ENCH_5X2 = new("5x2", 0, 16 * doubleChance, 37, 200);
            ENCH_6 = new("6", 0, 12, 50, 240);
            ENCH_6X2 = new("6x2", 0, 12 * doubleChance, 50, 240);

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

        static Form? EnchantArmor(IArmorGetter itemGetter, ItemType itemType, int enchantTier, string name) {
            var key = new Tuple<FormKey, int>(itemGetter.FormKey, -enchantTier);
            if(!enchantedItems.ContainsKey(key)) {
                if(!enchantmentDict.ContainsKey(itemType)) {
                    throw new Exception("No enchantments for item type.");
                }
                var dict = enchantmentDict[itemType];
                if(!dict.ContainsKey(enchantTier)) {
                    return null;
                }
                var numEnchantments = dict[enchantTier].Count();
                LeveledItem? leveledList = null;
                if(numEnchantments > 1) {
                    leveledList = Program.State!.PatchMod.LeveledItems.AddNew();
                    leveledList.EditorID = prefix + name + "_LItemArmor_EnchTier" + enchantTier + "_" + itemGetter.EditorID;

                    foreach(var enchTuple in dict[enchantTier]) {
                        LeveledItemEntry entry = new();
                        entry.Data ??= new LeveledItemEntryData();
                        leveledList.ChanceNone = 0;
                        entry.Data.Count = 1;
                        entry.Data.Level = 1;
                        Statistics.instance.enchantedArmor++;
                        var itemCopy = Program.State!.PatchMod.Armors.AddNew();
                        var itemName = itemGetter.Name == null ? "" : itemGetter.Name.String;
                        itemCopy.DeepCopyIn(itemGetter);
                        itemCopy.ObjectEffect.SetTo(enchTuple.Key);
                        itemCopy.EnchantmentAmount = enchTuple.Value.enchAmount;
                        itemCopy.Name = enchTuple.Value.enchantedItemName.Replace("$NAME$", itemName);
                        itemCopy.EditorID += "_" + enchTuple.Value.enchantmentEditorID;
                        entry.Data.Reference.SetTo(itemCopy.ToLink());

                        if(leveledList.Entries == null) {
                            leveledList.Entries = new Noggog.ExtendedList<LeveledItemEntry>();
                        }
                        leveledList.Entries!.Add(entry);
                    }
                    enchantedItems[key] = leveledList.ToLink();
                } else if(numEnchantments == 1) {
                    var enchTuple = dict[enchantTier].First();
                    Statistics.instance.enchantedArmor++;
                    var itemCopy = Program.State!.PatchMod.Armors.AddNew();
                    var itemName = itemGetter.Name == null ? "" : itemGetter.Name.String;
                    itemCopy.DeepCopyIn(itemGetter);
                    itemCopy.ObjectEffect.SetTo(enchTuple.Key);
                    itemCopy.EnchantmentAmount = enchTuple.Value.enchAmount;
                    itemCopy.Name = enchTuple.Value.enchantedItemName.Replace("$NAME$", itemName);
                    itemCopy.EditorID += "_" + enchTuple.Value.enchantmentEditorID;
                    enchantedItems[key] = itemCopy.ToLink();
                } else {
                    throw new Exception("No enchantment available.");
                }

            }
            return enchantedItems[key];
        }

        static Form? EnchantWeapon(IWeaponGetter itemGetter, ItemType itemType, int enchantTier, string name) {
            var key = new Tuple<FormKey, int>(itemGetter.FormKey, -enchantTier);
            if(!enchantedItems.ContainsKey(key)) {
                if(!enchantmentDict.ContainsKey(itemType)) {
                    throw new Exception("No enchantments for item type.");
                }
                var dict = enchantmentDict[itemType];
                if(!dict.ContainsKey(enchantTier)) {
                    return null;
                }
                var numEnchantments = dict[enchantTier].Count();
                LeveledItem? leveledList = null;
                if(numEnchantments > 1) {
                    leveledList = Program.State!.PatchMod.LeveledItems.AddNew();
                    leveledList.EditorID = prefix + name + "_LItemWeapon_EnchTier" + enchantTier + "_" + itemGetter.EditorID;

                    foreach(var enchTuple in dict[enchantTier]) {
                        LeveledItemEntry entry = new();
                        entry.Data ??= new LeveledItemEntryData();
                        leveledList.ChanceNone = 0;
                        entry.Data.Count = 1;
                        entry.Data.Level = 1;
                        Statistics.instance.enchantedWeapons++;
                        var itemCopy = Program.State!.PatchMod.Weapons.AddNew();
                        var itemName = itemGetter.Name == null ? "" : itemGetter.Name.String;
                        itemCopy.DeepCopyIn(itemGetter);
                        itemCopy.ObjectEffect.SetTo(enchTuple.Key);
                        itemCopy.EnchantmentAmount = (ushort)enchTuple.Value.enchAmount;
                        itemCopy.Name = enchTuple.Value.enchantedItemName.Replace("$NAME$", itemName);
                        itemCopy.EditorID += "_" + enchTuple.Value.enchantmentEditorID;
                        entry.Data.Reference.SetTo(itemCopy.ToLink());

                        if(leveledList.Entries == null) {
                            leveledList.Entries = new Noggog.ExtendedList<LeveledItemEntry>();
                        }
                        leveledList.Entries!.Add(entry);
                    }
                    enchantedItems[key] = leveledList.ToLink();
                } else if(numEnchantments == 1) {
                    var enchTuple = dict[enchantTier].First();
                    Statistics.instance.enchantedWeapons++;
                    var itemCopy = Program.State!.PatchMod.Weapons.AddNew();
                    var itemName = itemGetter.Name == null ? "" : itemGetter.Name.String;
                    itemCopy.DeepCopyIn(itemGetter);
                    itemCopy.ObjectEffect.SetTo(enchTuple.Key);
                    itemCopy.EnchantmentAmount = (ushort)enchTuple.Value.enchAmount;
                    itemCopy.Name = enchTuple.Value.enchantedItemName.Replace("$NAME$", itemName);
                    itemCopy.EditorID += "_" + enchTuple.Value.enchantmentEditorID;
                    enchantedItems[key] = itemCopy.ToLink();
                } else {
                    throw new Exception("No enchantment available.");
                }

            }
            return enchantedItems[key];
        }

        public static Form Enchant(Form itemLink, int lootLevel, string name) {
            var key = new Tuple<FormKey, int>(itemLink.FormKey, lootLevel);
            if(!enchantedItems.ContainsKey(key)) {
                if(itemLink.TryResolve(Program.State.LinkCache, out var item)) {

                    IArmorGetter? armorGetter = null;
                    IWeaponGetter? weaponGetter = null;
                    if(item is IArmorGetter a) {
                        armorGetter = a;

                    } else if(item is IWeaponGetter w) {
                        weaponGetter = w;
                    }
                    if(armorGetter == null && weaponGetter == null) {
                        throw new Exception("Item must be armor or weapon.");
                    }
                    ItemType? itemType = ItemTypeConfig.GetItemTypeFromKeywords(item);
                    if(itemType == null) {
                        throw new Exception("ItemType is null.");
                    }

                    double totalChance = 0;
                    List<double> itemChancesDouble = new();
                    List<Form> newItemList = new();
                    string listName = prefix + name + "_EnchTierSelection_Lvl" + lootLevel + "_";
                    if(armorGetter != null) {
                        listName += armorGetter.EditorID;
                    } else {
                        listName += weaponGetter!.EditorID;
                    }

                    for(int t = 0; t < EnchTiers.Count; t++) {
                        Form? enchantedItem = null;
                        if(armorGetter != null) {
                            enchantedItem = EnchantArmor(armorGetter, itemType.Value, t + 1, name);
                        } else {
                            enchantedItem = EnchantWeapon(weaponGetter!, itemType.Value, t + 1, name);
                        }
                        if(enchantedItem == null) {
                            continue;
                        }
                        newItemList.Add(enchantedItem);

                        var itemMaterial = EnchTiers[t];
                        // linear weight x between 0 and 1 depending on player level and first and last level of the item
                        // 0 -> start chance
                        // 1 -> end chance
                        double x = Math.Min(1.0, Math.Max(0.0, (1.0 * lootLevel - itemMaterial.firstLevel) / (itemMaterial.lastLevel - itemMaterial.firstLevel)));

                        double chance = x * (itemMaterial.endChance - itemMaterial.startChance) + itemMaterial.startChance;
                        totalChance += chance;
                        itemChancesDouble.Add(chance);
                    }

                    var itemChancesIntBetter = CustomMath.ApproximateProbabilities2(itemChancesDouble);

                    var chanceList = ChanceList.GetChanceList(newItemList.ToArray(), itemChancesIntBetter.ToArray())!;
                    var tree = RandomTree.GetRandomTree(chanceList, listName, ref Statistics.instance.enchTierSelectionLists);

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
                enchantmentDict.Add(itemType, new Dictionary<int, Dictionary<IFormLink<IEffectRecordGetter>, EnchantmentEntry>>());
            }
            var dict = enchantmentDict[itemType];
            if(!dict.ContainsKey(tier)) {
                dict.Add(tier, new Dictionary<IFormLink<IEffectRecordGetter>, EnchantmentEntry>());
            }
            if(enchantmentLists.TryResolve(Program.State.LinkCache, out var enchList)) {
                foreach(var entry in enchList.Entries!) {
                    if(entry.Data!.Reference.TryResolve(Program.State.LinkCache, out var enchantedItem)) {
                        string enchantedItemName = "";
                        IFormLink<IEffectRecordGetter> ench;
                        ushort enchAmount = 0;
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
                            dict[tier][ench] = new EnchantmentEntry(enchAmount, enchantedItemName.Replace(itemName, "$NAME$"), effectRecord.EditorID!);
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
                enchantmentDict.Add(itemType, new Dictionary<int, Dictionary<IFormLink<IEffectRecordGetter>, EnchantmentEntry>>());
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
                                        dict.Add(i, new Dictionary<IFormLink<IEffectRecordGetter>, EnchantmentEntry>());
                                    }
                                    if(subEntry.Data!.Reference.TryResolve(Program.State.LinkCache, out var enchantedItem)) {
                                        string enchantedItemName = "";
                                        IFormLink<IEffectRecordGetter> ench;
                                        ushort enchAmount = 0;
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
                                            dict[i][ench] = new EnchantmentEntry(enchAmount, enchantedItemName.Replace(itemName, "$NAME$"), effectRecord.EditorID!);
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

        public static void RegisterNecklaceEnchantments(ItemType itemType, IFormLink<ILeveledItemGetter> enchantmentLists, params string[] itemNames)
        {
            if (!enchantmentDict.ContainsKey(itemType))
            {
                enchantmentDict.Add(itemType, new Dictionary<int, Dictionary<IFormLink<IEffectRecordGetter>, EnchantmentEntry>>());
            }
            var dict = enchantmentDict[itemType];
            if (enchantmentLists.TryResolve(Program.State.LinkCache, out var enchList))
            {
                var armorList = new List<Tuple<int,IArmorGetter>>();
                foreach (var entry in enchList.Entries!)
                {

                    if (entry.Data!.Reference.TryResolve(Program.State.LinkCache, out var entryLink))
                    {
                        if (entryLink is ILeveledItemGetter leveldItem)
                        {
                            RegisterNecklaceEnchantments(itemType, leveldItem.ToLink(), itemNames);
                        } 
                        else if(entryLink is IArmorGetter armor)
                        {
                            armorList.Add(new Tuple<int, IArmorGetter>(entry.Data.Level, armor));
                        }
                    }
                }
                armorList.Sort((a, b) =>    {
                    return a.Item1 - b.Item1;
                });

                int i = 1;
                foreach(var pair in armorList)
                {
                    var armor = pair.Item2;
                    string enchantedItemName = armor.Name!.String!;
                    IFormLink<IEffectRecordGetter> ench = armor.ObjectEffect.AsSetter();
                    var enchAmount = armor.EnchantmentAmount.GetValueOrDefault(0);
                    if (ench.IsNull)
                    {
                        throw new Exception("Enchanted item has no enchantment.");
                    }
                    if (ench.TryResolve(Program.State.LinkCache, out var effectRecord))
                    {
                        var editorID = effectRecord.EditorID!;

                        int tier = -1;
                        var match = Regex.Match(editorID, @"\d+$", RegexOptions.RightToLeft);
                        if(match.Success)
                        {
                            tier = int.Parse(match.Value);
                        }
                        if(tier < 1 || tier > 6)
                        {
                            tier = i;
                            i++;
                        }

                        var name = enchantedItemName;
                        foreach(var itemName in itemNames)
                        {
                            name = name.Replace(itemName, "$NAME$");
                        }
                        if (!dict.ContainsKey(tier))
                        {
                            dict.Add(tier, new Dictionary<IFormLink<IEffectRecordGetter>, EnchantmentEntry>());
                        }
                        dict[tier][ench] = new EnchantmentEntry(enchAmount, name, effectRecord.EditorID!);
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
                            enchantmentDict.Add(itemType, new Dictionary<int, Dictionary<IFormLink<IEffectRecordGetter>, EnchantmentEntry>>());
                        }
                        var dict = enchantmentDict[itemType];
                        if(!dict.ContainsKey(tier)) {
                            dict.Add(tier, new Dictionary<IFormLink<IEffectRecordGetter>, EnchantmentEntry>());
                        }
                        dict[tier][ench] = new EnchantmentEntry(enchAmount, enchantedItemName.Replace(itemName, "$NAME$"), enchEditorID);
                    }
                }
            }
        }

        public static string CombineNames(string a, string b) {
            if(a.StartsWith("$NAME$ of") && b.StartsWith("$NAME$ of")) {
                var aSplit = a.Split(" ");
                var bSplit = b.Split(" ");
                // $NAME$ of ADJCETIVE ENCHANTMENT
                // remove adjective if it is equal for both enchantments
                // only treat as ADJCETIVE, if it starts with capital letter to avoid words like "the"
                var minLength = Math.Min(aSplit.Length, bSplit.Length);
                var name = a + " and";
                bool doNameMerging = true;
                for(int i = 2; i < minLength; i++) {
                    if(doNameMerging && aSplit[i] == bSplit[i] && aSplit[i].Substring(0, 1).ToUpper() == aSplit[i].Substring(0, 1)) {
                        continue;
                    }
                    if(aSplit[i] != bSplit[i] && aSplit[i].Substring(0, 1).ToUpper() == aSplit[i].Substring(0, 1))
                    {
                        doNameMerging = false;
                    }
                    name += " " + bSplit[i];
                }
                for(int i = minLength; i < bSplit.Length; i++) {
                    name += " " + bSplit[i];
                }
                return name;
            }
            if(b.StartsWith("$NAME$ of")) {
                return b.Replace("$NAME$", a);
            }
            return a.Replace("$NAME$", b);
        }

        public static void PostProcessEnchantments(ItemType[] itemTypes) {
            foreach(var itemType in itemTypes) {
                for (int tier = 1; tier <= 6; tier++)
                {
                    if (!enchantmentDict[itemType].ContainsKey(tier))
                    {
                        continue;
                    }
                    var tierSingle = enchantmentDict[itemType][tier];
                    
                    if (!enchantmentDict[itemType].ContainsKey(tier+6))
                    {
                        enchantmentDict[itemType].Add(tier + 6, new Dictionary<IFormLink<IEffectRecordGetter>, EnchantmentEntry>());
                    }
                    var tierDouble = enchantmentDict[itemType][tier + 6];
                    var keys = tierSingle.Keys.ToArray();
                    for (int i = 0; i < keys.Length; i++)
                    {
                        if (keys[i].TryResolve(Program.State.LinkCache, out var effectRecord1))
                        {
                            if (effectRecord1 is IObjectEffectGetter ench1)
                            {
                                for (int j = i + 1; j < keys.Length; j++)
                                {

                                    if (keys[j].TryResolve(Program.State.LinkCache, out var effectRecord2))
                                    {
                                        if (effectRecord2 is IObjectEffectGetter ench2)
                                        {
                                            if (ench1.CastType != ench2.CastType || ench1.TargetType != ench2.TargetType || ench1.EnchantType != ench2.EnchantType)
                                            {
                                                continue;
                                            }
                                            var enchCombined = Program.State.PatchMod.ObjectEffects.AddNew();
                                            enchCombined.DeepCopyIn(ench1);
                                            foreach (var effect2 in ench2.Effects)
                                            {
                                                enchCombined.Effects.Add(effect2.DeepCopy());
                                            }
                                            enchCombined.EditorID = prefix + ench1.EditorID + "_" + ench2.EditorID;
                                            enchCombined.Name += " & " + ench2.Name;
                                            var v1 = tierSingle[keys[i]];
                                            var v2 = tierSingle[keys[j]];
                                            tierDouble.Add(enchCombined.ToLink(), new EnchantmentEntry((ushort) (v1.enchAmount + v2.enchAmount), CombineNames(v1.enchantedItemName, v2.enchantedItemName), enchCombined.EditorID));
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
