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
using SKY = Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.Armor;
using DB = Mutagen.Bethesda.FormKeys.SkyrimLE.Dragonborn.Armor;
using DG = Mutagen.Bethesda.FormKeys.SkyrimLE.Dawnguard.Armor;
using SKYL = Mutagen.Bethesda.FormKeys.SkyrimLE.Skyrim.LeveledItem;
using DBL = Mutagen.Bethesda.FormKeys.SkyrimLE.Dragonborn.LeveledItem;
using DGL = Mutagen.Bethesda.FormKeys.SkyrimLE.Dawnguard.LeveledItem;

namespace LeveledLoot {


    class ArmorConfig {
        public static ItemMaterial IRON = new("Iron", 75, 22, 0, 20);
        public static ItemMaterial DRAUGR = new("Draugr", 75, 22, 0, 20, LootRQ.Special);
        public static ItemMaterial STEEL = new("Steel", 20, 18, 0, 30);
        public static ItemMaterial DWARVEN = new("Dwarven", 5, 15, 0, 50);
        public static ItemMaterial BONEMOLD = new("Bonemold", 20, 18, 0, 30, LootRQ.DLC2);
        public static ItemMaterial CHITIN_HEAVY = new("ChitinHeavy", 5, 15, 0, 50, LootRQ.DLC2);
        public static ItemMaterial ORCISH = new("Orcish", 0, 10, 16, 75);
        public static ItemMaterial STEELPLATE = new("SteelPlate", 0, 12, 8, 60);
        public static ItemMaterial NORDIC = new("Nordic", 0, 8, 32, 125, LootRQ.DLC2);
        public static ItemMaterial EBONY = new("Ebony", 0, 6, 48, 160);
        public static ItemMaterial STALHRIM_HEAVY = new("StalhrimHeavy", 0, 5, 56, 180, LootRQ.Rare, LootRQ.DLC2);
        public static ItemMaterial DRAGON_HEAVY = new("DragonHeavy", 0, 4, 64, 200, LootRQ.Rare);
        public static ItemMaterial DAEDRIC = new("Daedric", 0, 3, 80, 220, LootRQ.Rare);
        public static ItemMaterial ULTIMATE = new("Ultimate", 0, 2, 100, 240, LootRQ.Rare);


        public static ItemMaterial HIDE = new("Hide", 75, 22, 0, 20);
        //public static ItemMaterial FUR = new("Fur");
        public static ItemMaterial LEATHER = new("Leather", 20, 18, 0, 30);
        public static ItemMaterial ELVEN = new("Elven", 5, 15, 0, 50);
        public static ItemMaterial ELVEN_LIGHT = new("ElvenLight", 75, 22, 0, 20, LootRQ.Special);

        public static ItemMaterial CHITIN_LIGHT = new("ChitinLight", 5, 15, 0, 50, LootRQ.DLC2);
        public static ItemMaterial SCALED = new("Scaled", 0, 12, 8, 60);
        public static ItemMaterial GLASS = new("Glass", 0, 6, 48, 160);
        public static ItemMaterial STALHRIM_LIGHT = new("StalhrimLight", 0, 5, 56, 180, LootRQ.Rare, LootRQ.DLC2);
        public static ItemMaterial DRAGON_LIGHT = new("DragonLight", 0, 3, 80, 220, LootRQ.Rare);


        public static void Config() {
            IRON.DefaultHeavyArmor(SKY.ArmorIronHelmet, null, SKY.ArmorIronGauntlets, SKY.ArmorIronBoots, null);
            IRON.AddItem(ItemType.HeavyCuirass, SKY.ArmorIronCuirass, 3);
            IRON.AddItem(ItemType.HeavyCuirass, SKY.ArmorIronBandedCuirass, 1);
            IRON.AddItem(ItemType.HeavyShield, SKY.ArmorIronShield, 3);
            IRON.AddItem(ItemType.HeavyShield, SKY.ArmorIronBandedShield, 1);

            DRAUGR.DefaultHeavyArmor(SKY.ArmorDraugrHelmet, SKY.ArmorDraugrCuirass, SKY.ArmorDraugrGauntlets, SKY.ArmorDraugrBoots, null);

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

            // Find enchantments
            Enchanter.RegisterEnchantments(ItemType.HeavyHelmet, Skyrim.Armor.ArmorIronHelmet, Skyrim.LeveledItem.SublistEnchArmorIronHelmet01, 1);
            Enchanter.RegisterEnchantments(ItemType.HeavyHelmet, Skyrim.Armor.ArmorIronHelmet, Skyrim.LeveledItem.SublistEnchArmorIronHelmet02, 2);
            Enchanter.RegisterEnchantments(ItemType.HeavyHelmet, Skyrim.Armor.ArmorDwarvenHelmet, Skyrim.LeveledItem.SublistEnchArmorDwarvenHelmet02, 2);            
            Enchanter.RegisterEnchantments(ItemType.HeavyHelmet, Skyrim.Armor.ArmorIronHelmet, Skyrim.LeveledItem.SublistEnchArmorIronHelmet03, 3);
            Enchanter.RegisterEnchantments(ItemType.HeavyHelmet, Skyrim.Armor.ArmorDwarvenHelmet, Skyrim.LeveledItem.SublistEnchArmorDwarvenHelmet03, 3);
            Enchanter.RegisterEnchantments(ItemType.HeavyHelmet, Skyrim.Armor.ArmorOrcishHelmet, Skyrim.LeveledItem.SublistEnchArmorOrcishHelmet03, 3);
            Enchanter.RegisterEnchantments(ItemType.HeavyHelmet, Skyrim.Armor.ArmorDwarvenHelmet, Skyrim.LeveledItem.SublistEnchArmorDwarvenHelmet04, 4);
            Enchanter.RegisterEnchantments(ItemType.HeavyHelmet, Skyrim.Armor.ArmorOrcishHelmet, Skyrim.LeveledItem.SublistEnchArmorOrcishHelmet04, 4);
            Enchanter.RegisterEnchantments(ItemType.HeavyHelmet, Skyrim.Armor.ArmorEbonyHelmet, Skyrim.LeveledItem.SublistEnchArmorEbonyHelmet04, 4);
            Enchanter.RegisterEnchantments(ItemType.HeavyHelmet, Skyrim.Armor.ArmorDaedricHelmet, Skyrim.LeveledItem.SublistEnchArmorDaedricHelmet04, 4);
            Enchanter.RegisterEnchantments(ItemType.HeavyHelmet, Skyrim.Armor.ArmorOrcishHelmet, Skyrim.LeveledItem.SublistEnchArmorOrcishHelmet05, 5);
            Enchanter.RegisterEnchantments(ItemType.HeavyHelmet, Skyrim.Armor.ArmorEbonyHelmet, Skyrim.LeveledItem.SublistEnchArmorEbonyHelmet05, 5);
            Enchanter.RegisterEnchantments(ItemType.HeavyHelmet, Skyrim.Armor.ArmorDaedricHelmet, Skyrim.LeveledItem.SublistEnchArmorDaedricHelmet05, 5);
            Enchanter.RegisterEnchantments(ItemType.HeavyHelmet, Skyrim.Armor.ArmorDaedricHelmet, Skyrim.LeveledItem.SublistEnchArmorDaedricHelmet06, 6);

            Enchanter.RegisterEnchantments(ItemType.HeavyCuirass, Skyrim.Armor.ArmorIronCuirass, Skyrim.LeveledItem.SublistEnchArmorIronCuirass01, 1);
            Enchanter.RegisterEnchantments(ItemType.HeavyCuirass, Skyrim.Armor.ArmorIronCuirass, Skyrim.LeveledItem.SublistEnchArmorIronCuirass02, 2);
            Enchanter.RegisterEnchantments(ItemType.HeavyCuirass, Skyrim.Armor.ArmorDwarvenCuirass, Skyrim.LeveledItem.SublistEnchArmorDwarvenCuirass02, 2);
            Enchanter.RegisterEnchantments(ItemType.HeavyCuirass, Skyrim.Armor.ArmorIronCuirass, Skyrim.LeveledItem.SublistEnchArmorIronCuirass03, 3);
            Enchanter.RegisterEnchantments(ItemType.HeavyCuirass, Skyrim.Armor.ArmorDwarvenCuirass, Skyrim.LeveledItem.SublistEnchArmorDwarvenCuirass03, 3);
            Enchanter.RegisterEnchantments(ItemType.HeavyCuirass, Skyrim.Armor.ArmorOrcishCuirass, Skyrim.LeveledItem.SublistEnchArmorOrcishCuirass03, 3);
            Enchanter.RegisterEnchantments(ItemType.HeavyCuirass, Skyrim.Armor.ArmorDwarvenCuirass, Skyrim.LeveledItem.SublistEnchArmorDwarvenCuirass04, 4);
            Enchanter.RegisterEnchantments(ItemType.HeavyCuirass, Skyrim.Armor.ArmorOrcishCuirass, Skyrim.LeveledItem.SublistEnchArmorOrcishCuirass04, 4);
            Enchanter.RegisterEnchantments(ItemType.HeavyCuirass, Skyrim.Armor.ArmorEbonyCuirass, Skyrim.LeveledItem.SublistEnchArmorEbonyCuirass04, 4);
            Enchanter.RegisterEnchantments(ItemType.HeavyCuirass, Skyrim.Armor.ArmorDaedricCuirass, Skyrim.LeveledItem.SublistEnchArmorDaedricCuirass04, 4);
            Enchanter.RegisterEnchantments(ItemType.HeavyCuirass, Skyrim.Armor.ArmorOrcishCuirass, Skyrim.LeveledItem.SublistEnchArmorOrcishCuirass05, 5);
            Enchanter.RegisterEnchantments(ItemType.HeavyCuirass, Skyrim.Armor.ArmorEbonyCuirass, Skyrim.LeveledItem.SublistEnchArmorEbonyCuirass05, 5);
            Enchanter.RegisterEnchantments(ItemType.HeavyCuirass, Skyrim.Armor.ArmorDaedricCuirass, Skyrim.LeveledItem.SublistEnchArmorDaedricCuirass05, 5);
            Enchanter.RegisterEnchantments(ItemType.HeavyCuirass, Skyrim.Armor.ArmorDaedricCuirass, Skyrim.LeveledItem.SublistEnchArmorDaedricCuirass06, 6);

            Enchanter.RegisterEnchantments(ItemType.HeavyGauntlets, Skyrim.Armor.ArmorIronGauntlets, Skyrim.LeveledItem.SublistEnchArmorIronGauntlets01, 1);
            Enchanter.RegisterEnchantments(ItemType.HeavyGauntlets, Skyrim.Armor.ArmorIronGauntlets, Skyrim.LeveledItem.SublistEnchArmorIronGauntlets02, 2);
            Enchanter.RegisterEnchantments(ItemType.HeavyGauntlets, Skyrim.Armor.ArmorDwarvenGauntlets, Skyrim.LeveledItem.SublistEnchArmorDwarvenGauntlets02, 2);
            Enchanter.RegisterEnchantments(ItemType.HeavyGauntlets, Skyrim.Armor.ArmorIronGauntlets, Skyrim.LeveledItem.SublistEnchArmorIronGauntlets03, 3);
            Enchanter.RegisterEnchantments(ItemType.HeavyGauntlets, Skyrim.Armor.ArmorDwarvenGauntlets, Skyrim.LeveledItem.SublistEnchArmorDwarvenGauntlets03, 3);
            Enchanter.RegisterEnchantments(ItemType.HeavyGauntlets, Skyrim.Armor.ArmorOrcishGauntlets, Skyrim.LeveledItem.SublistEnchArmorOrcishGauntlets03, 3);
            Enchanter.RegisterEnchantments(ItemType.HeavyGauntlets, Skyrim.Armor.ArmorDwarvenGauntlets, Skyrim.LeveledItem.SublistEnchArmorDwarvenGauntlets04, 4);
            Enchanter.RegisterEnchantments(ItemType.HeavyGauntlets, Skyrim.Armor.ArmorOrcishGauntlets, Skyrim.LeveledItem.SublistEnchArmorOrcishGauntlets04, 4);
            Enchanter.RegisterEnchantments(ItemType.HeavyGauntlets, Skyrim.Armor.ArmorEbonyGauntlets, Skyrim.LeveledItem.SublistEnchArmorEbonyGauntlets04, 4);
            Enchanter.RegisterEnchantments(ItemType.HeavyGauntlets, Skyrim.Armor.ArmorDaedricGauntlets, Skyrim.LeveledItem.SublistEnchARmorDaedricGauntlets04, 4);
            Enchanter.RegisterEnchantments(ItemType.HeavyGauntlets, Skyrim.Armor.ArmorOrcishGauntlets, Skyrim.LeveledItem.SublistEnchArmorOrcishGauntlets05, 5);
            Enchanter.RegisterEnchantments(ItemType.HeavyGauntlets, Skyrim.Armor.ArmorEbonyGauntlets, Skyrim.LeveledItem.SublistEnchArmorEbonyGauntlets05, 5);
            Enchanter.RegisterEnchantments(ItemType.HeavyGauntlets, Skyrim.Armor.ArmorDaedricGauntlets, Skyrim.LeveledItem.SublistEnchARmorDaedricGauntlets05, 5);
            Enchanter.RegisterEnchantments(ItemType.HeavyGauntlets, Skyrim.Armor.ArmorDaedricGauntlets, Skyrim.LeveledItem.SublistEnchARmorDaedricGauntlets06, 6);

            Enchanter.RegisterEnchantments(ItemType.HeavyBoots, Skyrim.Armor.ArmorIronBoots, Skyrim.LeveledItem.SublistEnchArmorIronBoots01, 1);
            Enchanter.RegisterEnchantments(ItemType.HeavyBoots, Skyrim.Armor.ArmorIronBoots, Skyrim.LeveledItem.SublistEnchArmorIronBoots02, 2);
            Enchanter.RegisterEnchantments(ItemType.HeavyBoots, Skyrim.Armor.ArmorDwarvenBoots, Skyrim.LeveledItem.SublistEnchArmorDwarvenBoots02, 2);
            Enchanter.RegisterEnchantments(ItemType.HeavyBoots, Skyrim.Armor.ArmorIronBoots, Skyrim.LeveledItem.SublistEnchArmorIronBoots03, 3);
            Enchanter.RegisterEnchantments(ItemType.HeavyBoots, Skyrim.Armor.ArmorDwarvenBoots, Skyrim.LeveledItem.SublistEnchArmorDwarvenBoots03, 3);
            Enchanter.RegisterEnchantments(ItemType.HeavyBoots, Skyrim.Armor.ArmorOrcishBoots, Skyrim.LeveledItem.SublistEnchArmorOrcishBoots03, 3);
            Enchanter.RegisterEnchantments(ItemType.HeavyBoots, Skyrim.Armor.ArmorDwarvenBoots, Skyrim.LeveledItem.SublistEnchArmorDwarvenBoots04, 4);
            Enchanter.RegisterEnchantments(ItemType.HeavyBoots, Skyrim.Armor.ArmorOrcishBoots, Skyrim.LeveledItem.SublistEnchArmorOrcishBoots04, 4);
            Enchanter.RegisterEnchantments(ItemType.HeavyBoots, Skyrim.Armor.ArmorEbonyBoots, Skyrim.LeveledItem.SublistEnchArmorEbonyBoots04, 4);
            Enchanter.RegisterEnchantments(ItemType.HeavyBoots, Skyrim.Armor.ArmorDaedricBoots, Skyrim.LeveledItem.SublistEnchArmorDaedricBoots04, 4);
            Enchanter.RegisterEnchantments(ItemType.HeavyBoots, Skyrim.Armor.ArmorOrcishBoots, Skyrim.LeveledItem.SublistEnchArmorOrcishBoots05, 5);
            Enchanter.RegisterEnchantments(ItemType.HeavyBoots, Skyrim.Armor.ArmorEbonyBoots, Skyrim.LeveledItem.SublistEnchArmorEbonyBoots05, 5);
            Enchanter.RegisterEnchantments(ItemType.HeavyBoots, Skyrim.Armor.ArmorDaedricBoots, Skyrim.LeveledItem.SublistEnchArmorDaedricBoots05, 5);
            Enchanter.RegisterEnchantments(ItemType.HeavyBoots, Skyrim.Armor.ArmorDaedricBoots, Skyrim.LeveledItem.SublistEnchArmorDaedricBoots06, 6);

            Enchanter.RegisterEnchantments(ItemType.HeavyShield, Skyrim.Armor.ArmorIronShield, Skyrim.LeveledItem.SublistEnchArmorIronShield01, 1);
            Enchanter.RegisterEnchantments(ItemType.HeavyShield, Skyrim.Armor.ArmorIronShield, Skyrim.LeveledItem.SublistEnchArmorIronShield02, 2);
            Enchanter.RegisterEnchantments(ItemType.HeavyShield, Skyrim.Armor.ArmorDwarvenShield, Skyrim.LeveledItem.SublistEnchArmorDwarvenShield02, 2);
            Enchanter.RegisterEnchantments(ItemType.HeavyShield, Skyrim.Armor.ArmorIronShield, Skyrim.LeveledItem.SublistEnchArmorIronShield03, 3);
            Enchanter.RegisterEnchantments(ItemType.HeavyShield, Skyrim.Armor.ArmorDwarvenShield, Skyrim.LeveledItem.SublistEnchArmorDwarvenShield03, 3);
            Enchanter.RegisterEnchantments(ItemType.HeavyShield, Skyrim.Armor.ArmorOrcishShield, Skyrim.LeveledItem.SublistEnchArmorOrcishShield03, 3);
            Enchanter.RegisterEnchantments(ItemType.HeavyShield, Skyrim.Armor.ArmorDwarvenShield, Skyrim.LeveledItem.SublistEnchArmorDwarvenShield04, 4);
            Enchanter.RegisterEnchantments(ItemType.HeavyShield, Skyrim.Armor.ArmorOrcishShield, Skyrim.LeveledItem.SublistEnchArmorOrcishShield04, 4);
            Enchanter.RegisterEnchantments(ItemType.HeavyShield, Skyrim.Armor.ArmorEbonyShield, Skyrim.LeveledItem.SublistEnchArmorEbonyShield04, 4);
            Enchanter.RegisterEnchantments(ItemType.HeavyShield, Skyrim.Armor.ArmorDaedricShield, Skyrim.LeveledItem.SublistEnchArmorDaedricShield04, 4);
            Enchanter.RegisterEnchantments(ItemType.HeavyShield, Skyrim.Armor.ArmorOrcishShield, Skyrim.LeveledItem.SublistEnchArmorOrcishShield05, 5);
            Enchanter.RegisterEnchantments(ItemType.HeavyShield, Skyrim.Armor.ArmorEbonyShield, Skyrim.LeveledItem.SublistEnchArmorEbonyShield05, 5);
            Enchanter.RegisterEnchantments(ItemType.HeavyShield, Skyrim.Armor.ArmorDaedricShield, Skyrim.LeveledItem.SublistEnchArmorDaedricShield05, 5);
            Enchanter.RegisterEnchantments(ItemType.HeavyShield, Skyrim.Armor.ArmorDaedricShield, Skyrim.LeveledItem.SublistEnchArmorDaedricShield06, 6);

            Enchanter.RegisterEnchantments(ItemType.LightHelmet, Skyrim.Armor.ArmorHideHelmet, Skyrim.LeveledItem.SublistEnchArmorHideHelmet01, 1);
            Enchanter.RegisterEnchantments(ItemType.LightHelmet, Skyrim.Armor.ArmorHideHelmet, Skyrim.LeveledItem.SublistEnchArmorHideHelmet02, 2);
            Enchanter.RegisterEnchantments(ItemType.LightHelmet, Skyrim.Armor.ArmorElvenHelmet, Skyrim.LeveledItem.SublistEnchArmorElvenHelmet02, 2);
            Enchanter.RegisterEnchantments(ItemType.LightHelmet, Skyrim.Armor.ArmorHideHelmet, Skyrim.LeveledItem.SublistEnchArmorHideHelmet03, 3);
            Enchanter.RegisterEnchantments(ItemType.LightHelmet, Skyrim.Armor.ArmorElvenHelmet, Skyrim.LeveledItem.SublistEnchArmorElvenHelmet03, 3);
            Enchanter.RegisterEnchantments(ItemType.LightHelmet, Skyrim.Armor.ArmorScaledHelmet, Skyrim.LeveledItem.SublistEnchArmorScaledHelmet03, 3);
            Enchanter.RegisterEnchantments(ItemType.LightHelmet, Skyrim.Armor.ArmorElvenHelmet, Skyrim.LeveledItem.SublistEnchArmorElvenHelmet04, 4);
            Enchanter.RegisterEnchantments(ItemType.LightHelmet, Skyrim.Armor.ArmorScaledHelmet, Skyrim.LeveledItem.SublistEnchArmorScaledHelmet04, 4);
            Enchanter.RegisterEnchantments(ItemType.LightHelmet, Skyrim.Armor.ArmorGlassHelmet, Skyrim.LeveledItem.SublistEnchArmorGlassHelmet04, 4);
            Enchanter.RegisterEnchantments(ItemType.LightHelmet, Skyrim.Armor.ArmorDragonscaleHelmet, Skyrim.LeveledItem.SublistEnchArmorDragonscaleHelmet04, 4);
            Enchanter.RegisterEnchantments(ItemType.LightHelmet, Skyrim.Armor.ArmorGlassHelmet, Skyrim.LeveledItem.SublistEnchArmorGlassHelmet05, 5);
            Enchanter.RegisterEnchantments(ItemType.LightHelmet, Skyrim.Armor.ArmorDragonscaleHelmet, Skyrim.LeveledItem.SublistEnchArmorDragonscaleHelmet05, 5);
            Enchanter.RegisterEnchantments(ItemType.LightHelmet, Skyrim.Armor.ArmorDragonscaleHelmet, Skyrim.LeveledItem.SublistEnchArmorDragonscaleHelmet06, 6);

            Enchanter.RegisterEnchantments(ItemType.LightCuirass, Skyrim.Armor.ArmorHideCuirass, Skyrim.LeveledItem.SublistEnchArmorHideCuirass01, 1);
            Enchanter.RegisterEnchantments(ItemType.LightCuirass, Skyrim.Armor.ArmorHideCuirass, Skyrim.LeveledItem.SublistEnchArmorHideCuirass02, 2);
            Enchanter.RegisterEnchantments(ItemType.LightCuirass, Skyrim.Armor.ArmorElvenCuirass, Skyrim.LeveledItem.SublistEnchArmorElvenCuirass02, 2);
            Enchanter.RegisterEnchantments(ItemType.LightCuirass, Skyrim.Armor.ArmorHideCuirass, Skyrim.LeveledItem.SublistEnchArmorHideCuirass03, 3);
            Enchanter.RegisterEnchantments(ItemType.LightCuirass, Skyrim.Armor.ArmorElvenCuirass, Skyrim.LeveledItem.SublistEnchArmorElvenCuirass03, 3);
            Enchanter.RegisterEnchantments(ItemType.LightCuirass, Skyrim.Armor.ArmorScaledCuirass, Skyrim.LeveledItem.SublistEnchArmorScaledCuirass03, 3);
            Enchanter.RegisterEnchantments(ItemType.LightCuirass, Skyrim.Armor.ArmorElvenCuirass, Skyrim.LeveledItem.SublistEnchArmorElvenCuirass04, 4);
            Enchanter.RegisterEnchantments(ItemType.LightCuirass, Skyrim.Armor.ArmorScaledCuirass, Skyrim.LeveledItem.SublistEnchArmorScaledCuirass04, 4);
            Enchanter.RegisterEnchantments(ItemType.LightCuirass, Skyrim.Armor.ArmorGlassCuirass, Skyrim.LeveledItem.SublistEnchArmorGlassCuirass04, 4);
            Enchanter.RegisterEnchantments(ItemType.LightCuirass, Skyrim.Armor.ArmorDragonscaleCuirass, Skyrim.LeveledItem.SublistEnchArmorDragonscaleCuirass04, 4);
            Enchanter.RegisterEnchantments(ItemType.LightCuirass, Skyrim.Armor.ArmorGlassCuirass, Skyrim.LeveledItem.SublistEnchArmorGlassCuirass05, 5);
            Enchanter.RegisterEnchantments(ItemType.LightCuirass, Skyrim.Armor.ArmorDragonscaleCuirass, Skyrim.LeveledItem.SublistEnchArmorDragonscaleCuirass05, 5);
            Enchanter.RegisterEnchantments(ItemType.LightCuirass, Skyrim.Armor.ArmorDragonscaleCuirass, Skyrim.LeveledItem.SublistEnchArmorDragonscaleCuirass06, 6);

            Enchanter.RegisterEnchantments(ItemType.LightGauntlets, Skyrim.Armor.ArmorHideGauntlets, Skyrim.LeveledItem.SublistEnchArmorHideGauntlets01, 1);
            Enchanter.RegisterEnchantments(ItemType.LightGauntlets, Skyrim.Armor.ArmorHideGauntlets, Skyrim.LeveledItem.SublistEnchArmorHideGauntlets02, 2);
            Enchanter.RegisterEnchantments(ItemType.LightGauntlets, Skyrim.Armor.ArmorElvenGauntlets, Skyrim.LeveledItem.SublistEnchArmorElvenGauntlets02, 2);
            Enchanter.RegisterEnchantments(ItemType.LightGauntlets, Skyrim.Armor.ArmorHideGauntlets, Skyrim.LeveledItem.SublistEnchArmorHideGauntlets03, 3);
            Enchanter.RegisterEnchantments(ItemType.LightGauntlets, Skyrim.Armor.ArmorElvenGauntlets, Skyrim.LeveledItem.SublistEnchArmorElvenGauntlets03, 3);
            Enchanter.RegisterEnchantments(ItemType.LightGauntlets, Skyrim.Armor.ArmorScaledGauntlets, Skyrim.LeveledItem.SublistEnchArmorScaledGauntlets03, 3);
            Enchanter.RegisterEnchantments(ItemType.LightGauntlets, Skyrim.Armor.ArmorElvenGauntlets, Skyrim.LeveledItem.SublistEnchArmorElvenGauntlets04, 4);
            Enchanter.RegisterEnchantments(ItemType.LightGauntlets, Skyrim.Armor.ArmorScaledGauntlets, Skyrim.LeveledItem.SublistEnchArmorScaledGauntlets04, 4);
            Enchanter.RegisterEnchantments(ItemType.LightGauntlets, Skyrim.Armor.ArmorGlassGauntlets, Skyrim.LeveledItem.SublistEnchArmorGlassGauntlets04, 4);
            Enchanter.RegisterEnchantments(ItemType.LightGauntlets, Skyrim.Armor.ArmorDragonscaleGauntlets, Skyrim.LeveledItem.SublistEnchArmorDragonscaleGauntlets04, 4);
            Enchanter.RegisterEnchantments(ItemType.LightGauntlets, Skyrim.Armor.ArmorGlassGauntlets, Skyrim.LeveledItem.SublistEnchArmorGlassGauntlets05, 5);
            Enchanter.RegisterEnchantments(ItemType.LightGauntlets, Skyrim.Armor.ArmorDragonscaleGauntlets, Skyrim.LeveledItem.SublistEnchArmorDragonscaleGauntlets05, 5);
            Enchanter.RegisterEnchantments(ItemType.LightGauntlets, Skyrim.Armor.ArmorDragonscaleGauntlets, Skyrim.LeveledItem.SublistEnchArmorDragonscaleGauntlets06, 6);

            Enchanter.RegisterEnchantments(ItemType.LightBoots, Skyrim.Armor.ArmorHideBoots, Skyrim.LeveledItem.SublistEnchArmorHideBoots01, 1);
            Enchanter.RegisterEnchantments(ItemType.LightBoots, Skyrim.Armor.ArmorHideBoots, Skyrim.LeveledItem.SublistEnchArmorHideBoots02, 2);
            Enchanter.RegisterEnchantments(ItemType.LightBoots, Skyrim.Armor.ArmorElvenBoots, Skyrim.LeveledItem.SublistEnchArmorElvenBoots02, 2);
            Enchanter.RegisterEnchantments(ItemType.LightBoots, Skyrim.Armor.ArmorHideBoots, Skyrim.LeveledItem.SublistEnchArmorHideBoots03, 3);
            Enchanter.RegisterEnchantments(ItemType.LightBoots, Skyrim.Armor.ArmorElvenBoots, Skyrim.LeveledItem.SublistEnchArmorElvenBoots03, 3);
            Enchanter.RegisterEnchantments(ItemType.LightBoots, Skyrim.Armor.ArmorScaledBoots, Skyrim.LeveledItem.SublistEnchArmorScaledBoots03, 3);
            Enchanter.RegisterEnchantments(ItemType.LightBoots, Skyrim.Armor.ArmorElvenBoots, Skyrim.LeveledItem.SublistEnchArmorElvenBoots04, 4);
            Enchanter.RegisterEnchantments(ItemType.LightBoots, Skyrim.Armor.ArmorScaledBoots, Skyrim.LeveledItem.SublistEnchArmorScaledBoots04, 4);
            Enchanter.RegisterEnchantments(ItemType.LightBoots, Skyrim.Armor.ArmorGlassBoots, Skyrim.LeveledItem.SublistEnchArmorGlassBoots04, 4);
            Enchanter.RegisterEnchantments(ItemType.LightBoots, Skyrim.Armor.ArmorDragonscaleBoots, Skyrim.LeveledItem.SublistEnchArmorDragonscaleBoots04, 4);
            Enchanter.RegisterEnchantments(ItemType.LightBoots, Skyrim.Armor.ArmorGlassBoots, Skyrim.LeveledItem.SublistEnchArmorGlassBoots05, 5);
            Enchanter.RegisterEnchantments(ItemType.LightBoots, Skyrim.Armor.ArmorDragonscaleBoots, Skyrim.LeveledItem.SublistEnchArmorDragonscaleBoots05, 5);
            Enchanter.RegisterEnchantments(ItemType.LightBoots, Skyrim.Armor.ArmorDragonscaleBoots, Skyrim.LeveledItem.SublistEnchArmorDragonscaleBoots06, 6);

            Enchanter.RegisterEnchantments(ItemType.LightShield, Skyrim.Armor.ArmorHideShield, Skyrim.LeveledItem.SublistEnchArmorHideShield01, 1);
            Enchanter.RegisterEnchantments(ItemType.LightShield, Skyrim.Armor.ArmorHideShield, Skyrim.LeveledItem.SublistEnchArmorHideShield02, 2);
            Enchanter.RegisterEnchantments(ItemType.LightShield, Skyrim.Armor.ArmorElvenShield, Skyrim.LeveledItem.SublistEnchArmorElvenShield02, 2);
            Enchanter.RegisterEnchantments(ItemType.LightShield, Skyrim.Armor.ArmorHideShield, Skyrim.LeveledItem.SublistEnchArmorHideShield03, 3);
            Enchanter.RegisterEnchantments(ItemType.LightShield, Skyrim.Armor.ArmorElvenShield, Skyrim.LeveledItem.SublistEnchArmorElvenShield03, 3);
            Enchanter.RegisterEnchantments(ItemType.LightShield, Skyrim.Armor.ArmorElvenShield, Skyrim.LeveledItem.SublistEnchArmorElvenShield04, 4);
            Enchanter.RegisterEnchantments(ItemType.LightShield, Skyrim.Armor.ArmorGlassShield, Skyrim.LeveledItem.SublistEnchArmorGlassShield04, 4);
            Enchanter.RegisterEnchantments(ItemType.LightShield, Skyrim.Armor.ArmorDragonscaleShield, Skyrim.LeveledItem.SublistEnchArmorDragonscaleShield04, 4);
            Enchanter.RegisterEnchantments(ItemType.LightShield, Skyrim.Armor.ArmorGlassShield, Skyrim.LeveledItem.SublistEnchArmorGlassShield05, 5);
            Enchanter.RegisterEnchantments(ItemType.LightShield, Skyrim.Armor.ArmorDragonscaleShield, Skyrim.LeveledItem.SublistEnchArmorDragonscaleShield05, 5);
            Enchanter.RegisterEnchantments(ItemType.LightShield, Skyrim.Armor.ArmorDragonscaleShield, Skyrim.LeveledItem.SublistEnchArmorDragonscaleShield06, 6);

            // Add craftable armor to loot
            HashSet<FormKey> craftingPerks = new();
            if(Skyrim.ActorValueInformation.AVSmithing.TryResolve(Program.state.LinkCache, out var avSmithing)) {
                foreach(var perk in avSmithing.PerkTree) {
                    craftingPerks.Add(perk.Perk.FormKey);
                }
            }

            List<ItemMaterial> regularMaterials = new();
            foreach(var material in ItemMaterial.ALL) {
                if(!material.requirements.Contains(LootRQ.Special) && !material.requirements.Contains(LootRQ.DLC2)) {
                    regularMaterials.Add(material);
                }
            }

            List<ItemType> itemTypes = new List<ItemType>();
            itemTypes.Add(ItemType.HeavyHelmet);
            itemTypes.Add(ItemType.HeavyCuirass);
            itemTypes.Add(ItemType.HeavyGauntlets);
            itemTypes.Add(ItemType.HeavyBoots);
            itemTypes.Add(ItemType.HeavyShield);
            itemTypes.Add(ItemType.LightHelmet);
            itemTypes.Add(ItemType.LightCuirass);
            itemTypes.Add(ItemType.LightGauntlets);
            itemTypes.Add(ItemType.LightBoots);
            itemTypes.Add(ItemType.LightShield);

            Dictionary<ItemType, List<Tuple<uint, ItemMaterial>>> tierList = new();
            foreach(var itemType in itemTypes) {
                tierList.Add(itemType, new List<Tuple<uint, ItemMaterial>>());
                foreach(var mat in regularMaterials) {
                    var item = mat.GetFirst(itemType);
                    if(item is IFormLink<IArmorGetter> armorLink) {
                        if(armorLink.TryResolve(Program.state.LinkCache, out var armor)) {
                            tierList[itemType].Add(new Tuple<uint, ItemMaterial>(armor.Value, mat));
                        }
                    }
                }
                tierList[itemType].Sort((Tuple<uint, ItemMaterial> a, Tuple<uint, ItemMaterial> b) => {
                    return (int)a.Item1 - (int)b.Item1;
                });
                tierList[itemType].Add(new Tuple<uint, ItemMaterial>(uint.MaxValue, ULTIMATE));
            }

            foreach(var cob in Program.state.LoadOrder.PriorityOrder.ConstructibleObject().WinningOverrides()) {
                bool isValidCob = true;
                var modName = cob.FormKey.ModKey.Name;
                if(modName == "Skyrim" || modName == "Dawnguard" || modName == "HearthFires" || modName == "Dragonborn") {
                    continue;
                }
                if(cob.CreatedObjectCount == 1) {
                    foreach(var cond in cob.Conditions) {
                        if(cond.Data.Function == Condition.Function.HasPerk) {
                            var hasPerk = cond.Data as HasPerkConditionData;
                            if(hasPerk != null) {
                                if(!craftingPerks.Contains(hasPerk.Perk.Link.FormKey)) {
                                    isValidCob = false;
                                    break;
                                }
                            } else {
                                isValidCob = false;
                                break;
                            }
                        } else {
                            isValidCob = false;
                            break;
                        }
                    }
                } else {
                    isValidCob = false;
                }
                if(isValidCob) {
                    if(cob.CreatedObject.TryResolve(Program.state.LinkCache, out var createdItem)) {
                        if(createdItem is IArmorGetter armor) {
                            ItemType? itemType = null;
                            if(armor.BodyTemplate.ArmorType == ArmorType.HeavyArmor) {
                                if(armor.HasKeyword(Skyrim.Keyword.ArmorHelmet)) {
                                    itemType = ItemType.HeavyHelmet;
                                } else if(armor.HasKeyword(Skyrim.Keyword.ArmorCuirass)) {
                                    itemType = ItemType.HeavyCuirass;
                                } else if(armor.HasKeyword(Skyrim.Keyword.ArmorGauntlets)) {
                                    itemType = ItemType.HeavyGauntlets;
                                } else if(armor.HasKeyword(Skyrim.Keyword.ArmorBoots)) {
                                    itemType = ItemType.HeavyBoots;
                                } else if(armor.HasKeyword(Skyrim.Keyword.ArmorShield)) {
                                    itemType = ItemType.HeavyShield;
                                }
                            } else if(armor.BodyTemplate.ArmorType == ArmorType.LightArmor) {
                                if(armor.HasKeyword(Skyrim.Keyword.ArmorHelmet)) {
                                    itemType = ItemType.LightHelmet;
                                } else if(armor.HasKeyword(Skyrim.Keyword.ArmorCuirass)) {
                                    itemType = ItemType.LightCuirass;
                                } else if(armor.HasKeyword(Skyrim.Keyword.ArmorGauntlets)) {
                                    itemType = ItemType.LightGauntlets;
                                } else if(armor.HasKeyword(Skyrim.Keyword.ArmorBoots)) {
                                    itemType = ItemType.LightBoots;
                                } else if(armor.HasKeyword(Skyrim.Keyword.ArmorShield)) {
                                    itemType = ItemType.LightShield;
                                }
                            }
                            if(itemType != null) {
                                var list = tierList[itemType.Value];
                                for(int i = 0; i < list.Count; i++) {
                                    if(armor.Value <= list[i].Item1) {
                                        list[i].Item2.AddItem(itemType, armor.AsLink());
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Best = 4
            // Special/Reward = 3
            // Blacksmith/Town/Normal = 2

            // Light

            LeveledList.LinkList(SKYL.LItemArmorHelmetLightSpecial, LeveledList.FACTOR_RARE, ItemType.LightHelmet, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorCuirassLightSpecial, LeveledList.FACTOR_RARE, ItemType.LightCuirass, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorGauntletsLightSpecial, LeveledList.FACTOR_RARE, ItemType.LightGauntlets, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorBootsLightSpecial, LeveledList.FACTOR_RARE, ItemType.LightBoots, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorShieldLightSpecial, LeveledList.FACTOR_RARE, ItemType.LightShield, LootRQ.NoEnch, LootRQ.Rare);

            LeveledList.LinkList(SKYL.LItemArmorHelmetLightTown, LeveledList.FACTOR_JUNK, ItemType.LightHelmet, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorCuirassLightTown, LeveledList.FACTOR_JUNK, ItemType.LightCuirass, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorGauntletsLightTown, LeveledList.FACTOR_JUNK, ItemType.LightGauntlets, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LitemArmorBootsLightTown, LeveledList.FACTOR_JUNK, ItemType.LightBoots, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorShieldLightTown, LeveledList.FACTOR_JUNK, ItemType.LightShield, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemArmorHelmetLightReward, LeveledList.FACTOR_RARE, ItemType.LightHelmet, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorCuirassLightReward, LeveledList.FACTOR_RARE, ItemType.LightCuirass, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorShieldLightReward, LeveledList.FACTOR_RARE, ItemType.LightShield, LootRQ.NoEnch, LootRQ.Rare);

            LeveledList.LinkList(SKYL.LItemArmorHelmetLight, LeveledList.FACTOR_COMMON, ItemType.LightHelmet, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorCuirassLight, LeveledList.FACTOR_COMMON, ItemType.LightCuirass, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorGauntletsLight, LeveledList.FACTOR_COMMON, ItemType.LightGauntlets, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorBootsLight, LeveledList.FACTOR_COMMON, ItemType.LightBoots, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorShieldLight, LeveledList.FACTOR_COMMON, ItemType.LightShield, LootRQ.NoEnch, LootRQ.Rare);

            LeveledList.LinkList(SKYL.LItemArmorHelmetLightBlacksmith, LeveledList.FACTOR_COMMON, ItemType.LightHelmet, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorCuirassLightBlacksmith, LeveledList.FACTOR_COMMON, ItemType.LightCuirass, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorGauntletsLightBlacksmith, LeveledList.FACTOR_COMMON, ItemType.LightGauntlets, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LitemArmorBootsLightBlacksmith, LeveledList.FACTOR_COMMON, ItemType.LightBoots, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorShieldLightBlacksmith, LeveledList.FACTOR_COMMON, ItemType.LightShield, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemArmorHelmetLightBest, LeveledList.FACTOR_BEST, ItemType.LightHelmet, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorCuirassLightBest, LeveledList.FACTOR_BEST, ItemType.LightCuirass, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorGauntletsLightBest, LeveledList.FACTOR_BEST, ItemType.LightGauntlets, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorBootsLightBest, LeveledList.FACTOR_BEST, ItemType.LightBoots, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorShieldLightBest, LeveledList.FACTOR_BEST, ItemType.LightShield, LootRQ.NoEnch, LootRQ.Rare);

            LeveledList.LinkList(SKYL.LItemEnchArmorLightHelmet, LeveledList.FACTOR_COMMON, ItemType.LightHelmet, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightCuirass, LeveledList.FACTOR_COMMON, ItemType.LightCuirass, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightGauntlets, LeveledList.FACTOR_COMMON, ItemType.LightGauntlets, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightBoots, LeveledList.FACTOR_COMMON, ItemType.LightBoots, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightShield, LeveledList.FACTOR_COMMON, ItemType.LightShield, LootRQ.Ench);

            LeveledList.LinkList(SKYL.LItemEnchArmorLightHelmetNoDragon, LeveledList.FACTOR_COMMON, ItemType.LightHelmet, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightCuirassNoDragon, LeveledList.FACTOR_COMMON, ItemType.LightCuirass, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightGauntletsNoDragon, LeveledList.FACTOR_COMMON, ItemType.LightGauntlets, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightBootsNoDragon, LeveledList.FACTOR_COMMON, ItemType.LightBoots, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightShieldNoDragon, LeveledList.FACTOR_COMMON, ItemType.LightShield, LootRQ.Ench);

            LeveledList.LinkList(SKYL.LItemEnchArmorLightHelmetSpecial, LeveledList.FACTOR_RARE, ItemType.LightHelmet, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightCuirassSpecial, LeveledList.FACTOR_RARE, ItemType.LightCuirass, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightGauntletsSpecial, LeveledList.FACTOR_RARE, ItemType.LightGauntlets, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightBootsSpecial, LeveledList.FACTOR_RARE, ItemType.LightBoots, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchArmorLightShieldSpecial, LeveledList.FACTOR_RARE, ItemType.LightShield, LootRQ.Ench, LootRQ.Rare);

            // DLC2 Light

            LeveledList.LinkList(DBL.DLC2LItemArmorHelmetLight, LeveledList.FACTOR_COMMON, ItemType.LightHelmet, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorCuirassLight, LeveledList.FACTOR_COMMON, ItemType.LightCuirass, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorGauntletsLight, LeveledList.FACTOR_COMMON, ItemType.LightGauntlets, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorBootsLight, LeveledList.FACTOR_COMMON, ItemType.LightBoots, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorShieldLight, LeveledList.FACTOR_COMMON, ItemType.LightShield, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);

            LeveledList.LinkList(DBL.DLC2LItemArmorHelmetLightTown, LeveledList.FACTOR_JUNK, ItemType.LightHelmet, LootRQ.DLC2, LootRQ.NoEnch);
            LeveledList.LinkList(DBL.DLC2LItemArmorCuirassLightTown, LeveledList.FACTOR_JUNK, ItemType.LightCuirass, LootRQ.DLC2, LootRQ.NoEnch);
            LeveledList.LinkList(DBL.DLC2LItemArmorGauntletsLightTown, LeveledList.FACTOR_JUNK, ItemType.LightGauntlets, LootRQ.DLC2, LootRQ.NoEnch);
            LeveledList.LinkList(DBL.DLC2LitemArmorBootsLightTown, LeveledList.FACTOR_JUNK, ItemType.LightBoots, LootRQ.DLC2, LootRQ.NoEnch);
            LeveledList.LinkList(DBL.DLC2LItemArmorShieldLightTown, LeveledList.FACTOR_JUNK, ItemType.LightShield, LootRQ.DLC2, LootRQ.NoEnch);

            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightHelmet, LeveledList.FACTOR_COMMON, ItemType.LightHelmet, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightCuirass, LeveledList.FACTOR_COMMON, ItemType.LightCuirass, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightGauntlets, LeveledList.FACTOR_COMMON, ItemType.LightGauntlets, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightBoots, LeveledList.FACTOR_COMMON, ItemType.LightBoots, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightShield, LeveledList.FACTOR_COMMON, ItemType.LightShield, LootRQ.DLC2, LootRQ.Ench);

            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightHelmetNoDragon, LeveledList.FACTOR_COMMON, ItemType.LightHelmet, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightCuirassNoDragon, LeveledList.FACTOR_COMMON, ItemType.LightCuirass, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightGauntletsNoDragon, LeveledList.FACTOR_COMMON, ItemType.LightGauntlets, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightBootsNoDragon, LeveledList.FACTOR_COMMON, ItemType.LightBoots, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightShieldNoDragon, LeveledList.FACTOR_COMMON, ItemType.LightShield, LootRQ.DLC2, LootRQ.Ench);

            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightHelmetSpecial, LeveledList.FACTOR_RARE, ItemType.LightHelmet, LootRQ.DLC2, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightCuirassSpecial, LeveledList.FACTOR_RARE, ItemType.LightCuirass, LootRQ.DLC2, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightGauntletsSpecial, LeveledList.FACTOR_RARE, ItemType.LightGauntlets, LootRQ.DLC2, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightBootsSpecial, LeveledList.FACTOR_RARE, ItemType.LightBoots, LootRQ.DLC2, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorLightShieldSpecial, LeveledList.FACTOR_RARE, ItemType.LightShield, LootRQ.DLC2, LootRQ.Ench, LootRQ.Rare);


            // Heavy

            LeveledList.LinkList(SKYL.LItemArmorHelmetHeavySpecial, LeveledList.FACTOR_RARE, ItemType.HeavyHelmet, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorCuirassHeavySpecial, LeveledList.FACTOR_RARE, ItemType.HeavyCuirass, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorGauntletsHeavySpecial, LeveledList.FACTOR_RARE, ItemType.HeavyGauntlets, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorBootsHeavySpecial, LeveledList.FACTOR_RARE, ItemType.HeavyBoots, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorShieldHeavySpecial, LeveledList.FACTOR_RARE, ItemType.HeavyShield, LootRQ.NoEnch, LootRQ.Rare);

            LeveledList.LinkList(SKYL.LItemArmorHelmetHeavyTown, LeveledList.FACTOR_JUNK, ItemType.HeavyHelmet, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorCuirassHeavyTown, LeveledList.FACTOR_JUNK, ItemType.HeavyCuirass, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorGauntletsHeavyTown, LeveledList.FACTOR_JUNK, ItemType.HeavyGauntlets, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorBootsHeavyTown, LeveledList.FACTOR_JUNK, ItemType.HeavyBoots, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LitemArmorShieldHeavyTown, LeveledList.FACTOR_JUNK, ItemType.HeavyShield, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemArmorHelmetHeavyReward, LeveledList.FACTOR_RARE, ItemType.HeavyHelmet, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorCuirassHeavyReward, LeveledList.FACTOR_RARE, ItemType.HeavyCuirass, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorShieldHeavyReward, LeveledList.FACTOR_RARE, ItemType.HeavyShield, LootRQ.NoEnch, LootRQ.Rare);

            LeveledList.LinkList(SKYL.LItemArmorHelmetHeavy, LeveledList.FACTOR_COMMON, ItemType.HeavyHelmet, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorCuirassHeavy, LeveledList.FACTOR_COMMON, ItemType.HeavyCuirass, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorGauntletsHeavy, LeveledList.FACTOR_COMMON, ItemType.HeavyGauntlets, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorBootsHeavy, LeveledList.FACTOR_COMMON, ItemType.HeavyBoots, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorShieldHeavy, LeveledList.FACTOR_COMMON, ItemType.HeavyShield, LootRQ.NoEnch, LootRQ.Rare);

            LeveledList.LinkList(SKYL.LItemArmorHelmetHeavyBlacksmith, LeveledList.FACTOR_COMMON, ItemType.HeavyHelmet, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorCuirassHeavyBlacksmith, LeveledList.FACTOR_COMMON, ItemType.HeavyCuirass, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorGauntletsHeavyBlacksmith, LeveledList.FACTOR_COMMON, ItemType.HeavyGauntlets, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LItemArmorBootsHeavyBlacksmith, LeveledList.FACTOR_COMMON, ItemType.HeavyBoots, LootRQ.NoEnch);
            LeveledList.LinkList(SKYL.LitemArmorShieldHeavyBlacksmith, LeveledList.FACTOR_COMMON, ItemType.HeavyShield, LootRQ.NoEnch);

            LeveledList.LinkList(SKYL.LItemArmorHelmetHeavyBest, LeveledList.FACTOR_BEST, ItemType.HeavyHelmet, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorCuirassHeavyBest, LeveledList.FACTOR_BEST, ItemType.HeavyCuirass, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorGauntletsHeavyBest, LeveledList.FACTOR_BEST, ItemType.HeavyGauntlets, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorBootsHeavyBest, LeveledList.FACTOR_BEST, ItemType.HeavyBoots, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemArmorShieldHeavyBest, LeveledList.FACTOR_BEST, ItemType.HeavyShield, LootRQ.NoEnch, LootRQ.Rare);

            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyHelmet, LeveledList.FACTOR_COMMON, ItemType.HeavyHelmet, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyCuirass, LeveledList.FACTOR_COMMON, ItemType.HeavyCuirass, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyGauntlets, LeveledList.FACTOR_COMMON, ItemType.HeavyGauntlets, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyBoots, LeveledList.FACTOR_COMMON, ItemType.HeavyBoots, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyShield, LeveledList.FACTOR_COMMON, ItemType.HeavyShield, LootRQ.Ench);

            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyHelmetNoDragon, LeveledList.FACTOR_COMMON, ItemType.HeavyHelmet, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyCuirassNoDragon, LeveledList.FACTOR_COMMON, ItemType.HeavyCuirass, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyGauntletsNoDragon, LeveledList.FACTOR_COMMON, ItemType.HeavyGauntlets, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyBootsNoDragon, LeveledList.FACTOR_COMMON, ItemType.HeavyBoots, LootRQ.Ench);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyShieldNoDragon, LeveledList.FACTOR_COMMON, ItemType.HeavyShield, LootRQ.Ench);

            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyHelmetSpecial, LeveledList.FACTOR_RARE, ItemType.HeavyHelmet, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyCuirassSpecial, LeveledList.FACTOR_RARE, ItemType.HeavyCuirass, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyGauntletsSpecial, LeveledList.FACTOR_RARE, ItemType.HeavyGauntlets, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyBootsSpecial, LeveledList.FACTOR_RARE, ItemType.HeavyBoots, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(SKYL.LItemEnchArmorHeavyShieldSpecial, LeveledList.FACTOR_RARE, ItemType.HeavyShield, LootRQ.Ench, LootRQ.Rare);

            // DLC2 Heavy

            LeveledList.LinkList(DBL.DLC2LItemArmorHelmetHeavy, LeveledList.FACTOR_COMMON, ItemType.HeavyHelmet, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorCuirassHeavy, LeveledList.FACTOR_COMMON, ItemType.HeavyCuirass, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorGauntletsHeavy, LeveledList.FACTOR_COMMON, ItemType.HeavyGauntlets, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorBootsHeavy, LeveledList.FACTOR_COMMON, ItemType.HeavyBoots, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorShieldHeavy, LeveledList.FACTOR_COMMON, ItemType.HeavyShield, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);

            LeveledList.LinkList(DBL.DLC2LItemArmorHelmetHeavyTown, LeveledList.FACTOR_JUNK, ItemType.HeavyHelmet, LootRQ.DLC2, LootRQ.NoEnch);
            LeveledList.LinkList(DBL.DLC2LItemArmorCuirassHeavyTown, LeveledList.FACTOR_JUNK, ItemType.HeavyCuirass, LootRQ.DLC2, LootRQ.NoEnch);
            LeveledList.LinkList(DBL.DLC2LItemArmorGauntletsHeavyTown, LeveledList.FACTOR_JUNK, ItemType.HeavyGauntlets, LootRQ.DLC2, LootRQ.NoEnch);
            LeveledList.LinkList(DBL.DLC2LItemArmorBootsHeavyTown, LeveledList.FACTOR_JUNK, ItemType.HeavyBoots, LootRQ.DLC2, LootRQ.NoEnch);
            LeveledList.LinkList(DBL.DLC2LitemArmorShieldHeavyTown, LeveledList.FACTOR_JUNK, ItemType.HeavyShield, LootRQ.DLC2, LootRQ.NoEnch);

            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyHelmet, LeveledList.FACTOR_COMMON, ItemType.HeavyHelmet, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyCuirass, LeveledList.FACTOR_COMMON, ItemType.HeavyCuirass, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyGauntlets, LeveledList.FACTOR_COMMON, ItemType.HeavyGauntlets, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyBoots, LeveledList.FACTOR_COMMON, ItemType.HeavyBoots, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyShield, LeveledList.FACTOR_COMMON, ItemType.HeavyShield, LootRQ.DLC2, LootRQ.Ench);

            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyHelmetNoDragon, LeveledList.FACTOR_COMMON, ItemType.HeavyHelmet, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyCuirassNoDragon, LeveledList.FACTOR_COMMON, ItemType.HeavyCuirass, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyGauntletsNoDragon, LeveledList.FACTOR_COMMON, ItemType.HeavyGauntlets, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyBootsNoDragon, LeveledList.FACTOR_COMMON, ItemType.HeavyBoots, LootRQ.DLC2, LootRQ.Ench);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyShieldNoDragon, LeveledList.FACTOR_COMMON, ItemType.HeavyShield, LootRQ.DLC2, LootRQ.Ench);

            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyHelmetSpecial, LeveledList.FACTOR_RARE, ItemType.HeavyHelmet, LootRQ.DLC2, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyCuirassSpecial, LeveledList.FACTOR_RARE, ItemType.HeavyCuirass, LootRQ.DLC2, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyGauntletsSpecial, LeveledList.FACTOR_RARE, ItemType.HeavyGauntlets, LootRQ.DLC2, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyBootsSpecial, LeveledList.FACTOR_RARE, ItemType.HeavyBoots, LootRQ.DLC2, LootRQ.Ench, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemEnchArmorHeavyShieldSpecial, LeveledList.FACTOR_RARE, ItemType.HeavyShield, LootRQ.DLC2, LootRQ.Ench, LootRQ.Rare);

            LeveledList.LinkList(DBL.DLC2LItemArmorHelmetHeavyBest, LeveledList.FACTOR_BEST, ItemType.HeavyHelmet, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorCuirassHeavyBest, LeveledList.FACTOR_BEST, ItemType.HeavyCuirass, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorGauntletsHeavyBest, LeveledList.FACTOR_BEST, ItemType.HeavyGauntlets, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorBootsHeavyBest, LeveledList.FACTOR_BEST, ItemType.HeavyBoots, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);
            LeveledList.LinkList(DBL.DLC2LItemArmorShieldHeavyBest, LeveledList.FACTOR_BEST, ItemType.HeavyShield, LootRQ.DLC2, LootRQ.NoEnch, LootRQ.Rare);


            // Bandit

            LinkedList<ItemMaterial> banditArmor = new();
            banditArmor.AddLast(IRON);
            banditArmor.AddLast(STEEL);
            banditArmor.AddLast(STEELPLATE);
            banditArmor.AddLast(NORDIC);

            LeveledList.LinkList(SKYL.LItemBanditBossCuirass, LeveledList.FACTOR_JUNK, ItemType.HeavyCuirass, banditArmor, LootRQ.NoEnch, LootRQ.DLC2);
            LeveledList.LinkList(SKYL.LItemBanditBossHelmet50, LeveledList.FACTOR_JUNK, ItemType.HeavyHelmet, banditArmor, LootRQ.NoEnch, LootRQ.DLC2);
            LeveledList.LinkList(SKYL.LItemBanditBossBoots, LeveledList.FACTOR_JUNK, ItemType.HeavyBoots, banditArmor, LootRQ.NoEnch, LootRQ.DLC2);
            LeveledList.LinkList(SKYL.LItemBanditBossGauntlets50, LeveledList.FACTOR_JUNK, ItemType.HeavyGauntlets, banditArmor, LootRQ.NoEnch, LootRQ.DLC2);
            LeveledList.LinkList(SKYL.LItemBanditBossShield, LeveledList.FACTOR_JUNK, ItemType.HeavyShield, LootRQ.NoEnch, LootRQ.DLC2);

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

            LeveledList.LinkList(SKYL.LItemThalmorArmorNoHelmetAll, LeveledList.FACTOR_RARE, ItemType.SetNoHelmet, thalmorArmor, LootRQ.NoEnch, LootRQ.Special);
            LeveledList.LinkList(SKYL.LItemThalmorArmorWithHelmetBest, LeveledList.FACTOR_RARE, ItemType.SetWithHelmet, thalmorArmor, LootRQ.NoEnch, LootRQ.Special);

            LeveledList.LinkList(SKYL.LItemThalmorShield, LeveledList.FACTOR_RARE, ItemType.LightShield, thalmorArmor, LootRQ.NoEnch, LootRQ.Special);


            // Draugr / Nordic

            LinkedList<ItemMaterial> draugrArmor = new();
            draugrArmor.AddLast(DRAUGR);
            draugrArmor.AddLast(HIDE);
            draugrArmor.AddLast(LEATHER);
            draugrArmor.AddLast(IRON);
            draugrArmor.AddLast(STEEL);
            draugrArmor.AddLast(STEELPLATE);
            draugrArmor.AddLast(SCALED);
            draugrArmor.AddLast(EBONY);
            draugrArmor.AddLast(DRAGON_HEAVY);
            draugrArmor.AddLast(DRAGON_LIGHT);


            var factorDraugr = LeveledList.FACTOR_COMMON;
            var draugrHelmet = LeveledList.CreateList(ItemType.Helmet, "JLL_DraugrHelmet", factorDraugr, draugrArmor, LootRQ.NoEnch, LootRQ.Special, LootRQ.Rare);
            var draugrCuirass = LeveledList.CreateList(ItemType.Cuirass, "JLL_DraugrCuirass", factorDraugr, draugrArmor, LootRQ.NoEnch, LootRQ.Special, LootRQ.Rare);
            var draugrGauntlets = LeveledList.CreateList(ItemType.Gauntlets, "JLL_DraugrGauntlest", factorDraugr, draugrArmor, LootRQ.NoEnch, LootRQ.Special, LootRQ.Rare);
            var draugrBoots = LeveledList.CreateList(ItemType.Boots, "JLL_DraugrBoots", factorDraugr, draugrArmor, LootRQ.NoEnch, LootRQ.Special, LootRQ.Rare);
            var draugrShield = LeveledList.CreateList(ItemType.Shield, "JLL_DraugrShield", factorDraugr, draugrArmor, LootRQ.NoEnch, LootRQ.Special, LootRQ.Rare);

            var draugrHelmetEnch = LeveledList.CreateList(ItemType.Helmet, "JLL_DraugrHelmetEnch", 2, draugrArmor, LootRQ.Ench, LootRQ.Special, LootRQ.Rare);
            var draugrCuirassEnch = LeveledList.CreateList(ItemType.Cuirass, "JLL_DraugrCuirassEnch", 2, draugrArmor, LootRQ.Ench, LootRQ.Special, LootRQ.Rare);
            var draugrGauntletsEnch = LeveledList.CreateList(ItemType.Gauntlets, "JLL_DraugrGauntlestEnch", 2, draugrArmor, LootRQ.Ench, LootRQ.Special, LootRQ.Rare);
            var draugrBootsEnch = LeveledList.CreateList(ItemType.Boots, "JLL_DraugrBootsEnch", 2, draugrArmor, LootRQ.Ench, LootRQ.Special, LootRQ.Rare);
            var draugrShieldEnch = LeveledList.CreateList(ItemType.Shield, "JLL_DraugrShieldEnch", 2, draugrArmor, LootRQ.Ench, LootRQ.Special, LootRQ.Rare);

            LeveledList.LinkList(SKYL.LootDraugrArmor10, draugrHelmet.ToLink(), draugrCuirass.ToLink(), draugrGauntlets.ToLink(), draugrBoots.ToLink(), draugrShield.ToLink());
            LeveledList.LinkList(SKYL.LootDraugrArmor100, draugrHelmet.ToLink(), draugrCuirass.ToLink(), draugrGauntlets.ToLink(), draugrBoots.ToLink(), draugrShield.ToLink());
            LeveledList.LinkList(SKYL.LootDraugrArmor25, draugrHelmet.ToLink(), draugrCuirass.ToLink(), draugrGauntlets.ToLink(), draugrBoots.ToLink(), draugrShield.ToLink());
            LeveledList.LinkList(SKYL.LootDraugrArmor50, draugrHelmet.ToLink(), draugrCuirass.ToLink(), draugrGauntlets.ToLink(), draugrBoots.ToLink(), draugrShield.ToLink());

            LeveledList.LinkList(SKYL.LootDraugrEnchArmor100, draugrHelmetEnch.ToLink(), draugrCuirassEnch.ToLink(), draugrGauntletsEnch.ToLink(), draugrBootsEnch.ToLink(), draugrShieldEnch.ToLink());
            LeveledList.LinkList(SKYL.LootDraugrEnchArmor25, draugrHelmetEnch.ToLink(), draugrCuirassEnch.ToLink(), draugrGauntletsEnch.ToLink(), draugrBootsEnch.ToLink(), draugrShieldEnch.ToLink());
            LeveledList.LinkList(SKYL.LootDraugrEnchArmor15, draugrHelmetEnch.ToLink(), draugrCuirassEnch.ToLink(), draugrGauntletsEnch.ToLink(), draugrBootsEnch.ToLink(), draugrShieldEnch.ToLink());

        }
    }
}
