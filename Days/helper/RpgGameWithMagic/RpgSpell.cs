namespace ClassLib.helper.RpgGameWithMagic
{
    public abstract class RpgSpell
    {
        public int ManaCost { get; }

        protected readonly RpgGameMagic Game;

        protected RpgSpell(RpgGameMagic game, int manaCost)
        {
            Game = game;
            ManaCost = manaCost;
        }

        public abstract void CastSpell();
    }
    
    public sealed class DamageSpell : RpgSpell
    {
        private int Damage { get; }

        public DamageSpell(RpgGameMagic game, int manaCost, int damage) : base(game, manaCost)
        {
            Damage = damage;
        }

        public override void CastSpell()
        {
            Game.Attack(Damage);
            Game.UseMana(ManaCost);
        }
    }
    
    public sealed class DrainSpell : RpgSpell
    {
        private int Damage { get; }
        private int Healing { get; }

        public DrainSpell(RpgGameMagic game, int manaCost, int damage, int healing) : base(game, manaCost)
        {
            Damage = damage;
            Healing = healing;
        }

        public override void CastSpell()
        {
            Game.Attack(Damage);
            Game.Heal(Healing);
            Game.UseMana(ManaCost);
        }
    }
    
    public abstract class TimedSpell : RpgSpell
    {
        private int ActiveForNoOfTurns { get; }
        public int RemainingTurns { get; set; }
        public bool IsActive => RemainingTurns > 0;

        protected TimedSpell(RpgGameMagic game, int manaCost, int noOfActiveTurns) : base(game, manaCost)
        {
            ActiveForNoOfTurns = noOfActiveTurns;
        }
        
        public override void CastSpell()
        {
            Game.UseMana(ManaCost);
            RemainingTurns = ActiveForNoOfTurns;
        }
    }
    
    public sealed class ShieldSpell : TimedSpell
    {
        public int ArmorPoints { get; }

        public ShieldSpell(RpgGameMagic game, int manaCost, int armor, int noOfActiveTurns) : base(game, manaCost, noOfActiveTurns)
        {
            ArmorPoints = armor;
        }
    }
    
    public sealed class PoisonSpell : TimedSpell
    {
        public int DamagePoints { get; }

        public PoisonSpell(RpgGameMagic game, int manaCost, int damage, int activeForNoOfTurns) : base(game, manaCost, activeForNoOfTurns)
        {
            DamagePoints = damage;
        }
    }
    
    public sealed class RechargeSpell : TimedSpell
    {
        public int NewManaValue { get; }

        public RechargeSpell(RpgGameMagic game, int manaCost, int activeForNoOfTurns, int newMana) : base(game, manaCost, activeForNoOfTurns)
        {
            NewManaValue = newMana;
        }
    }
}