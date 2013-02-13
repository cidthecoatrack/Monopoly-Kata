using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly;
using Monopoly.Handlers;
using Monopoly.Board;
using Monopoly.Tests.Strategies.JailStrategies;
using Monopoly.Tests.Strategies.MortgageStrategies;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Strategies;

namespace Monopoly.Tests.Hendlers
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
            jailHandler = new JailHandler(dice);

            var strategies = new StrategyCollection();
            strategies.CreateNeverStrategyCollection();
            player = new Player("name", strategies);
        }

        [TestMethod]
        public void RollThreeDoubles_GoToJail()
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
            var playerMoney = player.Money; 
            jailHandler.Imprison(player);
            dice.RollTwoDice();
            jailHandler.HandleJail(0, player);

            Assert.AreEqual(BoardConstants.JAIL_OR_JUST_VISITING, player.Position);
            Assert.AreEqual(playerMoney, player.Money);
            Assert.IsTrue(jailHandler.HasImprisoned(player), "first turn");

            dice.RollTwoDice();
            jailHandler.HandleJail(0, player);

            Assert.AreEqual(BoardConstants.JAIL_OR_JUST_VISITING, player.Position);
            Assert.AreEqual(playerMoney, player.Money);
            Assert.IsTrue(jailHandler.HasImprisoned(player), "second turn");
        }

        [TestMethod]
        public void InJailThreeTurnsAndNoDoubles_Pay50AndGetOut()
        {
            var playerMoney = player.Money;
            jailHandler.Imprison(player);
            
            dice.RollTwoDice();
            jailHandler.HandleJail(0, player);
            jailHandler.HandleJail(0, player);
            jailHandler.HandleJail(0, player);

            Assert.AreEqual(playerMoney - GameConstants.COST_TO_GET_OUT_OF_JAIL, player.Money);
            Assert.IsFalse(jailHandler.HasImprisoned(player));
        }
    }
}