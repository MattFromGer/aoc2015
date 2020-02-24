using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ClassLib.helper.RpgGame;
using ClassLib.util;

namespace ClassLib
{
    public class Day21 : AocDay
    {
        public int GetMinCostOfWinning()
        {
            var game = new RpgGameWeapons(Input);
            game.FightWithAllItems();

            return game.MinCostOfWinning;
        }

        public int GetMaxCostOfLosing()
        {
            var game = new RpgGameWeapons(Input);
            game.FightWithAllItems();

            return game.MaxCostOfLosing;
        }
    }
}