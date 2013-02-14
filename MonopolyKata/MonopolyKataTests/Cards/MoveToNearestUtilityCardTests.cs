using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Cards;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class MoveToNearestUtilityCardTests
    {
        MoveToNearestUtilityCard card;
        Player player;

        [TestInitialize]
        public void Setup()
        {
            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();
            player = new Player("name", strategies);

            var dice = new ControlledDice();
            var railroads = new[]
                {
                    new Utility("Electric Company", dice),
                    new Utility("Water Works", dice)
                };

            card = new MoveToNearestUtilityCard(railroads);
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