using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly;
using Monopoly.Tests.Strategies.JailStrategies;
using Monopoly.Tests.Strategies.MortgageStrategies;

namespace Monopoly.Tests.Board.Spaces
{
    [TestClass]
    public class LuxuryTaxTests
    {
        [TestMethod]
        public void LandOnLuxuryTax_PlayerPays75()
        {
            var luxuryTax = new LuxuryTax();
            var player = new Player("name", new RandomlyMortgage(), new RandomlyPay());

            var playerMoney = player.Money;
            luxuryTax.LandOn(player);

            Assert.AreEqual(playerMoney - LuxuryTax.LUXURY_TAX, player.Money);
        }
    }
}