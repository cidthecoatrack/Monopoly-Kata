using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Cards;
using Monopoly.Handlers;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class GetOutOfJailFreeCardTests
    {
        GetOutOfJailFreeCard card;

        [TestInitialize]
        public void Setup()
        {
            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();
            var player = new Player("name", strategies);

            var dice = new ControlledDice();
            var jailHandler = new JailHandler(dice);
            card = new GetOutOfJailFreeCard(jailHandler);

            card.Execute(player);
        }

        [TestMethod]
        public void Initialize()
        {
            Assert.AreEqual("Get Out Of Jail, Free", card.Name);
        }

        [TestMethod]
        public void GetOutOfJail()
        {
            Assert.IsTrue(card.Held);
        }
    }
}