using System.Collections.Generic;
using System.Linq;
using MonopolyKata.MonopolyDice;
using MonopolyKata.MonopolyPlayer;

namespace MonopolyKata
{
    public class PlayerOrderRandomizer
    {
        public IEnumerable<Player> Execute(IEnumerable<Player> newPlayers)
        {
            return newPlayers.OrderBy(player => new Dice().RollSingleDie());
        }
    }
}