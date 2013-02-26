using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Cards;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class HousesAndHotelsCardTests
    {
        private HousesAndHotelsCard card;
        private Player player;
        private Property property;
        private Property otherProperty;
        private Banker banker;

        [TestInitialize]
        public void Setup()
        {
            property = new Property("property", 0, 0, GROUPING.PURPLE, 0, new[] { 0, 0, 0, 0, 0 });
            otherProperty = new Property("property", 0, 0, GROUPING.PURPLE, 0, new[] { 0, 0, 0, 0, 0 });

            var dict = new Dictionary<Int32, OwnableSpace>();
            dict.Add(0, property);
            dict.Add(1, otherProperty);

            player = new Player("name");
            var players = new[] { player };
            banker = new Banker(players);
            var realEstateHandler = new OwnableHandler(dict, banker);
            card = new HousesAndHotelsCard("card", 40, 115, realEstateHandler, banker);

            realEstateHandler.Land(player, 0);
            realEstateHandler.Land(player, 1);
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("card", card.ToString());
        }

        [TestMethod]
        public void Pay()
        {
            BuyHousesAndHotel();

            var playerMoney = banker.GetMoney(player);
            card.Execute(player);

            Assert.AreEqual(playerMoney - 435, banker.GetMoney(player));
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
            var playerMoney = banker.GetMoney(player);
            card.Execute(player);

            Assert.AreEqual(playerMoney, banker.GetMoney(player));
        }
    }
}