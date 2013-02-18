using System;
using System.Collections.Generic;

namespace Monopoly.Cards
{
    public class PayAllPlayersCard : ICard
    {
        public Boolean Held { get; private set; }

        private IEnumerable<Player> players;

        public PayAllPlayersCard(IEnumerable<Player> players)
        {
            this.players = players;
        }

        public void Execute(Player player)
        {
            foreach (var otherPlayer in players)
            {
                player.Pay(50);
                otherPlayer.Collect(50);
            }
        }

        public override String ToString()
        {
            return "You Have Been Elected Chairman Of The Board";
        }
    }
}