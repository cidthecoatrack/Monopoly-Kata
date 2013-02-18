using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Tests.Strategies;

namespace Monopoly.Tests.Board.Spaces
{
    [TestClass]
    public class PropertyTests
    {
        private Property property;
        private Property otherProperty;
        private Player player;
        private Player renter;
        private Player otherPlayer;
        private const Int32 PRICE = 50;
        private const Int32 RENT = 5;
        private const Int32 HOUSE_COST = 30;
        private Int32[] houseRents;

        [TestInitialize]
        public void Setup()
        {
            houseRents = new Int32[] { 25, 75, 225, 400, 500 };
            property = new Property("property", PRICE, RENT, GROUPING.DARK_BLUE, HOUSE_COST, houseRents);
            SetUpGroup();

            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();

            player = new Player("player", strategies);
            renter = new Player("renter", strategies);
            otherPlayer = new Player("other name", strategies);

            property.LandOn(player);
        }

        private void SetUpGroup()
        {
            otherProperty = new Property("other property", PRICE, RENT, GROUPING.DARK_BLUE, HOUSE_COST, houseRents);
            var group = new[] { property, otherProperty };
            property.SetPropertiesInGroup(group);
            otherProperty.SetPropertiesInGroup(group);
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("property", property.ToString());
            Assert.AreEqual(PRICE, property.Price);
            Assert.AreEqual(GROUPING.DARK_BLUE, property.Grouping);
        }

        [TestMethod]
        public void DoNotOwnAllPropertiesInGroup_RentIsNormal()
        {
            var renterMoney = renter.Money;
            property.LandOn(renter);

            Assert.AreEqual(renterMoney - RENT, renter.Money);

            otherProperty.LandOn(otherPlayer);

            renterMoney = renter.Money;
            property.LandOn(renter);

            Assert.AreEqual(renterMoney - RENT, renter.Money);
        }

        [TestMethod]
        public void OwnAllPropertiesInGroup_RentIsDoubled()
        {
            otherProperty.LandOn(player);

            var previousMoney = renter.Money;
            property.LandOn(renter);

            Assert.AreEqual(previousMoney - RENT * 2, renter.Money);

            previousMoney = renter.Money;
            otherProperty.LandOn(renter);

            Assert.AreEqual(previousMoney - RENT * 2, renter.Money);
        }

        [TestMethod]
        public void LandOnOthersOwnedProperty_PaysRent()
        {
            var renterMoney = renter.Money;
            var playerMoney = player.Money;
            property.LandOn(renter);

            Assert.AreEqual(renterMoney - RENT, renter.Money);
            Assert.AreEqual(playerMoney + RENT, player.Money);
        }

        [TestMethod]
        public void BuyHouse()
        {
            otherProperty.LandOn(player);

            var playerMoney = player.Money;
            property.BuyHouse();

            Assert.AreEqual(playerMoney - HOUSE_COST, player.Money);
        }

        [TestMethod]
        public void CantBuyHouseIfDontOwnMonopoly()
        {
            otherProperty.LandOn(otherPlayer);

            var playerMoney = player.Money;
            property.BuyHouse();

            Assert.AreEqual(playerMoney, player.Money);
        }

        [TestMethod]
        public void EvenBuildEnforced()
        {
            otherProperty.LandOn(player);

            var playerMoney = player.Money;
            property.BuyHouse();
            property.BuyHouse();

            Assert.AreEqual(playerMoney - HOUSE_COST, player.Money);
        }

        [TestMethod]
        public void HousesIncreaseRent()
        {
            otherProperty.LandOn(player);

            BuyHousesAndAssertRent(1, renter);
            BuyHousesAndAssertRent(2, renter);
            BuyHousesAndAssertRent(3, renter);
            BuyHousesAndAssertRent(4, renter);
        }

        private void BuyHousesAndAssertRent(Int32 numberOfHouses, Player renter)
        {
            var rent = houseRents[numberOfHouses - 1];

            property.BuyHouse();
            otherProperty.BuyHouse();

            var previousPlayerMoney = player.Money;
            var previousRenterMoney = renter.Money; 
            
            property.LandOn(renter);

            Assert.AreEqual(previousRenterMoney - rent, renter.Money, String.Format("renter wrong for {0} houses", numberOfHouses));
            Assert.AreEqual(previousPlayerMoney + rent, player.Money, String.Format("renter wrong for {0} houses", numberOfHouses));
        }

        [TestMethod]
        public void CannotBuyMoreThan4Houses()
        {
            otherProperty.LandOn(player);

            for (var i = 4; i > 0; i--)
            {
                property.BuyHouse();
                otherProperty.BuyHouse();
            }

            var previousPlayerMoney = player.Money;
            var previousRenterMoney = renter.Money;

            property.LandOn(renter);

            Assert.AreEqual(previousRenterMoney - houseRents[3], renter.Money);
            Assert.AreEqual(previousPlayerMoney + houseRents[3], player.Money);

            property.BuyHouse();
            otherProperty.BuyHouse();
            previousPlayerMoney = player.Money;
            previousRenterMoney = renter.Money;

            property.LandOn(renter);

            Assert.AreEqual(previousRenterMoney - houseRents[3], renter.Money);
            Assert.AreEqual(previousPlayerMoney + houseRents[3], player.Money);
        }

        [TestMethod]
        public void BuyHotel()
        {
            otherProperty.LandOn(player);

            for (var i = 4; i > 0; i--)
            {
                property.BuyHouse();
                otherProperty.BuyHouse();
            }

            property.BuyHotel();

            var previousPlayerMoney = player.Money;
            var previousRenterMoney = renter.Money;

            property.LandOn(renter);

            Assert.AreEqual(previousRenterMoney - houseRents[4], renter.Money);
            Assert.AreEqual(previousPlayerMoney + houseRents[4], player.Money);
        }

        [TestMethod]
        public void EvenBuildEnforcedOnHotels()
        {
            otherProperty.LandOn(player);

            for (var i = 3; i > 0; i--)
            {
                property.BuyHouse();
                otherProperty.BuyHouse();
            }

            property.BuyHouse();
            property.BuyHotel();

            var previousPlayerMoney = player.Money;
            var previousRenterMoney = renter.Money;

            property.LandOn(renter);

            Assert.AreEqual(previousRenterMoney - houseRents[3], renter.Money);
            Assert.AreEqual(previousPlayerMoney + houseRents[3], player.Money);
        }

        [TestMethod]
        public void CantBuyMoreThanOneHotel()
        {
            otherProperty.LandOn(player);

            for (var i = 4; i > 0; i--)
            {
                property.BuyHouse();
                otherProperty.BuyHouse();
            }

            property.BuyHotel();
            property.BuyHotel();

            var previousPlayerMoney = player.Money;
            var previousRenterMoney = renter.Money;

            property.LandOn(renter);

            Assert.AreEqual(previousRenterMoney - houseRents[4], renter.Money);
            Assert.AreEqual(previousPlayerMoney + houseRents[4], player.Money);
        }

        [TestMethod]
        public void CantBuyHousesIfAnyPropertyInGroupIsMortgaged()
        {
            otherProperty.LandOn(player);

            property.Mortgage();

            var previousPlayerMoney = player.Money;
            property.BuyHouse();

            Assert.AreEqual(previousPlayerMoney, player.Money);

            property.PayOffMortgage();
            otherProperty.Mortgage();

            previousPlayerMoney = player.Money;
            property.BuyHouse();

            Assert.AreEqual(previousPlayerMoney, player.Money);
        }

        [TestMethod]
        public void MortgagingSellsHousesAtHalfPrice()
        {
            otherProperty.LandOn(player);

            property.BuyHouse();
            
            var previousPlayerMoney = player.Money;
            property.Mortgage();

            Assert.IsFalse(property.Mortgaged);
            Assert.AreEqual(previousPlayerMoney + HOUSE_COST / 2, player.Money);
        }

        [TestMethod]
        public void SellingHousesEnforcesEvenBuild()
        {
            otherProperty.LandOn(player);

            for (var i = 0; i < 2; i++)
            {
                property.BuyHouse();
                otherProperty.BuyHouse();
            }

            var previousPlayerMoney = player.Money;
            property.Mortgage();
            property.Mortgage();

            Assert.IsFalse(property.Mortgaged);
            Assert.AreEqual(previousPlayerMoney + HOUSE_COST / 2, player.Money);
        }

        [TestMethod]
        public void CannotMortgagePropertyIfOtherPropertyInGroupHasHouses()
        {
            otherProperty.LandOn(player);

            otherProperty.BuyHouse();
            var previousPlayerMoney = player.Money;
            property.Mortgage();

            Assert.IsFalse(property.Mortgaged);
            Assert.AreEqual(previousPlayerMoney, player.Money);
        }
    }
}