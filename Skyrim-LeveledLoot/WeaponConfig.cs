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
        public static ItemMaterial IRON = new("Iron", 0.0001, 250, 1, 50, LootRQ.NoEnch);
        public static ItemMaterial STEEL = new("Steel", 0, 250, 4, 50, LootRQ.NoEnch);
        public static ItemMaterial DWARVEN = new("Dwarven", 0, 100, 7, 75, LootRQ.NoEnch);
        public static ItemMaterial ELVEN = new("Elven", 0, 90, 12, 150, LootRQ.NoEnch);
        public static ItemMaterial ORCISH = new("Orcish", 0, 80, 16, 250, LootRQ.NoEnch);

        public static ItemMaterial NORDIC = new("Nordic", 0, 100, 17, 275, LootRQ.NoEnch, LootRQ.DLC2);
        public static ItemMaterial GLASS = new("Glass", 0, 70, 19, 300, LootRQ.NoEnch);
        public static ItemMaterial EBONY = new("Ebony", 0, 60, 22, 350, LootRQ.NoEnch);
        public static ItemMaterial STALHRIM = new("Stalhrim", 0, 40, 25, 375, LootRQ.NoEnch, LootRQ.Rare, LootRQ.DLC2);
        public static ItemMaterial DRAGON = new("Dragon", 0, 30, 27, 375, LootRQ.NoEnch, LootRQ.Rare);
        public static ItemMaterial DAEDRIC = new("Daedric", 0, 20, 35, 400, LootRQ.NoEnch, LootRQ.Rare);

        public static ItemMaterial IRON_ENCH = new("IronEnch", 0.0001, 250, 1, 50, LootRQ.Ench);
        public static ItemMaterial STEEL_ENCH = new("SteelEnch", 0, 250, 4, 50, LootRQ.Ench);
        public static ItemMaterial DWARVEN_ENCH = new("DwarvenEnch", 0, 100, 7, 75, LootRQ.Ench);
        public static ItemMaterial ELVEN_ENCH = new("ElvenEnch", 0, 90, 12, 150, LootRQ.Ench);
        public static ItemMaterial ORCISH_ENCH = new("OrcishEnch", 0, 80, 16, 250, LootRQ.Ench);

        public static ItemMaterial NORDIC_ENCH = new("NordicEnch", 0, 100, 17, 275, LootRQ.Ench, LootRQ.DLC2);
        public static ItemMaterial GLASS_ENCH = new("GlassEnch", 0, 70, 19, 300, LootRQ.Ench);
        public static ItemMaterial EBONY_ENCH = new("EbonyEnch", 0, 60, 22, 350, LootRQ.Ench);
        public static ItemMaterial STALHRIM_ENCH = new("StalhrimEnch", 0, 30, 25, 375, LootRQ.Ench, LootRQ.Rare, LootRQ.DLC2);
        //public static ItemMaterial DRAGON_ENCH = new("DragonEnch", 0, 6, 27, LootRQ.Ench, LootRQ.Rare);
        public static ItemMaterial DAEDRIC_ENCH = new("DaedricEnch", 0, 20, 35, 400, LootRQ.Ench, LootRQ.Rare);
        public static void Config() {
            IRON.DefaultWeapon(SKY.IronSword, SKY.IronWarAxe, SKY.IronMace, SKY.IronDagger, SKY.IronGreatsword, SKY.IronBattleaxe, SKY.IronWarhammer, SKY.LongBow);
            IRON.AddItem(ItemType.Arrow, Skyrim.Ammunition.IronArrow);
            STEEL.DefaultWeapon(SKY.SteelSword, SKY.SteelWarAxe, SKY.SteelMace, SKY.SteelDagger, SKY.SteelGreatsword, SKY.SteelBattleaxe, SKY.SteelWarhammer, SKY.HuntingBow);
            STEEL.AddItem(ItemType.Arrow, Skyrim.Ammunition.SteelArrow);
            DWARVEN.DefaultWeapon(SKY.DwarvenSword, SKY.DwarvenWarAxe, SKY.DwarvenMace, SKY.DwarvenDagger, SKY.DwarvenGreatsword, SKY.DwarvenBattleaxe, SKY.DwarvenWarhammer, SKY.DwarvenBow);
            DWARVEN.AddItem(ItemType.Arrow, Skyrim.Ammunition.DwarvenArrow);
            ORCISH.DefaultWeapon(SKY.OrcishSword, SKY.OrcishWarAxe, SKY.OrcishMace, SKY.OrcishDagger, SKY.OrcishGreatsword, SKY.OrcishBattleaxe, SKY.OrcishWarhammer, SKY.OrcishBow);
            ORCISH.AddItem(ItemType.Arrow, Skyrim.Ammunition.OrcishArrow);
            ELVEN.DefaultWeapon(SKY.ElvenSword, SKY.ElvenWarAxe, SKY.ElvenMace, SKY.ElvenDagger, SKY.ElvenGreatsword, SKY.ElvenBattleaxe, SKY.ElvenWarhammer, SKY.ElvenBow);
            ELVEN.AddItem(ItemType.Arrow, Skyrim.Ammunition.ElvenArrow);
            NORDIC.DefaultWeapon(DB.DLC2NordicSword, DB.DLC2NordicWarAxe, DB.DLC2NordicMace, DB.DLC2NordicDagger, DB.DLC2NordicGreatsword, DB.DLC2NordicBattleaxe, DB.DLC2NordicWarhammer, DB.DLC2NordicBow);
            NORDIC.AddItem(ItemType.Arrow, Dragonborn.Ammunition.DLC2NordicArrow);
            GLASS.DefaultWeapon(SKY.GlassSword, SKY.GlassWarAxe, SKY.GlassMace, SKY.GlassDagger, SKY.GlassGreatsword, SKY.GlassBattleaxe, SKY.GlassWarhammer, SKY.GlassBow);
            GLASS.AddItem(ItemType.Arrow, Skyrim.Ammunition.GlassArrow);
            EBONY.DefaultWeapon(SKY.EbonySword, SKY.EbonyWarAxe, SKY.EbonyMace, SKY.EbonyDagger, SKY.EbonyGreatsword, SKY.EbonyBattleaxe, SKY.EbonyWarhammer, SKY.EbonyBow);
            EBONY.AddItem(ItemType.Arrow, Skyrim.Ammunition.EbonyArrow);
            STALHRIM.DefaultWeapon(DB.DLC2StalhrimSword, DB.DLC2StalhrimWarAxe, DB.DLC2StalhrimMace, DB.DLC2StalhrimDagger, DB.DLC2StalhrimGreatsword, DB.DLC2StalhrimBattleaxe, DB.DLC2StalhrimWarhammer, DB.DLC2StalhrimBow);
            STALHRIM.AddItem(ItemType.Arrow, Dragonborn.Ammunition.DLC2StalhrimArrow);
            DRAGON.DefaultWeapon(DG.DLC1DragonboneSword, DG.DLC1DragonboneWarAxe, DG.DLC1DragonboneMace, DG.DLC1DragonboneDagger, DG.DLC1DragonboneGreatsword, DG.DLC1DragonboneBattleaxe, DG.DLC1DragonboneWarhammer, DG.DLC1DragonboneBow);
            DRAGON.AddItem(ItemType.Arrow, Dawnguard.Ammunition.DLC1DragonboneArrow);
            DAEDRIC.DefaultWeapon(SKY.DaedricSword, SKY.DaedricWarAxe, SKY.DaedricMace, SKY.DaedricDagger, SKY.DaedricGreatsword, SKY.DaedricBattleaxe, SKY.DaedricWarhammer, SKY.DaedricBow);
            DAEDRIC.AddItem(ItemType.Arrow, Skyrim.Ammunition.DaedricArrow);

            IRON_ENCH.DefaultWeapon(SKYL.LItemEnchIronSword, SKYL.LItemEnchIronWarAxe, SKYL.LItemEnchIronMace, SKYL.LItemEnchIronDagger, SKYL.LItemEnchIronGreatsword, SKYL.LItemEnchIronBattleaxe, SKYL.LItemEnchIronWarhammer, SKYL.LItemEnchHuntingBow);
            STEEL_ENCH.DefaultWeapon(SKYL.LItemEnchSteelSword, SKYL.LItemEnchSteelWarAxe, SKYL.LItemEnchSteelMace, SKYL.LItemEnchSteelDagger, SKYL.LItemEnchSteelGreatsword, SKYL.LItemEnchSteelBattleaxe, SKYL.LItemEnchSteelWarhammer, SKYL.LItemEnchHuntingBow);
            DWARVEN_ENCH.DefaultWeapon(SKYL.LItemEnchDwarvenSword, SKYL.LItemEnchDwarvenWarAxe, SKYL.LItemEnchDwarvenMace, SKYL.LItemEnchDwarvenDagger, SKYL.LItemEnchDwarvenGreatSword, SKYL.LItemEnchDwarvenBattleaxe, SKYL.LItemEnchDwarvenWarhammer, SKYL.LItemEnchDwarvenBow);
            ORCISH_ENCH.DefaultWeapon(SKYL.LItemEnchOrcishSword, SKYL.LItemEnchOrcishWarAxe, SKYL.LItemEnchOrcishMace, SKYL.LItemEnchOrcishDagger, SKYL.LItemEnchOrcishGreatsword, SKYL.LItemEnchOrcishBattleaxe, SKYL.LItemEnchOrcishWarHammer, SKYL.LItemEnchOrcishBow);
            ELVEN_ENCH.DefaultWeapon(SKYL.LItemEnchElvenSword, SKYL.LItemEnchElvenWarAxe, SKYL.LItemEnchElvenMace, SKYL.LItemEnchElvenDagger, SKYL.LItemEnchElvenGreatsword, SKYL.LItemEnchElvenBattleaxe, SKYL.LItemEnchElvenWarhammer, SKYL.LItemEnchElvenBow);
            NORDIC_ENCH.DefaultWeapon(DBL.DLC2LItemEnchNordicSword, DBL.DLC2LItemEnchNordicWarAxe, DBL.DLC2LItemEnchNordicMace, DBL.DLC2LItemEnchNordicDagger, DBL.DLC2LItemEnchNordicGreatsword, DBL.DLC2LItemEnchNordicBattleaxe, DBL.DLC2LItemEnchNordicWarhammer, DBL.DLC2LItemEnchNordicBow);
            GLASS_ENCH.DefaultWeapon(SKYL.LItemEnchGlassSword, SKYL.LItemEnchGlassWarAxe, SKYL.LItemEnchGlassMace, SKYL.LItemEnchGlassDagger, SKYL.LItemEnchGlassGreatsword, SKYL.LItemEnchGlassBattleaxe, SKYL.LItemEnchGlassWarhammer, SKYL.LItemEnchGlassBow);
            EBONY_ENCH.DefaultWeapon(SKYL.LItemEnchEbonySword, SKYL.LItemEnchEbonyWarAxe, SKYL.LItemEnchEbonyMace, SKYL.LItemEnchEbonyDagger, SKYL.LItemEnchEbonyGreatsword, SKYL.LItemEnchEbonyBattleaxe, SKYL.LItemEnchEbonyWarhammer, SKYL.LItemEnchEbonyBow);
            STALHRIM_ENCH.DefaultWeapon(DBL.DLC2LItemEnchStalhrimSword, DBL.DLC2LItemEnchStalhrimWarAxe, DBL.DLC2LItemEnchStalhrimMace, DBL.DLC2LItemEnchStalhrimDagger, DBL.DLC2LItemEnchStalhrimGreatsword, DBL.DLC2LItemEnchStalhrimBattleaxe, DBL.DLC2LItemEnchStalhrimWarhammer, DBL.DLC2LItemEnchStalhrimBow);
            DAEDRIC_ENCH.DefaultWeapon(SKYL.LItemEnchDaedricSword, SKYL.LItemEnchDaedricWarAxe, SKYL.LItemEnchDaedricMace, SKYL.LItemEnchDaedricDagger, SKYL.LItemEnchDaedricGreatsword, SKYL.LItemEnchDaedricBattleaxe, SKYL.LItemEnchDaedricWarhammer, SKYL.LItemEnchDaedricBow);

            LeveledList.LinkList(SKYL.LItemWeaponSword, 2, ItemType.Sword, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponSwordBest, 4, ItemType.Sword, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponSwordBlacksmith, 2, ItemType.Sword, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemWeaponSwordSpecial, 3, ItemType.Sword, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponSwordTown, 1, ItemType.Sword, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemEnchWeaponSword, 2, ItemType.Sword, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchWeaponSwordSpecial, 3, ItemType.Sword, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchWeaponSwordBlacksmith, 2, ItemType.Sword, LootRQ.Ench);

            LeveledList.LinkList(SKYL.LItemWeaponWarAxe, 2, ItemType.Waraxe, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponWarAxeBest, 4, ItemType.Waraxe, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponWarAxeBlacksmith, 2, ItemType.Waraxe, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemWeaponWarAxeSpecial, 3, ItemType.Waraxe, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponWarAxeTown, 1, ItemType.Waraxe, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemEnchWeaponWarAxe, 2, ItemType.Waraxe, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchWeaponWarAxeBest, 4, ItemType.Waraxe, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchWeaponWarAxeSpecial, 3, ItemType.Waraxe, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchWeaponWarAxeBlacksmith, 2, ItemType.Waraxe, LootRQ.Ench);

            LeveledList.LinkList(SKYL.LItemWeaponMace, 2, ItemType.Mace, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponMaceBest, 4, ItemType.Mace, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponMaceBlacksmith, 2, ItemType.Mace, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemWeaponMaceSpecial, 3, ItemType.Mace, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponMaceTown, 1, ItemType.Mace, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemEnchWeaponMace, 2, ItemType.Mace, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchWeaponMaceSpecial, 3, ItemType.Mace, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchWeaponMaceBlacksmith, 2, ItemType.Mace, LootRQ.Ench);

            LeveledList.LinkList(SKYL.LItemWeaponDagger, 2, ItemType.Dagger, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponDaggerBest, 4, ItemType.Dagger, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponDaggerBlacksmith, 2, ItemType.Dagger, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemWeaponDaggerSpecial, 3, ItemType.Dagger, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponDaggerTown, 1, ItemType.Dagger, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemEnchWeaponDagger, 2, ItemType.Dagger, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchWeaponDaggerSpecial, 3, ItemType.Dagger, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchWeaponDaggerBlacksmith, 2, ItemType.Dagger, LootRQ.Ench);


            LeveledList.LinkList(SKYL.LItemWeaponBattleAxe, 2, ItemType.Battleaxe, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponBattleAxeBest, 4, ItemType.Battleaxe, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponBattleAxeBlacksmith, 2, ItemType.Battleaxe, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemWeaponBattleAxeSpecial, 3, ItemType.Battleaxe, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponBattleAxeTown, 1, ItemType.Battleaxe, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemEnchWeaponBattleAxe, 2, ItemType.Battleaxe, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchWeaponBattleAxeBest, 4, ItemType.Battleaxe, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchWeaponBattleAxeSpecial, 3, ItemType.Battleaxe, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchWeaponBattleaxeBlacksmith, 2, ItemType.Battleaxe, LootRQ.Ench);


            LeveledList.LinkList(SKYL.LItemWeaponGreatSword, 2, ItemType.Greatsword, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponGreatSwordBest, 4, ItemType.Greatsword, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponGreatSwordBlacksmith, 2, ItemType.Greatsword, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemWeaponGreatSwordSpecial, 3, ItemType.Greatsword, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponGreatSwordTown, 1, ItemType.Greatsword, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemEnchWeaponGreatsword, 2, ItemType.Greatsword, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchWeaponGreatswordSpecial, 3, ItemType.Greatsword, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchWeaponGreatswordBlacksmith, 2, ItemType.Greatsword, LootRQ.Ench);

            LeveledList.LinkList(SKYL.LItemWeaponWarhammer, 2, ItemType.Warhammer, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponWarhammerBest, 4, ItemType.Warhammer, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponWarhammerBlacksmith, 2, ItemType.Warhammer, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemWeaponWarhammerSpecial, 3, ItemType.Warhammer, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponWarhammerTown, 1, ItemType.Warhammer, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemEnchWeaponWarhammer, 2, ItemType.Warhammer, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchWeaponWarhammerSpecial, 3, ItemType.Warhammer, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchWeaponWarhammerBlacksmith, 2, ItemType.Warhammer, LootRQ.Ench);


            LeveledList.LinkList(SKYL.LItemWeaponBow, 2, ItemType.Bow, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponBowBest, 4, ItemType.Bow, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponBowBlacksmith, 2, ItemType.Bow, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemWeaponBowSpecial, 3, ItemType.Bow, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponBowTown, 1, ItemType.Bow, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemEnchWeaponBow, 2, ItemType.Bow, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchWeaponBowSpecial, 3, ItemType.Bow, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchWeaponBowBlacksmith, 2, ItemType.Bow, LootRQ.Ench);

            // DLC2

            LeveledList.LinkList(DBL.DLC2LItemWeaponSword, 2, ItemType.Sword, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponWarAxe, 2, ItemType.Waraxe, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponMace, 2, ItemType.Mace, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponDagger, 2, ItemType.Dagger, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponGreatSword, 2, ItemType.Greatsword, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponBattleAxe, 2, ItemType.Battleaxe, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponWarhammer, 2, ItemType.Warhammer, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponBow, 2, ItemType.Bow, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);

            LeveledList.LinkList(DBL.DLC2LItemWeaponSwordTown, 1, ItemType.Sword, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponWarAxeTown, 1, ItemType.Waraxe, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponMaceTown, 1, ItemType.Mace, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponDaggerTown, 1, ItemType.Dagger, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponGreatSwordTown, 1, ItemType.Greatsword, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponBattleAxeTown, 1, ItemType.Battleaxe, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponWarhammerTown, 1, ItemType.Warhammer, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponBowTown, 1, ItemType.Bow, LootRQ.NoEnch, LootRQ.DLC2, LootRQ.Rare);

            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponSword, 2, ItemType.Sword, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponWarAxe, 2, ItemType.Waraxe, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponMace, 2, ItemType.Mace, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponDagger, 2, ItemType.Dagger, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponGreatsword, 2, ItemType.Greatsword, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponBattleAxe, 2, ItemType.Battleaxe, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponWarhammer, 2, ItemType.Warhammer, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponBow, 2, ItemType.Bow, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);

            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponSwordSpecial, 3, ItemType.Sword, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponWarAxeSpecial, 3, ItemType.Waraxe, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponMaceSpecial, 3, ItemType.Mace, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponDaggerSpecial, 3, ItemType.Dagger, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponGreatswordSpecial, 3, ItemType.Greatsword, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponBattleAxeSpecial, 3, ItemType.Battleaxe, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponWarhammerSpecial, 3, ItemType.Warhammer, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponBowSpecial, 3, ItemType.Bow, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);

            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponSwordBest, 4, ItemType.Sword, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponWarAxeBest, 4, ItemType.Waraxe, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponMaceBest, 4, ItemType.Mace, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponDaggerBest, 4, ItemType.Dagger, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponGreatswordBest, 4, ItemType.Greatsword, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponBattleAxeBest, 4, ItemType.Battleaxe, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponWarhammerBest, 4, ItemType.Warhammer, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchWeaponBowBest, 4, ItemType.Bow, LootRQ.Ench, LootRQ.DLC2, LootRQ.Rare);


            // Bandit

            LeveledList.LinkList(SKYL.LItemBanditBattleaxe, 1, ItemType.Battleaxe, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemBanditGreatsword, 1, ItemType.Greatsword, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemBanditWarhammer, 1, ItemType.Warhammer, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemBanditWarAxe, 1, ItemType.Waraxe, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemBanditSword, 1, ItemType.Sword, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemBanditMace, 1, ItemType.Mace, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemBanditWeaponBow, 1, ItemType.Bow, LootRQ.NoEnch);

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

            // Thalmor

            LinkedList<ItemMaterial> thalmorWeapons = new();
            thalmorWeapons.AddLast(IRON);
            thalmorWeapons.AddLast(STEEL);
            thalmorWeapons.AddLast(ELVEN);
            thalmorWeapons.AddLast(GLASS);

            var thalmorSword = LeveledList.CreateList(ItemType.Sword, "JLL_ThalmorSword", 3, thalmorWeapons, LootRQ.NoEnch).AsLink();
            var thalmorWarAxe = LeveledList.CreateList(ItemType.Waraxe, "JLL_ThalmorWarAxe", 3, thalmorWeapons, LootRQ.NoEnch).AsLink();
            var thalmorMace = LeveledList.CreateList(ItemType.Mace, "JLL_ThalmorMace", 3, thalmorWeapons, LootRQ.NoEnch).AsLink();

            LeveledList.LinkList(SKYL.LItemThalmorWeapon1H, thalmorSword, thalmorWarAxe, thalmorMace);
            LeveledList.LinkList(SKYL.LItemThalmorWeaponBow, 3, ItemType.Bow, thalmorWeapons, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemThalmorDagger, 3, ItemType.Dagger, thalmorWeapons, LootRQ.NoEnch);

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
