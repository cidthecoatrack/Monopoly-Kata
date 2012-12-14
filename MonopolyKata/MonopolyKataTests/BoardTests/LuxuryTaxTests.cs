using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata.MonopolyBoard;
using MonopolyKata.MonopolyPlayer;
using MonopolyKataTests.MortgageStrategies;

namespace MonopolyKataTests.BoardTests
{
    [TestClass]
    public class LuxuryTaxTests
    {
        [TestMethod]
        public void PlayerLandsOnLuxuryTax_PlayerPays75()
        {
            var luxuryTax = new LuxuryTax();
            var player = new Player("name", new RandomlyMortgage());
            player.ReceiveMoney(75);
            luxuryTax.LandOn(player);
            Assert.AreEqual(0, player.Money);
        }
    }
}