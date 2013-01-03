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
            player.ReceiveMoney(LuxuryTax.LUXURY_TAX);
            luxuryTax.LandOn(player);
            Assert.AreEqual(0, player.Money);
        }
    }
}