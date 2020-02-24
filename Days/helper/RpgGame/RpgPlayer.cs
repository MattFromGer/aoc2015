using System;

namespace ClassLib.helper.RpgGame
{
    public class RpgPlayer : ICloneable
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

        public void Attack(RpgPlayer p2)
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