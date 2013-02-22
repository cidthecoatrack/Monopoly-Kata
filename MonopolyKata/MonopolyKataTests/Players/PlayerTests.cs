using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Players;
using Monopoly.Tests.Players.Strategies;
using Monopoly.Tests.Players.Strategies.JailStrategies;
using Monopoly.Tests.Players.Strategies.MortgageStrategies;
using Monopoly.Tests.Players.Strategies.RealEstateStrategies;

namespace Monopoly.Tests.Players
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void Constructor()
        {
            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();
            var player = new Player("Name", strategies);

            Assert.AreEqual("Name", player.ToString());
            Assert.IsInstanceOfType(player.JailStrategy, typeof(RandomlyPay));
            Assert.IsInstanceOfType(player.MortgageStrategy, typeof(RandomlyMortgage));
            Assert.IsInstanceOfType(player.RealEstateStrategy, typeof(RandomlyBuy));
        }
    }
}