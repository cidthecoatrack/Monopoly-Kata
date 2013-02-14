using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Cards;
using Monopoly.Tests.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class MoveToNearestRailroadCardTests
    {
        MoveToNearestRailroadCard card;
        Player player;

        [TestInitialize]
        public void Setup()
        {
            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();
            player = new Player("name", strategies);

            var railroads = new[]
                {
                    new Railroad("RxR"),
                    new Railroad("Railroad"),
                    new Railroad("Train")
                };

            card = new MoveToNearestRailroadCard(railroads);
        }

        [TestMethod]
        public void Initialize()
        {
            Assert.AreEqual("move", card.Name);
        }

        [TestMethod]
        public void Move()
        {
            card.Execute(player);

            Assert.AreEqual(10, player.Position);
        }

        [TestMethod]
        public void MoveAndPassGo()
        {
            var playerMoney = player.Money;
            player.Move(11);
            card.Execute(player);

            Assert.AreEqual(10, player.Position);
            Assert.AreEqual(playerMoney + GameConstants.PASS_GO_PAYMENT, player.Money);
        }
    }
}