using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ClassLib.helper.RpgGame
{
    public class RpgGameWeapons
    {
        private static readonly Regex ItemRegex = new Regex(@"^(\w+\s?\+?\w+?)\s+(\d+)\s+(\d+)\s+(\d+)");
        private readonly IList<RpgItem> _weapons;
        private readonly IList<RpgItem> _armors;
        private readonly IList<RpgItem> _rings;
        private readonly RpgPlayer _enemy;
        private readonly RpgPlayer _player;

        public int MinCostOfWinning { get; private set; } = int.MaxValue;
        public int MaxCostOfLosing { get; private set; } = 0;

        public RpgGameWeapons(string[] input)
        {
            var wholeInput = string.Join("\n", input);
            var bossArmorValue = Convert.ToInt32(Regex.Match(wholeInput, @"Armor: (\d+)").Groups[1].Value);
            var bossDamageValue = Convert.ToInt32(Regex.Match(wholeInput, @"Damage: (\d+)").Groups[1].Value);
            var bossWeapon = new RpgItem("Boss Weapon", 0, bossDamageValue, 0);
            var bossArmor = new RpgItem("Boss Armor", 0, 0, bossArmorValue);

            _enemy = new RpgPlayer()
            {
                Name = "Boss",
                HitPoints = Convert.ToInt32(Regex.Match(wholeInput, @"Hit Points: (\d+)").Groups[1].Value),
                Weapon = bossWeapon,
                Armor = bossArmor
            };

            _player = new RpgPlayer()
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
                            var boss = (RpgPlayer) _enemy.Clone();
                            var hero = (RpgPlayer) _player.Clone();
                            hero.Armor = a < _armors.Count() ? _armors[a] : null;
                            hero.Weapon = w < _weapons.Count() ? _weapons[w] : null;
                            hero.Ring1 = r1 < _rings.Count() ? _rings[r1] : null;
                            hero.Ring2 = r2 < _rings.Count() ? _rings[r2] : null;

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
        
        private int GetHitPointsAfterFight(RpgPlayer p1, RpgPlayer p2)
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
    }
}