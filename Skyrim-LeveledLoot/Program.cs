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
using System.Runtime;
using DynamicData;
using System.Collections;

namespace LeveledLoot {
    public class Program {
        public static Lazy<Settings> _settings = null!;
        public static Settings Settings => _settings.Value;

        private static IPatcherState<ISkyrimMod, ISkyrimModGetter>? _state = null;
        public static IPatcherState<ISkyrimMod, ISkyrimModGetter> State {
            get {
                if (_state == null) {
                    throw new NullReferenceException();
                } else {
                    return _state;
                }
            }
        }

        public static async Task<int> Main(string[] args) {
            return await SynthesisPipeline.Instance
                .AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
                .SetTypicalOpen(GameRelease.SkyrimSE, "LeveledLoot.esp")
                .SetAutogeneratedSettings(
                    nickname: "Settings",
                    path: "settings.json",
                    out _settings)
                .AddRunnabilityCheck((IRunnabilityState state) => {
                    // Since Bethesda can't name enchanted items properly, the unofficial patches are required
                    // If an enchanted item is named different than the base item, the enchantment name cannot be extracted.
                    // For example:
                    // Dwarven Gauntlets enchanted to Dwarven Bracers of ...
                    // Glass Helmet enchanted to Glass Armor of ...
                    if (state.GameRelease == GameRelease.SkyrimLE) {
                        state.LoadOrder.AssertListsMod("Unofficial Skyrim Legendary Edition Patch.esp");
                    } else {
                        state.LoadOrder.AssertListsMod("Unofficial Skyrim Special Edition Patch.esp");
                    }
                })
                .Run(args);
        }

        public static bool TestTest() {
            var formKey = new FormKey(State.PatchMod.ModKey, 0x800);
            return State.PatchMod.LeveledItems.ContainsKey(formKey);
        }

        public static void RunPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state) {
            //Your code here!
            _state = state;

            LeveledList.InitializePatch();

            if (Settings.apparel.enabled) {
                Enchanter.Reset();
                Enchanter.numTiers = Program.Settings.apparel.maxTiersPerMaterial;
                ArmorConfig.Run();
            }
            if (Settings.weapons.enabled) {
                Enchanter.Reset();
                Enchanter.numTiers = Program.Settings.weapons.maxTiersPerMaterial;
                WeaponConfig.Run();
            }
            MiscConfig.Run();
            if (Settings.misc.soulGems) {
                SoulGemConfig.Run();
            }

            // Dragon Loot
            LeveledList.LinkList(Dawnguard.LeveledItem.DLC1LootDragonDaedric25, Skyrim.LeveledItem.LItemArmorAllSpeciall, Skyrim.LeveledItem.LItemArmorAllSpeciall, Skyrim.LeveledItem.LItemEnchArmorAllSpecial, Skyrim.LeveledItem.LItemWeaponAllSpecial, Skyrim.LeveledItem.LItemWeaponAllSpecial, Skyrim.LeveledItem.LItemEnchWeaponAnySpecial);

            LeveledList.FinalizePatch();
        }
    }

    public abstract class LootConfig<T> where T : new() {
        public static void Run() {
            _ = new T();
        }
    }
}
