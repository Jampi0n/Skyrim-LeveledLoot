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

using Form = Mutagen.Bethesda.Plugins.IFormLink<Mutagen.Bethesda.Plugins.Records.IMajorRecordCommonGetter>;

using SKYA = Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.Armor;
using DBA = Mutagen.Bethesda.FormKeys.SkyrimLE.Dragonborn.Armor;
using DGA = Mutagen.Bethesda.FormKeys.SkyrimLE.Dawnguard.Armor;
using SKYL = Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.LeveledItem;
using DBL = Mutagen.Bethesda.FormKeys.SkyrimLE.Dragonborn.LeveledItem;
using DGL = Mutagen.Bethesda.FormKeys.SkyrimLE.Dawnguard.LeveledItem;

namespace LeveledLoot {

    enum Enchantment {
        Regen,
        Restoration,
        Destruction,
        Illusion,
        Conjuration,
        Alteration
    }

    class MiscConfig {

        public static ItemMaterial NOVICE = new("Iron", 0.0001, 250, 1, 50, LootRQ.Special);
        public static ItemMaterial APPRENTICE = new("Apprentice", 0, 170, 7, 75, LootRQ.Special);
        public static ItemMaterial ADEPT = new("Adept", 0, 100, 16, 250, LootRQ.Special);
        public static ItemMaterial EXPERT = new("Expert", 0, 60, 22, 350, LootRQ.Special);
        public static ItemMaterial MASTER = new("Master", 0, 25, 35, 400, LootRQ.Special);

        public static void Setup(Enum itemType, Form? novice, Form? apprentice, Form? adept, Form? expert, Form? master) {
            NOVICE.AddItem(itemType, novice);
            APPRENTICE.AddItem(itemType, apprentice);
            ADEPT.AddItem(itemType, adept);
            EXPERT.AddItem(itemType, expert);
            MASTER.AddItem(itemType, master);
        }
        public static void Config() {
            var coolgeRobes = new LinkedList<ItemMaterial>();
            coolgeRobes.AddLast(NOVICE);
            coolgeRobes.AddLast(APPRENTICE);
            coolgeRobes.AddLast(ADEPT);
            coolgeRobes.AddLast(EXPERT);
            coolgeRobes.AddLast(MASTER);

            Setup(Enchantment.Regen, SKYA.EnchClothesRobesMageRegen01, SKYA.EnchClothesRobesMageRegen02, SKYA.EnchClothesRobesMageRegen03, SKYA.EnchClothesRobesMageRegen04, SKYA.EnchClothesRobesMageRegen05);
            Setup(Enchantment.Restoration, SKYA.EnchClothesRobesMageRestoration01, SKYA.EnchClothesRobesMageRestoration02, SKYA.EnchClothesRobesMageRestoration03, SKYA.EnchClothesRobesMageRestoration04, SKYA.EnchClothesRobesMageRestoration05);
            Setup(Enchantment.Destruction, SKYA.EnchClothesRobesMageDestruction01, SKYA.EnchClothesRobesMageDestruction02, SKYA.EnchClothesRobesMageDestruction03, SKYA.EnchClothesRobesMageDestruction04, SKYA.EnchClothesRobesMageDestruction05);
            Setup(Enchantment.Illusion, SKYA.EnchClothesRobesMageIllusion01, SKYA.EnchClothesRobesMageIllusion02, SKYA.EnchClothesRobesMageIllusion03, SKYA.EnchClothesRobesMageIllusion04, SKYA.EnchClothesRobesMageIllusion05);
            Setup(Enchantment.Conjuration, SKYA.EnchClothesRobesMageConjuration01, SKYA.EnchClothesRobesMageConjuration02, SKYA.EnchClothesRobesMageConjuration03, SKYA.EnchClothesRobesMageConjuration04, SKYA.EnchClothesRobesMageConjuration05);
            Setup(Enchantment.Alteration, SKYA.EnchClothesRobesMageAlteration01, SKYA.EnchClothesRobesMageAlteration02, SKYA.EnchClothesRobesMageAlteration03, SKYA.EnchClothesRobesMageAlteration04, SKYA.EnchClothesRobesMageAlteration05);

            LeveledList.LinkList(SKYL.LItemRobesCollegeMagickaRegen, 4, Enchantment.Regen, coolgeRobes, LootRQ.Special);
            LeveledList.LinkList(SKYL.LItemRobesCollegeRestoration, 4, Enchantment.Restoration, coolgeRobes, LootRQ.Special);
            LeveledList.LinkList(SKYL.LItemRobesCollegeDestruction, 4, Enchantment.Destruction, coolgeRobes, LootRQ.Special);
            LeveledList.LinkList(SKYL.LItemRobesCollegeIllusion, 4, Enchantment.Illusion, coolgeRobes, LootRQ.Special);
            LeveledList.LinkList(SKYL.LItemRobesCollegeConjuration, 4, Enchantment.Conjuration, coolgeRobes, LootRQ.Special);
            LeveledList.LinkList(SKYL.LItemRobesCollegeAlteration, 4, Enchantment.Alteration, coolgeRobes, LootRQ.Special);
        }
    }
}
