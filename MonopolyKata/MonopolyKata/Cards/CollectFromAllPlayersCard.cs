using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Handlers;
using Monopoly.Players;

namespace Monopoly.Cards
{
    public class CollectFromAllPlayersCard : ICard
    {
        public Boolean Held { get; private set; }

        private IEnumerable<IPlayer> players;
        private IBanker banker;

        public CollectFromAllPlayersCard(IEnumerable<IPlayer> players, IBanker banker)
        {
            this.players = players;
            this.banker = banker;
        }

        public void Execute(IPlayer player)
        {
            foreach (var payer in players.Where(p => !banker.IsBankrupt(p)))
                banker.Transact(payer, player, 50);
        }

        public override String ToString()
        {
            return "Grand Opera Opening: Every Player Pays For Opening Night Seats";
        }
    }
}