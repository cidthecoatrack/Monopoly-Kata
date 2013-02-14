using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
    public class AllPlayersCard : ICard
    {
        public readonly String Name;
        public Boolean Held { get; private set; }

        private readonly Int32 paymentPerPlayer;
        private IEnumerable<Player> players;

        public AllPlayersCard(String name, Int32 paymentPerPlayer, IEnumerable<Player> players)
        {
            Name = name;
            this.paymentPerPlayer = paymentPerPlayer;
            this.players = players;
        }

        public void Execute(Player player)
        {
            foreach (var otherPlayer in players)
            {
                otherPlayer.Pay(paymentPerPlayer);
                player.Collect(paymentPerPlayer);
            }
        }
    }
}