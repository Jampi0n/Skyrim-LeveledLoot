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
using SKY = Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.SoulGem;
using SKYL = Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.LeveledItem;

namespace LeveledLoot {
    class SoulGemConfig {
        public static ItemMaterial PETTY = new("Petty", 0.0001, 250, 1, 50);
        public static ItemMaterial LESSER = new("Lesser", 0.00007, 200, 1, 75);
        public static ItemMaterial COMMON = new("Common", 0.00003, 135, 1, 125);
        public static ItemMaterial GREATER = new("Greater", 0, 70, 5, 175);
        public static ItemMaterial GRAND = new("Grand", 0, 40, 10, 225);
        public static ItemMaterial BLACK = new("Black", 0, 25, 10, 225, LootRQ.Rare);

        static List<ItemMaterial> SOUL_GEMS = new() {
            PETTY, LESSER, COMMON, GREATER, GRAND, BLACK
        };

        public static void Config() {
            PETTY.AddItem(ItemType.SoulGemEmpty, SKY.SoulGemPetty);
            PETTY.AddItem(ItemType.SoulGemFilled, SKY.SoulGemPettyFilled);
            LESSER.AddItem(ItemType.SoulGemEmpty, SKY.SoulGemLesser);
            LESSER.AddItem(ItemType.SoulGemFilled, SKY.SoulGemLesserFilled);
            COMMON.AddItem(ItemType.SoulGemEmpty, SKY.SoulGemCommon);
            COMMON.AddItem(ItemType.SoulGemFilled, SKY.SoulGemCommonFilled);
            GREATER.AddItem(ItemType.SoulGemEmpty, SKY.SoulGemGreater);
            GREATER.AddItem(ItemType.SoulGemFilled, SKY.SoulGemGreaterFilled);
            GRAND.AddItem(ItemType.SoulGemEmpty, SKY.SoulGemGrand);
            GRAND.AddItem(ItemType.SoulGemFilled, SKY.SoulGemGrandFilled);
            BLACK.AddItem(ItemType.SoulGemEmpty, SKY.SoulGemBlack);
            BLACK.AddItem(ItemType.SoulGemFilled, SKY.SoulGemBlackFilled);

            LeveledList.LinkList(SKYL.LItemMiscVendorSoulGemEmpty, 2, ItemType.SoulGemEmpty, SOUL_GEMS);
            LeveledList.LinkList(SKYL.LItemSoulGemEmptyNoBlack, 2, ItemType.SoulGemEmpty, SOUL_GEMS);
            LeveledList.LinkList(SKYL.LItemSoulGemEmptySpecial, 3, ItemType.SoulGemEmpty, SOUL_GEMS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemSoulGemEmptyTown, 2, ItemType.SoulGemEmpty, SOUL_GEMS);


            LeveledList.LinkList(SKYL.LItemMiscVendorSoulGemFull, 2, ItemType.SoulGemFilled, SOUL_GEMS);
            LeveledList.LinkList(SKYL.LItemSoulGemFullNoBlack, 2, ItemType.SoulGemFilled, SOUL_GEMS);
            LeveledList.LinkList(SKYL.LItemSoulGemFullSpecial, 3, ItemType.SoulGemFilled, SOUL_GEMS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemSoulGemFullTown, 2, ItemType.SoulGemFilled, SOUL_GEMS);
            LeveledList.LinkList(SKYL.LItemSoulGemFullMagicTrap, 3, ItemType.SoulGemFilled, SOUL_GEMS);
        }
    }
}
