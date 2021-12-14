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
        public static IPatcherState<ISkyrimMod, ISkyrimModGetter>? state = null;

        public static async Task<int> Main(string[] args)
        {
            return await SynthesisPipeline.Instance
                .AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
                .SetTypicalOpen(GameRelease.SkyrimLE, "YourPatcher.esp")
                .Run(args);
        }

        public static void RunPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            //Your code here!
            Program.state = state;

            LeveledList.CreateQuest();
            ArmorConfig.Config();
            WeaponConfig.Config();
            MiscConfig.Config();

            // Dragon Loot
            LeveledList.LinkList(Dawnguard.LeveledItem.DLC1LootDragonDaedric25, Skyrim.LeveledItem.LItemArmorAllSpeciall, Skyrim.LeveledItem.LItemArmorAllSpeciall, Skyrim.LeveledItem.LItemEnchArmorAllSpecial, Skyrim.LeveledItem.LItemWeaponAllSpecial, Skyrim.LeveledItem.LItemWeaponAllSpecial, Skyrim.LeveledItem.LItemEnchWeaponAnySpecial);
        }
    }
}
