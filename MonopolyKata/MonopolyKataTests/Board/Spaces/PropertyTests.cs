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
    }
}