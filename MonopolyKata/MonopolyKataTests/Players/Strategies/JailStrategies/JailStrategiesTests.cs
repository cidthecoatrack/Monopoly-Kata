using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board;
using Monopoly.Games;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Players.Strategies;
using Monopoly.Tests.Board;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Handlers;

namespace Monopoly.Tests.Players.Strategies.JailStrategies
{
    [TestClass]
    public class JailStrategiesTests
    {
        private ControlledDice dice;
        private JailHandler jailHandler;
        private BoardHandler boardHandler;
        private Player player;
        private Banker banker;

        [TestInitialize]
        public void SetupPlayerWithStrategy()
        {
            dice = new ControlledDice();
            
            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();
            player = new Player("name", strategies);

            var players = new[] { player };
            banker = new Banker(players);
            var realEstateHandler = FakeHandlerFactory.CreateEmptyRealEstateHandler(players);
            boardHandler = FakeHandlerFactory.CreateBoardHandlerForFakeBoard(players, realEstateHandler, banker);
            jailHandler = new JailHandler(dice, boardHandler, banker);
        }
        
        [TestMethod]
        public void NeverPay()
        {
            player.JailStrategy = new NeverPay();

            Assert.IsFalse(player.WillUseGetOutOfJailCard());

            var playerMoney = banker.GetMoney(player);

            dice.RollTwoDice();
            boardHandler.MoveTo(player, BoardConstants.GO_TO_JAIL);
            jailHandler.HandleJail(0, player);
            jailHandler.HandleJail(0, player);

            Assert.AreEqual(playerMoney, banker.GetMoney(player));
            Assert.IsTrue(jailHandler.HasImprisoned(player));
        }

        [TestMethod]
        public void AlwaysPay()
        {
            player.JailStrategy = new AlwaysPay();

            Assert.IsTrue(player.WillUseGetOutOfJailCard());

            var playerMoney = banker.GetMoney(player);

            dice.RollTwoDice();
            boardHandler.MoveTo(player, BoardConstants.GO_TO_JAIL);
            jailHandler.HandleJail(0, player);
            jailHandler.HandleJail(0, player);

            Assert.AreEqual(playerMoney - GameConstants.COST_TO_GET_OUT_OF_JAIL, banker.GetMoney(player));
            Assert.IsFalse(jailHandler.HasImprisoned(player));
        }
    }
}