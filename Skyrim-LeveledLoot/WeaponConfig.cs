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

using SKY = Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.Weapon;
using DB = Mutagen.Bethesda.FormKeys.SkyrimLE.Dragonborn.Weapon;
using DG = Mutagen.Bethesda.FormKeys.SkyrimLE.Dawnguard.Weapon;
using SKYL = Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.LeveledItem;
using DBL = Mutagen.Bethesda.FormKeys.SkyrimLE.Dragonborn.LeveledItem;
using DGL = Mutagen.Bethesda.FormKeys.SkyrimLE.Dawnguard.LeveledItem;

namespace LeveledLoot {


    class WeaponConfig {
        public static ItemMaterial DRAUGR = new("Draugr", 75, 22, 0, 20, LootRQ.NoEnch, LootRQ.Special);
        public static ItemMaterial DRAUGR_HONED = new("Draugr Honed", 0, 300, 8, 150, LootRQ.Special);
        public static ItemMaterial DRAUGR_HERO = new("Draugr Hero", 0, 200, 17, 275, LootRQ.Special);
        public static ItemMaterial IRON = new("Iron", 75, 22, 0, 20);
        public static ItemMaterial STEEL = new("Steel", 20, 18, 0, 30);
        public static ItemMaterial DWARVEN = new("Dwarven", 5, 15, 0, 45);
        public static ItemMaterial ELVEN = new("Elven", 0, 13, 10, 55);
        public static ItemMaterial ORCISH = new("Orcish", 0, 10, 16, 75);

        public static ItemMaterial NORDIC = new("Nordic", 0, 8, 32, 125, LootRQ.DLC2);
        public static ItemMaterial GLASS = new("Glass", 0, 7, 40, 140);
        public static ItemMaterial EBONY = new("Ebony", 0, 6, 48, 160);
        public static ItemMaterial STALHRIM = new("Stalhrim", 0, 5, 56, 180, LootRQ.Rare, LootRQ.DLC2);
        public static ItemMaterial DRAGON = new("Dragon", 0, 4, 64, 200, LootRQ.Rare);
        public static ItemMaterial DAEDRIC = new("Daedric", 0, 3, 80, 220, LootRQ.Rare);
        public static ItemMaterial ULTIMATE = new("Ultimate", 0, 2, 100, 240, LootRQ.Rare);

        public static void Config() {
            var baseArrowDragon75 = Program.state.PatchMod.LeveledItems.AddNew();
            baseArrowDragon75.EditorID = "JLL_BaseArrowDragon75";
            baseArrowDragon75.ChanceNone = 25;
            baseArrowDragon75.Flags = LeveledItem.Flag.CalculateForEachItemInCount;
            baseArrowDragon75.Entries = new Noggog.ExtendedList<LeveledItemEntry>() {
                new LeveledItemEntry() {
                    Data = new LeveledItemEntryData() {
                        Count = 1,
                        Level = 1,
                        Reference = Dawnguard.Ammunition.DLC1DragonboneArrow
                    }
                }
            };

            DRAUGR.DefaultWeapon(SKY.DraugrSword, SKY.DraugrWarAxe, null, null, SKY.DraugrGreatsword, SKY.DraugrBattleAxe, null, SKY.DraugrBow);
            DRAUGR.Arrow75(SKYL.BaseArrowDraugr75);
            DRAUGR.AddItem(ItemType.Mace, SKY.DraugrSword, SKY.DraugrWarAxe);
            DRAUGR.AddItem(ItemType.Warhammer, SKY.DraugrGreatsword, SKY.DraugrBattleAxe);

            DRAUGR_HONED.DefaultWeapon(SKY.DraugrSwordHoned, SKY.DraugrWarAxeHoned, null, null, SKY.DraugrGreatswordHoned, SKY.DraugrBattleAxeHoned, null, SKY.DraugrBowSupple);
            DRAUGR_HONED.Arrow75(SKYL.BaseArrowDraugr75);
            DRAUGR_HONED.AddItem(ItemType.Mace, SKY.DraugrSwordHoned, SKY.DraugrWarAxeHoned);
            DRAUGR_HONED.AddItem(ItemType.Warhammer, SKY.DraugrGreatswordHoned, SKY.DraugrBattleAxeHoned);

            DRAUGR_HERO.DefaultWeapon(SKY.NordHeroSword, SKY.NordHeroWarAxe, null, null, SKY.NordHeroGreatsword, SKY.NordHeroBattleAxe, null, SKY.NordHeroBow);
            DRAUGR_HERO.Arrow75(SKYL.BaseArrowDraugr75);
            DRAUGR_HERO.AddItem(ItemType.Mace, SKY.NordHeroSword, SKY.NordHeroWarAxe);
            DRAUGR_HERO.AddItem(ItemType.Warhammer, SKY.NordHeroGreatsword, SKY.NordHeroBattleAxe);

            IRON.DefaultWeapon(SKY.IronSword, SKY.IronWarAxe, SKY.IronMace, SKY.IronDagger, SKY.IronGreatsword, SKY.IronBattleaxe, SKY.IronWarhammer, SKY.LongBow);
            IRON.Arrow75(SKYL.BaseArrowIron75);
            STEEL.DefaultWeapon(SKY.SteelSword, SKY.SteelWarAxe, SKY.SteelMace, SKY.SteelDagger, SKY.SteelGreatsword, SKY.SteelBattleaxe, SKY.SteelWarhammer, SKY.HuntingBow);
            STEEL.Arrow75(SKYL.BaseArrowSteel75);
            DWARVEN.DefaultWeapon(SKY.DwarvenSword, SKY.DwarvenWarAxe, SKY.DwarvenMace, SKY.DwarvenDagger, SKY.DwarvenGreatsword, SKY.DwarvenBattleaxe, SKY.DwarvenWarhammer, SKY.DwarvenBow);
            DWARVEN.Arrow75(SKYL.BaseArrowDwarven75);
            ORCISH.DefaultWeapon(SKY.OrcishSword, SKY.OrcishWarAxe, SKY.OrcishMace, SKY.OrcishDagger, SKY.OrcishGreatsword, SKY.OrcishBattleaxe, SKY.OrcishWarhammer, SKY.OrcishBow);
            ORCISH.Arrow75(SKYL.BaseArrowOrcish75);
            ELVEN.DefaultWeapon(SKY.ElvenSword, SKY.ElvenWarAxe, SKY.ElvenMace, SKY.ElvenDagger, SKY.ElvenGreatsword, SKY.ElvenBattleaxe, SKY.ElvenWarhammer, SKY.ElvenBow);
            ELVEN.Arrow75(SKYL.BaseArrowElven75);
            NORDIC.DefaultWeapon(DB.DLC2NordicSword, DB.DLC2NordicWarAxe, DB.DLC2NordicMace, DB.DLC2NordicDagger, DB.DLC2NordicGreatsword, DB.DLC2NordicBattleaxe, DB.DLC2NordicWarhammer, DB.DLC2NordicBow);
            NORDIC.Arrow75(DBL.DLC2BaseArrowNordic75);
            GLASS.DefaultWeapon(SKY.GlassSword, SKY.GlassWarAxe, SKY.GlassMace, SKY.GlassDagger, SKY.GlassGreatsword, SKY.GlassBattleaxe, SKY.GlassWarhammer, SKY.GlassBow);
            GLASS.Arrow75(SKYL.BaseArrowGlass75);
            EBONY.DefaultWeapon(SKY.EbonySword, SKY.EbonyWarAxe, SKY.EbonyMace, SKY.EbonyDagger, SKY.EbonyGreatsword, SKY.EbonyBattleaxe, SKY.EbonyWarhammer, SKY.EbonyBow);
            EBONY.Arrow75(SKYL.BaseArrowEbony75);
            STALHRIM.DefaultWeapon(DB.DLC2StalhrimSword, DB.DLC2StalhrimWarAxe, DB.DLC2StalhrimMace, DB.DLC2StalhrimDagger, DB.DLC2StalhrimGreatsword, DB.DLC2StalhrimBattleaxe, DB.DLC2StalhrimWarhammer, DB.DLC2StalhrimBow);
            STALHRIM.Arrow75(DBL.DLC2BaseArrowStalhrim75);
            DRAGON.DefaultWeapon(DG.DLC1DragonboneSword, DG.DLC1DragonboneWarAxe, DG.DLC1DragonboneMace, DG.DLC1DragonboneDagger, DG.DLC1DragonboneGreatsword, DG.DLC1DragonboneBattleaxe, DG.DLC1DragonboneWarhammer, DG.DLC1DragonboneBow);
            DRAGON.Arrow75(baseArrowDragon75.ToLink());
            DAEDRIC.DefaultWeapon(SKY.DaedricSword, SKY.DaedricWarAxe, SKY.DaedricMace, SKY.DaedricDagger, SKY.DaedricGreatsword, SKY.DaedricBattleaxe, SKY.DaedricWarhammer, SKY.DaedricBow);
            DAEDRIC.Arrow75(SKYL.BaseArrowDaedric75);


            LeveledList.LinkList(SKYL.LItemWeaponSword, LeveledList.FACTOR_COMMON, ItemType.Sword, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponSwordBest, LeveledList.FACTOR_BEST, ItemType.Sword, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponSwordBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Sword, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemWeaponSwordSpecial, LeveledList.FACTOR_RARE, ItemType.Sword, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponSwordTown, LeveledList.FACTOR_JUNK, ItemType.Sword, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemEnchWeaponSword, LeveledList.FACTOR_COMMON, ItemType.Sword, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchWeaponSwordSpecial, LeveledList.FACTOR_RARE, ItemType.Sword, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchWeaponSwordBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Sword, LootRQ.Ench);

            LeveledList.LinkList(SKYL.LItemWeaponWarAxe, LeveledList.FACTOR_COMMON, ItemType.Waraxe, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponWarAxeBest, LeveledList.FACTOR_BEST, ItemType.Waraxe, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponWarAxeBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Waraxe, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemWeaponWarAxeSpecial, LeveledList.FACTOR_RARE, ItemType.Waraxe, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponWarAxeTown, LeveledList.FACTOR_JUNK, ItemType.Waraxe, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemEnchWeaponWarAxe, LeveledList.FACTOR_COMMON, ItemType.Waraxe, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchWeaponWarAxeBest, LeveledList.FACTOR_BEST, ItemType.Waraxe, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchWeaponWarAxeSpecial, LeveledList.FACTOR_RARE, ItemType.Waraxe, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchWeaponWarAxeBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Waraxe, LootRQ.Ench);

            LeveledList.LinkList(SKYL.LItemWeaponMace, LeveledList.FACTOR_COMMON, ItemType.Mace, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponMaceBest, LeveledList.FACTOR_BEST, ItemType.Mace, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponMaceBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Mace, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemWeaponMaceSpecial, LeveledList.FACTOR_RARE, ItemType.Mace, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponMaceTown, LeveledList.FACTOR_JUNK, ItemType.Mace, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemEnchWeaponMace, LeveledList.FACTOR_COMMON, ItemType.Mace, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchWeaponMaceSpecial, LeveledList.FACTOR_RARE, ItemType.Mace, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchWeaponMaceBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Mace, LootRQ.Ench);

            LeveledList.LinkList(SKYL.LItemWeaponDagger, LeveledList.FACTOR_COMMON, ItemType.Dagger, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponDaggerBest, LeveledList.FACTOR_BEST, ItemType.Dagger, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponDaggerBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Dagger, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemWeaponDaggerSpecial, LeveledList.FACTOR_RARE, ItemType.Dagger, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponDaggerTown, LeveledList.FACTOR_JUNK, ItemType.Dagger, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemEnchWeaponDagger, LeveledList.FACTOR_COMMON, ItemType.Dagger, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchWeaponDaggerSpecial, LeveledList.FACTOR_RARE, ItemType.Dagger, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchWeaponDaggerBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Dagger, LootRQ.Ench);


            LeveledList.LinkList(SKYL.LItemWeaponBattleAxe, LeveledList.FACTOR_COMMON, ItemType.Battleaxe, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponBattleAxeBest, LeveledList.FACTOR_BEST, ItemType.Battleaxe, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponBattleAxeBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Battleaxe, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemWeaponBattleAxeSpecial, LeveledList.FACTOR_RARE, ItemType.Battleaxe, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponBattleAxeTown, LeveledList.FACTOR_JUNK, ItemType.Battleaxe, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemEnchWeaponBattleAxe, LeveledList.FACTOR_COMMON, ItemType.Battleaxe, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchWeaponBattleAxeBest, LeveledList.FACTOR_BEST, ItemType.Battleaxe, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchWeaponBattleAxeSpecial, LeveledList.FACTOR_RARE, ItemType.Battleaxe, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchWeaponBattleaxeBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Battleaxe, LootRQ.Ench);


            LeveledList.LinkList(SKYL.LItemWeaponGreatSword, LeveledList.FACTOR_COMMON, ItemType.Greatsword, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponGreatSwordBest, LeveledList.FACTOR_BEST, ItemType.Greatsword, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponGreatSwordBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Greatsword, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemWeaponGreatSwordSpecial, LeveledList.FACTOR_RARE, ItemType.Greatsword, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponGreatSwordTown, LeveledList.FACTOR_JUNK, ItemType.Greatsword, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemEnchWeaponGreatsword, LeveledList.FACTOR_COMMON, ItemType.Greatsword, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchWeaponGreatswordSpecial, LeveledList.FACTOR_RARE, ItemType.Greatsword, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchWeaponGreatswordBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Greatsword, LootRQ.Ench);

            LeveledList.LinkList(SKYL.LItemWeaponWarhammer, LeveledList.FACTOR_COMMON, ItemType.Warhammer, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponWarhammerBest, LeveledList.FACTOR_BEST, ItemType.Warhammer, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponWarhammerBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Warhammer, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemWeaponWarhammerSpecial, LeveledList.FACTOR_RARE, ItemType.Warhammer, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponWarhammerTown, LeveledList.FACTOR_JUNK, ItemType.Warhammer, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemEnchWeaponWarhammer, LeveledList.FACTOR_COMMON, ItemType.Warhammer, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchWeaponWarhammerSpecial, LeveledList.FACTOR_RARE, ItemType.Warhammer, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchWeaponWarhammerBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Warhammer, LootRQ.Ench);


            LeveledList.LinkList(SKYL.LItemWeaponBow, LeveledList.FACTOR_COMMON, ItemType.Bow, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponBowBest, LeveledList.FACTOR_BEST, ItemType.Bow, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponBowBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Bow, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemWeaponBowSpecial, LeveledList.FACTOR_RARE, ItemType.Bow, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponBowTown, LeveledList.FACTOR_JUNK, ItemType.Bow, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemEnchWeaponBow, LeveledList.FACTOR_COMMON, ItemType.Bow, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchWeaponBowSpecial, LeveledList.FACTOR_RARE, ItemType.Bow, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchWeaponBowBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Bow, LootRQ.Ench);

            // DLC2

            LeveledList.LinkList(DBL.DLC2LItemWeaponSword, LeveledList.FACTOR_COMMON, ItemType.Sword, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponWarAxe, LeveledList.FACTOR_COMMON, ItemType.Waraxe, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponMace, LeveledList.FACTOR_COMMON, ItemType.Mace, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponDagger, LeveledList.FACTOR_COMMON, ItemType.Dagger, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponGreatSword, LeveledList.FACTOR_COMMON, ItemType.Greatsword, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponBattleAxe, LeveledList.FACTOR_COMMON, ItemType.Battleaxe, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponWarhammer, LeveledList.FACTOR_COMMON, ItemType.Warhammer, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponBow, LeveledList.FACTOR_COMMON, ItemType.Bow, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);

            LeveledList.LinkList(DBL.DLC2LItemWeaponSwordTown, LeveledList.FACTOR_JUNK, ItemType.Sword, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponWarAxeTown, LeveledList.FACTOR_JUNK, ItemType.Waraxe, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponMaceTown, LeveledList.FACTOR_JUNK, ItemType.Mace, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponDaggerTown, LeveledList.FACTOR_JUNK, ItemType.Dagger, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponGreatSwordTown, LeveledList.FACTOR_JUNK, ItemType.Greatsword, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponBattleAxeTown, LeveledList.FACTOR_JUNK, ItemType.Battleaxe, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponWarhammerTown, LeveledList.FACTOR_JUNK, ItemType.Warhammer, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponBowTown, LeveledList.FACTOR_JUNK, ItemType.Bow, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);

            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponSword, LeveledList.FACTOR_COMMON, ItemType.Sword, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponWarAxe, LeveledList.FACTOR_COMMON, ItemType.Waraxe, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponMace, LeveledList.FACTOR_COMMON, ItemType.Mace, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponDagger, LeveledList.FACTOR_COMMON, ItemType.Dagger, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponGreatsword, LeveledList.FACTOR_COMMON, ItemType.Greatsword, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponBattleAxe, LeveledList.FACTOR_COMMON, ItemType.Battleaxe, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponWarhammer, LeveledList.FACTOR_COMMON, ItemType.Warhammer, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponBow, LeveledList.FACTOR_COMMON, ItemType.Bow, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);

            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponSwordSpecial, LeveledList.FACTOR_RARE, ItemType.Sword, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponWarAxeSpecial, LeveledList.FACTOR_RARE, ItemType.Waraxe, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponMaceSpecial, LeveledList.FACTOR_RARE, ItemType.Mace, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponDaggerSpecial, LeveledList.FACTOR_RARE, ItemType.Dagger, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponGreatswordSpecial, LeveledList.FACTOR_RARE, ItemType.Greatsword, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponBattleAxeSpecial, LeveledList.FACTOR_RARE, ItemType.Battleaxe, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponWarhammerSpecial, LeveledList.FACTOR_RARE, ItemType.Warhammer, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponBowSpecial, LeveledList.FACTOR_RARE, ItemType.Bow, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);

            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponSwordBest, LeveledList.FACTOR_BEST, ItemType.Sword, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponWarAxeBest, LeveledList.FACTOR_BEST, ItemType.Waraxe, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponMaceBest, LeveledList.FACTOR_BEST, ItemType.Mace, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponDaggerBest, LeveledList.FACTOR_BEST, ItemType.Dagger, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponGreatswordBest, LeveledList.FACTOR_BEST, ItemType.Greatsword, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponBattleAxeBest, LeveledList.FACTOR_BEST, ItemType.Battleaxe, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponWarhammerBest, LeveledList.FACTOR_BEST, ItemType.Warhammer, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponBowBest, LeveledList.FACTOR_BEST, ItemType.Bow, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);

            // Arrows
            LeveledList.LinkList(SKYL.LItemArrowsAll, LeveledList.FACTOR_COMMON, ItemType.Arrow12, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArrowsAllBest, LeveledList.FACTOR_BEST, ItemType.Arrow12, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArrowsAllRandomLoot, LeveledList.FACTOR_COMMON, ItemType.Arrow6, LootRQ.NoEnch);
            LeveledList.LinkList(DBL.DLC2LItemArrowsAll, LeveledList.FACTOR_COMMON, ItemType.Arrow12, LootRQ.NoEnch, LootRQ.DLC2);
            LeveledList.LinkList(DBL.DLC2LootArrowsAll100, LeveledList.FACTOR_BEST, ItemType.Arrow12, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LootArrowsAll15, LeveledList.FACTOR_COMMON, ItemType.Arrow12, LootRQ.NoEnch, LootRQ.DLC2);

            // Bandit

            LeveledList.LinkList(SKYL.LItemBanditBattleaxe, LeveledList.FACTOR_JUNK, ItemType.Battleaxe, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemBanditGreatsword, LeveledList.FACTOR_JUNK, ItemType.Greatsword, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemBanditWarhammer, LeveledList.FACTOR_JUNK, ItemType.Warhammer, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemBanditWarAxe, LeveledList.FACTOR_JUNK, ItemType.Waraxe, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemBanditSword, LeveledList.FACTOR_JUNK, ItemType.Sword, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemBanditMace, LeveledList.FACTOR_JUNK, ItemType.Mace, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemBanditWeaponBow, LeveledList.FACTOR_JUNK, ItemType.Bow, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemBanditBossBattleaxe, SKYL.LItemWeaponBattleAxe, SKYL.LItemWeaponBattleAxe, SKYL.LItemEnchWeaponBattleAxe);
            LeveledList.LinkList(SKYL.LItemBanditBossGreatsword, SKYL.LItemWeaponGreatSword, SKYL.LItemWeaponGreatSword, SKYL.LItemEnchWeaponGreatsword);
            LeveledList.LinkList(SKYL.LItemBanditBossWarhammer, SKYL.LItemWeaponWarhammer, SKYL.LItemWeaponWarhammer, SKYL.LItemEnchWeaponWarhammer);
            LeveledList.LinkList(SKYL.LItemBanditBossSword, SKYL.LItemWeaponSword, SKYL.LItemWeaponSword, SKYL.LItemEnchWeaponSword);
            LeveledList.LinkList(SKYL.LItemBanditBossWarAxe, SKYL.LItemWeaponWarAxe, SKYL.LItemWeaponWarAxe, SKYL.LItemEnchWeaponWarAxe);
            LeveledList.LinkList(SKYL.LItemBanditBossMace, SKYL.LItemWeaponMace, SKYL.LItemWeaponMace, SKYL.LItemEnchWeaponMace);

            LeveledList.LockLists(SKYL.LItemBanditBattleaxe, SKYL.LItemBanditGreatsword, SKYL.LItemBanditWarhammer, SKYL.LItemBanditSword, SKYL.LItemBanditWarAxe, SKYL.LItemBanditMace, SKYL.LItemBanditWeaponBow);
            LeveledList.LockLists(SKYL.LItemBanditBossBattleaxe, SKYL.LItemBanditBossGreatsword, SKYL.LItemBanditBossWarhammer, SKYL.LItemBanditBossSword, SKYL.LItemBanditBossWarAxe, SKYL.LItemBanditBossMace);

            // Draugr
            LeveledList.LinkList(SKYL.LItemDraugr05Weapon1H, SKY.NordHeroSword, SKY.NordHeroWarAxe);
            LeveledList.LinkList(SKYL.LItemDraugr05EWeapon1H, SKY.NordHeroSword, SKY.NordHeroWarAxe);
            LeveledList.LinkList(SKYL.LItemDraugr05EWeapon2H, SKY.NordHeroGreatsword, SKY.NordHeroBattleAxe);
            LeveledList.LinkList(SKYL.LItemDraugr05EWeaponBowSublist, SKY.NordHeroBow);
            LeveledList.LinkList(SKYL.LItemDraugr05EWeaponArrowSublist, SKYL.LItemDraugr02WeaponArrowSublist);
            LeveledList.LinkList(SKYL.LItemDraugrEbonyShield50, SKYL.LItemDraugrShield100);

            LinkedList<ItemMaterial> draugrWeapons = new();
            draugrWeapons.AddLast(DRAUGR);
            draugrWeapons.AddLast(DRAUGR_HONED);
            draugrWeapons.AddLast(IRON);
            draugrWeapons.AddLast(STEEL);
            draugrWeapons.AddLast(DRAUGR_HERO);
            draugrWeapons.AddLast(EBONY);
            draugrWeapons.AddLast(DRAGON);


            var factorDraugr = LeveledList.FACTOR_COMMON;
            var draugrSword = LeveledList.CreateList(ItemType.Sword, "JLL_DraugrSword", factorDraugr, draugrWeapons, LootRQ.NoEnch, LootRQ.Special, LootRQ.Rare);
            var draugrWaraxe = LeveledList.CreateList(ItemType.Waraxe, "JLL_DraugrWaraxe", factorDraugr, draugrWeapons, LootRQ.NoEnch, LootRQ.Special, LootRQ.Rare);
            var draugrMace = LeveledList.CreateList(ItemType.Mace, "JLL_DraugrMace", factorDraugr, draugrWeapons, LootRQ.NoEnch, LootRQ.Special, LootRQ.Rare);
            var draugrDagger = LeveledList.CreateList(ItemType.Dagger, "JLL_DraugrDagger", factorDraugr, draugrWeapons, LootRQ.NoEnch, LootRQ.Special, LootRQ.Rare);
            var draugrBow = LeveledList.CreateList(ItemType.Bow, "JLL_DraugrBow", factorDraugr, draugrWeapons, LootRQ.NoEnch, LootRQ.Special, LootRQ.Rare);
            var draugrGreatsword = LeveledList.CreateList(ItemType.Greatsword, "JLL_DraugrGreatsword", factorDraugr, draugrWeapons, LootRQ.NoEnch, LootRQ.Special, LootRQ.Rare);
            var draugrBattleaxe = LeveledList.CreateList(ItemType.Battleaxe, "JLL_DraugrBattleaxe", factorDraugr, draugrWeapons, LootRQ.NoEnch, LootRQ.Special, LootRQ.Rare);
            var draugrWarhammer = LeveledList.CreateList(ItemType.Warhammer, "JLL_DraugrWarhammer", factorDraugr, draugrWeapons, LootRQ.NoEnch, LootRQ.Special, LootRQ.Rare);
            var draugrArrows = LeveledList.CreateList(ItemType.Arrow12, "JLL_DraugrArrows", factorDraugr, draugrWeapons, LootRQ.NoEnch, LootRQ.Special, LootRQ.Rare);

            var draugrSwordEnch = LeveledList.CreateList(ItemType.Sword, "JLL_DraugrSwordEnch", factorDraugr, draugrWeapons, LootRQ.Ench, LootRQ.Special, LootRQ.Rare);
            var draugrWaraxeEnch = LeveledList.CreateList(ItemType.Waraxe, "JLL_DraugrWaraxeEnch", factorDraugr, draugrWeapons, LootRQ.Ench, LootRQ.Special, LootRQ.Rare);
            var draugrMaceEnch = LeveledList.CreateList(ItemType.Mace, "JLL_DraugrMaceEnch", factorDraugr, draugrWeapons, LootRQ.Ench, LootRQ.Special, LootRQ.Rare);
            var draugrDaggerEnch = LeveledList.CreateList(ItemType.Dagger, "JLL_DraugrDaggerEnch", factorDraugr, draugrWeapons, LootRQ.Ench, LootRQ.Special, LootRQ.Rare);
            var draugrBowEnch = LeveledList.CreateList(ItemType.Bow, "JLL_DraugrBowEnch", factorDraugr, draugrWeapons, LootRQ.Ench, LootRQ.Special, LootRQ.Rare);
            var draugrGreatswordEnch = LeveledList.CreateList(ItemType.Greatsword, "JLL_DraugrGreatswordEnch", factorDraugr, draugrWeapons, LootRQ.Ench, LootRQ.Special, LootRQ.Rare);
            var draugrBattleaxeEnch = LeveledList.CreateList(ItemType.Battleaxe, "JLL_DraugrBattleaxeEnch", factorDraugr, draugrWeapons, LootRQ.Ench, LootRQ.Special, LootRQ.Rare);
            var draugrWarhammerEnch = LeveledList.CreateList(ItemType.Warhammer, "JLL_DraugrWarhammerEnch", factorDraugr, draugrWeapons, LootRQ.Ench, LootRQ.Special, LootRQ.Rare);

            LeveledList.LinkList(SKYL.LootDraugrWeapon100, draugrSword.ToLink(), draugrWaraxe.ToLink(), draugrMace.ToLink(), draugrDagger.ToLink(), draugrBow.ToLink(), draugrGreatsword.ToLink(), draugrBattleaxe.ToLink(), draugrWarhammer.ToLink(), draugrArrows.ToLink());
            LeveledList.LinkList(SKYL.LootDraugrWeapon15, draugrSword.ToLink(), draugrWaraxe.ToLink(), draugrMace.ToLink(), draugrDagger.ToLink(), draugrBow.ToLink(), draugrGreatsword.ToLink(), draugrBattleaxe.ToLink(), draugrWarhammer.ToLink(), draugrArrows.ToLink());
            LeveledList.LinkList(SKYL.LootDraugrWeapon25, draugrSword.ToLink(), draugrWaraxe.ToLink(), draugrMace.ToLink(), draugrDagger.ToLink(), draugrBow.ToLink(), draugrGreatsword.ToLink(), draugrBattleaxe.ToLink(), draugrWarhammer.ToLink(), draugrArrows.ToLink());

            LeveledList.LinkList(SKYL.LootDraugrEnchWeapons100, draugrSwordEnch.ToLink(), draugrWaraxeEnch.ToLink(), draugrMaceEnch.ToLink(), draugrDaggerEnch.ToLink(), draugrBowEnch.ToLink(), draugrGreatswordEnch.ToLink(), draugrBattleaxeEnch.ToLink(), draugrWarhammerEnch.ToLink());
            LeveledList.LinkList(SKYL.LootDraugrEnchWeapons25, draugrSwordEnch.ToLink(), draugrWaraxeEnch.ToLink(), draugrMaceEnch.ToLink(), draugrDaggerEnch.ToLink(), draugrBowEnch.ToLink(), draugrGreatswordEnch.ToLink(), draugrBattleaxeEnch.ToLink(), draugrWarhammerEnch.ToLink());
            LeveledList.LinkList(SKYL.LootDraugrEnchWeapons15, draugrSwordEnch.ToLink(), draugrWaraxeEnch.ToLink(), draugrMaceEnch.ToLink(), draugrDaggerEnch.ToLink(), draugrBowEnch.ToLink(), draugrGreatswordEnch.ToLink(), draugrBattleaxeEnch.ToLink(), draugrWarhammerEnch.ToLink());

            LeveledList.LinkList(SKYL.LootDraugrArrows15, draugrArrows.ToLink());



            // Thalmor

            LinkedList<ItemMaterial> thalmorWeapons = new();
            thalmorWeapons.AddLast(IRON);
            thalmorWeapons.AddLast(STEEL);
            thalmorWeapons.AddLast(ELVEN);
            thalmorWeapons.AddLast(GLASS);

            var thalmorSword = LeveledList.CreateList(ItemType.Sword, "JLL_ThalmorSword", 3, thalmorWeapons, LootRQ.NoEnch).ToLink();
            var thalmorWarAxe = LeveledList.CreateList(ItemType.Waraxe, "JLL_ThalmorWarAxe", 3, thalmorWeapons, LootRQ.NoEnch).ToLink();
            var thalmorMace = LeveledList.CreateList(ItemType.Mace, "JLL_ThalmorMace", 3, thalmorWeapons, LootRQ.NoEnch).ToLink();

            LeveledList.LinkList(SKYL.LItemThalmorWeapon1H, thalmorSword, thalmorWarAxe, thalmorMace);
            LeveledList.LinkList(SKYL.LItemThalmorWeaponBow, LeveledList.FACTOR_RARE, ItemType.Bow, thalmorWeapons, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemThalmorDagger, LeveledList.FACTOR_RARE, ItemType.Dagger, thalmorWeapons, LootRQ.NoEnch);

            // Vampire

            LeveledList.LinkList(SKYL.LItemVampireBossDagger, SKYL.LItemWeaponDaggerBest);
            LeveledList.LinkList(SKYL.LItemVampireBossSword, SKYL.LItemWeaponSwordBest);
            LeveledList.LinkList(SKYL.LItemVampireBossWarAxe, SKYL.LItemWeaponWarAxeBest);

            LeveledList.LinkList(SKYL.LItemVampireDagger, SKYL.LItemWeaponDagger);
            LeveledList.LinkList(SKYL.LItemVampireSword, SKYL.LItemWeaponSword);
            LeveledList.LinkList(SKYL.LItemVampireWarAxe, SKYL.LItemWeaponWarAxe);

            LeveledList.LinkList(SKYL.LItemVampireWeaponBow, SKYL.LItemWeaponBow);
        }
    }
}
