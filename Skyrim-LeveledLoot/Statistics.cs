using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeveledLoot {
    class Statistics {
        public int enchantedWeapons = 0;
        public int enchantedArmor = 0;
        public int enchTierSelectionLists = 0;
        public int variantSelectionLists = 0;
        public int levelSelectionLists = 0;
        public int materialSelectionLists = 0;

        public static readonly Statistics instance = new();
    }
}
