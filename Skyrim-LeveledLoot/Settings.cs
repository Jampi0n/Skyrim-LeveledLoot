using Mutagen.Bethesda.Synthesis.Settings;
using System;
using System.Collections.Generic;



namespace LeveledLoot {

    public enum EnchantmentExploration
    {
        LeveledList,
        LeveledListCombineItemSlots,
        LeveledListCombineItemSlotsSeparateItemType,
        LeveledListCombineItemSlotsArmorTypeCheck,
        None,
    }

    public static class EnumInfo
    {
        public const string enchantmentExploration = "" +
            "LeveledList: Parses the leveled list of generic enchanted armor/weapons to determine available enchantments for specific item types.\n" +
                "    Example: 'Fortify Magicka' will not be used for Gauntlets, because regular enchanted Gauntlets do not exist with this enchantment.\n" +
            "LeveledListCombineItemSlots: Same as LeveledList, but adds enchantments available to different item slots.\n" +
                "    Example: 'Fortify Magicka' will now be used for Gauntlets, because the enchantment exists for a different item type (Helmet) and " +
                "the enchantment can also be put on Gauntlets.\n" +
            "LeveledListCombineItemSlotsSeparateItemType: Same as LeveledListCombineItemSlots, but different armor types (light, heavy, clothing, jewelry) " +
                "and weapon types (onehanded, twohanded, bow) are treated separately.\n" +
                "    Example: 'Fortify Heavy Armor' is only used on heavy cuirasses, rings and necklaces (heavy and jewelry). " +
                "This will add the enchantment to other slots of heavy armors and jewelry, but not to light armor or clothing.\n" +
            "LeveledListCombineItemSlotsArmorTypeCheck: Same as LeveledListCombineItemSlots, but enchantments affecting heavy or light armor are not added to " +
                "the other armor type.\n" +
                "    Example: 'Fortify Heavy Armor' will not be added to light armor, because it is only part of the heavy armor leveled lists " +
                "and the enchantment contains the heavy armor actor value.\n";
    }

    public class EnchantmentSettings {
        [SynthesisTooltip("How available enchantments are determined.\n" + EnumInfo.enchantmentExploration)]
        public EnchantmentExploration enchantmentExploration = EnchantmentExploration.LeveledListCombineItemSlotsSeparateItemType;
        [SynthesisTooltip("Enchantments can be marked for use with this patcher by containing \"JLL\" in the editorID. These enchantments will also be used to generate enchanted items. This can be useful for patching mods adding new enchantments.")]
        public bool considerMarkedEnchantments = true;
        [SynthesisTooltip("This setting will not generate enchantments on items if the enchantments violate WornRestrictions. This is useful, if a mod modifies WornRestrictions without adjusting the leveled lists accordingly.")]
        public bool enforceWornRestrictions = true;
        public double doubleEnchantmentsPowerFactor = 0.666667;
    }

    public class Apparel {
        [SynthesisTooltip("Patch regular armor loot table")]
        public bool enabled = true;
        [SynthesisTooltip("Add craftable armor from mods to loot table.")]
        public bool addCraftableItems = true;
        [SynthesisTooltip("Generate enchanted versions of armor for enchanted loot table.")]
        public bool enchantedArmor = true;
        [SynthesisTooltip("Generate enchanted versions of jewlry for enchanted loot table.")]
        public bool enchantedJewelry = true;
        public EnchantmentSettings enchantmentSettings = new();
        [SynthesisTooltip("Limits the number of jewelry variants for the same enchantment to a fraction of the total variants.")]
        public double maxEnchJewelryVariantsFraction = 1.0;
        [SynthesisTooltip("Limits the number of jewelry variants for the same enchantment to an absolute value. -1 is no limit. Vanilla: 1")]
        public int maxEnchJewelryVariants = 1;
        [SynthesisTooltip("Limits the number of enchantment tiers per material. Vanilla: 3\nFor example, iron enchanted items will always have tier 1-3, while daedric will have have tier 4-6.")]
        public int maxTiersPerMaterial = 3;
        [SynthesisTooltip("Loot table changes for bandit armor (low tier armor)")]
        public bool bandit = true;
        [SynthesisTooltip("Loot table changes for draugr loot armor (hide, leather, iron, steel, draugr, scaled, ebony, dragon)")]
        public bool draugrLoot = true;
        [SynthesisTooltip("Requires DraugrLoot. Draugr Deathlords will use shields from the Draugr Loot table instead of Ebony Shields.")]
        public bool draugrShields = true;
        [SynthesisTooltip("Loot table changes for thalmor armor (elven light, elven, glass")]
        public bool thalmor = true;
    }
    public class Weapons {
        [SynthesisTooltip("Patch regular weapon loot table")]
        public bool enabled = true;
        [SynthesisTooltip("Add craftable weapons from mods to loot table.")]
        public bool addCraftableItems = true;
        [SynthesisTooltip("Generate enchanted versions of weapons for enchanted loot table.")]
        public bool enchantedItems = true;
        public EnchantmentSettings enchantmentSettings = new();
        [SynthesisTooltip("Limits the number of enchantment tiers per material. Vanilla: 3\nFor example, iron enchanted items will always have tier 1-3, while daedric will have have tier 4-6.")]
        public int maxTiersPerMaterial = 3;
        [SynthesisTooltip("Loot table changes for bandit weapons (regular weapons)")]
        public bool bandit = true;
        [SynthesisTooltip("Loot table changes for draugr loot weapons (iron, steel, draugr, draugr honed, draugr hero, ebony, dragon)")]
        public bool draugrLoot = true;
        [SynthesisTooltip("Loot table changes for draugr shields (iron, steel, ebony, drago)")]
        public bool draugrWeapons = true;
        [SynthesisTooltip("Loot table changes for vampire weapons (regular weapons)")]
        public bool vampire = true;
        [SynthesisTooltip("Loot table changes for draugr weapons (ebony replaced by nord hero)")]
        public bool thalmor = true;
        [SynthesisTooltip("Loot table changes for dremora weapons (regular weapons with fire enchantments)")]
        public bool dremora = true;
    }

    public class Misc {
        public bool collegeRobes = true;
        public bool soulGems = true;
        public bool ingotsAndOre = true;
    }

    public class LootEntry
    {
        public double startChance;
        public double endChance;
        public int startLevel;
        public int endLevel;
    }

    public static class LootEntryManager
    {
        /*public static string Get(double startChance, double endChance, int startLevel, int endLevel)
        {
            return "" + startChance + "," + endChance + "," + startLevel + "," + endLevel;
        }*/
        public static LootEntry Get(double startChance, double endChance, int startLevel, int endLevel)
        {
            return new LootEntry()
            {
                startChance = startChance,
                endChance = endChance,
                startLevel = startLevel,
                endLevel = endLevel
            };
        }
    }

    public class EnchantmentLootTable
    {
        public LootEntry TIER_1 = LootEntryManager.Get(80, 24, 0, 40);
        public LootEntry TIER_1x2 = LootEntryManager.Get(0, 0, 0, 1);
        public LootEntry TIER_2 = LootEntryManager.Get(20, 28, 0, 80);
        public LootEntry TIER_2x2 = LootEntryManager.Get(0, 0, 0, 1);
        public LootEntry TIER_3 = LootEntryManager.Get(0, 24, 11, 120);
        public LootEntry TIER_3x2 = LootEntryManager.Get(0, 6, 11, 120);
        public LootEntry TIER_4 = LootEntryManager.Get(0, 20, 24, 160);
        public LootEntry TIER_4x2 = LootEntryManager.Get(0, 5, 24, 160);
        public LootEntry TIER_5 = LootEntryManager.Get(0, 16, 37, 200);
        public LootEntry TIER_5x2 = LootEntryManager.Get(0, 4, 37, 200);
        public LootEntry TIER_6 = LootEntryManager.Get(0, 12, 50, 240);
        public LootEntry TIER_6x2 = LootEntryManager.Get(0, 3, 50, 240);
    }

    public class WeaponLootTable
    {
        public LootEntry IRON = LootEntryManager.Get(75, 22, 0, 20);
        public LootEntry STEEL = LootEntryManager.Get(20, 18, 0, 30);
        public LootEntry DWARVEN = LootEntryManager.Get(5, 15, 0, 45);
        public LootEntry ELVEN = LootEntryManager.Get(0, 13, 10, 55);
        public LootEntry ORCISH = LootEntryManager.Get(0, 10, 16, 75);
        public LootEntry GLASS = LootEntryManager.Get(0, 7, 40, 140);
        public LootEntry EBONY = LootEntryManager.Get(0, 6, 48, 160);
        public LootEntry DRAGON = LootEntryManager.Get(0, 4, 64, 200);
        public LootEntry DAEDRIC = LootEntryManager.Get(0, 3, 80, 220);
        [SynthesisTooltip("This will contain modded weapons with higher value than daedric weapons.")]
        public LootEntry ULTIMATE = LootEntryManager.Get(0, 2, 100, 240);

        [SynthesisTooltip("Nordic weapons are part of the DLC2 loot table.")]
        public LootEntry NORDIC = LootEntryManager.Get(0, 8, 32, 125);
        [SynthesisTooltip("Stalhrim weapons are part of the DLC2 loot table.")]
        public LootEntry STALHRIM = LootEntryManager.Get(0, 5, 56, 180);

        [SynthesisTooltip("Draugr weapons are only part of the draugr loot table and can be found on draugr and in draugr chests.")]
        public LootEntry DRAUGR = LootEntryManager.Get(75, 22, 0, 20);
        [SynthesisTooltip("Draugr weapons are only part of the draugr loot table and can be found on draugr and in draugr chests.")]
        public LootEntry DRAUGR_HONED = LootEntryManager.Get(0, 17, 16, 75);
        [SynthesisTooltip("Draugr weapons are only part of the draugr loot table and can be found on draugr and in draugr chests.")]
        public LootEntry DRAUGR_HERO = LootEntryManager.Get(0, 12, 48, 160);
    }

    public class LightArmorLootTable
    {
        public LootEntry HIDE = LootEntryManager.Get(75, 22, 0, 20);
        public LootEntry LEATHER = LootEntryManager.Get(20, 18, 0, 30);
        public LootEntry ELVEN = LootEntryManager.Get(5, 15, 0, 50);
        public LootEntry SCALED = LootEntryManager.Get(0, 12, 8, 60);
        public LootEntry GLASS = LootEntryManager.Get(0, 6, 48, 160);
        public LootEntry DRAGON_LIGHT = LootEntryManager.Get(0, 3, 80, 220);
        [SynthesisTooltip("This will contain modded armor with higher value than dragonscale armor.")]
        public LootEntry ULTIMATE = LootEntryManager.Get(0, 2, 100, 240);

        [SynthesisTooltip("Chitin armor is part of the DLC2 loot table.")]
        public LootEntry CHITIN_LIGHT = LootEntryManager.Get(5, 15, 0, 50);
        [SynthesisTooltip("Stalhrim armor is part of the DLC2 loot table.")]
        public LootEntry STALHRIM_LIGHT = LootEntryManager.Get(0, 5, 56, 180);

        [SynthesisTooltip("Elven light armor is only part of the thalmor loot table and can be found on thalmor.")]
        public LootEntry ELVEN_LIGHT = LootEntryManager.Get(75, 22, 0, 20);
    }

    public class HeavyArmorLootTable
    {
        public LootEntry IRON = LootEntryManager.Get(75, 22, 0, 20);
        public LootEntry STEEL = LootEntryManager.Get(20, 18, 0, 30);
        public LootEntry DWARVEN = LootEntryManager.Get(5, 15, 0, 50);
        public LootEntry ORCISH = LootEntryManager.Get(0, 10, 16, 75);
        public LootEntry STEELPLATE = LootEntryManager.Get(0, 12, 8, 60);
        public LootEntry EBONY = LootEntryManager.Get(0, 6, 48, 160);
        public LootEntry DRAGON_HEAVY = LootEntryManager.Get(0, 4, 64, 200);
        public LootEntry DAEDRIC = LootEntryManager.Get(0, 3, 80, 220);
        [SynthesisTooltip("This will contain modded armor with higher value than daedric armor.")]
        public LootEntry ULTIMATE = LootEntryManager.Get(0, 2, 100, 240);

        [SynthesisTooltip("Bonemold armor is part of the DLC2 loot table.")]
        public LootEntry BONEMOLD = LootEntryManager.Get(20, 18, 0, 30);
        [SynthesisTooltip("Chitin armor is part of the DLC2 loot table.")]
        public LootEntry CHITIN_HEAVY = LootEntryManager.Get(5, 15, 0, 50);
        [SynthesisTooltip("Stalhrim armor is part of the DLC2 loot table.")]
        public LootEntry STALHRIM_HEAVY = LootEntryManager.Get(0, 5, 56, 180);
        [SynthesisTooltip("Nordic armor is part of the DLC2 loot table.")]
        public LootEntry NORDIC = LootEntryManager.Get(0, 8, 32, 125);

        [SynthesisTooltip("Draugr armor is only part of the draugr loot table and can be found in draugr chests.")]
        public LootEntry DRAUGR = LootEntryManager.Get(75, 22, 0, 20);
    }

    public class ArmorLootTable
    {
        public HeavyArmorLootTable heavyArmorLootTable = new();
        public LightArmorLootTable LightArmorLootTable = new();
    }

    public class CollegeRobesLootTable
    {
        public LootEntry NOVICE = LootEntryManager.Get(80, 33, 0, 20);
        public LootEntry APPRENTICE = LootEntryManager.Get(20, 29, 0, 80);
        public LootEntry ADEPT = LootEntryManager.Get(0, 20, 12, 140);
        public LootEntry EXPERT = LootEntryManager.Get(0, 12, 20, 200);
        public LootEntry MASTER = LootEntryManager.Get(0, 6, 28, 240);
    }

    public class SmithingMaterialLootTable
    {
        public LootEntry COAL = LootEntryManager.Get(10, 150, 0, 50);
        public LootEntry IRON = LootEntryManager.Get(50, 250, 0, 50);
        public LootEntry STEEL = LootEntryManager.Get(40, 250, 0, 50);
        public LootEntry CORUNDUM = LootEntryManager.Get(0, 120, 5, 80);
        public LootEntry DWARVEN = LootEntryManager.Get(0, 80, 5, 80);
        public LootEntry ORICHALCUM = LootEntryManager.Get(0, 60, 15, 150);
        public LootEntry QUICKSILVER = LootEntryManager.Get(0, 90, 10, 120);
        public LootEntry MOONSTONE = LootEntryManager.Get(0, 70, 5, 150);
        public LootEntry MALACHITE = LootEntryManager.Get(0, 50, 30, 200);
        public LootEntry EBONY = LootEntryManager.Get(0, 40, 35, 240);
        public LootEntry SILVER = LootEntryManager.Get(0, 110, 5, 80);
        public LootEntry GOLD = LootEntryManager.Get(0, 70, 15, 150);
    }

    public class SoulGemLootTable
    {
        public LootEntry PETTY = LootEntryManager.Get(75, 20, 0, 50);
        public LootEntry LESSER = LootEntryManager.Get(20, 20, 0, 75);
        public LootEntry COMMON = LootEntryManager.Get(5, 20, 0, 125);
        public LootEntry GREATER = LootEntryManager.Get(0, 20, 5, 175);
        public LootEntry GRAND = LootEntryManager.Get(0, 12, 10, 225);
        public LootEntry BLACK = LootEntryManager.Get(0, 12, 10, 225);
    }

    public class MiscLootTable
    {
        public CollegeRobesLootTable collegeRobesLootTable = new();
        public SmithingMaterialLootTable smithingMaterialLootTable = new();
        public SoulGemLootTable soulGemLootTable = new();
    }

    public class JewelryLootTable {

        public LootEntry SILVER = LootEntryManager.Get(75, 20, 0, 50);
        public LootEntry GOLD_AND_SILVER_WITH_GEMS = LootEntryManager.Get(20, 20, 0, 75);
        public LootEntry GOLD_WITH_GEMS = LootEntryManager.Get(5, 20, 0, 125);
        public LootEntry GOLD_WITH_DIAMONDS = LootEntryManager.Get(0, 20, 5, 175);
        public LootEntry ULTIMATE = LootEntryManager.Get(0, 12, 10, 225);

    }

    public class Settings {
        public Apparel apparel = new();
        public Weapons weapons = new();
        public Misc misc = new();
        public ArmorLootTable armorLootTable = new();
        public WeaponLootTable weaponLootTable = new();
        public EnchantmentLootTable enchantmentLootTable = new();
        public MiscLootTable miscLootTable = new();
        public JewelryLootTable jewelryLootTable = new();
    }
}
