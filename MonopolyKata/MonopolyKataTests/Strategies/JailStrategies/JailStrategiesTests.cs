using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board;
using Monopoly.Handlers;
using Monopoly.Strategies;
using Monopoly.Tests.Board;
using Monopoly.Tests.Dice;

namespace Monopoly.Tests.Strategies.JailStrategies
{
    [TestClass]
    public class JailStrategiesTests
    {
        private ControlledDice dice;
        private JailHandler jailHandler;
        private BoardHandler boardHandler;
        private Player player;

        private void SetupPlayerWithStrategy(IJailStrategy strategy)
        {
            dice = new ControlledDice();
            
            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();
            strategies.JailStrategy = strategy;

            player = new Player("name", strategies);

            var players = new[]
                {
                    player,
                    new Player("other player", strategies)
                };

            var emptyBoard = FakeBoardFactory.CreateBoardOfNormalSpaces();
            boardHandler = new BoardHandler(players, emptyBoard);
            jailHandler = new JailHandler(dice, boardHandler);
        }
        
        [TestMethod]
        public void NeverPay()
        {
            SetupPlayerWithStrategy(new NeverPay());

            Assert.IsFalse(player.WillUseGetOutOfJailCard());

            var playerMoney = player.Money;

            dice.RollTwoDice();
            boardHandler.MoveTo(player, BoardConstants.GO_TO_JAIL);
            jailHandler.HandleJail(0, player);
            jailHandler.HandleJail(0, player);

            Assert.AreEqual(playerMoney, player.Money);
            Assert.IsTrue(jailHandler.HasImprisoned(player));
        }

        [TestMethod]
        public void AlwaysPay()
        {
            SetupPlayerWithStrategy(new AlwaysPay());

            Assert.IsTrue(player.WillUseGetOutOfJailCard());

            var playerMoney = player.Money;

            dice.RollTwoDice();
            boardHandler.MoveTo(player, BoardConstants.GO_TO_JAIL);
            jailHandler.HandleJail(0, player);
            jailHandler.HandleJail(0, player);

            Assert.AreEqual(playerMoney - GameConstants.COST_TO_GET_OUT_OF_JAIL, player.Money);
            Assert.IsFalse(jailHandler.HasImprisoned(player));
        }
    }
}