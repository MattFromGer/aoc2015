using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ClassLib.util;

namespace ClassLib
{
    public class Day21 : AocDay
    {
        private static readonly Regex ItemRegex = new Regex(@"^(\w+\s?\+?\w+?)\s+(\d+)\s+(\d+)\s+(\d+)");

        public int GetMinCostOfWinning()
        {
            var game = new RpgGame(Input);
            game.FightWithAllItems();

            return game.MinCostOfWinning;
        }

        public int GetMaxCostOfLosing()
        {
            var game = new RpgGame(Input);
            game.FightWithAllItems();

            return game.MaxCostOfLosing;
        }

        private class RpgGame
        {
            private readonly IList<RpgItem> _weapons;
            private readonly IList<RpgItem> _armors;
            private readonly IList<RpgItem> _rings;
            private readonly Player _enemy;
            private readonly Player _player;

            public int MinCostOfWinning { get; private set; } = int.MaxValue;
            public int MaxCostOfLosing { get; private set; } = 0;

            public RpgGame(string[] input)
            {
                var wholeInput = string.Join("\n", input);
                var bossArmorValue = Convert.ToInt32(Regex.Match(wholeInput, @"Armor: (\d+)").Groups[1].Value);
                var bossDamageValue = Convert.ToInt32(Regex.Match(wholeInput, @"Damage: (\d+)").Groups[1].Value);
                var bossWeapon = new RpgItem("Boss Weapon", 0, bossDamageValue, 0);
                var bossArmor = new RpgItem("Boss Armor", 0, 0, bossArmorValue);
                
                _enemy = new Player()
                {
                    Name = "Boss",
                    HitPoints = Convert.ToInt32(Regex.Match(wholeInput, @"Hit Points: (\d+)").Groups[1].Value),
                    Weapon = bossWeapon,
                    Armor = bossArmor
                };

                _player = new Player()
                {
                    Name = "Hero",
                    HitPoints = 100
                };

                _weapons = ParseRpgItem(input, "Weapons").ToList();
                _armors = ParseRpgItem(input, "Armor:  ").ToList(); // Two white-space characters are required
                _rings = ParseRpgItem(input, "Rings").ToList();
            }

            public void FightWithAllItems()
            {
                for (int w = 0; w < _weapons.Count(); w++)
                {
                    for (var a = 0; a <= _armors.Count; a++)
                    {
                        for (int r1 = 0; r1 < _rings.Count(); r1++)
                        {
                            for (int r2 = r1 + 1; r2 <= _rings.Count(); r2++)
                            {
                                var boss = (Player) _enemy.Clone();
                                var hero = (Player) _player.Clone();
                                hero.Armor = a < _armors.Count() ? _armors[a] : null;
                                hero.Weapon = w < _weapons.Count() ? _weapons[w] : null;
                                hero.Ring1 = r1 < _rings.Count() ? _rings[r1] : null;
                                hero.Ring2 = r2 < _rings.Count() ? _rings[r2]: null;

                                var remainingHitPoints = GetHitPointsAfterFight(hero, boss);
                                if (remainingHitPoints > 0)
                                {
                                    if (hero.ValueInGold < MinCostOfWinning)
                                    {
                                        MinCostOfWinning = hero.ValueInGold;
                                    }
                                }

                                if (remainingHitPoints < 0)
                                {
                                    if (hero.ValueInGold > MaxCostOfLosing)
                                    {
                                        MaxCostOfLosing = hero.ValueInGold;
                                    }
                                }
                            }
                        }
                    }
                }
            }


            private IEnumerable<RpgItem> ParseRpgItem(string[] input, string identifier)
            {
                return input
                    .SkipWhile(x => !x.Contains(identifier))
                    .Skip(1)
                    .TakeWhile(x => !string.IsNullOrEmpty(x))
                    .Select(x => ItemRegex.Match(x))
                    .Select(m =>
                        new RpgItem(
                            m.Groups[1].Value,
                            Convert.ToInt32(m.Groups[2].Value),
                            Convert.ToInt32(m.Groups[3].Value),
                            Convert.ToInt32(m.Groups[4].Value)
                        )
                    );
            }
            
            private int GetHitPointsAfterFight(Player p1, Player p2)
            {
                while (p1.HitPoints > 0 && p2.HitPoints > 0)
                {
                    p1.Attack(p2);
                    if (p2.HitPoints > 0)
                    {
                        p2.Attack(p1);
                    }
                }

                return p1.HitPoints;
            }
        }


        private class RpgItem
        {
            public RpgItem(string name, int cost, int damage, int armor)
            {
                Name = name;
                Cost = cost;
                Damage = damage;
                Armor = armor;
            }

            public string Name { get; }
            public int Cost { get; }
            public int Damage { get; }
            public int Armor { get; }

            public override string ToString()
            {
                return $"Name: {Name}, Cost: {Cost}, Damage: {Damage}, Armor: {Armor}";
            }
        }

        private class Player : ICloneable
        {
            public int HitPoints { get; set; }
            public RpgItem Weapon { get; set; }
            public RpgItem? Armor { get; set; }
            public RpgItem? Ring1 { get; set; }
            public RpgItem? Ring2 { get; set; }
            public string Name { get; set; }
            public int ValueInGold
            {
                get
                {
                    var value = Weapon.Cost;
                    if (Armor != null)
                    {
                        value += Armor.Cost;
                    }

                    if (Ring1 != null)
                    {
                        value += Ring1.Cost;
                    }

                    if (Ring2 != null)
                    {
                        value += Ring2.Cost;
                    }

                    return value;
                }
            }

            private int Damage
            {
                get
                {
                    var damage = Weapon.Damage;
                    if (Ring1 != null)
                    {
                        damage += Ring1.Damage;
                    }

                    if (Ring2 != null)
                    {
                        damage += Ring2.Damage;
                    }

                    return damage;
                }
            }

            private int ArmorPoints
            {
                get
                {
                    var armor = 0;

                    if (Armor != null)
                    {
                        armor += Armor.Armor;    
                    }
                    
                    if (Ring1 != null)
                    {
                        armor += Ring1.Armor;
                    }

                    if (Ring2 != null)
                    {
                        armor += Ring2.Armor;
                    }

                    return armor;
                }
            }

            public void Attack(Player p2)
            {
                p2.HitPoints -= Math.Max(1, Damage - p2.ArmorPoints);
            }

            public override string ToString()
            {
                return $"Name: {Name}, Hitpoints: {HitPoints}";
            }

            public object Clone()
            {
                return MemberwiseClone();
            }
        }
    }
}