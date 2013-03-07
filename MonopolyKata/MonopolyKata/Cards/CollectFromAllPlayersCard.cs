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

        private IEnumerable<Player> players;
        private Banker banker;

        public CollectFromAllPlayersCard(IEnumerable<Player> players, Banker banker)
        {
            this.players = players;
            this.banker = banker;
        }

        public void Execute(Player player)
        {
            foreach (var payer in players.Where(p => !banker.IsBankrupt(p)))
                banker.Transact(payer, player, 50, ToString());
        }

        public override String ToString()
        {
            return "Grand Opera Opening: Every Player Pays For Opening Night Seats";
        }
    }
}