using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly;
using Monopoly.Tests.Strategies.JailStrategies;
using Monopoly.Tests.Strategies.MortgageStrategies;

namespace Monopoly.Tests.Board.Spaces
{
    [TestClass]
    public class PropertyTests
    {
        private Property property;
        private Property otherProperty;
        private Player player;
        private const Int32 PRICE = 50;
        private const Int32 RENT = 5;
        private const Int32 HOUSE_COST = 30;

        [TestInitialize]
        public void Setup()
        {
            property = new Property("property", PRICE, RENT, GROUPING.DARK_BLUE, HOUSE_COST);
            player = new Player("player", new RandomlyMortgage(), new RandomlyPay());
            SetUpGroup();
        }

        private void SetUpGroup()
        {
            otherProperty = new Property("other property", PRICE, RENT, GROUPING.DARK_BLUE, HOUSE_COST);
            var group = new Property[] { property, otherProperty };
            property.SetPropertiesInGroup(group);
            otherProperty.SetPropertiesInGroup(group);
        }

        [TestMethod]
        public void InitializeProperty()
        {
            Assert.AreEqual("property", property.Name);
            Assert.AreEqual(PRICE, property.Price);
            Assert.AreEqual(GROUPING.DARK_BLUE, property.Grouping);
            Assert.AreEqual(HOUSE_COST, property.HousePrice);
        }

        [TestMethod]
        public void PlayerDoesNotOwnAllPropertiesInGroup_RentIsNormal()
        {
            property.LandOn(player);
            var renter = new Player("renter", new RandomlyMortgage(), new RandomlyPay());

            var renterMoney = renter.Money;
            property.LandOn(renter);

            Assert.AreEqual(renterMoney - RENT, renter.Money);

            var otherPlayer = new Player("other name", new RandomlyMortgage(), new RandomlyPay());
            otherProperty.LandOn(otherPlayer);

            renterMoney = renter.Money;
            property.LandOn(renter);

            Assert.AreEqual(renterMoney - RENT, renter.Money);
        }

        [TestMethod]
        public void PlayerOwnsAllPropertiesInGroup_RentIsDoubled()
        {
            property.LandOn(player);
            otherProperty.LandOn(player);

            var renter = new Player("renter", new RandomlyMortgage(), new RandomlyPay());
            var previousMoney = renter.Money;
            property.LandOn(renter);

            Assert.AreEqual(previousMoney - RENT * 2, renter.Money);

            previousMoney = renter.Money;
            otherProperty.LandOn(renter);

            Assert.AreEqual(previousMoney - RENT * 2, renter.Money);
        }

        [TestMethod]
        public void PlayerLandsOnOthersOwnedProperty_PaysRent()
        {
            property.LandOn(player);
            var renter = new Player("renter", new RandomlyMortgage(), new RandomlyPay());

            var renterMoney = renter.Money;
            var playerMoney = player.Money;
            property.LandOn(renter);

            Assert.AreEqual(renterMoney - RENT, renter.Money);
            Assert.AreEqual(playerMoney + RENT, player.Money);
        }

        [TestMethod]
        public void BuyHouse()
        {
            property.LandOn(player);
            otherProperty.LandOn(player);

            var playerMoney = player.Money;
            property.BuyHouse();

            Assert.AreEqual(playerMoney - HOUSE_COST, player.Money);
            Assert.AreEqual(1, property.HouseCount);
        }

        [TestMethod]
        public void CantBuyHouseIfDontOwnMonopoly()
        {
            property.LandOn(player);
            var otherPlayer = new Player("other player", new RandomlyMortgage(), new RandomlyPay());
            otherProperty.LandOn(otherPlayer);

            var playerMoney = player.Money;
            property.BuyHouse();

            Assert.AreEqual(playerMoney, player.Money);
            Assert.AreEqual(0, property.HouseCount);
        }

        [TestMethod]
        public void EvenBuildEnforced()
        {
            property.LandOn(player);
            otherProperty.LandOn(player);

            var playerMoney = player.Money;
            property.BuyHouse();
            property.BuyHouse();

            Assert.AreEqual(playerMoney - HOUSE_COST, player.Money);
            Assert.AreEqual(1, property.HouseCount);
        }

        [TestMethod]
        public void HousesIncreaseRent()
        {
            property.LandOn(player);
            otherProperty.LandOn(player);

            var playerMoney = player.Money;
            property.BuyHouse();

            var renter = new Player("renter", new RandomlyMortgage(), new RandomlyPay());
            var renterMoney = renter.Money;

            property.LandOn(renter);

            Assert.AreEqual(renterMoney - 25, renter.Money);
            Assert.AreEqual(playerMoney + 25, player.Money);
        }
    }
}