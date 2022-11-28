using System;
using System.Collections.Generic;
using System.Linq;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.Skyrim;
using System.Threading.Tasks;
using Mutagen.Bethesda.FormKeys.SkyrimLE;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;

namespace LeveledLoot
{
    public class Program
    {
        private static IPatcherState<ISkyrimMod, ISkyrimModGetter>? _state = null;
        public static IPatcherState<ISkyrimMod, ISkyrimModGetter> state {
            get {
                if(_state == null) {
                    throw new NullReferenceException();
                } else {
                    return _state;
                }
            }
        }

        public static async Task<int> Main(string[] args)
        {
            return await SynthesisPipeline.Instance
                .AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
                .SetTypicalOpen(GameRelease.SkyrimSE, "LeveledLoot.esp")
                .Run(args);
        }

        public static void RunPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            //Your code here!
            _state = state;

            LeveledList.InitializePatch();

            LeveledList.AdjustWeaponEnchList(Dragonborn.LeveledItem.DLC2LItemEnchWeaponAny);
            LeveledList.AdjustWeaponEnchList(Dragonborn.LeveledItem.DLC2LItemEnchWeaponBow);

            ItemTypeConfig.Config();


            ArmorConfig.Config();
            WeaponConfig.Config();
            MiscConfig.Config();
            SoulGemConfig.Config();

            // Dragon Loot
            LeveledList.LinkList(Dawnguard.LeveledItem.DLC1LootDragonDaedric25, Skyrim.LeveledItem.LItemArmorAllSpeciall, Skyrim.LeveledItem.LItemArmorAllSpeciall, Skyrim.LeveledItem.LItemEnchArmorAllSpecial, Skyrim.LeveledItem.LItemWeaponAllSpecial, Skyrim.LeveledItem.LItemWeaponAllSpecial, Skyrim.LeveledItem.LItemEnchWeaponAnySpecial);

            LeveledList.FinalizePatch();
        }
    }
}
