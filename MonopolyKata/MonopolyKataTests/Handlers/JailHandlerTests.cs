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
            jailHandler = new JailHandler(dice);
        }

        [TestMethod]
        public void PlayerRolls3Doubles_GoesToJail()
        {
            jailHandler.HandleJail(3, player);
            Assert.AreEqual(BoardConstants.JAIL_OR_JUST_VISITING, player.Position);
            Assert.IsTrue(jailHandler.HasImprisoned(player));
        }

        [TestMethod]
        public void RollDoublesInJail_GetOut()
        {
            dice.SetPredeterminedDieValues(3, 3, 3, 1);
            jailHandler.Imprison(player);

            dice.RollTwoDice();
            jailHandler.HandleJail(0, player);
            Assert.IsFalse(jailHandler.HasImprisoned(player));
        }

        [TestMethod]
        public void DontRollDoublesInJail_StillInJailAndNoTurn()
        {
            dice.SetPredeterminedDieValues(3, 1);
            jailHandler.Imprison(player);
            jailHandler.HandleJail(0, player);
            var playerMoney = player.Money;

            Assert.AreEqual(BoardConstants.JAIL_OR_JUST_VISITING, player.Position);
            Assert.AreEqual(playerMoney, player.Money);
            Assert.IsTrue(jailHandler.HasImprisoned(player));

            jailHandler.HandleJail(0, player);

            Assert.AreEqual(BoardConstants.JAIL_OR_JUST_VISITING, player.Position);
            Assert.AreEqual(playerMoney, player.Money);
            Assert.IsTrue(jailHandler.HasImprisoned(player));
        }

        [TestMethod]
        public void InJailThreeTurnsAndNoDoubles_Pay50AndGetOut()
        {
            dice.SetPredeterminedDieValues(3, 1);
            jailHandler.Imprison(player);
            var playerMoney = player.Money;
            
            dice.RollTwoDice();
            jailHandler.HandleJail(0, player);
            jailHandler.HandleJail(0, player);
            jailHandler.HandleJail(0, player);

            Assert.AreEqual(playerMoney - GameConstants.COST_TO_GET_OUT_OF_JAIL, player.Money);
            Assert.IsFalse(jailHandler.HasImprisoned(player));
        }
    }
}