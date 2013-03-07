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
        private IJailHandler jailHandler;
        private IBoardHandler boardHandler;
        private IPlayer player;
        private IBanker banker;

        [TestInitialize]
        public void SetupPlayerWithStrategy()
        {
            dice = new ControlledDice();
            
            player = new Player("name");

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

            Assert.IsFalse(player.JailStrategy.UseCard());

            var playerMoney = banker.Money[player];

            dice.RollTwoDice();
            boardHandler.MoveTo(player, BoardConstants.GO_TO_JAIL);
            jailHandler.HandleJail(0, player);
            jailHandler.HandleJail(0, player);

            Assert.AreEqual(playerMoney, banker.Money[player]);
            Assert.IsTrue(jailHandler.HasImprisoned(player));
        }

        [TestMethod]
        public void AlwaysPay()
        {
            player.JailStrategy = new AlwaysPay();

            Assert.IsTrue(player.JailStrategy.UseCard());

            var playerMoney = banker.Money[player];

            dice.RollTwoDice();
            boardHandler.MoveTo(player, BoardConstants.GO_TO_JAIL);
            jailHandler.HandleJail(0, player);
            jailHandler.HandleJail(0, player);

            Assert.AreEqual(playerMoney - GameConstants.COST_TO_GET_OUT_OF_JAIL, banker.Money[player]);
            Assert.IsFalse(jailHandler.HasImprisoned(player));
        }
    }
}