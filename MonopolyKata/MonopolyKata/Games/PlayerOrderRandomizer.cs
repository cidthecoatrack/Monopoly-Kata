using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Players;

namespace Monopoly.Games
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