using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Handlers;
using Monopoly.Board;
using Monopoly.Board.Spaces;
using Monopoly;
using Monopoly.Tests.Strategies.JailStrategies;
using Monopoly.Tests.Strategies.MortgageStrategies;
using Monopoly.Tests.Dice;

namespace Monopoly.Tests.Board.Spaces
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
            jailHandler = new JailHandler(new ControlledDice());
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