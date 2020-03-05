using System;
using System.Collections.Generic;
using ClassLib.helper.RpgGameWithMagic;
using ClassLib.util;

namespace ClassLib
{
    public class Day22 : AocDay
    {
        private int _minAmount = int.MaxValue;
        private RpgSaveGame _saveGame;
        private static IEnumerable<Type> AllSpellTypes => new List<Type>
        {
            typeof(DamageSpell), typeof(DrainSpell) , typeof(PoisonSpell), typeof(ShieldSpell), typeof(RechargeSpell)
        };
        
        public int GetMinAmountOfManaToWin()
        {
            _minAmount = int.MaxValue;
            var game = new RpgGameMagic(Input, RpgDifficulty.Normal);
            PlayGameRecursive(game);

            return _minAmount;
        }
        
        public int GetMinAmountOfManaToWinHardMode()
        {
            _minAmount = int.MaxValue;
            var game = new RpgGameMagic(Input, RpgDifficulty.Hard);
            PlayGameRecursive(game);

            return _minAmount;
        }

        private RpgGameMagic PlayGameRecursive(RpgGameMagic game)
        {
            foreach (var spellType in AllSpellTypes)
            {
                var saveGame = game.SaveGame();
                game.Play(spellType);

                if (!game.IsOver)
                {
                    game = PlayGameRecursive(game);
                    game.LoadGame(saveGame);
                }
                else
                {
                    if (game.HeroWon)
                    {
                        _minAmount = Math.Min(game.ManaSpent, _minAmount);    
                    }
                      
                    game.LoadGame(saveGame);    
                }
            }

            return game;
        }
    }
}