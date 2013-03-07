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

        private IEnumerable<IPlayer> players;
        private IBanker banker;

        public PayAllPlayersCard(IEnumerable<IPlayer> players, IBanker banker)
        {
            this.players = players;
            this.banker = banker;
        }

        public void Execute(IPlayer player)
        {
            var ineligiblePlayers = banker.GetBankrupcies(players);
            players = players.Except(ineligiblePlayers);

            var count = 0;
            while (count < players.Count() && !banker.IsBankrupt(player))
                banker.Transact(player, players.ElementAt(count++), 50);
        }

        public override String ToString()
        {
            return "You Have Been Elected Chairman Of The Board";
        }
    }
}