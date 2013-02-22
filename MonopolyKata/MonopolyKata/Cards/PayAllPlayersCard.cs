using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Handlers;
using Monopoly.Players;

namespace Monopoly.Cards
{
    public class PayAllPlayersCard : ICard
    {
        public Boolean Held { get; private set; }

        private IEnumerable<Player> players;
        private Banker banker;

        public PayAllPlayersCard(IEnumerable<Player> players, Banker banker)
        {
            this.players = players;
            this.banker = banker;
        }

        public void Execute(Player player)
        {
            var count = 0;
            var eligiblePlayers = players.Where(p => !banker.IsBankrupt(p));
            while (count < eligiblePlayers.Count() && !banker.IsBankrupt(player))
                banker.Transact(player, eligiblePlayers.ElementAt(count++), 50);
        }

        public override String ToString()
        {
            return "You Have Been Elected Chairman Of The Board";
        }
    }
}