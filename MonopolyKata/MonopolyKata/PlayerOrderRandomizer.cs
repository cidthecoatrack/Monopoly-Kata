using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Dice;

namespace Monopoly
{
    public class PlayerOrderRandomizer
    {
        Random random = new Random();

        public IEnumerable<Player> Execute(IEnumerable<Player> newPlayers)
        {
            return newPlayers.OrderBy(player => random.Next());
        }
    }
}