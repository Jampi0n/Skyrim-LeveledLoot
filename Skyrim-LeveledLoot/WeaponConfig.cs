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
        public static ItemMaterial DRAUGR = new("Draugr Weapons", 75, 22, 0, 20);
        public static ItemMaterial DRAUGR_HONED = new("Draugr Honed Weapons", 0, 300, 8, 150);
        public static ItemMaterial DRAUGR_HERO = new("Draugr Hero Weapons", 0, 200, 17, 275);
        public static ItemMaterial IRON = new("Iron Weapons", 75, 22, 0, 20);
        public static ItemMaterial STEEL = new("Steel Weapons", 20, 18, 0, 30);
        public static ItemMaterial DWARVEN = new("Dwarven Weapons", 5, 15, 0, 45);
        public static ItemMaterial ELVEN = new("Elven Weapons", 0, 13, 10, 55);
        public static ItemMaterial ORCISH = new("Orcish Weapons", 0, 10, 16, 75);
        public static ItemMaterial NORDIC = new("Nordic Weapons", 0, 8, 32, 125, LootRQ.DLC2);
        public static ItemMaterial GLASS = new("Glass Weapons", 0, 7, 40, 140);
        public static ItemMaterial EBONY = new("Ebony Weapons", 0, 6, 48, 160);
        public static ItemMaterial STALHRIM = new("Stalhrim Weapons", 0, 5, 56, 180, LootRQ.Rare, LootRQ.DLC2);
        public static ItemMaterial DRAGON = new("Dragon Weapons", 0, 4, 64, 200, LootRQ.Rare);
        public static ItemMaterial DAEDRIC = new("Daedric Weapons", 0, 3, 80, 220, LootRQ.Rare);
        public static ItemMaterial ULTIMATE = new("Ultimate Weapons", 0, 2, 100, 240, LootRQ.Rare);

        public static List<ItemMaterial> REGULAR_MATERIALS = new() {
            IRON,
            STEEL,
            DWARVEN,
            ELVEN,
            ORCISH,
            NORDIC,
            GLASS,
            EBONY,
            STALHRIM,
            DRAGON,
            DAEDRIC,
            ULTIMATE
        };

        public static void Config() {
            var baseArrowDragon75 = Program.State.PatchMod.LeveledItems.AddNew();
            baseArrowDragon75.EditorID = LeveledList.prefix + "BaseArrowDragon75";
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

            // Find enchantments
            Enchanter.Reset();
            Enchanter.RegisterWeaponEnchantments(ItemType.Battleaxe, SKY.IronBattleaxe, SKYL.LItemEnchIronBattleaxe, 1);
            Enchanter.RegisterWeaponEnchantments(ItemType.Battleaxe, SKY.DaedricBattleaxe, SKYL.LItemEnchDaedricBattleaxe, 4);

            Enchanter.RegisterWeaponEnchantments(ItemType.Bow, SKY.HuntingBow, SKYL.LItemEnchHuntingBow, 1);
            Enchanter.RegisterWeaponEnchantments(ItemType.Bow, SKY.DaedricBow, SKYL.LItemEnchDaedricBow, 4);

            Enchanter.RegisterWeaponEnchantments(ItemType.Greatsword, SKY.IronGreatsword, SKYL.LItemEnchIronGreatsword, 1);
            Enchanter.RegisterWeaponEnchantments(ItemType.Greatsword, SKY.DaedricGreatsword, SKYL.LItemEnchDaedricGreatsword, 4);

            Enchanter.RegisterWeaponEnchantments(ItemType.Mace, SKY.IronMace, SKYL.LItemEnchIronMace, 1);
            Enchanter.RegisterWeaponEnchantments(ItemType.Mace, SKY.DaedricMace, SKYL.LItemEnchDaedricMace, 4);

            Enchanter.RegisterWeaponEnchantments(ItemType.Dagger, SKY.IronDagger, SKYL.LItemEnchIronDagger, 1);
            Enchanter.RegisterWeaponEnchantments(ItemType.Dagger, SKY.DaedricDagger, SKYL.LItemEnchDaedricDagger, 4);

            Enchanter.RegisterWeaponEnchantments(ItemType.Sword, SKY.IronSword, SKYL.LItemEnchIronSword, 1);
            Enchanter.RegisterWeaponEnchantments(ItemType.Sword, SKY.DaedricSword, SKYL.LItemEnchDaedricSword, 4);

            Enchanter.RegisterWeaponEnchantments(ItemType.Waraxe, SKY.IronWarAxe, SKYL.LItemEnchIronWarAxe, 1);
            Enchanter.RegisterWeaponEnchantments(ItemType.Waraxe, SKY.DaedricWarAxe, SKYL.LItemEnchDaedricWarAxe, 4);

            Enchanter.RegisterWeaponEnchantments(ItemType.Warhammer, SKY.IronWarhammer, SKYL.LItemEnchIronWarhammer, 1);
            Enchanter.RegisterWeaponEnchantments(ItemType.Warhammer, SKY.DaedricWarhammer, SKYL.LItemEnchDaedricWarhammer, 4);


            RecipeParser.Parse(ULTIMATE, REGULAR_MATERIALS, false, true);

            LeveledList.LinkList(SKYL.LItemWeaponSword, LeveledList.FACTOR_COMMON, ItemType.Sword, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponSwordBest, LeveledList.FACTOR_BEST, ItemType.Sword, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponSwordBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Sword, REGULAR_MATERIALS);
            LeveledList.LinkList(SKYL.LItemWeaponSwordSpecial, LeveledList.FACTOR_RARE, ItemType.Sword, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponSwordTown, LeveledList.FACTOR_JUNK, ItemType.Sword, REGULAR_MATERIALS);

            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponSword, LeveledList.FACTOR_COMMON, ItemType.Sword, REGULAR_MATERIALS);
            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponSwordSpecial, LeveledList.FACTOR_RARE, ItemType.Sword, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponSwordBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Sword, REGULAR_MATERIALS);

            LeveledList.LinkList(SKYL.LItemWeaponWarAxe, LeveledList.FACTOR_COMMON, ItemType.Waraxe, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponWarAxeBest, LeveledList.FACTOR_BEST, ItemType.Waraxe, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponWarAxeBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Waraxe, REGULAR_MATERIALS);
            LeveledList.LinkList(SKYL.LItemWeaponWarAxeSpecial, LeveledList.FACTOR_RARE, ItemType.Waraxe, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponWarAxeTown, LeveledList.FACTOR_JUNK, ItemType.Waraxe, REGULAR_MATERIALS);

            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponWarAxe, LeveledList.FACTOR_COMMON, ItemType.Waraxe, REGULAR_MATERIALS);
            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponWarAxeBest, LeveledList.FACTOR_BEST, ItemType.Waraxe, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponWarAxeSpecial, LeveledList.FACTOR_RARE, ItemType.Waraxe, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponWarAxeBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Waraxe, REGULAR_MATERIALS);

            LeveledList.LinkList(SKYL.LItemWeaponMace, LeveledList.FACTOR_COMMON, ItemType.Mace, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponMaceBest, LeveledList.FACTOR_BEST, ItemType.Mace, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponMaceBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Mace, REGULAR_MATERIALS);
            LeveledList.LinkList(SKYL.LItemWeaponMaceSpecial, LeveledList.FACTOR_RARE, ItemType.Mace, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponMaceTown, LeveledList.FACTOR_JUNK, ItemType.Mace, REGULAR_MATERIALS);

            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponMace, LeveledList.FACTOR_COMMON, ItemType.Mace, REGULAR_MATERIALS);
            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponMaceSpecial, LeveledList.FACTOR_RARE, ItemType.Mace, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponMaceBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Mace, REGULAR_MATERIALS);

            LeveledList.LinkList(SKYL.LItemWeaponDagger, LeveledList.FACTOR_COMMON, ItemType.Dagger, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponDaggerBest, LeveledList.FACTOR_BEST, ItemType.Dagger, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponDaggerBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Dagger, REGULAR_MATERIALS);
            LeveledList.LinkList(SKYL.LItemWeaponDaggerSpecial, LeveledList.FACTOR_RARE, ItemType.Dagger, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponDaggerTown, LeveledList.FACTOR_JUNK, ItemType.Dagger, REGULAR_MATERIALS);

            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponDagger, LeveledList.FACTOR_COMMON, ItemType.Dagger, REGULAR_MATERIALS);
            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponDaggerSpecial, LeveledList.FACTOR_RARE, ItemType.Dagger, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponDaggerBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Dagger, REGULAR_MATERIALS);


            LeveledList.LinkList(SKYL.LItemWeaponBattleAxe, LeveledList.FACTOR_COMMON, ItemType.Battleaxe, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponBattleAxeBest, LeveledList.FACTOR_BEST, ItemType.Battleaxe, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponBattleAxeBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Battleaxe, REGULAR_MATERIALS);
            LeveledList.LinkList(SKYL.LItemWeaponBattleAxeSpecial, LeveledList.FACTOR_RARE, ItemType.Battleaxe, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponBattleAxeTown, LeveledList.FACTOR_JUNK, ItemType.Battleaxe, REGULAR_MATERIALS);

            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponBattleAxe, LeveledList.FACTOR_COMMON, ItemType.Battleaxe, REGULAR_MATERIALS);
            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponBattleAxeBest, LeveledList.FACTOR_BEST, ItemType.Battleaxe, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponBattleAxeSpecial, LeveledList.FACTOR_RARE, ItemType.Battleaxe, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponBattleaxeBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Battleaxe, REGULAR_MATERIALS);


            LeveledList.LinkList(SKYL.LItemWeaponGreatSword, LeveledList.FACTOR_COMMON, ItemType.Greatsword, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponGreatSwordBest, LeveledList.FACTOR_BEST, ItemType.Greatsword, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponGreatSwordBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Greatsword, REGULAR_MATERIALS);
            LeveledList.LinkList(SKYL.LItemWeaponGreatSwordSpecial, LeveledList.FACTOR_RARE, ItemType.Greatsword, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponGreatSwordTown, LeveledList.FACTOR_JUNK, ItemType.Greatsword, REGULAR_MATERIALS);

            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponGreatsword, LeveledList.FACTOR_COMMON, ItemType.Greatsword, REGULAR_MATERIALS);
            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponGreatswordSpecial, LeveledList.FACTOR_RARE, ItemType.Greatsword, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponGreatswordBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Greatsword, REGULAR_MATERIALS);

            LeveledList.LinkList(SKYL.LItemWeaponWarhammer, LeveledList.FACTOR_COMMON, ItemType.Warhammer, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponWarhammerBest, LeveledList.FACTOR_BEST, ItemType.Warhammer, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponWarhammerBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Warhammer, REGULAR_MATERIALS);
            LeveledList.LinkList(SKYL.LItemWeaponWarhammerSpecial, LeveledList.FACTOR_RARE, ItemType.Warhammer, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponWarhammerTown, LeveledList.FACTOR_JUNK, ItemType.Warhammer, REGULAR_MATERIALS);

            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponWarhammer, LeveledList.FACTOR_COMMON, ItemType.Warhammer, REGULAR_MATERIALS);
            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponWarhammerSpecial, LeveledList.FACTOR_RARE, ItemType.Warhammer, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponWarhammerBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Warhammer, REGULAR_MATERIALS);


            LeveledList.LinkList(SKYL.LItemWeaponBow, LeveledList.FACTOR_COMMON, ItemType.Bow, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponBowBest, LeveledList.FACTOR_BEST, ItemType.Bow, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponBowBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Bow, REGULAR_MATERIALS);
            LeveledList.LinkList(SKYL.LItemWeaponBowSpecial, LeveledList.FACTOR_RARE, ItemType.Bow, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemWeaponBowTown, LeveledList.FACTOR_JUNK, ItemType.Bow, REGULAR_MATERIALS);

            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponBow, LeveledList.FACTOR_COMMON, ItemType.Bow, REGULAR_MATERIALS);
            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponBowSpecial, LeveledList.FACTOR_RARE, ItemType.Bow, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkListEnchanted(SKYL.LItemEnchWeaponBowBlacksmith, LeveledList.FACTOR_COMMON, ItemType.Bow, REGULAR_MATERIALS);

            // DLC2

            LeveledList.LinkList(DBL.DLC2LItemWeaponSword, LeveledList.FACTOR_COMMON, ItemType.Sword, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponWarAxe, LeveledList.FACTOR_COMMON, ItemType.Waraxe, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponMace, LeveledList.FACTOR_COMMON, ItemType.Mace, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponDagger, LeveledList.FACTOR_COMMON, ItemType.Dagger, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponGreatSword, LeveledList.FACTOR_COMMON, ItemType.Greatsword, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponBattleAxe, LeveledList.FACTOR_COMMON, ItemType.Battleaxe, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponWarhammer, LeveledList.FACTOR_COMMON, ItemType.Warhammer, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponBow, LeveledList.FACTOR_COMMON, ItemType.Bow, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);

            LeveledList.LinkList(DBL.DLC2LItemWeaponSwordTown, LeveledList.FACTOR_JUNK, ItemType.Sword, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponWarAxeTown, LeveledList.FACTOR_JUNK, ItemType.Waraxe, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponMaceTown, LeveledList.FACTOR_JUNK, ItemType.Mace, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponDaggerTown, LeveledList.FACTOR_JUNK, ItemType.Dagger, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponGreatSwordTown, LeveledList.FACTOR_JUNK, ItemType.Greatsword, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponBattleAxeTown, LeveledList.FACTOR_JUNK, ItemType.Battleaxe, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponWarhammerTown, LeveledList.FACTOR_JUNK, ItemType.Warhammer, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemWeaponBowTown, LeveledList.FACTOR_JUNK, ItemType.Bow, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);

            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponSword, LeveledList.FACTOR_COMMON, ItemType.Sword, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponWarAxe, LeveledList.FACTOR_COMMON, ItemType.Waraxe, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponMace, LeveledList.FACTOR_COMMON, ItemType.Mace, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponDagger, LeveledList.FACTOR_COMMON, ItemType.Dagger, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponGreatsword, LeveledList.FACTOR_COMMON, ItemType.Greatsword, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponBattleAxe, LeveledList.FACTOR_COMMON, ItemType.Battleaxe, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponWarhammer, LeveledList.FACTOR_COMMON, ItemType.Warhammer, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponBow, LeveledList.FACTOR_COMMON, ItemType.Bow, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);

            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponSwordSpecial, LeveledList.FACTOR_RARE, ItemType.Sword, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponWarAxeSpecial, LeveledList.FACTOR_RARE, ItemType.Waraxe, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponMaceSpecial, LeveledList.FACTOR_RARE, ItemType.Mace, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponDaggerSpecial, LeveledList.FACTOR_RARE, ItemType.Dagger, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponGreatswordSpecial, LeveledList.FACTOR_RARE, ItemType.Greatsword, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponBattleAxeSpecial, LeveledList.FACTOR_RARE, ItemType.Battleaxe, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponWarhammerSpecial, LeveledList.FACTOR_RARE, ItemType.Warhammer, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponBowSpecial, LeveledList.FACTOR_RARE, ItemType.Bow, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);

            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponSwordBest, LeveledList.FACTOR_BEST, ItemType.Sword, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponWarAxeBest, LeveledList.FACTOR_BEST, ItemType.Waraxe, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponMaceBest, LeveledList.FACTOR_BEST, ItemType.Mace, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponDaggerBest, LeveledList.FACTOR_BEST, ItemType.Dagger, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponGreatswordBest, LeveledList.FACTOR_BEST, ItemType.Greatsword, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponBattleAxeBest, LeveledList.FACTOR_BEST, ItemType.Battleaxe, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponWarhammerBest, LeveledList.FACTOR_BEST, ItemType.Warhammer, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkListEnchanted(DBL.DLC2LItemEnchWeaponBowBest, LeveledList.FACTOR_BEST, ItemType.Bow, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);

            // Arrows
            LeveledList.LinkList(SKYL.LItemArrowsAll, LeveledList.FACTOR_COMMON, ItemType.Arrow12, REGULAR_MATERIALS);
            LeveledList.LinkList(SKYL.LItemArrowsAllBest, LeveledList.FACTOR_BEST, ItemType.Arrow12, REGULAR_MATERIALS, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArrowsAllRandomLoot, LeveledList.FACTOR_COMMON, ItemType.Arrow6, REGULAR_MATERIALS);
            LeveledList.LinkList(DBL.DLC2LItemArrowsAll, LeveledList.FACTOR_COMMON, ItemType.Arrow12, REGULAR_MATERIALS, LootRQ.DLC2);
            LeveledList.LinkList(DBL.DLC2LootArrowsAll100, LeveledList.FACTOR_BEST, ItemType.Arrow12, REGULAR_MATERIALS, LootRQ.DLC2, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LootArrowsAll15, LeveledList.FACTOR_COMMON, ItemType.Arrow12, REGULAR_MATERIALS, LootRQ.DLC2);

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
            var draugrSword = LeveledList.CreateList(ItemType.Sword, "DraugrSword", factorDraugr, draugrWeapons, LootRQ.Rare);
            var draugrWaraxe = LeveledList.CreateList(ItemType.Waraxe, "DraugrWaraxe", factorDraugr, draugrWeapons, LootRQ.Rare);
            var draugrMace = LeveledList.CreateList(ItemType.Mace, "DraugrMace", factorDraugr, draugrWeapons, LootRQ.Rare);
            var draugrDagger = LeveledList.CreateList(ItemType.Dagger, "DraugrDagger", factorDraugr, draugrWeapons, LootRQ.Rare);
            var draugrBow = LeveledList.CreateList(ItemType.Bow, "DraugrBow", factorDraugr, draugrWeapons, LootRQ.Rare);
            var draugrGreatsword = LeveledList.CreateList(ItemType.Greatsword, "DraugrGreatsword", factorDraugr, draugrWeapons, LootRQ.Rare);
            var draugrBattleaxe = LeveledList.CreateList(ItemType.Battleaxe, "DraugrBattleaxe", factorDraugr, draugrWeapons, LootRQ.Rare);
            var draugrWarhammer = LeveledList.CreateList(ItemType.Warhammer, "DraugrWarhammer", factorDraugr, draugrWeapons, LootRQ.Rare);
            var draugrArrows = LeveledList.CreateList(ItemType.Arrow12, "DraugrArrows", factorDraugr, draugrWeapons, LootRQ.Rare);

            var draugrSwordEnch = LeveledList.CreateListEnchanted(ItemType.Sword, "DraugrSwordEnch", factorDraugr, draugrWeapons, LootRQ.Rare);
            var draugrWaraxeEnch = LeveledList.CreateListEnchanted(ItemType.Waraxe, "DraugrWaraxeEnch", factorDraugr, draugrWeapons, LootRQ.Rare);
            var draugrMaceEnch = LeveledList.CreateListEnchanted(ItemType.Mace, "DraugrMaceEnch", factorDraugr, draugrWeapons, LootRQ.Rare);
            var draugrDaggerEnch = LeveledList.CreateListEnchanted(ItemType.Dagger, "DraugrDaggerEnch", factorDraugr, draugrWeapons, LootRQ.Rare);
            var draugrBowEnch = LeveledList.CreateListEnchanted(ItemType.Bow, "DraugrBowEnch", factorDraugr, draugrWeapons, LootRQ.Rare);
            var draugrGreatswordEnch = LeveledList.CreateListEnchanted(ItemType.Greatsword, "DraugrGreatswordEnch", factorDraugr, draugrWeapons, LootRQ.Rare);
            var draugrBattleaxeEnch = LeveledList.CreateListEnchanted(ItemType.Battleaxe, "DraugrBattleaxeEnch", factorDraugr, draugrWeapons, LootRQ.Rare);
            var draugrWarhammerEnch = LeveledList.CreateListEnchanted(ItemType.Warhammer, "DraugrWarhammerEnch", factorDraugr, draugrWeapons, LootRQ.Rare);

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

            var thalmorSword = LeveledList.CreateList(ItemType.Sword, "ThalmorSword", 3, thalmorWeapons).ToLink();
            var thalmorWarAxe = LeveledList.CreateList(ItemType.Waraxe, "ThalmorWarAxe", 3, thalmorWeapons).ToLink();
            var thalmorMace = LeveledList.CreateList(ItemType.Mace, "ThalmorMace", 3, thalmorWeapons).ToLink();

            LeveledList.LinkList(SKYL.LItemThalmorWeapon1H, thalmorSword, thalmorWarAxe, thalmorMace);
            LeveledList.LinkList(SKYL.LItemThalmorWeaponBow, LeveledList.FACTOR_RARE, ItemType.Bow, thalmorWeapons);
            LeveledList.LinkList(SKYL.LItemThalmorDagger, LeveledList.FACTOR_RARE, ItemType.Dagger, thalmorWeapons);

            // Vampire

            LeveledList.LinkList(SKYL.LItemVampireBossDagger, SKYL.LItemWeaponDaggerBest);
            LeveledList.LinkList(SKYL.LItemVampireBossSword, SKYL.LItemWeaponSwordBest);
            LeveledList.LinkList(SKYL.LItemVampireBossWarAxe, SKYL.LItemWeaponWarAxeBest);

            LeveledList.LinkList(SKYL.LItemVampireDagger, SKYL.LItemWeaponDagger);
            LeveledList.LinkList(SKYL.LItemVampireSword, SKYL.LItemWeaponSword);
            LeveledList.LinkList(SKYL.LItemVampireWarAxe, SKYL.LItemWeaponWarAxe);

            LeveledList.LinkList(SKYL.LItemVampireWeaponBow, SKYL.LItemWeaponBow);

            // Bandit

            // Bandit boss weapons uses limited enchantment set
            Enchanter.Reset();
            Enchanter.RegisterWeaponEnchantments(ItemType.Battleaxe, SKY.IronBattleaxe, SKYL.LItemEnchIronBattleaxeBoss, 1);
            Enchanter.RegisterWeaponEnchantments(ItemType.Battleaxe, SKY.DaedricBattleaxe, SKYL.LItemEnchDaedricBattleaxeBoss, 4);

            Enchanter.RegisterWeaponEnchantments(ItemType.Greatsword, SKY.IronGreatsword, SKYL.LItemEnchIronGreatswordBoss, 1);
            Enchanter.RegisterWeaponEnchantments(ItemType.Greatsword, SKY.DaedricGreatsword, SKYL.LItemEnchDaedricGreatswordBoss, 4);

            Enchanter.RegisterWeaponEnchantments(ItemType.Mace, SKY.IronMace, SKYL.LItemEnchIronMaceBoss, 1);
            Enchanter.RegisterWeaponEnchantments(ItemType.Mace, SKY.DaedricMace, SKYL.LItemEnchDaedricMaceBoss, 4);

            Enchanter.RegisterWeaponEnchantments(ItemType.Sword, SKY.IronSword, SKYL.LItemEnchIronSwordBoss, 1);
            Enchanter.RegisterWeaponEnchantments(ItemType.Sword, SKY.DaedricSword, SKYL.LItemEnchDaedricSwordBoss, 4);

            Enchanter.RegisterWeaponEnchantments(ItemType.Waraxe, SKY.IronWarAxe, SKYL.LItemEnchIronWarAxeBoss, 1);
            Enchanter.RegisterWeaponEnchantments(ItemType.Waraxe, SKY.DaedricWarAxe, SKYL.LItemEnchDaedricWarAxeBoss, 4);

            Enchanter.RegisterWeaponEnchantments(ItemType.Warhammer, SKY.IronWarhammer, SKYL.LItemEnchIronWarhammerBoss, 1);
            Enchanter.RegisterWeaponEnchantments(ItemType.Warhammer, SKY.DaedricWarhammer, SKYL.LItemEnchDaedricWarhammerBoss, 4);

            var bossBattleAxe = LeveledList.CreateListEnchanted(ItemType.Battleaxe, "BossBattleAxe", LeveledList.FACTOR_COMMON, REGULAR_MATERIALS).ToLink();
            var bossGreatsword = LeveledList.CreateListEnchanted(ItemType.Greatsword, "BossGreatsword", LeveledList.FACTOR_COMMON, REGULAR_MATERIALS).ToLink();
            var bossMace = LeveledList.CreateListEnchanted(ItemType.Mace, "BossMace", LeveledList.FACTOR_COMMON, REGULAR_MATERIALS).ToLink();
            var bossSword = LeveledList.CreateListEnchanted(ItemType.Sword, "BossSword", LeveledList.FACTOR_COMMON, REGULAR_MATERIALS).ToLink();
            var bossWaraxe = LeveledList.CreateListEnchanted(ItemType.Waraxe, "BossWaraxe", LeveledList.FACTOR_COMMON, REGULAR_MATERIALS).ToLink();
            var bossWarhammer = LeveledList.CreateListEnchanted(ItemType.Warhammer, "BossWarhammer", LeveledList.FACTOR_COMMON, REGULAR_MATERIALS).ToLink();


            LeveledList.LinkList(SKYL.LItemBanditBossBattleaxe, bossBattleAxe, SKYL.LItemWeaponBattleAxeSpecial);
            LeveledList.LinkList(SKYL.LItemBanditBossGreatsword, bossGreatsword, SKYL.LItemWeaponGreatSwordSpecial);
            LeveledList.LinkList(SKYL.LItemBanditBossMace, bossMace, SKYL.LItemWeaponMaceSpecial);
            LeveledList.LinkList(SKYL.LItemBanditBossSword, bossSword, SKYL.LItemWeaponSwordSpecial);
            LeveledList.LinkList(SKYL.LItemBanditBossWarAxe, bossWaraxe, SKYL.LItemWeaponWarAxeSpecial);
            LeveledList.LinkList(SKYL.LItemBanditBossWarhammer, bossWarhammer, SKYL.LItemWeaponWarhammerSpecial);

            LeveledList.LinkList(SKYL.LItemBanditBattleaxe, LeveledList.FACTOR_JUNK, ItemType.Battleaxe, REGULAR_MATERIALS);
            LeveledList.LinkList(SKYL.LItemBanditGreatsword, LeveledList.FACTOR_JUNK, ItemType.Greatsword, REGULAR_MATERIALS);
            LeveledList.LinkList(SKYL.LItemBanditWarhammer, LeveledList.FACTOR_JUNK, ItemType.Warhammer, REGULAR_MATERIALS);
            LeveledList.LinkList(SKYL.LItemBanditWarAxe, LeveledList.FACTOR_JUNK, ItemType.Waraxe, REGULAR_MATERIALS);
            LeveledList.LinkList(SKYL.LItemBanditSword, LeveledList.FACTOR_JUNK, ItemType.Sword, REGULAR_MATERIALS);
            LeveledList.LinkList(SKYL.LItemBanditMace, LeveledList.FACTOR_JUNK, ItemType.Mace, REGULAR_MATERIALS);
            LeveledList.LinkList(SKYL.LItemBanditWeaponBow, LeveledList.FACTOR_JUNK, ItemType.Bow, REGULAR_MATERIALS);


            // DLC2 touches bandit lists
            LeveledList.LockLists(SKYL.LItemBanditBattleaxe, SKYL.LItemBanditGreatsword, SKYL.LItemBanditWarhammer, SKYL.LItemBanditSword, SKYL.LItemBanditWarAxe, SKYL.LItemBanditMace, SKYL.LItemBanditWeaponBow);
            LeveledList.LockLists(SKYL.LItemBanditBossBattleaxe, SKYL.LItemBanditBossGreatsword, SKYL.LItemBanditBossWarhammer, SKYL.LItemBanditBossSword, SKYL.LItemBanditBossWarAxe, SKYL.LItemBanditBossMace);


            // Dremora
            Enchanter.Reset();

            var weaponItemTypes = new ItemType[] {
                ItemType.Sword,
                ItemType.Mace,
                ItemType.Waraxe,
                ItemType.Greatsword,
                ItemType.Battleaxe,
                ItemType.Warhammer
            };

            Enchanter.RegisterWeaponEnchantmentManual(SKY.IronSword, SKY.EnchIronSwordFire01, 1, weaponItemTypes);
            Enchanter.RegisterWeaponEnchantmentManual(SKY.IronSword, SKY.EnchIronSwordFire02, 2, weaponItemTypes);
            Enchanter.RegisterWeaponEnchantmentManual(SKY.IronSword, SKY.EnchIronSwordFire03, 3, weaponItemTypes);
            Enchanter.RegisterWeaponEnchantmentManual(SKY.DaedricSword, SKY.EnchDaedricSwordFire04, 4, weaponItemTypes);
            Enchanter.RegisterWeaponEnchantmentManual(SKY.DaedricSword, SKY.EnchDaedricSwordFire05, 5, weaponItemTypes);
            Enchanter.RegisterWeaponEnchantmentManual(SKY.DaedricSword, SKY.EnchDaedricSwordFire06, 6, weaponItemTypes);

            var dremoraBattleAxe = LeveledList.CreateListEnchanted(ItemType.Battleaxe, "DremoraBattleAxe", LeveledList.FACTOR_COMMON, REGULAR_MATERIALS).ToLink();
            var dremoraGreatsword = LeveledList.CreateListEnchanted(ItemType.Greatsword, "DremoraGreatsword", LeveledList.FACTOR_COMMON, REGULAR_MATERIALS).ToLink();
            var dremoraMace = LeveledList.CreateListEnchanted(ItemType.Mace, "DremoraMace", LeveledList.FACTOR_COMMON, REGULAR_MATERIALS).ToLink();
            var dremoraSword = LeveledList.CreateListEnchanted(ItemType.Sword, "DremoraSword", LeveledList.FACTOR_COMMON, REGULAR_MATERIALS).ToLink();
            var dremoraWaraxe = LeveledList.CreateListEnchanted(ItemType.Waraxe, "DremoraWaraxe", LeveledList.FACTOR_COMMON, REGULAR_MATERIALS).ToLink();
            var dremoraWarhammer = LeveledList.CreateListEnchanted(ItemType.Warhammer, "DremoraWarhammer", LeveledList.FACTOR_COMMON, REGULAR_MATERIALS).ToLink();

            LeveledList.LinkList(SKYL.LItemEnchWeapon1HDremoraFire, dremoraBattleAxe, dremoraGreatsword, dremoraMace, dremoraSword, dremoraWaraxe, dremoraWarhammer);

        }
    }
}
