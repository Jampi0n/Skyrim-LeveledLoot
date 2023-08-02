using DynamicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        class Node {
            public int index;
            public int currentApproximation;
            public double exact;
            public double errorLow;
            public double errorHigh;

            public Node(int index, double exact) {
                this.index = index;
                this.exact = exact;
            }
            public override string ToString() {
                return "[" + index + "]: " + exact + "/" + currentApproximation + "/" + errorLow + "/" + errorHigh + "___" + (errorHigh - errorLow);
            }
            public void Update() {
                if(exact > 0) {
                    errorLow = Math.Abs(currentApproximation - exact) / exact;
                    errorHigh = Math.Abs(currentApproximation + 1 - exact) / exact;
                } else {
                    if(currentApproximation != 0) {
                        throw new Exception("Error!");
                    }
                    errorLow = 0;
                    errorHigh = 1000000;
                }
            }
        }

        public static List<int> ApproximateProbabilities(List<double> sourceList, int sum, double total=-1) {
            List<Node> approxList = new();

            if(total < 0) {
                total = 0;
                foreach(var x in sourceList) {
                    total += x;
                }
            }

            int currentSum = 0;
            for(int i = 0; i < sourceList.Count; ++i) {
                var exact = sourceList.ElementAt(i) / total * sum;
                var node = new Node(i, exact) {
                    currentApproximation = (int)exact
                };
                node.Update();
                currentSum += (int)exact;
                approxList.Add(node);
            }
            int missing = sum - currentSum;

            while(missing > 0) {
                approxList.Sort((a,b) => {
                    var introducedErrorA = a.errorHigh - a.errorLow;
                    var introducedErrorB = b.errorHigh - b.errorLow;
                    if(introducedErrorA < introducedErrorB) {
                        return -1;
                    } else if(introducedErrorA > introducedErrorB) {
                        return 1;
                    }
                    return 0;
                });
                var node = approxList.First();
                node.currentApproximation++;
                node.Update();
                currentSum++;
                missing--;
            }
            approxList.Sort((a,b) => a.index - b.index);
            return approxList.Select(node => node.currentApproximation).ToList();
        }
    }
}
