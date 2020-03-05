using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLib.helper.RpgGameWithMagic
{
    public class RpgWizard : RpgPlayer
    {
        public int Mana { get; set; }
        public int ArmorPoints => Shield.IsActive ? Shield.ArmorPoints : 0;
        
        private readonly DamageSpell _magicMissile;
        private readonly DrainSpell _drain;
        public readonly ShieldSpell Shield;
        public readonly PoisonSpell Poison;
        public readonly RechargeSpell Recharge;
        
        public IEnumerable<RpgSpell> AllSpells => new List<RpgSpell>
        {
            _magicMissile, _drain, Shield, Poison, Recharge
        };
        
        public IEnumerable<TimedSpell> TimedSpells => new List<TimedSpell>
        {
            Shield, Poison, Recharge
        };

        public RpgWizard(RpgGameMagic game) : base(game)
        {
            _magicMissile = new DamageSpell(Game, 53, 4);
            _drain = new DrainSpell(Game, 73, 2, 2);
            Shield = new ShieldSpell(Game, 113, 7, 6);
            Poison = new PoisonSpell(Game, 173, 3, 6);
            Recharge = new RechargeSpell(Game, 229, 5, 101);
        }

        public override string ToString()
        {
            return $"Hero HP: {HitPoints}, Mana: {Mana}";
        }

        public int CastSpell(Type spellType)
        {
            var spell = AllSpells.First(x => spellType == x.GetType());
            
            if (spell.ManaCost > Mana)
            {
                // Hero dies
                HitPoints = 0;    
                return 0;
            }
            
            spell.CastSpell();

            return spell.ManaCost;
        }
    }
}