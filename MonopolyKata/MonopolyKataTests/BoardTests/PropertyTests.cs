using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata.MonopolyBoard;
using MonopolyKata.MonopolyPlayer;
using MonopolyKataTests.MortgageStrategies;

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
            player = new Player("player", new RandomlyMortgage());
            SetUpGroup();
        }

        [TestMethod]
        public void OwnerLandsOnProperty_NothingHappens()
        {
            MakePlayerBuyProperty(player, property);
            var previousPlayer = player;
            property.LandOn(player);
            Assert.IsTrue(player.Equals(previousPlayer));
        }

        private void MakePlayerBuyProperty(Player purchaser, Property toBuy)
        {
            purchaser.ReceiveMoney(toBuy.Price);
            toBuy.LandOn(purchaser);
        }
        
        [TestMethod]
        public void PlayerDoesNotBuyUnaffordableProperty()
        {
            var previousPlayer = player;
            property.LandOn(player);
            Assert.IsTrue(player.Equals(previousPlayer));
            Assert.IsFalse(player.Owns(property));
            Assert.IsFalse(property.Owned);
            Assert.IsNull(property.Owner);
        }

        [TestMethod]
        public void PlayerDoesNotOwnAllPropertiesInGroup_RentIsNormal()
        {
            MakePlayerBuyProperty(player, property);
            Assert.AreEqual(RENT, property.GetRent());
            var otherPlayer = new Player("other name", new RandomlyMortgage());
            MakePlayerBuyProperty(otherPlayer, otherProperty);
            Assert.AreEqual(RENT, property.GetRent());
        }

        private void SetUpGroup()
        {
            otherProperty = new Property("other property", PRICE, RENT, GROUPING.DARK_BLUE);
            var group = new Property[] { property, otherProperty };
            property.SetPropertiesInGroup(group);
            otherProperty.SetPropertiesInGroup(group);
        }

        [TestMethod]
        public void PlayerOwnsAllPropertiesInGroup_RentIsDoubled()
        {
            MakePlayerBuyProperty(player, property);
            MakePlayerBuyProperty(player, otherProperty);
            Assert.AreEqual(RENT * 2, property.GetRent());
            Assert.AreEqual(RENT * 2, otherProperty.GetRent());
        }

        [TestMethod]
        public void PlayerMortgagesPropertyFor90PercentPurchasePrice()
        {
            MakePlayerBuyProperty(player, property);
            property.Mortgage();
            Assert.AreEqual(PRICE * .9, player.Money);
        }

        [TestMethod]
        public void CannotMortgageAlreadyMortgagedProperty()
        {
            MakePlayerBuyProperty(player, property);
            property.Mortgage();
            property.Mortgage();
            Assert.AreEqual(PRICE * .9, player.Money);
        }

        [TestMethod]
        public void PlayerCanPayOffMortgage()
        {
            MakePlayerBuyProperty(player, property);
            property.Mortgage();
            player.ReceiveMoney(PRICE - player.Money);
            property.PayOffMortgage();
            Assert.AreEqual(0, player.Money);
            Assert.IsFalse(property.Mortgaged);
        }

        [TestMethod]
        public void PlayerCantPayOffUnmortgagedProperty()
        {
            MakePlayerBuyProperty(player, property);
            player.ReceiveMoney(PRICE);
            property.PayOffMortgage();
            Assert.AreEqual(PRICE, player.Money);
            Assert.IsFalse(property.Mortgaged);
        }

        [TestMethod]
        public void PlayerLandsOnOthersOwnedProperty_PaysRent()
        {
            MakePlayerBuyProperty(player, property);
            var renter = new Player("renter", new RandomlyMortgage());
            renter.ReceiveMoney(RENT);
            property.LandOn(renter);
            Assert.AreEqual(0, renter.Money);
            Assert.AreEqual(RENT, player.Money);
        }

        [TestMethod]
        public void LosingPlayerDoesNotOwnPropertyAnymore()
        {
            var loser = new Player("loser", new RandomlyMortgage());
            loser.ReceiveMoney(PRICE);
            property.LandOn(loser);
            Assert.IsTrue(property.Owned);
            loser.Pay(loser.Money + 1);
            Assert.IsTrue(loser.LostTheGame);
            Assert.IsFalse(property.Owned);
        }
    }
}