using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Players;
using Monopoly.Tests.Players.Strategies;
using Monopoly.Tests.Players.Strategies.JailStrategies;
using Monopoly.Tests.Players.Strategies.MortgageStrategies;
using Monopoly.Tests.Players.Strategies.RealEstateStrategies;

namespace Monopoly.Tests.Board.Spaces
{
    [TestClass]
    public class LuxuryTaxTests
    {
        [TestMethod]
        public void LandOnLuxuryTax_PlayerPays75()
        {
            var luxuryTax = new LuxuryTax();

            var strategies = new StrategyCollection();
            strategies.JailStrategy = new RandomlyPay();
            strategies.MortgageStrategy = new RandomlyMortgage();
            strategies.RealEstateStrategy = new RandomlyBuy();

            var player = new Player("name", strategies);
            var playerMoney = player.Money;

            luxuryTax.LandOn(player);

            Assert.AreEqual(playerMoney - LuxuryTax.LUXURY_TAX, player.Money);
        }
    }
}