using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeveledLoot {
    class CustomMath {
        public static int Gcd(int a, int b) {
            if(a == 0) {
                return b;
            }
            return Gcd(b % a, a);
        }

        public static int GcdList(List<int> a) {
            if(a.Count <= 0) {
                return 1;
            }
            if(a.Count == 1) {
                return a.First();
            }
            int first = a.First();
            a.RemoveAt(0);
            return Gcd(first, GcdList(a));
        }
    }
}
