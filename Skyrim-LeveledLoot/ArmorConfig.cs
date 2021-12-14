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
using SKY = Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.Armor;
using DB = Mutagen.Bethesda.FormKeys.SkyrimLE.Dragonborn.Armor;
using DG = Mutagen.Bethesda.FormKeys.SkyrimLE.Dawnguard.Armor;
using SKYL = Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.LeveledItem;
using DBL = Mutagen.Bethesda.FormKeys.SkyrimLE.Dragonborn.LeveledItem;
using DGL = Mutagen.Bethesda.FormKeys.SkyrimLE.Dawnguard.LeveledItem;

namespace LeveledLoot {


    class ArmorConfig {
        public static ItemMaterial IRON = new("Iron", 0.0001, 250, 1, 50, LootRQ.NoEnch);
        public static ItemMaterial STEEL = new("Steel", 0, 250, 4, 50, LootRQ.NoEnch);
        public static ItemMaterial DWARVEN = new("Dwarven", 0, 80, 7, 75, LootRQ.NoEnch);
        public static ItemMaterial BONEMOLD = new("Bonemold", 0, 100, 4, 75, LootRQ.NoEnch, LootRQ.DLC2);
        public static ItemMaterial CHITIN_HEAVY = new("ChitinHeavy", 0, 100, 12, 150, LootRQ.NoEnch, LootRQ.DLC2);
        public static ItemMaterial ORCISH = new("Orcish", 0, 80, 16, 250, LootRQ.NoEnch);
        public static ItemMaterial STEELPLATE = new("SteelPlate", 0, 120, 11, 200, LootRQ.NoEnch);
        public static ItemMaterial NORDIC = new("Nordic", 0, 90, 19, 275, LootRQ.NoEnch, LootRQ.DLC2);
        public static ItemMaterial EBONY = new("Ebony", 0, 60, 22, 350, LootRQ.NoEnch);
        public static ItemMaterial STALHRIM_HEAVY = new("StalhrimHeavy", 0, 40, 25, 375, LootRQ.NoEnch, LootRQ.Rare, LootRQ.DLC2);
        public static ItemMaterial DRAGON_HEAVY = new("DragonHeavy", 0, 30, 27, 375, LootRQ.NoEnch, LootRQ.Rare);
        public static ItemMaterial DAEDRIC = new("Daedric", 0, 20, 35, 400, LootRQ.NoEnch, LootRQ.Rare);


        public static ItemMaterial HIDE = new("Hide", 0.0001, 250, 1, 50, LootRQ.NoEnch);
        //public static ItemMaterial FUR = new("Fur");
        public static ItemMaterial LEATHER = new("Leather", 0, 250, 4, 50, LootRQ.NoEnch);
        public static ItemMaterial ELVEN = new("Elven", 0, 80, 7, 150, LootRQ.NoEnch);
        public static ItemMaterial ELVEN_LIGHT = new("ElvenLight", 0.0001, 200, 1, 75, LootRQ.NoEnch, LootRQ.Special);

        public static ItemMaterial CHITIN_LIGHT = new("ChitinLight", 0, 100, 10, 150, LootRQ.NoEnch, LootRQ.DLC2);
        public static ItemMaterial SCALED = new("Scaled", 0, 120, 13, 175, LootRQ.NoEnch);
        public static ItemMaterial GLASS = new("Glass", 0, 60, 19, 300, LootRQ.NoEnch);
        public static ItemMaterial STALHRIM_LIGHT = new("StalhrimLight", 0, 40, 25, 375, LootRQ.NoEnch, LootRQ.Rare, LootRQ.DLC2);
        public static ItemMaterial DRAGON_LIGHT = new("DragonLight", 0, 20, 27, 400, LootRQ.NoEnch, LootRQ.Rare);

        public static ItemMaterial IRON_ENCH = new("IronEnch", 0.0001, 250, 1, 50, LootRQ.Ench);
        public static ItemMaterial STEEL_ENCH = new("SteelEnch", 0, 250, 4, 50, LootRQ.Ench);
        public static ItemMaterial DWARVEN_ENCH = new("DwarvenEnch", 0, 80, 7, 75, LootRQ.Ench);
        public static ItemMaterial BONEMOLD_ENCH = new("BonemoldEnch", 0, 100, 4, 75, LootRQ.Ench, LootRQ.DLC2);
        public static ItemMaterial CHITIN_HEAVY_ENCH = new("ChitinHeavyEnch", 0, 100, 12, 150, LootRQ.Ench, LootRQ.DLC2);
        public static ItemMaterial ORCISH_ENCH = new("OrcishEnch", 0, 70, 16, 250, LootRQ.Ench);
        public static ItemMaterial STEELPLATE_ENCH = new("SteelplateEnch", 0, 120, 11, 200, LootRQ.Ench);
        public static ItemMaterial NORDIC_ENCH = new("NordicEnch", 0, 90, 19, 275, LootRQ.Ench, LootRQ.DLC2);
        public static ItemMaterial EBONY_ENCH = new("EbonyEnch", 0, 60, 22, 350, LootRQ.Ench);
        public static ItemMaterial STALHRIM_HEAVY_ENCH = new("StalhrimHeavyEnch", 0, 40, 25, 375, LootRQ.Ench, LootRQ.Rare, LootRQ.DLC2);
        public static ItemMaterial DRAGON_HEAVY_ENCH = new("DragonHeavyEnch", 0, 30, 27, 375, LootRQ.Ench, LootRQ.Rare);
        public static ItemMaterial DAEDRIC_ENCH = new("DaedricEnch", 0, 20, 35, 400, LootRQ.Ench, LootRQ.Rare);

        public static ItemMaterial HIDE_ENCH = new("HideEnch", 0.0001, 250, 1, 50, LootRQ.Ench);
        public static ItemMaterial LEATHER_ENCH = new("LeatherEnch", 0, 250, 4, 50, LootRQ.Ench);
        public static ItemMaterial ELVEN_ENCH = new("ElvenEnch", 0, 80, 7, 150, LootRQ.Ench);
        public static ItemMaterial CHITIN_LIGHT_ENCH = new("ChitinLightEnch", 0, 100, 10, 150, LootRQ.Ench, LootRQ.DLC2);
        public static ItemMaterial SCALED_ENCH = new("ScaledEnch", 0, 120, 13, 175, LootRQ.Ench);
        public static ItemMaterial GLASS_ENCH = new("GlassEnch", 0, 60, 19, 300, LootRQ.Ench);
        public static ItemMaterial STALHRIM_LIGHT_ENCH = new("StalhrimLightEnch", 0, 40, 25, 375, LootRQ.Ench, LootRQ.Rare, LootRQ.DLC2);
        public static ItemMaterial DRAGON_LIGHT_ENCH = new("DragonLightEnch", 0, 20, 27, 400, LootRQ.Ench, LootRQ.Rare);


        public static void Config() {
            IRON.DefaultHeavyArmor(SKY.ArmorIronHelmet, null, SKY.ArmorIronGauntlets, SKY.ArmorIronBoots, null);
            IRON.AddItem(ItemType.HeavyCuirass, SKY.ArmorIronCuirass, 3);
            IRON.AddItem(ItemType.HeavyCuirass, SKY.ArmorIronBandedCuirass, 1);
            IRON.AddItem(ItemType.HeavyShield, SKY.ArmorIronShield, 3);
            IRON.AddItem(ItemType.HeavyShield, SKY.ArmorIronBandedShield, 1);

            STEEL.AddItem(ItemType.HeavyHelmet, SKY.ArmorSteelHelmetA, 1);
            STEEL.AddItem(ItemType.HeavyHelmet, SKY.ArmorSteelHelmetB, 1);
            STEEL.AddItem(ItemType.HeavyCuirass, SKY.ArmorSteelCuirassA, 1);
            STEEL.AddItem(ItemType.HeavyCuirass, SKY.ArmorSteelCuirassB, 1);
            STEEL.AddItem(ItemType.HeavyGauntlets, SKY.ArmorSteelGauntletsA, 3);
            STEEL.AddItem(ItemType.HeavyGauntlets, SKY.ArmorSteelGauntletsB, 1);
            STEEL.AddItem(ItemType.HeavyBoots, SKY.ArmorSteelBootsA, 3);
            STEEL.AddItem(ItemType.HeavyBoots, SKY.ArmorSteelBootsB, 1);
            STEEL.AddItem(ItemType.HeavyShield, SKY.ArmorSteelShield, 1);

            DWARVEN.DefaultHeavyArmor(SKY.ArmorDwarvenHelmet, SKY.ArmorDwarvenCuirass, SKY.ArmorDwarvenGauntlets, SKY.ArmorDwarvenBoots, SKY.ArmorDwarvenShield);

            BONEMOLD.DefaultHeavyArmor(DB.DLC2ArmorBonemoldHelmet, null, DB.DLC2ArmorBonemoldGauntlets, DB.DLC2ArmorBonemoldBoots, DB.DLC2ArmorBonemoldShield);
            BONEMOLD.AddItem(ItemType.HeavyCuirass, DB.DLC2ArmorBonemoldCuirassVariant01);
            BONEMOLD.AddItem(ItemType.HeavyCuirass, DB.DLC2ArmorBonemoldCuirassVariant02);

            CHITIN_HEAVY.DefaultHeavyArmor(DB.DLC2ArmorChitinHeavyHelmet, DB.DLC2ArmorChitinHeavyCuirass, DB.DLC2ArmorChitinHeavyGauntlets, DB.DLC2ArmorChitinHeavyBoots, null);
            ORCISH.DefaultHeavyArmor(SKY.ArmorOrcishHelmet, SKY.ArmorOrcishCuirass, SKY.ArmorOrcishGauntlets, SKY.ArmorOrcishBoots, SKY.ArmorOrcishShield);
            STEELPLATE.DefaultHeavyArmor(SKY.ArmorSteelPlateHelmet, SKY.ArmorSteelPlateCuirass, SKY.ArmorSteelPlateGauntlets, SKY.ArmorSteelPlateBoots, null);
            NORDIC.DefaultHeavyArmor(DB.DLC2ArmorNordicHeavyHelmet, DB.DLC2ArmorNordicHeavyCuirass, DB.DLC2ArmorNordicHeavyGauntlets, DB.DLC2ArmorNordicHeavyBoots, DB.DLC2ArmorNordicShield);
            EBONY.DefaultHeavyArmor(SKY.ArmorEbonyHelmet, SKY.ArmorEbonyCuirass, SKY.ArmorEbonyGauntlets, SKY.ArmorEbonyBoots, SKY.ArmorEbonyShield);
            STALHRIM_HEAVY.DefaultHeavyArmor(DB.DLC2ArmorStalhrimHeavyHelmet, DB.DLC2ArmorStalhrimHeavyCuirass, DB.DLC2ArmorStalhrimHeavyGauntlets, DB.DLC2ArmorStalhrimHeavyBoots, null);
            DRAGON_HEAVY.DefaultHeavyArmor(SKY.ArmorDragonplateHelmet, SKY.ArmorDragonplateCuirass, SKY.ArmorDragonplateGauntlets, SKY.ArmorDragonplateBoots, SKY.ArmorDragonplateShield);
            DAEDRIC.DefaultHeavyArmor(SKY.ArmorDaedricHelmet, SKY.ArmorDaedricCuirass, SKY.ArmorDaedricGauntlets, SKY.ArmorDaedricBoots, SKY.ArmorDaedricShield);


            HIDE.DefaultLightArmor(SKY.ArmorHideHelmet, null, SKY.ArmorHideGauntlets, SKY.ArmorHideBoots, SKY.ArmorHideShield);
            HIDE.AddItem(ItemType.LightCuirass, SKY.ArmorHideCuirass, 3);
            HIDE.AddItem(ItemType.LightCuirass, SKY.ArmorStuddedCuirass, 1);

            /*FUR.AddItem(ItemType.LightHelmet, SKY.ArmorBanditHelmet, 1);
            FUR.AddItem(ItemType.LightCuirass, SKY.ArmorBanditCuirass, 1);
            FUR.AddItem(ItemType.LightCuirass, SKY.ArmorBanditCuirass1, 1);
            FUR.AddItem(ItemType.LightCuirass, SKY.ArmorBanditCuirass2, 1);
            FUR.AddItem(ItemType.LightCuirass, SKY.ArmorBanditCuirass3, 1);
            FUR.AddItem(ItemType.LightGauntlets, SKY.ArmorBanditGauntlets, 1);
            FUR.AddItem(ItemType.LightBoots, SKY.ArmorBanditBoots, 1);*/

            LEATHER.DefaultLightArmor(SKY.ArmorLeatherHelmet, SKY.ArmorLeatherCuirass, SKY.ArmorLeatherGauntlets, SKY.ArmorLeatherBoots, null);

            ELVEN_LIGHT.DefaultLightArmor(SKY.ArmorElvenLightHelmet, SKY.ArmorElvenLightCuirass, SKY.ArmorElvenLightGauntlets, SKY.ArmorElvenLightBoots, SKY.ArmorElvenShield);
            ELVEN.DefaultLightArmor(SKY.ArmorElvenHelmet, null, SKY.ArmorElvenGauntlets, SKY.ArmorElvenBoots, SKY.ArmorElvenShield);
            ELVEN.AddItem(ItemType.LightCuirass, SKY.ArmorElvenCuirass, 3);
            ELVEN.AddItem(ItemType.LightCuirass, SKY.ArmorElvenGildedCuirass, 1);

            CHITIN_LIGHT.DefaultLightArmor(DB.DLC2ArmorChitinLightHelmet, null, DB.DLC2ArmorChitinLightCuirass, DB.DLC2ArmorChitinLightBoots, DB.DLC2ArmorChitinShield);

            SCALED.DefaultLightArmor(SKY.ArmorScaledHelmet, null, SKY.ArmorScaledGauntlets, SKY.ArmorScaledBoots, null);
            SCALED.AddItem(ItemType.LightCuirass, SKY.ArmorScaledCuirass, 1);
            SCALED.AddItem(ItemType.LightCuirass, SKY.ArmorScaledCuirassB, 1);

            GLASS.DefaultLightArmor(SKY.ArmorGlassHelmet, SKY.ArmorGlassCuirass, SKY.ArmorGlassGauntlets, SKY.ArmorGlassBoots, SKY.ArmorGlassShield);

            STALHRIM_LIGHT.DefaultLightArmor(DB.DLC2ArmorStalhrimLightHelmet, DB.DLC2ArmorStalhrimLightCuirass, DB.DLC2ArmorStalhrimLightGauntlets, DB.DLC2ArmorStalhrimLightBoots, DB.DLC2ArmorStalhrimShield);
            DRAGON_LIGHT.DefaultLightArmor(SKY.ArmorDragonscaleHelmet, SKY.ArmorDragonscaleCuirass, SKY.ArmorDragonscaleGauntlets, SKY.ArmorDragonscaleBoots, SKY.ArmorDragonscaleShield);

            IRON_ENCH.AddItem(ItemType.HeavyHelmet, SKYL.SublistEnchArmorIronHelmet01, SKYL.SublistEnchArmorIronHelmet02, SKYL.SublistEnchArmorIronHelmet03);
            IRON_ENCH.AddItem(ItemType.HeavyCuirass, SKYL.SublistEnchArmorIronCuirass01, SKYL.SublistEnchArmorIronCuirass02, SKYL.SublistEnchArmorIronCuirass03);
            IRON_ENCH.AddItem(ItemType.HeavyGauntlets, SKYL.SublistEnchArmorIronGauntlets01, SKYL.SublistEnchArmorIronGauntlets02, SKYL.SublistEnchArmorIronGauntlets03);
            IRON_ENCH.AddItem(ItemType.HeavyBoots, SKYL.SublistEnchArmorIronBoots01, SKYL.SublistEnchArmorIronBoots02, SKYL.SublistEnchArmorIronBoots03);
            IRON_ENCH.AddItem(ItemType.HeavyShield, SKYL.SublistEnchArmorIronShield01, SKYL.SublistEnchArmorIronShield02, SKYL.SublistEnchArmorIronShield03);

            STEEL_ENCH.AddItem(ItemType.HeavyHelmet, SKYL.SublistEnchArmorSteelHelmet01, SKYL.SublistEnchArmorSteelHelmet02, SKYL.SublistEnchArmorSteelHelmet03);
            STEEL_ENCH.AddItem(ItemType.HeavyCuirass, SKYL.SublistEnchArmorSteelCuirass01, SKYL.SublistEnchArmorSteelCuirass02, SKYL.SublistEnchArmorSteelCuirass03);
            STEEL_ENCH.AddItem(ItemType.HeavyGauntlets, SKYL.SublistEnchArmorSteelGauntlets01, SKYL.SublistEnchArmorSteelGauntlets02, SKYL.SublistEnchArmorSteelGauntlets03);
            STEEL_ENCH.AddItem(ItemType.HeavyBoots, SKYL.SublistEnchArmorSteelBoots01, SKYL.SublistEnchArmorSteelBoots02, SKYL.SublistEnchArmorSteelBoots03);
            STEEL_ENCH.AddItem(ItemType.HeavyShield, SKYL.SublistEnchArmorSteelShield01, SKYL.SublistEnchArmorSteelShield02, SKYL.SublistEnchArmorSteelShield03);

            BONEMOLD_ENCH.AddItem(ItemType.HeavyHelmet, DBL.DLC2SublistEnchArmorBonemoldHelmet01, DBL.DLC2SublistEnchArmorBonemoldHelmet02, DBL.DLC2SublistEnchArmorBonemoldHelmet03);
            BONEMOLD_ENCH.AddItem(ItemType.HeavyCuirass, DBL.DLC2SublistEnchArmorBonemoldCuirass01, DBL.DLC2SublistEnchArmorBonemoldCuirass02, DBL.DLC2SublistEnchArmorBonemoldCuirass03);
            BONEMOLD_ENCH.AddItem(ItemType.HeavyGauntlets, DBL.DLC2SublistEnchArmorBonemoldGauntlets01, DBL.DLC2SublistEnchArmorBonemoldGauntlets02, DBL.DLC2SublistEnchArmorBonemoldGauntlets03);
            BONEMOLD_ENCH.AddItem(ItemType.HeavyBoots, DBL.DLC2SublistEnchArmorBonemoldBoots01, DBL.DLC2SublistEnchArmorBonemoldBoots02, DBL.DLC2SublistEnchArmorBonemoldBoots03);
            BONEMOLD_ENCH.AddItem(ItemType.HeavyShield, DBL.DLC2SublistEnchArmorBonemoldShield01, DBL.DLC2SublistEnchArmorBonemoldShield02, DBL.DLC2SublistEnchArmorBonemoldShield03);

            DWARVEN_ENCH.AddItem(ItemType.HeavyHelmet, SKYL.SublistEnchArmorDwarvenHelmet02, SKYL.SublistEnchArmorDwarvenHelmet03, SKYL.SublistEnchArmorDwarvenHelmet04);
            DWARVEN_ENCH.AddItem(ItemType.HeavyCuirass, SKYL.SublistEnchArmorDwarvenCuirass02, SKYL.SublistEnchArmorDwarvenCuirass03, SKYL.SublistEnchArmorDwarvenCuirass04);
            DWARVEN_ENCH.AddItem(ItemType.HeavyGauntlets, SKYL.SublistEnchArmorDwarvenGauntlets02, SKYL.SublistEnchArmorDwarvenGauntlets03, SKYL.SublistEnchArmorDwarvenGauntlets04);
            DWARVEN_ENCH.AddItem(ItemType.HeavyBoots, SKYL.SublistEnchArmorDwarvenBoots02, SKYL.SublistEnchArmorDwarvenBoots03, SKYL.SublistEnchArmorDwarvenBoots04);
            DWARVEN_ENCH.AddItem(ItemType.HeavyShield, SKYL.SublistEnchArmorDwarvenShield02, SKYL.SublistEnchArmorDwarvenShield03, SKYL.SublistEnchArmorDwarvenShield04);

            CHITIN_HEAVY_ENCH.AddItem(ItemType.HeavyHelmet, DBL.DLC2SublistEnchArmorChitinHeavyHelmet02, DBL.DLC2SublistEnchArmorChitinHeavyHelmet03, DBL.DLC2SublistEnchArmorChitinHeavyHelmet04);
            CHITIN_HEAVY_ENCH.AddItem(ItemType.HeavyCuirass, DBL.DLC2SublistEnchArmorChitinHeavyCuirass02, DBL.DLC2SublistEnchArmorChitinHeavyCuirass03, DBL.DLC2SublistEnchArmorChitinHeavyCuirass04);
            CHITIN_HEAVY_ENCH.AddItem(ItemType.HeavyGauntlets, DBL.DLC2SublistEnchArmorChitinHeavyGauntlets02, DBL.DLC2SublistEnchArmorChitinHeavyGauntlets03, DBL.DLC2SublistEnchArmorChitinHeavyGauntlets04);
            CHITIN_HEAVY_ENCH.AddItem(ItemType.HeavyBoots, DBL.DLC2SublistEnchArmorChitinHeavyBoots02, DBL.DLC2SublistEnchArmorChitinHeavyBoots03, DBL.DLC2SublistEnchArmorChitinHeavyBoots04);

            STEELPLATE_ENCH.AddItem(ItemType.HeavyHelmet, SKYL.SublistEnchArmorSteelPlateHelmet02, SKYL.SublistEnchArmorSteelPlateHelmet03, SKYL.SublistEnchArmorSteelPlateHelmet04);
            STEELPLATE_ENCH.AddItem(ItemType.HeavyCuirass, SKYL.SublistEnchArmorSteelPlateCuirass02, SKYL.SublistEnchArmorSteelPlateCuirass03, SKYL.SublistEnchArmorSteelPlateCuirass04);
            STEELPLATE_ENCH.AddItem(ItemType.HeavyGauntlets, SKYL.SublistEnchArmorSteelPlateGauntlets02, SKYL.SublistEnchArmorSteelPlateGauntlets03, SKYL.SublistEnchArmorSteelPlateGauntlets04);
            STEELPLATE_ENCH.AddItem(ItemType.HeavyBoots, SKYL.SublistEnchArmorSteelPlateBoots02, SKYL.SublistEnchArmorSteelPlateBoots03, SKYL.SublistEnchArmorSteelPlateBoots04);

            ORCISH_ENCH.AddItem(ItemType.HeavyHelmet, SKYL.SublistEnchArmorOrcishHelmet03, SKYL.SublistEnchArmorOrcishHelmet04, SKYL.SublistEnchArmorOrcishHelmet05);
            ORCISH_ENCH.AddItem(ItemType.HeavyCuirass, SKYL.SublistEnchArmorOrcishCuirass03, SKYL.SublistEnchArmorOrcishCuirass04, SKYL.SublistEnchArmorOrcishCuirass05);
            ORCISH_ENCH.AddItem(ItemType.HeavyGauntlets, SKYL.SublistEnchArmorOrcishGauntlets03, SKYL.SublistEnchArmorOrcishGauntlets04, SKYL.SublistEnchArmorOrcishGauntlets05);
            ORCISH_ENCH.AddItem(ItemType.HeavyBoots, SKYL.SublistEnchArmorOrcishBoots03, SKYL.SublistEnchArmorOrcishBoots04, SKYL.SublistEnchArmorOrcishBoots05);
            ORCISH_ENCH.AddItem(ItemType.HeavyShield, SKYL.SublistEnchArmorOrcishShield03, SKYL.SublistEnchArmorOrcishShield04, SKYL.SublistEnchArmorOrcishShield05);

            NORDIC_ENCH.AddItem(ItemType.HeavyHelmet, DBL.DLC2SublistEnchArmorNordicHeavyHelmet03, DBL.DLC2SublistEnchArmorNordicHeavyHelmet04, DBL.DLC2SublistEnchArmorNordicHeavyHelmet05);
            NORDIC_ENCH.AddItem(ItemType.HeavyCuirass, DBL.DLC2SublistEnchArmorNordicHeavyCuirass03, DBL.DLC2SublistEnchArmorNordicHeavyCuirass04, DBL.DLC2SublistEnchArmorNordicHeavyCuirass05);
            NORDIC_ENCH.AddItem(ItemType.HeavyGauntlets, DBL.DLC2SublistEnchArmorNordicHeavyGauntlets03, DBL.DLC2SublistEnchArmorNordicHeavyGauntlets04, DBL.DLC2SublistEnchArmorNordicHeavyGauntlets05);
            NORDIC_ENCH.AddItem(ItemType.HeavyBoots, DBL.DLC2SublistEnchArmorNordicHeavyBoots03, DBL.DLC2SublistEnchArmorNordicHeavyBoots04, DBL.DLC2SublistEnchArmorNordicHeavyBoots05);
            NORDIC_ENCH.AddItem(ItemType.HeavyShield, DBL.DLC2SublistEnchArmorNordicShield03, DBL.DLC2SublistEnchArmorNordicShield04, DBL.DLC2SublistEnchArmorNordicShield05);

            EBONY_ENCH.AddItem(ItemType.HeavyHelmet, SKYL.SublistEnchArmorEbonyHelmet03, SKYL.SublistEnchArmorEbonyHelmet04, SKYL.SublistEnchArmorEbonyHelmet05);
            EBONY_ENCH.AddItem(ItemType.HeavyCuirass, SKYL.SublistEnchArmorEbonyCuirass03, SKYL.SublistEnchArmorEbonyCuirass04, SKYL.SublistEnchArmorEbonyCuirass05);
            EBONY_ENCH.AddItem(ItemType.HeavyGauntlets, SKYL.SublistEnchArmorEbonyGauntlets03, SKYL.SublistEnchArmorEbonyGauntlets04, SKYL.SublistEnchArmorEbonyGauntlets05);
            EBONY_ENCH.AddItem(ItemType.HeavyBoots, SKYL.SublistEnchArmorEbonyBoots03, SKYL.SublistEnchArmorEbonyBoots04, SKYL.SublistEnchArmorEbonyBoots05);
            EBONY_ENCH.AddItem(ItemType.HeavyShield, SKYL.SublistEnchArmorEbonyShield03, SKYL.SublistEnchArmorEbonyShield04, SKYL.SublistEnchArmorEbonyShield05);

            STALHRIM_HEAVY_ENCH.AddItem(ItemType.HeavyHelmet, DBL.DLC2SublistEnchArmorStalhrimHeavyHelmet04, DBL.DLC2SublistEnchArmorStalhrimHeavyHelmet05, DBL.DLC2SublistEnchArmorStalhrimHeavyHelmet06);
            STALHRIM_HEAVY_ENCH.AddItem(ItemType.HeavyCuirass, DBL.DLC2SublistEnchArmorStalhrimHeavyCuirass04, DBL.DLC2SublistEnchArmorStalhrimHeavyCuirass05, DBL.DLC2SublistEnchArmorStalhrimHeavyCuirass06);
            STALHRIM_HEAVY_ENCH.AddItem(ItemType.HeavyGauntlets, DBL.DLC2SublistEnchArmorStalhrimHeavyGauntlets04, DBL.DLC2SublistEnchArmorStalhrimHeavyGauntlets05, DBL.DLC2SublistEnchArmorStalhrimHeavyGauntlets06);
            STALHRIM_HEAVY_ENCH.AddItem(ItemType.HeavyBoots, DBL.DLC2SublistEnchArmorStalhrimHeavyBoots04, DBL.DLC2SublistEnchArmorStalhrimHeavyBoots05, DBL.DLC2SublistEnchArmorStalhrimHeavyBoots06);

            DRAGON_HEAVY_ENCH.AddItem(ItemType.HeavyHelmet, SKYL.SublistEnchArmorDragonplateHelmet04, SKYL.SublistEnchArmorDragonplateHelmet05, SKYL.SublistEnchArmorDragonplateHelmet06);
            DRAGON_HEAVY_ENCH.AddItem(ItemType.HeavyCuirass, SKYL.SublistEnchArmorDragonplateCuirass04, SKYL.SublistEnchArmorDragonplateCuirass05, SKYL.SublistEnchArmorDragonplateCuirass06);
            DRAGON_HEAVY_ENCH.AddItem(ItemType.HeavyGauntlets, SKYL.SublistEnchArmorDragonplateGauntlets04, SKYL.SublistEnchArmorDragonplateGauntlets05, SKYL.SublistEnchArmorDragonplateGauntlets06);
            DRAGON_HEAVY_ENCH.AddItem(ItemType.HeavyBoots, SKYL.SublistEnchArmorDragonplateBoots04, SKYL.SublistEnchArmorDragonplateBoots05, SKYL.SublistEnchArmorDragonplateBoots06);
            DRAGON_HEAVY_ENCH.AddItem(ItemType.HeavyShield, SKYL.SublistEnchArmorDragonplateShield04, SKYL.SublistEnchArmorDragonplateShield05, SKYL.SublistEnchArmorDragonplateShield06);

            DAEDRIC_ENCH.AddItem(ItemType.HeavyHelmet, SKYL.SublistEnchArmorDaedricHelmet04, SKYL.SublistEnchArmorDaedricHelmet05, SKYL.SublistEnchArmorDaedricHelmet06);
            DAEDRIC_ENCH.AddItem(ItemType.HeavyCuirass, SKYL.SublistEnchArmorDaedricCuirass04, SKYL.SublistEnchArmorDaedricCuirass05, SKYL.SublistEnchArmorDaedricCuirass06);
            DAEDRIC_ENCH.AddItem(ItemType.HeavyGauntlets, SKYL.SublistEnchARmorDaedricGauntlets04, SKYL.SublistEnchARmorDaedricGauntlets05, SKYL.SublistEnchARmorDaedricGauntlets06);
            DAEDRIC_ENCH.AddItem(ItemType.HeavyBoots, SKYL.SublistEnchArmorDaedricBoots04, SKYL.SublistEnchArmorDaedricBoots05, SKYL.SublistEnchArmorDaedricBoots06);
            DAEDRIC_ENCH.AddItem(ItemType.HeavyShield, SKYL.SublistEnchArmorDaedricShield04, SKYL.SublistEnchArmorDaedricShield05, SKYL.SublistEnchArmorDaedricShield06);


            HIDE_ENCH.AddItem(ItemType.LightHelmet, SKYL.SublistEnchArmorHideHelmet01, SKYL.SublistEnchArmorHideHelmet02, SKYL.SublistEnchArmorHideHelmet03);
            HIDE_ENCH.AddItem(ItemType.LightCuirass, SKYL.SublistEnchArmorHideCuirass01, SKYL.SublistEnchArmorHideCuirass02, SKYL.SublistEnchArmorHideCuirass03);
            HIDE_ENCH.AddItem(ItemType.LightGauntlets, SKYL.SublistEnchArmorHideGauntlets01, SKYL.SublistEnchArmorHideGauntlets02, SKYL.SublistEnchArmorHideGauntlets03);
            HIDE_ENCH.AddItem(ItemType.LightBoots, SKYL.SublistEnchArmorHideBoots01, SKYL.SublistEnchArmorHideBoots02, SKYL.SublistEnchArmorHideBoots03);
            HIDE_ENCH.AddItem(ItemType.LightShield, SKYL.SublistEnchArmorHideShield01, SKYL.SublistEnchArmorHideShield02, SKYL.SublistEnchArmorHideShield03);

            LEATHER_ENCH.AddItem(ItemType.LightHelmet, SKYL.SublistEnchArmorLeatherHelmet01, SKYL.SublistEnchArmorLeatherHelmet02, SKYL.SublistEnchArmorLeatherHelmet03);
            LEATHER_ENCH.AddItem(ItemType.LightCuirass, SKYL.SublistEnchArmorLeatherCuirass01, SKYL.SublistEnchArmorLeatherCuirass02, SKYL.SublistEnchArmorLeatherCuirass03);
            LEATHER_ENCH.AddItem(ItemType.LightGauntlets, SKYL.SublistEnchArmorLeatherGauntlets01, SKYL.SublistEnchArmorLeatherGauntlets02, SKYL.SublistEnchArmorLeatherGauntlets03);
            LEATHER_ENCH.AddItem(ItemType.LightBoots, SKYL.SublistEnchArmorLeatherBoots01, SKYL.SublistEnchArmorLeatherBoots02, SKYL.SublistEnchArmorLeatherBoots03);

            ELVEN_ENCH.AddItem(ItemType.LightHelmet, SKYL.SublistEnchArmorElvenHelmet02, SKYL.SublistEnchArmorElvenHelmet03, SKYL.SublistEnchArmorElvenHelmet04);
            ELVEN_ENCH.AddItem(ItemType.LightCuirass, SKYL.SublistEnchArmorElvenCuirass02, SKYL.SublistEnchArmorElvenCuirass03, SKYL.SublistEnchArmorElvenCuirass04);
            ELVEN_ENCH.AddItem(ItemType.LightGauntlets, SKYL.SublistEnchArmorElvenGauntlets02, SKYL.SublistEnchArmorElvenGauntlets03, SKYL.SublistEnchArmorElvenGauntlets04);
            ELVEN_ENCH.AddItem(ItemType.LightBoots, SKYL.SublistEnchArmorElvenBoots02, SKYL.SublistEnchArmorElvenBoots03, SKYL.SublistEnchArmorElvenBoots04);
            ELVEN_ENCH.AddItem(ItemType.LightShield, SKYL.SublistEnchArmorElvenShield02, SKYL.SublistEnchArmorElvenShield03, SKYL.SublistEnchArmorElvenShield04);

            CHITIN_LIGHT_ENCH.AddItem(ItemType.LightHelmet, DBL.DLC2SublistEnchArmorChitinLightHelmet02, DBL.DLC2SublistEnchArmorChitinLightHelmet03, DBL.DLC2SublistEnchArmorChitinLightHelmet04);
            CHITIN_LIGHT_ENCH.AddItem(ItemType.LightCuirass, DBL.DLC2SublistEnchArmorChitinLightCuirass02, DBL.DLC2SublistEnchArmorChitinLightCuirass03, DBL.DLC2SublistEnchArmorChitinLightCuirass04);
            CHITIN_LIGHT_ENCH.AddItem(ItemType.LightGauntlets, DBL.DLC2SublistEnchArmorChitinLightGauntlets02, DBL.DLC2SublistEnchArmorChitinLightGauntlets03, DBL.DLC2SublistEnchArmorChitinLightGauntlets04);
            CHITIN_LIGHT_ENCH.AddItem(ItemType.LightBoots, DBL.DLC2SublistEnchArmorChitinLightBoots02, DBL.DLC2SublistEnchArmorChitinLightBoots03, DBL.DLC2SublistEnchArmorChitinLightBoots04);
            CHITIN_LIGHT_ENCH.AddItem(ItemType.LightShield, DBL.DLC2SublistEnchArmorChitinShield02, DBL.DLC2SublistEnchArmorChitinShield03, DBL.DLC2SublistEnchArmorChitinShield04);

            SCALED_ENCH.AddItem(ItemType.LightHelmet, SKYL.SublistEnchArmorScaledHelmet02, SKYL.SublistEnchArmorScaledHelmet03, SKYL.SublistEnchArmorScaledHelmet04);
            SCALED_ENCH.AddItem(ItemType.LightCuirass, SKYL.SublistEnchArmorScaledCuirass02, SKYL.SublistEnchArmorScaledCuirass03, SKYL.SublistEnchArmorScaledCuirass04);
            SCALED_ENCH.AddItem(ItemType.LightGauntlets, SKYL.SublistEnchArmorScaledGauntlets02, SKYL.SublistEnchArmorScaledGauntlets03, SKYL.SublistEnchArmorScaledGauntlets04);
            SCALED_ENCH.AddItem(ItemType.LightBoots, SKYL.SublistEnchArmorScaledBoots02, SKYL.SublistEnchArmorScaledBoots03, SKYL.SublistEnchArmorScaledBoots04);

            GLASS_ENCH.AddItem(ItemType.LightHelmet, SKYL.SublistEnchArmorGlassHelmet03, SKYL.SublistEnchArmorGlassHelmet04, SKYL.SublistEnchArmorGlassHelmet05);
            GLASS_ENCH.AddItem(ItemType.LightCuirass, SKYL.SublistEnchArmorGlassCuirass03, SKYL.SublistEnchArmorGlassCuirass04, SKYL.SublistEnchArmorGlassCuirass05);
            GLASS_ENCH.AddItem(ItemType.LightGauntlets, SKYL.SublistEnchArmorGlassGauntlets03, SKYL.SublistEnchArmorGlassGauntlets04, SKYL.SublistEnchArmorGlassGauntlets05);
            GLASS_ENCH.AddItem(ItemType.LightBoots, SKYL.SublistEnchArmorGlassBoots03, SKYL.SublistEnchArmorGlassBoots04, SKYL.SublistEnchArmorGlassBoots05);
            GLASS_ENCH.AddItem(ItemType.LightShield, SKYL.SublistEnchArmorGlassShield03, SKYL.SublistEnchArmorGlassShield04, SKYL.SublistEnchArmorGlassShield05);

            STALHRIM_LIGHT_ENCH.AddItem(ItemType.LightHelmet, DBL.DLC2SublistEnchArmorStalhrimLightHelmet03, DBL.DLC2SublistEnchArmorStalhrimLightHelmet04, DBL.DLC2SublistEnchArmorStalhrimLightHelmet05);
            STALHRIM_LIGHT_ENCH.AddItem(ItemType.LightCuirass, DBL.DLC2SublistEnchArmorStalhrimLightCuirass03, DBL.DLC2SublistEnchArmorStalhrimLightCuirass04, DBL.DLC2SublistEnchArmorStalhrimLightCuirass05);
            STALHRIM_LIGHT_ENCH.AddItem(ItemType.LightGauntlets, DBL.DLC2SublistEnchArmorStalhrimLightGauntlets03, DBL.DLC2SublistEnchArmorStalhrimLightGauntlets04, DBL.DLC2SublistEnchArmorStalhrimLightGauntlets05);
            STALHRIM_LIGHT_ENCH.AddItem(ItemType.LightBoots, DBL.DLC2SublistEnchArmorStalhrimLightBoots03, DBL.DLC2SublistEnchArmorStalhrimLightBoots04, DBL.DLC2SublistEnchArmorStalhrimLightBoots05);
            STALHRIM_LIGHT_ENCH.AddItem(ItemType.LightShield, DBL.DLC2SublistEnchArmorStalhrimShield03, DBL.DLC2SublistEnchArmorStalhrimShield04, DBL.DLC2SublistEnchArmorStalhrimShield05);

            DRAGON_LIGHT_ENCH.AddItem(ItemType.LightHelmet, SKYL.SublistEnchArmorDragonscaleHelmet04, SKYL.SublistEnchArmorDragonscaleHelmet05, SKYL.SublistEnchArmorDragonscaleHelmet06);
            DRAGON_LIGHT_ENCH.AddItem(ItemType.LightCuirass, SKYL.SublistEnchArmorDragonscaleCuirass04, SKYL.SublistEnchArmorDragonscaleCuirass05, SKYL.SublistEnchArmorDragonscaleCuirass06);
            DRAGON_LIGHT_ENCH.AddItem(ItemType.LightGauntlets, SKYL.SublistEnchArmorDragonscaleGauntlets04, SKYL.SublistEnchArmorDragonscaleGauntlets05, SKYL.SublistEnchArmorDragonscaleGauntlets06);
            DRAGON_LIGHT_ENCH.AddItem(ItemType.LightBoots, SKYL.SublistEnchArmorDragonscaleBoots04, SKYL.SublistEnchArmorDragonscaleBoots05, SKYL.SublistEnchArmorDragonscaleBoots06);
            DRAGON_LIGHT_ENCH.AddItem(ItemType.LightShield, SKYL.SublistEnchArmorDragonscaleShield04, SKYL.SublistEnchArmorDragonscaleShield05, SKYL.SublistEnchArmorDragonscaleShield06);

            // Best = 4
            // Special/Reward = 3
            // Blacksmith/Town/Normal = 2

            // Light

            LeveledList.LinkList(SKYL.LItemArmorHelmetLightSpecial, 3, ItemType.LightHelmet, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorCuirassLightSpecial, 3, ItemType.LightCuirass, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorGauntletsLightSpecial, 3, ItemType.LightGauntlets, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorBootsLightSpecial, 3, ItemType.LightBoots, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorShieldLightSpecial, 3, ItemType.LightShield, LootRQ.NoEnch, LootRQ.Rare);

            LeveledList.LinkList(SKYL.LItemArmorHelmetLightTown, 1, ItemType.LightHelmet, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorCuirassLightTown, 1, ItemType.LightCuirass, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorGauntletsLightTown, 1, ItemType.LightGauntlets, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LitemArmorBootsLightTown, 1, ItemType.LightBoots, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorShieldLightTown, 1, ItemType.LightShield, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemArmorHelmetLightReward, 3, ItemType.LightHelmet, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorCuirassLightReward, 3, ItemType.LightCuirass, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorShieldLightReward, 3, ItemType.LightShield, LootRQ.NoEnch, LootRQ.Rare);

            LeveledList.LinkList(SKYL.LItemArmorHelmetLight, 2, ItemType.LightHelmet, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorCuirassLight, 2, ItemType.LightCuirass, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorGauntletsLight, 2, ItemType.LightGauntlets, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorBootsLight, 2, ItemType.LightBoots, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorShieldLight, 2, ItemType.LightShield, LootRQ.NoEnch, LootRQ.Rare);

            LeveledList.LinkList(SKYL.LItemArmorHelmetLightBlacksmith, 2, ItemType.LightHelmet, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorCuirassLightBlacksmith, 2, ItemType.LightCuirass, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorGauntletsLightBlacksmith, 2, ItemType.LightGauntlets, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LitemArmorBootsLightBlacksmith, 2, ItemType.LightBoots, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorShieldLightBlacksmith, 2, ItemType.LightShield, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemArmorHelmetLightBest, 4, ItemType.LightHelmet, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorCuirassLightBest, 4, ItemType.LightCuirass, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorGauntletsLightBest, 4, ItemType.LightGauntlets, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorBootsLightBest, 4, ItemType.LightBoots, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorShieldLightBest, 4, ItemType.LightShield, LootRQ.NoEnch, LootRQ.Rare);

            LeveledList.LinkList(SKYL.LItemEnchArmorLightHelmet, 2, ItemType.LightHelmet, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightCuirass, 2, ItemType.LightCuirass, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightGauntlets, 2, ItemType.LightGauntlets, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightBoots, 2, ItemType.LightBoots, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightShield, 2, ItemType.LightShield, LootRQ.Ench);

            LeveledList.LinkList(SKYL.LItemEnchArmorLightHelmetNoDragon, 2, ItemType.LightHelmet, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightCuirassNoDragon, 2, ItemType.LightCuirass, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightGauntletsNoDragon, 2, ItemType.LightGauntlets, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightBootsNoDragon, 2, ItemType.LightBoots, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightShieldNoDragon, 2, ItemType.LightShield, LootRQ.Ench);

            LeveledList.LinkList(SKYL.LItemEnchArmorLightHelmetSpecial, 3, ItemType.LightHelmet, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightCuirassSpecial, 3, ItemType.LightCuirass, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightGauntletsSpecial, 3, ItemType.LightGauntlets, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightBootsSpecial, 3, ItemType.LightBoots, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightShieldSpecial, 3, ItemType.LightShield, LootRQ.Ench, LootRQ.Rare);

            // DLC2 Light

            LeveledList.LinkList(DBL.DLC2LItemArmorHelmetLight, 2, ItemType.LightHelmet, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorCuirassLight, 2, ItemType.LightCuirass, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorGauntletsLight, 2, ItemType.LightGauntlets, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorBootsLight, 2, ItemType.LightBoots, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorShieldLight, 2, ItemType.LightShield, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);

            LeveledList.LinkList(DBL.DLC2LItemArmorHelmetLightTown, 1, ItemType.LightHelmet, LootRQ.DLC2, LootRQ.NoEnch);
            LeveledList.LinkList(DBL.DLC2LItemArmorCuirassLightTown, 1, ItemType.LightCuirass, LootRQ.DLC2, LootRQ.NoEnch);
            LeveledList.LinkList(DBL.DLC2LItemArmorGauntletsLightTown, 1, ItemType.LightGauntlets, LootRQ.DLC2, LootRQ.NoEnch);
            LeveledList.LinkList(DBL.DLC2LitemArmorBootsLightTown, 1, ItemType.LightBoots, LootRQ.DLC2, LootRQ.NoEnch);
            LeveledList.LinkList(DBL.DLC2LItemArmorShieldLightTown, 1, ItemType.LightShield, LootRQ.DLC2, LootRQ.NoEnch);

            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightHelmet, 2, ItemType.LightHelmet, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightCuirass, 2, ItemType.LightCuirass, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightGauntlets, 2, ItemType.LightGauntlets, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightBoots, 2, ItemType.LightBoots, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightShield, 2, ItemType.LightShield, LootRQ.DLC2, LootRQ.Ench);

            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightHelmetNoDragon, 2, ItemType.LightHelmet, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightCuirassNoDragon, 2, ItemType.LightCuirass, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightGauntletsNoDragon, 2, ItemType.LightGauntlets, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightBootsNoDragon, 2, ItemType.LightBoots, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightShieldNoDragon, 2, ItemType.LightShield, LootRQ.DLC2, LootRQ.Ench);

            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightHelmetSpecial, 3, ItemType.LightHelmet, LootRQ.DLC2, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightCuirassSpecial, 3, ItemType.LightCuirass, LootRQ.DLC2, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightGauntletsSpecial, 3, ItemType.LightGauntlets, LootRQ.DLC2, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightBootsSpecial, 3, ItemType.LightBoots, LootRQ.DLC2, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightShieldSpecial, 3, ItemType.LightShield, LootRQ.DLC2, LootRQ.Ench, LootRQ.Rare);


            // Heavy

            LeveledList.LinkList(SKYL.LItemArmorHelmetHeavySpecial, 3, ItemType.HeavyHelmet, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorCuirassHeavySpecial, 3, ItemType.HeavyCuirass, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorGauntletsHeavySpecial, 3, ItemType.HeavyGauntlets, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorBootsHeavySpecial, 3, ItemType.HeavyBoots, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorShieldHeavySpecial, 3, ItemType.HeavyShield, LootRQ.NoEnch, LootRQ.Rare);

            LeveledList.LinkList(SKYL.LItemArmorHelmetHeavyTown, 1, ItemType.HeavyHelmet, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorCuirassHeavyTown, 1, ItemType.HeavyCuirass, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorGauntletsHeavyTown, 1, ItemType.HeavyGauntlets, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorBootsHeavyTown, 1, ItemType.HeavyBoots, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LitemArmorShieldHeavyTown, 1, ItemType.HeavyShield, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemArmorHelmetHeavyReward, 3, ItemType.HeavyHelmet, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorCuirassHeavyReward, 3, ItemType.HeavyCuirass, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorShieldHeavyReward, 3, ItemType.HeavyShield, LootRQ.NoEnch, LootRQ.Rare);

            LeveledList.LinkList(SKYL.LItemArmorHelmetHeavy, 2, ItemType.HeavyHelmet, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorCuirassHeavy, 2, ItemType.HeavyCuirass, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorGauntletsHeavy, 2, ItemType.HeavyGauntlets, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorBootsHeavy, 2, ItemType.HeavyBoots, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorShieldHeavy, 2, ItemType.HeavyShield, LootRQ.NoEnch, LootRQ.Rare);

            LeveledList.LinkList(SKYL.LItemArmorHelmetHeavyBlacksmith, 2, ItemType.HeavyHelmet, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorCuirassHeavyBlacksmith, 2, ItemType.HeavyCuirass, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorGauntletsHeavyBlacksmith, 2, ItemType.HeavyGauntlets, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorBootsHeavyBlacksmith, 2, ItemType.HeavyBoots, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LitemArmorShieldHeavyBlacksmith, 2, ItemType.HeavyShield, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemArmorHelmetHeavyBest, 4, ItemType.HeavyHelmet, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorCuirassHeavyBest, 4, ItemType.HeavyCuirass, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorGauntletsHeavyBest, 4, ItemType.HeavyGauntlets, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorBootsHeavyBest, 4, ItemType.HeavyBoots, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorShieldHeavyBest, 4, ItemType.HeavyShield, LootRQ.NoEnch, LootRQ.Rare);

            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyHelmet, 2, ItemType.HeavyHelmet, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyCuirass, 2, ItemType.HeavyCuirass, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyGauntlets, 2, ItemType.HeavyGauntlets, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyBoots, 2, ItemType.HeavyBoots, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyShield, 2, ItemType.HeavyShield, LootRQ.Ench);

            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyHelmetNoDragon, 2, ItemType.HeavyHelmet, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyCuirassNoDragon, 2, ItemType.HeavyCuirass, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyGauntletsNoDragon, 2, ItemType.HeavyGauntlets, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyBootsNoDragon, 2, ItemType.HeavyBoots, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyShieldNoDragon, 2, ItemType.HeavyShield, LootRQ.Ench);

            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyHelmetSpecial, 3, ItemType.HeavyHelmet, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyCuirassSpecial, 3, ItemType.HeavyCuirass, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyGauntletsSpecial, 3, ItemType.HeavyGauntlets, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyBootsSpecial, 3, ItemType.HeavyBoots, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyShieldSpecial, 3, ItemType.HeavyShield, LootRQ.Ench, LootRQ.Rare);

            // DLC2 Heavy

            LeveledList.LinkList(DBL.DLC2LItemArmorHelmetHeavy, 2, ItemType.HeavyHelmet, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorCuirassHeavy, 2, ItemType.HeavyCuirass, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorGauntletsHeavy, 2, ItemType.HeavyGauntlets, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorBootsHeavy, 2, ItemType.HeavyBoots, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorShieldHeavy, 2, ItemType.HeavyShield, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);

            LeveledList.LinkList(DBL.DLC2LItemArmorHelmetHeavyTown, 1, ItemType.HeavyHelmet, LootRQ.DLC2, LootRQ.NoEnch);
            LeveledList.LinkList(DBL.DLC2LItemArmorCuirassHeavyTown, 1, ItemType.HeavyCuirass, LootRQ.DLC2, LootRQ.NoEnch);
            LeveledList.LinkList(DBL.DLC2LItemArmorGauntletsHeavyTown, 1, ItemType.HeavyGauntlets, LootRQ.DLC2, LootRQ.NoEnch);
            LeveledList.LinkList(DBL.DLC2LItemArmorBootsHeavyTown, 1, ItemType.HeavyBoots, LootRQ.DLC2, LootRQ.NoEnch);
            LeveledList.LinkList(DBL.DLC2LitemArmorShieldHeavyTown, 1, ItemType.HeavyShield, LootRQ.DLC2, LootRQ.NoEnch);

            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyHelmet, 2, ItemType.HeavyHelmet, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyCuirass, 2, ItemType.HeavyCuirass, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyGauntlets, 2, ItemType.HeavyGauntlets, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyBoots, 2, ItemType.HeavyBoots, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyShield, 2, ItemType.HeavyShield, LootRQ.DLC2, LootRQ.Ench);

            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyHelmetNoDragon, 2, ItemType.HeavyHelmet, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyCuirassNoDragon, 2, ItemType.HeavyCuirass, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyGauntletsNoDragon, 2, ItemType.HeavyGauntlets, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyBootsNoDragon, 2, ItemType.HeavyBoots, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyShieldNoDragon, 2, ItemType.HeavyShield, LootRQ.DLC2, LootRQ.Ench);

            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyHelmetSpecial, 3, ItemType.HeavyHelmet, LootRQ.DLC2, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyCuirassSpecial, 3, ItemType.HeavyCuirass, LootRQ.DLC2, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyGauntletsSpecial, 3, ItemType.HeavyGauntlets, LootRQ.DLC2, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyBootsSpecial, 3, ItemType.HeavyBoots, LootRQ.DLC2, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyShieldSpecial, 3, ItemType.HeavyShield, LootRQ.DLC2, LootRQ.Ench, LootRQ.Rare);

            LeveledList.LinkList(DBL.DLC2LItemArmorHelmetHeavyBest, 4, ItemType.HeavyHelmet, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorCuirassHeavyBest, 4, ItemType.HeavyCuirass, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorGauntletsHeavyBest, 4, ItemType.HeavyGauntlets, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorBootsHeavyBest, 4, ItemType.HeavyBoots, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorShieldHeavyBest, 4, ItemType.HeavyShield, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);


            // Bandit

            LinkedList<ItemMaterial> banditArmor = new();
            banditArmor.AddLast(IRON);
            banditArmor.AddLast(STEEL);
            banditArmor.AddLast(STEELPLATE);
            banditArmor.AddLast(NORDIC);

            LeveledList.LinkList(SKYL.LItemBanditBossCuirass, 1, ItemType.HeavyCuirass, banditArmor, LootRQ.NoEnch, LootRQ.DLC2);
            LeveledList.LinkList(SKYL.LItemBanditBossHelmet50, 1, ItemType.HeavyHelmet, banditArmor, LootRQ.NoEnch, LootRQ.DLC2);
            LeveledList.LinkList(SKYL.LItemBanditBossBoots, 1, ItemType.HeavyBoots, banditArmor, LootRQ.NoEnch, LootRQ.DLC2);
            LeveledList.LinkList(SKYL.LItemBanditBossGauntlets50, 1, ItemType.HeavyGauntlets, banditArmor, LootRQ.NoEnch, LootRQ.DLC2);
            LeveledList.LinkList(SKYL.LItemBanditBossShield, 1, ItemType.HeavyShield, LootRQ.NoEnch, LootRQ.DLC2);

            LeveledList.LockLists(SKYL.LItemBanditBossCuirass, SKYL.LItemBanditBossHelmet50, SKYL.LItemBanditBossBoots, SKYL.LItemBanditBossGauntlets50, SKYL.LItemBanditBossShield);

            // Thalmor

            LinkedList<ItemMaterial> thalmorArmor = new();
            thalmorArmor.AddLast(ELVEN_LIGHT);
            thalmorArmor.AddLast(ELVEN);
            thalmorArmor.AddLast(GLASS);

            ELVEN_LIGHT.AddItem(ItemType.SetNoHelmet, SKYL.SublistThalmorElvenLightArmorNoHelmet);
            ELVEN_LIGHT.AddItem(ItemType.SetWithHelmet, SKYL.SublistThalmorElvenLightArmor);

            ELVEN.AddItem(ItemType.SetNoHelmet, SKYL.SublistThalmorElvenArmorNoHelmet);
            ELVEN.AddItem(ItemType.SetWithHelmet, SKYL.SublistThalmorElvenArmorWithHelmet);

            GLASS.AddItem(ItemType.SetNoHelmet, SKYL.SublistThalmorGlassArmorNoHelmet);
            GLASS.AddItem(ItemType.SetWithHelmet, SKYL.SublistThalmorGlassArmorWithHelmet);

            LeveledList.LinkList(SKYL.LItemThalmorArmorNoHelmetAll, 3, ItemType.SetNoHelmet, thalmorArmor, LootRQ.NoEnch, LootRQ.Special);
            LeveledList.LinkList(SKYL.LItemThalmorArmorWithHelmetBest, 3, ItemType.SetWithHelmet, thalmorArmor, LootRQ.NoEnch, LootRQ.Special);

            LeveledList.LinkList(SKYL.LItemThalmorShield, 3, ItemType.LightShield, thalmorArmor, LootRQ.NoEnch, LootRQ.Special);
        }
    }
}
