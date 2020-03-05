using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ClassLib.helper.RpgGameWithMagic
{
    public class RpgGameMagic
    {
        private readonly RpgBoss _boss;
        private readonly RpgWizard _hero;
        private readonly RpgDifficulty _difficulty;

        public bool IsOver => _hero.IsDead || _boss.IsDead || _hero.Mana < _hero.AllSpells.Min(s => s.ManaCost);

        public bool HeroWon => IsOver && !_hero.IsDead && _boss.IsDead;

        public int BossHP => _boss.HitPoints;
        public int HeroHP => _hero.HitPoints;

        public int ManaSpent { get; private set; }

        public RpgGameMagic(string[] input, RpgDifficulty difficulty)
        {
            _difficulty = difficulty;
            var bossHitPoints = Convert.ToInt32(Regex.Match(input[0], @"Hit Points: (\d+)").Groups[1].Value);
            var bossDamageValue = Convert.ToInt32(Regex.Match(input[1], @"Damage: (\d+)").Groups[1].Value);

            _boss = new RpgBoss(this, bossDamageValue)
            {
                HitPoints = bossHitPoints
            };

            _hero = new RpgWizard(this)
            {
                HitPoints = 50,
                Mana = 500
            };
        }

        public void Play(Type spellType)
        {
            //Hero turn
            HandleTimedSpells();
            if (_difficulty == RpgDifficulty.Hard)
            {
                _hero.HitPoints--;
            }

            if (_boss.IsDead || _hero.IsDead) return;

            var manaCost = _hero.CastSpell(spellType);

            ManaSpent += manaCost;

            if (_boss.IsDead || _hero.IsDead) return;

            // Boss turn
            HandleTimedSpells();

            if (_boss.IsDead || _hero.IsDead) return;

            _boss.Attack();
        }

        private void HandleTimedSpells()
        {
            if (_hero.Poison.IsActive)
            {
                Attack(_hero.Poison.DamagePoints);
            }

            if (_hero.Recharge.IsActive)
            {
                IncreaseMana(_hero.Recharge.NewManaValue);
            }

            foreach (var timedSpell in _hero.TimedSpells)
            {
                if (timedSpell.IsActive)
                {
                    timedSpell.RemainingTurns--;
                }
            }
        }

        public void Attack(int damage)
        {
            _boss.HitPoints -= damage;
        }

        public void GetAttacked(int damage)
        {
            _hero.HitPoints -= Math.Max(damage - _hero.ArmorPoints, 1);
        }

        public void Heal(int hp)
        {
            _hero.HitPoints += hp;
        }

        public void UseMana(int amount)
        {
            _hero.Mana -= amount;
        }

        public void IncreaseMana(int amount)
        {
            _hero.Mana += amount;
        }

        public RpgSaveGame SaveGame()
        {
            return new RpgSaveGame()
            {
                HeroHitPoints = _hero.HitPoints,
                HeroMana = _hero.Mana,
                ManaSpent = ManaSpent,
                BossHitPoints = _boss.HitPoints,
                PoisonSpellTimer = _hero.Poison.RemainingTurns,
                RechargeSpellTimer = _hero.Recharge.RemainingTurns,
                ShieldSpellTimer = _hero.Shield.RemainingTurns
            };
        }

        public void LoadGame(RpgSaveGame saveGame)
        {
            ManaSpent = saveGame.ManaSpent;
            _boss.HitPoints = saveGame.BossHitPoints;
            _hero.HitPoints = saveGame.HeroHitPoints;
            _hero.Mana = saveGame.HeroMana;
            _hero.Poison.RemainingTurns = saveGame.PoisonSpellTimer;
            _hero.Shield.RemainingTurns = saveGame.ShieldSpellTimer;
            _hero.Recharge.RemainingTurns = saveGame.RechargeSpellTimer;
        }

        public override string ToString()
        {
            return $"HHP: {_hero.HitPoints}, MS: {ManaSpent}, BHP: {_boss.HitPoints}";
        }
    }

    public enum RpgDifficulty
    {
        Normal,
        Hard
    }
}