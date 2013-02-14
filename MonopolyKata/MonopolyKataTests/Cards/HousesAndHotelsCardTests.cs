using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Cards;
using Monopoly.Tests.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class HousesAndHotelsCardTests
    {
        HousesAndHotelsCard card;
        Player player;

        [TestInitialize]
        public void Setup()
        {
            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();

            player = new Player("name", strategies);
            card = new HousesAndHotelsCard(40, 115);

            SetUpProperties();
        }

        private void SetUpProperties()
        {
            var property = new Property("property", 0, 0, GROUPING.PURPLE, 0, new[] { 0, 0, 0, 0, 0 });
            var otherProperty = new Property("property", 0, 0, GROUPING.PURPLE, 0, new[] { 0, 0, 0, 0, 0 });

            var group = new[] { property, otherProperty };
            property.SetPropertiesInGroup(group);
            otherProperty.SetPropertiesInGroup(group);

            property.LandOn(player);
            otherProperty.LandOn(player);

            for (var i = 0; i < 4; i++)
            {
                property.BuyHouse();
                otherProperty.BuyHouse();
            }

            property.BuyHotel();
        }

        [TestMethod]
        public void Initialize()
        {
            Assert.AreEqual("You Are Assessed For Street Repairs", card.Name);
        }

        [TestMethod]
        public void Pay()
        {
            var playerMoney = player.Money;

            card.Execute(player);

            Assert.AreEqual(playerMoney - 435, player.Money);
        }
    }
}