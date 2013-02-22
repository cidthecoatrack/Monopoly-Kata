using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Players;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Board.Spaces
{
    [TestClass]
    public class PropertyTests
    {
        private Property property;
        private const Int32 PRICE = 50;
        private const Int32 RENT = 5;
        private const Int32 HOUSE_COST = 30;
        private Int32[] houseRents;

        [TestInitialize]
        public void Setup()
        {
            houseRents = new Int32[] { 25, 75, 225, 400, 500 };
            property = new Property("property", PRICE, RENT, GROUPING.DARK_BLUE, HOUSE_COST, houseRents);
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("property", property.ToString());
            Assert.AreEqual(PRICE, property.Price);
            Assert.AreEqual(GROUPING.DARK_BLUE, property.Grouping);
            Assert.AreEqual(0, property.Houses);
            Assert.IsFalse(property.PartOfMonopoly);
            Assert.AreEqual(HOUSE_COST, property.HousePrice);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TooFewHouseRents()
        {
            property = new Property("name", 0, 0, GROUPING.DARK_BLUE, 0, new[] { 0, 0, 0, 0 });
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TooManyHouseRents()
        {
            property = new Property("name", 0, 0, GROUPING.DARK_BLUE, 0, new[] { 0, 0, 0, 0, 0, 0 });
        }

        [TestMethod]
        public void NotPartOfMonopoly_RentIsNormal()
        {
            Assert.AreEqual(RENT, property.GetRent());
        }

        [TestMethod]
        public void PartOfMonopoly_RentIsDoubled()
        {
            property.PartOfMonopoly = true;
            Assert.AreEqual(RENT * 2, property.GetRent());
        }

        [TestMethod]
        public void BuyHouse()
        {
            property.PartOfMonopoly = true;
            property.BuyHouseOrHotel();
            Assert.AreEqual(1, property.Houses);
        }

        [TestMethod]
        public void CantBuyHouseIfNotPartOfMonopoly()
        {
            property.BuyHouseOrHotel();
            Assert.AreEqual(0, property.Houses);
        }

        [TestMethod]
        public void HousesIncreaseRent()
        {
            property.PartOfMonopoly = true;

            for (var i = 0; i < houseRents.Length; i++)
            {
                property.BuyHouseOrHotel();
                Assert.AreEqual(houseRents[i], property.GetRent());
            }
        }

        [TestMethod]
        public void CannotBuyMoreThan5HousesAndHotels()
        {
            property.PartOfMonopoly = true;

            for(var i = 0; i < 6; i++)
                property.BuyHouseOrHotel();

            Assert.AreEqual(5, property.Houses);
        }

        [TestMethod]
        public void SellHouse()
        {
            property.PartOfMonopoly = true;
            property.BuyHouseOrHotel();
            property.SellHouseOrHotel();
            Assert.AreEqual(0, property.Houses);
        }

        [TestMethod]
        public void NoNegativeHouses()
        {
            property.SellHouseOrHotel();
            Assert.AreEqual(0, property.Houses);
        }
    }
}