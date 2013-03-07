using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Cards;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Players.Strategies;
using Monopoly.Tests.Players.Strategies.OwnableStrategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class HousesAndHotelsCardTests
    {
        private ICard housesAndHotelsCard;
        private IPlayer player;
        private Property property;
        private Property otherProperty;
        private IBanker banker;

        [TestInitialize]
        public void Setup()
        {
            property = new Property("property", 0, 0, GROUPING.PURPLE, 0, new[] { 0, 0, 0, 0, 0 });
            otherProperty = new Property("property", 0, 0, GROUPING.PURPLE, 0, new[] { 0, 0, 0, 0, 0 });

            var dict = new Dictionary<Int32, OwnableSpace>();
            dict.Add(0, property);
            dict.Add(1, otherProperty);

            player = new Player("name");
            player.OwnableStrategy = new AlwaysBuyOrMortgage();
            var players = new[] { player };
            banker = new Banker(players);
            var realEstateHandler = new OwnableHandler(dict, banker);
            housesAndHotelsCard = new HousesAndHotelsCard("card", 40, 115, realEstateHandler, banker);

            realEstateHandler.Land(player, 0);
            realEstateHandler.Land(player, 1);
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("card", housesAndHotelsCard.ToString());
            Assert.IsFalse(housesAndHotelsCard.Held);
        }

        [TestMethod]
        public void Pay()
        {
            BuyHousesAndHotel();

            var playerMoney = banker.Money[player];
            housesAndHotelsCard.Execute(player);

            Assert.AreEqual(playerMoney - 435, banker.Money[player]);
        }

        private void BuyHousesAndHotel()
        {
            for (var i = 0; i < 4; i++)
            {
                property.BuyHouseOrHotel();
                otherProperty.BuyHouseOrHotel();
            }

            property.BuyHouseOrHotel();
        }

        [TestMethod]
        public void NoHousesPays0()
        {
            var playerMoney = banker.Money[player];
            housesAndHotelsCard.Execute(player);

            Assert.AreEqual(playerMoney, banker.Money[player]);
        }
    }
}