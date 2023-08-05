using Mutagen.Bethesda.Synthesis.Settings;


namespace LeveledLoot {
    public class Armor {
        [SynthesisTooltip("Patch regular armor loot table")]
        public bool enabled = true;
        [SynthesisTooltip("Add craftable armor from mods to loot table.")]
        public bool addCraftableItems = true;
        [SynthesisTooltip("Generate enchanted versions of armor for enchanted loot table.")]
        public bool enchantedItems = true;
        [SynthesisTooltip("Generate extra rare enchantment tier containing two tier 6 enchantments.")]
        public bool generateDoubleEnchantments = true;
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
        [SynthesisTooltip("Generate extra rare enchantment tier containing two tier 6 enchantments.")]
        public bool generateDoubleEnchantments = true;

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

    public class Settings {
        public Armor armor = new();
        public Weapons weapons = new();
        public Misc misc = new();
    }
}
