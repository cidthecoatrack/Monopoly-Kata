﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
    public class CollectFromAllPlayersCard : ICard
    {
        public readonly String Name;
        public Boolean Held { get; private set; }

        private IEnumerable<Player> players;

        public CollectFromAllPlayersCard(IEnumerable<Player> players)
        {
            Name = "Grand Opera Opening: Every Player Pays For Opening Night Seats";
            this.players = players;
        }

        public void Execute(Player player)
        {
            foreach (var otherPlayer in players)
            {
                otherPlayer.Pay(50);
                player.Collect(50);
            }
        }
    }
}