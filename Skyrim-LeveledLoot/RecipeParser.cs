using System;
using System.Collections.Generic;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.FormKeys.SkyrimLE;
using Mutagen.Bethesda.Plugins;

namespace LeveledLoot {
    class RecipeParser {
        public static void Parse(ItemType[] itemTypes, List<ItemMaterial> materialList, ItemMaterial ultimate) {
            // Add craftable armor to loot
            HashSet<FormKey> craftingPerks = new();
            if (Skyrim.ActorValueInformation.AVSmithing.TryResolve(Program.State.LinkCache, out var avSmithing)) {
                foreach (var perk in avSmithing.PerkTree) {
                    craftingPerks.Add(perk.Perk.FormKey);
                }
            }
            Dictionary<ItemType, List<Tuple<uint, ItemMaterial>>> tierList = new();
            foreach (var itemType in itemTypes) {
                tierList.Add(itemType, new List<Tuple<uint, ItemMaterial>>());
                foreach (var mat in materialList) {
                    var item = mat.GetFirst(itemType);

                    if (item is IFormLink<IArmorGetter> armorLink) {
                        if (armorLink.TryResolve(Program.State.LinkCache, out var armor)) {
                            tierList[itemType].Add(new Tuple<uint, ItemMaterial>(armor.Value, mat));
                        }

                    }


                    if (item is IFormLink<IWeaponGetter> weaponLink) {
                        if (weaponLink.TryResolve(Program.State.LinkCache, out var weapon)) {
                            tierList[itemType].Add(new Tuple<uint, ItemMaterial>(weapon.BasicStats!.Value, mat));
                        }
                    }


                    if (item is IFormLink<IAmmunitionGetter> ammunitionLink) {
                        if (ammunitionLink.TryResolve(Program.State.LinkCache, out var ammo)) {
                            tierList[itemType].Add(new Tuple<uint, ItemMaterial>(ammo.Value, mat));
                        }
                    }

                }
                tierList[itemType].Sort((Tuple<uint, ItemMaterial> a, Tuple<uint, ItemMaterial> b) => {
                    return (int)a.Item1 - (int)b.Item1;
                });
                tierList[itemType].Add(new Tuple<uint, ItemMaterial>(uint.MaxValue, ultimate));
            }

            foreach (var cob in Program.State.LoadOrder.PriorityOrder.ConstructibleObject().WinningOverrides()) {
                bool isValidCob = true;
                var modName = cob.FormKey.ModKey.Name;
                if (modName == "Skyrim" || modName == "Dawnguard" || modName == "HearthFires" || modName == "Dragonborn") {
                    continue;
                }
                if (cob.CreatedObjectCount == 1) {
                    foreach (var cond in cob.Conditions) {
                        if (cond.Data.Function == Condition.Function.HasPerk) {
                            if (cond.Data is HasPerkConditionData hasPerk) {
                                if (!craftingPerks.Contains(hasPerk.Perk.Link.FormKey)) {
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
                if (isValidCob) {
                    if (cob.CreatedObject.TryResolve(Program.State.LinkCache, out var createdItem)) {
                        if (createdItem is IArmorGetter armor) {
                            ItemType? itemType = ItemTypeConfig.GetItemTypeFromKeywords(armor);
                            if (itemType != null && tierList.ContainsKey(itemType.Value)) {
                                var list = tierList[itemType.Value];
                                for (int i = 0; i < list.Count; i++) {
                                    if (armor.Value <= list[i].Item1) {
                                        list[i].Item2.AddItem(itemType, armor.ToLink());
                                        break;
                                    }
                                }
                            }
                        }
                        if (createdItem is IWeaponGetter weapon) {
                            ItemType? itemType = ItemTypeConfig.GetItemTypeFromKeywords(weapon);
                            if (itemType != null && tierList.ContainsKey(itemType.Value)) {
                                var list = tierList[itemType.Value];
                                for (int i = 0; i < list.Count; i++) {
                                    if (weapon.BasicStats!.Value <= list[i].Item1) {
                                        list[i].Item2.AddItem(itemType, weapon.ToLink());
                                        break;
                                    }
                                }
                            }
                        }
                        if (createdItem is IAmmunition ammo) {
                            ItemType? itemType = ItemTypeConfig.GetItemTypeFromKeywords(ammo);
                            if (itemType != null && tierList.ContainsKey(itemType.Value)) {
                                var list = tierList[itemType.Value];
                                for (int i = 0; i < list.Count; i++) {
                                    if (ammo.Value <= list[i].Item1) {
                                        if (itemType == ItemType.Arrow) {
                                            var baseArrow75 = Program.State.PatchMod.LeveledItems.AddNew();
                                            baseArrow75.EditorID = LeveledList.prefix + "BaseArrow75_" + ammo.EditorID;
                                            baseArrow75.ChanceNone = 25;
                                            baseArrow75.Flags = LeveledItem.Flag.CalculateForEachItemInCount;
                                            baseArrow75.Entries = new Noggog.ExtendedList<LeveledItemEntry>() {
                                                new LeveledItemEntry() {
                                                    Data = new LeveledItemEntryData() {
                                                        Count = 1,
                                                        Level = 1,
                                                        Reference = Dawnguard.Ammunition.DLC1DragonboneArrow
                                                    }
                                                }
                                            };
                                            var material = list[i].Item2;
                                            material.Arrow75(ammo.ToLink());
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
