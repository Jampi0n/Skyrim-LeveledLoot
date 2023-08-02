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
using Synthesis.Bethesda.Commands;

namespace LeveledLoot {
    public class Program {
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

        public static async Task<int> Main(string[] args) {
            return await SynthesisPipeline.Instance
                .AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
                .SetTypicalOpen(GameRelease.SkyrimLE, "LeveledLoot.esp")
                .AddRunnabilityCheck((IRunnabilityState state) => {
                    // Since Bethesda can't name enchanted items properly, the unofficial patches are required
                    // If an enchanted item is named different than the base item, the enchantment name cannot be extracted.
                    // For example:
                    // Dwarven Gauntlets enchanted to Dwarven Bracers of ...
                    // Glass Helmet enchanted to Glass Armor of ...
                    if(state.GameRelease == GameRelease.SkyrimLE) {
                        state.LoadOrder.AssertListsMod("Unofficial Skyrim Legendary Edition Patch.esp");
                    } else {
                        state.LoadOrder.AssertListsMod("Unofficial Skyrim Special Edition Patch.esp");
                    }
                })
                .Run(args);
        }

        public static void RunPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state) {
            //Your code here!
            _state = state;

            LeveledList.InitializePatch();

            //LeveledList.AdjustWeaponEnchList(Dragonborn.LeveledItem.DLC2LItemEnchWeaponAny);
            //LeveledList.AdjustWeaponEnchList(Dragonborn.LeveledItem.DLC2LItemEnchWeaponBow);

            ItemTypeConfig.Config();


            ArmorConfig.Config();
            //WeaponConfig.Config();
            //MiscConfig.Config();
            //SoulGemConfig.Config();

            // Dragon Loot
            LeveledList.LinkList(Dawnguard.LeveledItem.DLC1LootDragonDaedric25, Skyrim.LeveledItem.LItemArmorAllSpeciall, Skyrim.LeveledItem.LItemArmorAllSpeciall, Skyrim.LeveledItem.LItemEnchArmorAllSpecial, Skyrim.LeveledItem.LItemWeaponAllSpecial, Skyrim.LeveledItem.LItemWeaponAllSpecial, Skyrim.LeveledItem.LItemEnchWeaponAnySpecial);

            LeveledList.FinalizePatch();
        }
    }
}
