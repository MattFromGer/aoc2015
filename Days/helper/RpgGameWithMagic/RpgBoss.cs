using System;

namespace ClassLib.helper.RpgGameWithMagic
{
    public abstract class RpgPlayer
    {
        protected RpgGameMagic Game { get; }
        protected int DamagePoints { get; set; }
        public int HitPoints { get; set; }
        public bool IsDead => HitPoints < 1;

        protected RpgPlayer(RpgGameMagic game)
        {
            Game = game;
        }
    }

    public class RpgBoss : RpgPlayer
    {
        public RpgBoss(RpgGameMagic game, int damagePoints) : base(game)
        {
            DamagePoints = damagePoints;
        }

        public void Attack()
        {
          Game.GetAttacked(DamagePoints);  
        }

        public override string ToString()
        {
            return $"Boss HP: {HitPoints}";
        }
    }
}