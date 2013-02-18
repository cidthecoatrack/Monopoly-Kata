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
        Property property;
        Property otherProperty;

        [TestInitialize]
        public void Setup()
        {
            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();

            player = new Player("name", strategies);
            card = new HousesAndHotelsCard("card", 40, 115);

            SetUpProperties();
        }

        private void SetUpProperties()
        {
            property = new Property("property", 0, 0, GROUPING.PURPLE, 0, new[] { 0, 0, 0, 0, 0 });
            otherProperty = new Property("property", 0, 0, GROUPING.PURPLE, 0, new[] { 0, 0, 0, 0, 0 });

            var group = new[] { property, otherProperty };
            property.SetPropertiesInGroup(group);
            otherProperty.SetPropertiesInGroup(group);

            property.LandOn(player);
            otherProperty.LandOn(player);
        }

        private void BuyHousesAndHotel()
        {
            for (var i = 0; i < 4; i++)
            {
                property.BuyHouse();
                otherProperty.BuyHouse();
            }

            property.BuyHotel();
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

            var playerMoney = player.Money;
            card.Execute(player);

            Assert.AreEqual(playerMoney - 435, player.Money);
        }

        [TestMethod]
        public void NoHousesPays0()
        {
            var playerMoney = player.Money;
            card.Execute(player);

            Assert.AreEqual(playerMoney, player.Money);
        }
    }
}