using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata.MonopolyBoard.Spaces;
using MonopolyKata.MonopolyPlayer;
using MonopolyKataTests.Strategies.JailStrategies;
using MonopolyKataTests.Strategies.MortgageStrategies;

namespace MonopolyKataTests.BoardTests
{
    [TestClass]
    public class LuxuryTaxTests
    {
        [TestMethod]
        public void PlayerLandsOnLuxuryTax_PlayerPays75()
        {
            var luxuryTax = new LuxuryTax();
            var player = new Player("name", new RandomlyMortgage(), new RandomlyPay());

            var playerMoney = player.Money;
            luxuryTax.LandOn(player);

            Assert.AreEqual(playerMoney - LuxuryTax.LUXURY_TAX, player.Money);
        }
    }
}