using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly;
using Monopoly.Handlers;
using Monopoly.Board;
using Monopoly.Tests.Strategies.JailStrategies;
using Monopoly.Tests.Strategies.MortgageStrategies;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Strategies;
using Monopoly.Cards;
using Monopoly.Tests.Board;

namespace Monopoly.Tests.Hendlers
{
    [TestClass]
    public class JailHandlerTests
    {
        private ControlledDice dice;
        private JailHandler jailHandler;
        private BoardHandler boardHandler;
        private Player player;

        [TestInitialize]
        public void Setup()
        {
            var strategies = new StrategyCollection();
            strategies.CreateNeverStrategyCollection();
            player = new Player("name", strategies);

            dice = new ControlledDice();
            var board = FakeBoardFactory.CreateBoardOfNormalSpaces();
            var players = new[]
                {
                    player,
                    new Player("Player 1", strategies),
                    new Player("Player 2", strategies),
                    new Player("Player 3", strategies),
                    new Player("Player 4", strategies)
                };
            boardHandler = new BoardHandler(players, board);
            jailHandler = new JailHandler(dice, boardHandler);

        }

        [TestMethod]
        public void RollThreeDoubles_GoToJail()
        {
            jailHandler.HandleJail(3, player);

            Assert.AreEqual(BoardConstants.JAIL_OR_JUST_VISITING, boardHandler.PositionOf[player]);
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

            Assert.AreEqual(BoardConstants.JAIL_OR_JUST_VISITING, boardHandler.PositionOf[player]);
            Assert.AreEqual(playerMoney, player.Money);
            Assert.IsTrue(jailHandler.HasImprisoned(player), "first turn");

            dice.RollTwoDice();
            jailHandler.HandleJail(0, player);

            Assert.AreEqual(BoardConstants.JAIL_OR_JUST_VISITING, boardHandler.PositionOf[player]);
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

        [TestMethod]
        public void PayToGetOut()
        {
            var strategies = new StrategyCollection();
            strategies.CreateNeverStrategyCollection();
            strategies.JailStrategy = new AlwaysPay();
            player = new Player("name", strategies);

            var playerMoney = player.Money;
            jailHandler.Imprison(player);

            dice.RollTwoDice();
            jailHandler.HandleJail(0, player);

            Assert.AreEqual(playerMoney - GameConstants.COST_TO_GET_OUT_OF_JAIL, player.Money);
            Assert.IsFalse(jailHandler.HasImprisoned(player));
        }

        [TestMethod]
        public void UseGetOutOfJailCard()
        {
            var strategies = new StrategyCollection();
            strategies.CreateNeverStrategyCollection();
            strategies.JailStrategy = new AlwaysPay();
            player = new Player("name", strategies);

            var card = new GetOutOfJailFreeCard(jailHandler);
            card.Execute(player);

            var playerMoney = player.Money;
            jailHandler.Imprison(player);

            dice.RollTwoDice();
            jailHandler.HandleJail(0, player);

            Assert.AreEqual(playerMoney, player.Money);
            Assert.IsFalse(jailHandler.HasImprisoned(player));
        }
    }
}