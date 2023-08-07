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

    enum SoulGemType
    {
        SoulGemFilled,
        SoulGemEmpty
    }

    class SoulGemConfig : LootConfig<SoulGemConfig> {
        public ItemMaterial PETTY = new("Petty", Program.Settings.miscLootTable.soulGemLootTable.PETTY);
        public ItemMaterial LESSER = new("Lesser", Program.Settings.miscLootTable.soulGemLootTable.LESSER);
        public ItemMaterial COMMON = new("Common", Program.Settings.miscLootTable.soulGemLootTable.COMMON);
        public ItemMaterial GREATER = new("Greater", Program.Settings.miscLootTable.soulGemLootTable.GREATER);
        public ItemMaterial GRAND = new("Grand", Program.Settings.miscLootTable.soulGemLootTable.GRAND);
        public ItemMaterial BLACK = new("Black", Program.Settings.miscLootTable.soulGemLootTable.BLACK, LootRQ.Rare);

        public SoulGemConfig() {
            var soulGems = new List<ItemMaterial>() {
                PETTY, LESSER, COMMON, GREATER, GRAND, BLACK
            };

            PETTY.AddItem(SoulGemType.SoulGemEmpty, SKY.SoulGemPetty);
            PETTY.AddItem(SoulGemType.SoulGemFilled, SKY.SoulGemPettyFilled);
            LESSER.AddItem(SoulGemType.SoulGemEmpty, SKY.SoulGemLesser);
            LESSER.AddItem(SoulGemType.SoulGemFilled, SKY.SoulGemLesserFilled);
            COMMON.AddItem(SoulGemType.SoulGemEmpty, SKY.SoulGemCommon);
            COMMON.AddItem(SoulGemType.SoulGemFilled, SKY.SoulGemCommonFilled);
            GREATER.AddItem(SoulGemType.SoulGemEmpty, SKY.SoulGemGreater);
            GREATER.AddItem(SoulGemType.SoulGemFilled, SKY.SoulGemGreaterFilled);
            GRAND.AddItem(SoulGemType.SoulGemEmpty, SKY.SoulGemGrand);
            GRAND.AddItem(SoulGemType.SoulGemFilled, SKY.SoulGemGrandFilled);
            BLACK.AddItem(SoulGemType.SoulGemEmpty, SKY.SoulGemBlack);
            BLACK.AddItem(SoulGemType.SoulGemFilled, SKY.SoulGemBlackFilled);

            LeveledList.LinkList(SKYL.LItemMiscVendorSoulGemEmpty, 2, SoulGemType.SoulGemEmpty, soulGems);
            LeveledList.LinkList(SKYL.LItemSoulGemEmptyNoBlack, 2, SoulGemType.SoulGemEmpty, soulGems);
            LeveledList.LinkList(SKYL.LItemSoulGemEmptySpecial, 3, SoulGemType.SoulGemEmpty, soulGems, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemSoulGemEmptyTown, 2, SoulGemType.SoulGemEmpty, soulGems);


            LeveledList.LinkList(SKYL.LItemMiscVendorSoulGemFull, 2, SoulGemType.SoulGemFilled, soulGems);
            LeveledList.LinkList(SKYL.LItemSoulGemFullNoBlack, 2, SoulGemType.SoulGemFilled, soulGems);
            LeveledList.LinkList(SKYL.LItemSoulGemFullSpecial, 3, SoulGemType.SoulGemFilled, soulGems, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemSoulGemFullTown, 2, SoulGemType.SoulGemFilled, soulGems);
            LeveledList.LinkList(SKYL.LItemSoulGemFullMagicTrap, 3, SoulGemType.SoulGemFilled, soulGems);
        }
    }
}
