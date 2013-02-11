using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Dice;

namespace Monopoly
{
    public class PlayerOrderRandomizer
    {
        public IEnumerable<Player> Execute(IEnumerable<Player> newPlayers, IDice dice)
        {
            return newPlayers.OrderBy(player => RollToGoFirst(dice));
        }

        private Int32 RollToGoFirst(IDice dice)
        {
            dice.RollTwoDice();
            return dice.Value;
        }
    }
}