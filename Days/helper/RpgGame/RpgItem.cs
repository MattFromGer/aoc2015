namespace ClassLib.helper.RpgGame
{
    public class RpgItem
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
}