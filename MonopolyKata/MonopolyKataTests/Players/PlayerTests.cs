using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Players;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Players
{
    [TestClass]
    public class PlayerTests
    {
        Player player;
        
        [TestInitialize]
        public void Setup()
        {
            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();
            player = new Player("Name", strategies);
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("Name", player.ToString());
            Assert.AreEqual(1500, player.Money);
        }

        [TestMethod]
        public void CollectTwoHundred()
        {
            player.Collect(200);
            Assert.AreEqual(1700, player.Money);
        }

        [TestMethod]
        public void PayOneHundred()
        {
            player.Pay(100);
            Assert.AreEqual(1400, player.Money);
        }

        [TestMethod]
        public void PayUnaffordableAmount_Lose()
        {
            player.Pay(player.Money + 1);
            Assert.IsTrue(player.Money < 0);
            Assert.IsTrue(player.LostTheGame);
        }

        [TestMethod]
        public void NotLostTheGameAtStart()
        {
            Assert.IsFalse(player.LostTheGame);
        }

        [TestMethod]
        public void BuyProperty()
        {
            var property = new Property("property", 5, 1, GROUPING.DARK_BLUE, 4, new[] { 25, 75, 225, 400, 500 });
            var playerMoney = player.Money;
            property.LandOn(player);

            Assert.AreEqual(playerMoney - property.Price, player.Money);
            Assert.IsTrue(player.Owns(property));
            Assert.IsTrue(property.Owned);
            Assert.IsTrue(property.Owner.Equals(player));
        }
    }
}