using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata.MonopolyBoard.Spaces;
using MonopolyKata.MonopolyPlayer;
using MonopolyKataTests.Strategies.JailStrategies;
using MonopolyKataTests.Strategies.MortgageStrategies;

namespace MonopolyKataTests.BoardTests
{
    [TestClass]
    public class PropertyTests
    {
        Property property;
        Property otherProperty;
        Player player;
        private const Int32 PRICE = 50;
        private const Int32 RENT = 5;

        [TestInitialize]
        public void Setup()
        {
            property = new Property("property", PRICE, RENT, GROUPING.DARK_BLUE);
            player = new Player("player", new RandomlyMortgage(), new RandomlyPay());
            SetUpGroup();
        }

        private void SetUpGroup()
        {
            otherProperty = new Property("other property", PRICE, RENT, GROUPING.DARK_BLUE);
            var group = new Property[] { property, otherProperty };
            property.SetPropertiesInGroup(group);
            otherProperty.SetPropertiesInGroup(group);
        }

        [TestMethod]
        public void PlayerDoesNotOwnAllPropertiesInGroup_RentIsNormal()
        {
            MakePlayerBuyProperty(player, property);
            var renter = new Player("renter", new RandomlyMortgage(), new RandomlyPay());
            renter.ReceiveMoney(RENT);
            property.LandOn(renter);
            Assert.AreEqual(0, renter.Money);

            var otherPlayer = new Player("other name", new RandomlyMortgage(), new RandomlyPay());
            MakePlayerBuyProperty(otherPlayer, otherProperty);
            renter.ReceiveMoney(RENT);
            property.LandOn(renter);
            Assert.AreEqual(0, renter.Money);
        }

        private void MakePlayerBuyProperty(Player purchaser, Property toBuy)
        {
            purchaser.ReceiveMoney(toBuy.Price);
            toBuy.LandOn(purchaser);
        }

        [TestMethod]
        public void PlayerOwnsAllPropertiesInGroup_RentIsDoubled()
        {
            MakePlayerBuyProperty(player, property);
            MakePlayerBuyProperty(player, otherProperty);
            var renter = new Player("renter", new RandomlyMortgage(), new RandomlyPay());
            renter.ReceiveMoney(RENT * 2);
            property.LandOn(renter);
            Assert.AreEqual(0, renter.Money);

            renter.ReceiveMoney(RENT * 2);
            otherProperty.LandOn(renter);
            Assert.AreEqual(0, renter.Money);
        }

        [TestMethod]
        public void PlayerLandsOnOthersOwnedProperty_PaysRent()
        {
            MakePlayerBuyProperty(player, property);
            var renter = new Player("renter", new RandomlyMortgage(), new RandomlyPay());
            renter.ReceiveMoney(RENT);
            property.LandOn(renter);
            Assert.AreEqual(0, renter.Money);
            Assert.AreEqual(RENT, player.Money);
        }
    }
}