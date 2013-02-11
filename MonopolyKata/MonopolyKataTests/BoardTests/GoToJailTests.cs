using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata.Handlers;
using MonopolyKata.MonopolyBoard;
using MonopolyKata.MonopolyBoard.Spaces;
using MonopolyKata.MonopolyPlayer;
using MonopolyKataTests.Strategies.JailStrategies;
using MonopolyKataTests.Strategies.MortgageStrategies;

namespace MonopolyKataTests.BoardTests
{
    [TestClass]
    public class GoToJailTests
    {
        private GoToJail goToJail;
        private JailHandler jailHandler;
        private Player player;

        [TestInitialize]
        public void Setup()
        {
            jailHandler = new JailHandler(new DiceForTesting());
            goToJail = new GoToJail(jailHandler);
            player = new Player("name", new RandomlyMortgage(), new RandomlyPay());
        }

        [TestMethod]
        public void PlayerLandsOnGoToJail_PlayerGoesToJustVisiting()
        {
            goToJail.LandOn(player);
            Assert.AreEqual(BoardConstants.JAIL_OR_JUST_VISITING, player.Position);
        }

        [TestMethod]
        public void PlayerGoesToJail_MoneyUnaffected()
        {
            var playerMoney = player.Money;
            goToJail.LandOn(player);
            Assert.AreEqual(playerMoney, player.Money);
        }

        [TestMethod]
        public void PlayerGoesToJail_PlayerIsInJail()
        {
            goToJail.LandOn(player);
            Assert.IsTrue(jailHandler.HasImprisoned(player));
        }
    }
}