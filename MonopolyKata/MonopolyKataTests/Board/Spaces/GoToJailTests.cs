using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Handlers;
using Monopoly.Board;
using Monopoly.Board.Spaces;
using Monopoly;
using Monopoly.Tests.Strategies.JailStrategies;
using Monopoly.Tests.Strategies.MortgageStrategies;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Strategies;

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

            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();
            player = new Player("name", strategies);
        }

        [TestMethod]
        public void LandOnGoToJail_PlayerGoesToJustVisiting()
        {
            goToJail.LandOn(player);
            Assert.AreEqual(BoardConstants.JAIL_OR_JUST_VISITING, player.Position);
        }

        [TestMethod]
        public void GoToJail_MoneyUnaffected()
        {
            var playerMoney = player.Money;
            goToJail.LandOn(player);
            Assert.AreEqual(playerMoney, player.Money);
        }

        [TestMethod]
        public void GoToJail_PlayerIsInJail()
        {
            goToJail.LandOn(player);
            Assert.IsTrue(jailHandler.HasImprisoned(player));
        }
    }
}