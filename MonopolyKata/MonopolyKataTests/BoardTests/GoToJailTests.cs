using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata.MonopolyBoard;
using MonopolyKata.MonopolyPlayer;
using MonopolyKataTests.MortgageStrategies;

namespace MonopolyKataTests.BoardTests
{
    [TestClass]
    public class GoToJailTests
    {
        GoToJail goToJail;
        Player player;

        [TestInitialize]
        public void Setup()
        {
            goToJail = new GoToJail();
            player = new Player("name", new RandomlyMortgage());
        }

        [TestMethod]
        public void PlayerLandsOnGoToJail_PlayerGoesToJustVisiting()
        {
            goToJail.LandOn(player);
            Assert.AreEqual(Board.JAIL_OR_JUST_VISITING, player.Position);
        }

        [TestMethod]
        public void PlayerGoesToJail_MoneyUnaffected()
        {
            player.ReceiveMoney(9266);
            goToJail.LandOn(player);
            Assert.AreEqual(9266, player.Money);
        }

        [TestMethod]
        public void PlayerGoesToJail_PlayerDoesNotReceivePassGoMoney()
        {
            goToJail.LandOn(player);
            Assert.AreEqual(0, player.Money);
        }
    }
}