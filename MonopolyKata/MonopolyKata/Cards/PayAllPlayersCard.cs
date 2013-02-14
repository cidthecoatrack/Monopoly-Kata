using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
    public class PayAllPlayersCard : ICard
    {
        public Boolean Held { get; private set; }
        public readonly String Name;

        private IEnumerable<Player> players;

        public PayAllPlayersCard(IEnumerable<Player> players)
        {
            Name = "You Have Been Elected Chairman Of The Board";
            this.players = players;
        }

        public void Execute(Player player)
        {
            throw new NotImplementedException();
        }
    }
}