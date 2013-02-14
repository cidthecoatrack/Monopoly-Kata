using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Cards;
using Monopoly.Tests.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class MoveBackThreeCardTests
    {
        MoveBackThreeCard card;
        Player player;

        [TestInitialize]
        public void Setup()
        {
            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();

            player = new Player("name", strategies);

            card = new MoveBackThreeCard();
        }

        [TestMethod]
        public void Initialize()
        {
            Assert.AreEqual("Go Back 3 Spaces", card.Name);
        }

        [TestMethod]
        public void GoBackThreeSpaces()
        {
            var oldPosition = player.Position;
            card.Execute(player);

            Assert.AreEqual(oldPosition - 3, player.Position);
        }
    }
}