using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Cards;
using Monopoly.Tests.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class CollectFromAllPlayersCardTests
    {
        CollectFromAllPlayersCard card;
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

            card = new CollectFromAllPlayersCard(players);
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("Grand Opera Opening: Every Player Pays For Opening Night Seats", card.ToString());
        }

        [TestMethod]
        public void Pay()
        {
            var playerMoney = player.Money;
            card.Execute(player);
            Assert.AreEqual(playerMoney + 400, player.Money);
        }
    }
}