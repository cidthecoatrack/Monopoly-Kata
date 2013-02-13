using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Strategies;
using Monopoly.Tests.Board.Spaces;

namespace Monopoly.Tests.Strategies.RealEstateStrategies
{
    [TestClass]
    public class RealEstateStrategiesTests
    {
        private Property property;
        private StrategyCollection strategies;
        private Player player;
        private Player renter;

        [TestInitialize]
        public void Setup()
        {
            property = new Property("property", 1, 0, GROUPING.DARK_BLUE, 1, new[] { 1, 2, 3, 4, 5 });
            property.SetPropertiesInGroup(new[] { property });

            strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();
        }
        
        [TestMethod]
        public void AlwaysBuy()
        {
            strategies.RealEstateStrategy = new AlwaysBuy();
            player = new Player("name", strategies);
            renter = new Player("renter", strategies);

            property.LandOn(player);

            Assert.IsTrue(player.Owns(property));

            player.DevelopProperties();
            var previousRenterMoney = renter.Money;
            property.LandOn(renter);

            Assert.AreEqual(previousRenterMoney - 1, renter.Money);
        }

        [TestMethod]
        public void BuyIfAtLeast500OnHand()
        {
            strategies.RealEstateStrategy = new BuyIfAtLeast500OnHand();
            player = new Player("name", strategies);
            renter = new Player("renter", strategies);

            player.Pay(player.Money - 499);
            property.LandOn(player);

            Assert.IsFalse(player.Owns(property));

            player.ReceiveMoney(1);
            property.LandOn(player);

            Assert.IsTrue(player.Owns(property));

            player.DevelopProperties();
            var previousRenterMoney = renter.Money;
            property.LandOn(renter);

            Assert.AreEqual(previousRenterMoney, renter.Money);

            player.ReceiveMoney(1);
            player.DevelopProperties();
            property.LandOn(renter);

            Assert.AreEqual(previousRenterMoney - 1, renter.Money);
        }

        [TestMethod]
        public void NeverBuy()
        {
            strategies.RealEstateStrategy = new NeverBuy();
            player = new Player("name", strategies);
            renter = new Player("renter", strategies);

            property.LandOn(player);

            Assert.IsTrue(player.Owns(property));

            player.DevelopProperties();
            var previousRenterMoney = renter.Money;
            property.LandOn(renter);

            Assert.AreEqual(previousRenterMoney, renter.Money);
        }
    }
}