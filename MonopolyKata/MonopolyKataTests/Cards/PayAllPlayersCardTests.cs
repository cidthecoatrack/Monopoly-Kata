using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Cards;
using Monopoly.Tests.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class PayAllPlayersCardTests
    {
        PayAllPlayersCard card;
        Player player;

        [TestInitialize]
        public void Setup()
        {
            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();

            player = new Player("name", strategies);

            var players = new List<Player>();
            for (var i = 0; i < 8; i++)
                players.Add(new Player("player " + i, strategies));

            card = new PayAllPlayersCard(players);
        }

        [TestMethod]
        public void Initialize()
        {
            Assert.AreEqual("You Have Been Elected Chairman Of The Board", card.ToString());
        }

        [TestMethod]
        public void Pay()
        {
            var playerMoney = player.Money;
            card.Execute(player);
            Assert.AreEqual(playerMoney - 400, player.Money);
        }
    }
}