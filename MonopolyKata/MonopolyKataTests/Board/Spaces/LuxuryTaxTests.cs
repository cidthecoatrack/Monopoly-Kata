using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Players.Strategies;
using Monopoly.Tests.Players.Strategies.JailStrategies;
using Monopoly.Tests.Players.Strategies.OwnableStrategies;

namespace Monopoly.Tests.Board.Spaces
{
    [TestClass]
    public class LuxuryTaxTests
    {
        [TestMethod]
        public void LuxuryTaxTest()
        {
            var player = new Player("name");

            var banker = new Banker(new[] { player });
            var luxuryTax = new LuxuryTax(banker);

            var playerMoney = banker.GetMoney(player);
            luxuryTax.LandOn(player);

            Assert.AreEqual(playerMoney - LuxuryTax.LUXURY_TAX, banker.GetMoney(player));
        }
    }
}