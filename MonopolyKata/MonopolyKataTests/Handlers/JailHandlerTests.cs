using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;
using MonopolyKata.Handlers;
using MonopolyKata.MonopolyBoard;
using MonopolyKata.MonopolyPlayer;
using MonopolyKataTests.Strategies.JailStrategies;
using MonopolyKataTests.Strategies.MortgageStrategies;

namespace MonopolyKataTests.Hendlers
{
    [TestClass]
    public class JailHandlerTests
    {
        ControlledDice dice;
        JailHandler jailHandler;
        Player player;

        [TestInitialize]
        public void Setup()
        {
            dice = new ControlledDice();
            player = new Player("name", new NeverMortgage(), new NeverPay());
            jailHandler = new JailHandler(dice, player);
        }

        [TestMethod]
        public void PlayerRolls3Doubles_GoesToJail()
        {
            dice.SetPredeterminedDieValues(3, 3, 4, 4, 1, 1);
            for (var i = 3; i > 0; i--)
                dice.RollTwoDice();

            jailHandler.HandleJail();
            Assert.AreEqual(BoardConstants.JAIL_OR_JUST_VISITING, player.Position);
            Assert.IsTrue(player.IsInJail);
        }

        [TestMethod]
        public void GoToJailForDoubles_DoubleCountResets()
        {
            dice.SetPredeterminedDieValues(3, 3, 4, 4, 1, 1);
            for (var i = 3; i > 0; i--)
                dice.RollTwoDice();

            jailHandler.HandleJail();
            Assert.IsFalse(dice.Doubles);
            Assert.AreEqual(0, dice.DoublesCount);
        }

        [TestMethod]
        public void RollDoublesInJail_GetOut()
        {
            dice.SetPredeterminedDieValues(3, 3, 3, 1);
            player.GoToJail();

            dice.RollTwoDice();
            jailHandler.HandleJail();
            Assert.IsFalse(player.IsInJail);
        }

        [TestMethod]
        public void DontRollDoublesInJail_StillInJailAndNoTurn()
        {
            dice.SetPredeterminedDieValues(3, 1);
            player.GoToJail();
            jailHandler.HandleJail();
            Assert.AreEqual(BoardConstants.JAIL_OR_JUST_VISITING, player.Position);
            Assert.IsTrue(player.IsInJail);
            jailHandler.HandleJail();
            Assert.AreEqual(BoardConstants.JAIL_OR_JUST_VISITING, player.Position);
            Assert.IsTrue(player.IsInJail);
        }

        [TestMethod]
        public void InJailThreeTurnsAndNoDoubles_Pay50AndGetOut()
        {
            dice.SetPredeterminedDieValues(3, 1);
            player.ReceiveMoney(GameConstants.COST_TO_GET_OUT_OF_JAIL);
            player.GoToJail();
            
            dice.RollTwoDice();
            player.PreTurnChecks();
            jailHandler.HandleJail();
            Assert.AreEqual(BoardConstants.JAIL_OR_JUST_VISITING, player.Position);
            Assert.AreEqual(GameConstants.COST_TO_GET_OUT_OF_JAIL, player.Money);
            Assert.IsTrue(player.IsInJail);

            dice.RollTwoDice();
            player.PreTurnChecks();
            jailHandler.HandleJail();
            Assert.AreEqual(BoardConstants.JAIL_OR_JUST_VISITING, player.Position);
            Assert.AreEqual(GameConstants.COST_TO_GET_OUT_OF_JAIL, player.Money);
            Assert.IsTrue(player.IsInJail);

            dice.RollTwoDice();
            player.PreTurnChecks();
            jailHandler.HandleJail();
            Assert.AreEqual(0, player.Money);
            Assert.IsFalse(player.IsInJail);
        }
    }
}