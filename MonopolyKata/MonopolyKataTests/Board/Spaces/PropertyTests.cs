﻿using System;
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
        private Int32[] houseRents;

        [TestInitialize]
        public void Setup()
        {
            houseRents = new Int32[] { 25, 75, 225, 400, 500 };
            property = new Property("property", PRICE, RENT, GROUPING.DARK_BLUE, HOUSE_COST, houseRents);
            player = new Player("player", new RandomlyMortgage(), new RandomlyPay());
            SetUpGroup();
        }

        private void SetUpGroup()
        {
            otherProperty = new Property("other property", PRICE, RENT, GROUPING.DARK_BLUE, HOUSE_COST, houseRents);
            var group = new[] { property, otherProperty };
            property.SetPropertiesInGroup(group);
            otherProperty.SetPropertiesInGroup(group);
        }

        [TestMethod]
        public void InitializeProperty()
        {
            Assert.AreEqual("property", property.Name);
            Assert.AreEqual(PRICE, property.Price);
            Assert.AreEqual(GROUPING.DARK_BLUE, property.Grouping);
        }

        [TestMethod]
        public void DoNotOwnAllPropertiesInGroup_RentIsNormal()
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
        public void OwnAllPropertiesInGroup_RentIsDoubled()
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
        public void LandOnOthersOwnedProperty_PaysRent()
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
        }

        [TestMethod]
        public void HousesIncreaseRent()
        {
            property.LandOn(player);
            otherProperty.LandOn(player);

            var renter = new Player("renter", new RandomlyMortgage(), new RandomlyPay());

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
            property.LandOn(player);
            otherProperty.LandOn(player);

            var renter = new Player("renter", new RandomlyMortgage(), new RandomlyPay());

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
            property.LandOn(player);
            otherProperty.LandOn(player);

            var renter = new Player("renter", new RandomlyMortgage(), new RandomlyPay());

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
            property.LandOn(player);
            otherProperty.LandOn(player);

            var renter = new Player("renter", new RandomlyMortgage(), new RandomlyPay());

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
            property.LandOn(player);
            otherProperty.LandOn(player);

            var renter = new Player("renter", new RandomlyMortgage(), new RandomlyPay());

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
            property.LandOn(player);
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
            property.LandOn(player);
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
            property.LandOn(player);
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
            property.LandOn(player);
            otherProperty.LandOn(player);

            otherProperty.BuyHouse();
            var previousPlayerMoney = player.Money;
            property.Mortgage();

            Assert.IsFalse(property.Mortgaged);
            Assert.AreEqual(previousPlayerMoney, player.Money);
        }

        //mortgaging sells houses
        //add RealEstateStrategy [buy realestate, buy housesOrHotels]
        //add StrategyCollection
    }
}