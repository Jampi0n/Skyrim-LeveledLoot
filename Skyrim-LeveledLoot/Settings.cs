using Mutagen.Bethesda.Synthesis.Settings;
using System;
using System.Collections.Generic;



namespace LeveledLoot {

    public enum EnchantmentExploration
    {
        LeveledList,
        LeveledListCombineItemSlots,
        LeveledListCombineItemSlotsSeparateItemType,
        All,
    }

    public static class EnumInfo
    {
        public const string enchantmentExploration = "" +
            "LeveledList: Parses the leveled list of generic enchanted armor/weapons to determine available enchantments for specific item types. " +
                "For example 'Fortify Magicka' will not be used for Gauntlets, because regular enchanted Gauntlets do not exist with this enchantment.\n" +
            "LeveledListCombineItemSlots: Same as LeveledList, but adds enchantments available to different item slots.\n" +
                "For example 'Fortify Magicka' will now be used for Gauntlets, because the enchantment exists for a different item type (Helmet) and " +
                "the enchantment can also be put on Gauntlets.\n" +
            "LeveledListCombineItemSlotsSeparateItemType: Same as LeveledListCombineItemSlots, but different armor types (light, heavy, clothing, jewelry) " +
                "and weapon types (onehanded, twohanded, bow) are treated separately.\n" +
                "For example 'Fortify Heavy Armor' is only used on heavy cuirasses, rings and necklaces (heavy and jewelry). " +
                "This will add the enchantment to other slots of heavy armors and jewelry, but not to light armor or clothing." +
            "All: Parses all available enchantments for learnable enchantments and applys them according available item types.\n" +
                "For example 'Silent Moon Enchantment' is a learnable enchantment and will therefore be added to items in the regular loot table, even if the enchantment can usually only be found at a specific location.";
    }

    public class Apparel {
        [SynthesisTooltip("Patch regular armor loot table")]
        public bool enabled = true;
        [SynthesisTooltip("Add craftable armor from mods to loot table.")]
        public bool addCraftableItems = true;
        [SynthesisTooltip("Generate enchanted versions of armor and jewelry for enchanted loot table.")]
        public bool enchantedItems = true;
        [SynthesisTooltip("Chance to find double enchanted items instead of single enchanted items.")]
        public double doubleEnchantmentChance = 0.25;
        [SynthesisTooltip("How available enchantments are determined.\n" + EnumInfo.enchantmentExploration)]
        public EnchantmentExploration enchantmentExploration = EnchantmentExploration.LeveledListCombineItemSlotsSeparateItemType;
        [SynthesisTooltip("If disabled, every enchantments uses a random jewelry item per slot and tier.\n" +
            "For example, the enchantment 'Fortify Archery' at tier 6 can only appear on a silver garnet ring and a gold diamond nechlace. " +
            "This reduces the number of generated records significantly without affecting gameplay.")]
        public bool generateAllJewelry = true;
        [SynthesisTooltip("Loot table changes for bandit armor (low tier armor)")]
        public bool bandit = true;
        [SynthesisTooltip("Loot table changes for draugr loot armor (hide, leather, iron, steel, draugr, scaled, ebony, dragon)")]
        public bool draugrLoot = true;
        [SynthesisTooltip("Loot table changes for draugr shields (remove ebony shield)")]
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
        [SynthesisTooltip("Chance to find double enchanted items instead of single enchanted items.")]
        public double doubleEnchantmentChance = 0.25;
        [SynthesisTooltip("How available enchantments are determined.\n" + EnumInfo.enchantmentExploration)]
        public EnchantmentExploration enchantmentExploration = EnchantmentExploration.LeveledListCombineItemSlotsSeparateItemType;
        [SynthesisTooltip("Loot table changes for bandit weapons (regular weapons)")]
        public bool bandit = true;
        [SynthesisTooltip("Loot table changes for draugr loot weapons (iron, steel, draugr, draugr honed, draugr hero, ebony, dragon)")]
        public bool draugrLoot = true;
        [SynthesisTooltip("Loot table changes for draugr shields (iron, steel, ebony, drago)")]
        public bool draugrWeapons = true;
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
        public LootEntry TIER_2 = LootEntryManager.Get(20, 28, 0, 80);
        public LootEntry TIER_3 = LootEntryManager.Get(0, 24, 11, 120);
        public LootEntry TIER_4 = LootEntryManager.Get(0, 20, 24, 160);
        public LootEntry TIER_5 = LootEntryManager.Get(0, 16, 37, 200);
        public LootEntry TIER_6 = LootEntryManager.Get(0, 12, 50, 240);
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
        public LootEntry GREATER = LootEntryManager.Get(16, 20, 5, 175);
        public LootEntry GRAND = LootEntryManager.Get(0, 12, 10, 225);
        public LootEntry BLACK = LootEntryManager.Get(0, 12, 10, 225);
    }

    public class MiscLootTable
    {
        public CollegeRobesLootTable collegeRobesLootTable = new();
        public SmithingMaterialLootTable smithingMaterialLootTable = new();
        public SoulGemLootTable soulGemLootTable = new();
    }

    public class Settings {
        public Apparel apparel = new();
        public Weapons weapons = new();
        public Misc misc = new();
        public ArmorLootTable armorLootTable = new();
        public WeaponLootTable weaponLootTable = new();
        public EnchantmentLootTable enchantmentLootTable = new();
        public MiscLootTable miscLootTable = new();
    }
}
