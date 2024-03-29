using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.FormKeys.SkyrimLE;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;

using Form = Mutagen.Bethesda.Plugins.IFormLink<Mutagen.Bethesda.Plugins.Records.IMajorRecordGetter>;

using SKYA = Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.Armor;
using DBA = Mutagen.Bethesda.FormKeys.SkyrimLE.Dragonborn.Armor;
using DGA = Mutagen.Bethesda.FormKeys.SkyrimLE.Dawnguard.Armor;
using SKYL = Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.LeveledItem;
using DBL = Mutagen.Bethesda.FormKeys.SkyrimLE.Dragonborn.LeveledItem;
using DGL = Mutagen.Bethesda.FormKeys.SkyrimLE.Dawnguard.LeveledItem;
using SKYM = Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.MiscItem;
using DBM = Mutagen.Bethesda.FormKeys.SkyrimLE.Dragonborn.MiscItem;
using DGM = Mutagen.Bethesda.FormKeys.SkyrimLE.Dawnguard.MiscItem;
using Noggog;
using Newtonsoft.Json.Linq;

namespace LeveledLoot {

    enum Enchantment {
        Regen,
        Restoration,
        Destruction,
        Illusion,
        Conjuration,
        Alteration
    }

    enum SmithingMaterial {
        Raw,
        Processed,
        ProcessedBlacksmith
    }

    class MiscConfig : LootConfig<MiscConfig>{

        public ItemMaterial NOVICE = new("Novice", Program.Settings.miscLootTable.collegeRobesLootTable.NOVICE);
        public ItemMaterial APPRENTICE = new("Apprentice", Program.Settings.miscLootTable.collegeRobesLootTable.APPRENTICE);
        public ItemMaterial ADEPT = new("Adept", Program.Settings.miscLootTable.collegeRobesLootTable.ADEPT);
        public ItemMaterial EXPERT = new("Expert", Program.Settings.miscLootTable.collegeRobesLootTable.EXPERT);
        public ItemMaterial MASTER = new("Master", Program.Settings.miscLootTable.collegeRobesLootTable.MASTER);

        public ItemMaterial COAL = new("Coal", Program.Settings.miscLootTable.smithingMaterialLootTable.COAL);
        public ItemMaterial IRON = new("Iron", Program.Settings.miscLootTable.smithingMaterialLootTable.IRON);
        public ItemMaterial STEEL = new("Steel", Program.Settings.miscLootTable.smithingMaterialLootTable.STEEL);
        public ItemMaterial CORUNDUM = new("Corundum", Program.Settings.miscLootTable.smithingMaterialLootTable.CORUNDUM);
        public ItemMaterial DWARVEN = new("Dwarven", Program.Settings.miscLootTable.smithingMaterialLootTable.DWARVEN);
        public ItemMaterial ORICHALCUM = new("Orichalcum", Program.Settings.miscLootTable.smithingMaterialLootTable.ORICHALCUM);
        public ItemMaterial QUICKSILVER = new("Quicksilver", Program.Settings.miscLootTable.smithingMaterialLootTable.QUICKSILVER);
        public ItemMaterial MOONSTONE = new("Moonstone", Program.Settings.miscLootTable.smithingMaterialLootTable.MOONSTONE);
        public ItemMaterial MALACHITE = new("Malachite", Program.Settings.miscLootTable.smithingMaterialLootTable.MALACHITE);
        public ItemMaterial EBONY = new("Ebony", Program.Settings.miscLootTable.smithingMaterialLootTable.EBONY);
        public ItemMaterial SILVER = new("Silver", Program.Settings.miscLootTable.smithingMaterialLootTable.SILVER);
        public ItemMaterial GOLD = new("Gold", Program.Settings.miscLootTable.smithingMaterialLootTable.GOLD);

        public void AddRobes(Enum itemType, Form? novice, Form? apprentice, Form? adept, Form? expert, Form? master) {
            NOVICE.AddItem(itemType, novice);
            APPRENTICE.AddItem(itemType, apprentice);
            ADEPT.AddItem(itemType, adept);
            EXPERT.AddItem(itemType, expert);
            MASTER.AddItem(itemType, master);
        }

        public static void AddSmithingMaterial(ItemMaterial material, Form? raw, Form? processed, Form? blacksmith, short blackSmithCount=1) {
            material.AddItem(SmithingMaterial.Raw, raw);
            material.AddItem(SmithingMaterial.Processed, processed);
            material.AddItemCount(SmithingMaterial.ProcessedBlacksmith, blackSmithCount, 0, blacksmith);
        }

        public MiscConfig() {
            var coolgeRobes = new List<ItemMaterial>
            {
                NOVICE,
                APPRENTICE,
                ADEPT,
                EXPERT,
                MASTER
            };

            AddRobes(Enchantment.Regen, SKYA.EnchClothesRobesMageRegen01, SKYA.EnchClothesRobesMageRegen02, SKYA.EnchClothesRobesMageRegen03, SKYA.EnchClothesRobesMageRegen04, SKYA.EnchClothesRobesMageRegen05);
            AddRobes(Enchantment.Restoration, SKYA.EnchClothesRobesMageRestoration01, SKYA.EnchClothesRobesMageRestoration02, SKYA.EnchClothesRobesMageRestoration03, SKYA.EnchClothesRobesMageRestoration04, SKYA.EnchClothesRobesMageRestoration05);
            AddRobes(Enchantment.Destruction, SKYA.EnchClothesRobesMageDestruction01, SKYA.EnchClothesRobesMageDestruction02, SKYA.EnchClothesRobesMageDestruction03, SKYA.EnchClothesRobesMageDestruction04, SKYA.EnchClothesRobesMageDestruction05);
            AddRobes(Enchantment.Illusion, SKYA.EnchClothesRobesMageIllusion01, SKYA.EnchClothesRobesMageIllusion02, SKYA.EnchClothesRobesMageIllusion03, SKYA.EnchClothesRobesMageIllusion04, SKYA.EnchClothesRobesMageIllusion05);
            AddRobes(Enchantment.Conjuration, SKYA.EnchClothesRobesMageConjuration01, SKYA.EnchClothesRobesMageConjuration02, SKYA.EnchClothesRobesMageConjuration03, SKYA.EnchClothesRobesMageConjuration04, SKYA.EnchClothesRobesMageConjuration05);
            AddRobes(Enchantment.Alteration, SKYA.EnchClothesRobesMageAlteration01, SKYA.EnchClothesRobesMageAlteration02, SKYA.EnchClothesRobesMageAlteration03, SKYA.EnchClothesRobesMageAlteration04, SKYA.EnchClothesRobesMageAlteration05);

            LeveledList.LinkList(SKYL.LItemRobesCollegeMagickaRegen, 4, Enchantment.Regen, coolgeRobes);
            LeveledList.LinkList(SKYL.LItemRobesCollegeRestoration, 4, Enchantment.Restoration, coolgeRobes);
            LeveledList.LinkList(SKYL.LItemRobesCollegeDestruction, 4, Enchantment.Destruction, coolgeRobes);
            LeveledList.LinkList(SKYL.LItemRobesCollegeIllusion, 4, Enchantment.Illusion, coolgeRobes);
            LeveledList.LinkList(SKYL.LItemRobesCollegeConjuration, 4, Enchantment.Conjuration, coolgeRobes);
            LeveledList.LinkList(SKYL.LItemRobesCollegeAlteration, 4, Enchantment.Alteration, coolgeRobes);

            var oreIngotOnly = new List<ItemMaterial>
            {
                IRON,
                CORUNDUM,
                STEEL,
                DWARVEN,
                ORICHALCUM,
                MOONSTONE,
                MALACHITE,
                EBONY,
                QUICKSILVER,
                SILVER,
                GOLD
            };


            var smithingMaterials = oreIngotOnly.ToList();
            smithingMaterials.Add(COAL);

            AddSmithingMaterial(COAL, SKYM.Charcoal, null, null);
            AddSmithingMaterial(IRON, SKYM.OreIron, SKYM.IngotIron, SKYL.LItemIngotIron50, 12);
            AddSmithingMaterial(CORUNDUM, SKYM.OreCorundum, SKYM.IngotCorundum, SKYL.LItemIngotCorundum50, 9);
            AddSmithingMaterial(STEEL, null, SKYM.IngotSteel, SKYL.LItemIngotSteel50, 12);
            STEEL.AddItem(SmithingMaterial.Raw, SKYM.OreIron, SKYM.OreCorundum);
            AddSmithingMaterial(DWARVEN, null, SKYM.IngotDwarven, SKYL.LItemIngotDwarven50, 8);
            AddSmithingMaterial(ORICHALCUM, SKYM.OreOrichalcum, SKYM.IngotOrichalcum, SKYL.LItemIngotOrichalcum50, 7);
            AddSmithingMaterial(MOONSTONE, SKYM.OreMoonstone, SKYM.IngotIMoonstone, SKYL.LItemIngotMoonstone50, 7);
            AddSmithingMaterial(MALACHITE, SKYM.OreMalachite, SKYM.IngotMalachite, SKYL.LItemIngotMalachite50, 6);
            AddSmithingMaterial(EBONY, SKYM.OreEbony, SKYM.IngotEbony, SKYL.LItemIngotEbony50, 5);
            AddSmithingMaterial(QUICKSILVER, SKYM.OreQuicksilver, SKYM.IngotQuicksilver, SKYL.LItemIngotQuicksilver50, 9);
            AddSmithingMaterial(SILVER, SKYM.OreSilver, SKYM.ingotSilver, SKYL.LItemIngotSilver50, 9);
            AddSmithingMaterial(GOLD, SKYM.OreGold, SKYM.IngotGold, SKYL.LItemIngotGold50, 6);

            LeveledList.LinkList(SKYL.LItemLootIMineralsProcessed, 2, SmithingMaterial.Processed, smithingMaterials);
            LeveledList.LinkList(SKYL.LItemMiscVendorMineralsProcessed25, 2, SmithingMaterial.Processed, smithingMaterials);
            LeveledList.LinkList(SKYL.LItemMiscVendorMineralsRaw50, 2, SmithingMaterial.Raw, smithingMaterials);
            LeveledList.LinkList(SKYL.LItemBlacksmithMineralsProcessed75, 2, SmithingMaterial.ProcessedBlacksmith, smithingMaterials);
            LeveledList.LinkList(SKYL.LItemBlacksmithMineralsRaw75, 2, SmithingMaterial.Raw, smithingMaterials);

            LeveledList.LinkList(DGL.DLC1LItemGargoyleMineralsRaw75, 4, SmithingMaterial.Raw, oreIngotOnly);
            LeveledList.LinkList(DGL.DLC1LItemGargoyleMineralsRaw100, 4, SmithingMaterial.Raw, oreIngotOnly);

        }
    }
}
