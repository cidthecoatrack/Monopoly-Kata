using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board;
using Monopoly.Board.Spaces;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Handlers
{
    [TestClass]
    public class TurnHandlerTests
    {
        private TurnHandler turnHandler;
        private BoardHandler boardHandler;
        private JailHandler jailHandler;
        private Player player;
        private ControlledDice dice;
        private IEnumerable<Player> players;
        private RealEstate realEstate;
        private RealEstate otherRealEstate;

        [TestInitialize]
        public void Setup()
        {
            var strategies = new StrategyCollection();
            strategies.CreateNeverStrategyCollection();
            player = new Player("Player", strategies);

            players = new[]
                {
                    player,
                    new Player("other player", strategies)
                };

            dice = new ControlledDice();
            var board = BoardFactory.CreateMonopolyBoard(dice);
            boardHandler = new BoardHandler(players, board);
            jailHandler = new JailHandler(dice, boardHandler);
            turnHandler = new TurnHandler(dice, boardHandler, jailHandler);

            realEstate = board.ElementAt(6) as RealEstate;
            otherRealEstate = board.ElementAt(14) as RealEstate;
        }
        
        [TestMethod]
        public void OnTurn_PlayerMoves()
        {
            dice.SetPredeterminedRollValue(3);
            turnHandler.TakeTurn(player);

            Assert.AreEqual(3, boardHandler.PositionOf[player]);
        }

        [TestMethod]
        public void RollDoubles_GoesAgain()
        {
            dice.SetPredeterminedDieValues(3, 3, 3, 1);
            turnHandler.TakeTurn(player);

            Assert.AreEqual(10, boardHandler.PositionOf[player]);
            Assert.IsTrue(player.Owns(realEstate));
        }

        [TestMethod]
        public void DoNotRollDoubles_OnlyMovesOneRoll()
        {
            dice.SetPredeterminedDieValues(4, 2, 1, 1);
            turnHandler.TakeTurn(player);

            Assert.AreEqual(6, boardHandler.PositionOf[player]);
        }

        [TestMethod]
        public void RollTwoDoubles_MovesThreeTimes()
        {
            dice.SetPredeterminedDieValues(3, 3, 4, 4, 4, 2);
            turnHandler.TakeTurn(player);

            Assert.AreEqual(20, boardHandler.PositionOf[player]);
            Assert.IsTrue(player.Owns(realEstate));
            Assert.IsTrue(player.Owns(otherRealEstate));
        }

        [TestMethod]
        public void LandOnGoToJail_DoesNotGoAgain()
        {
            dice.SetPredeterminedDieValues(BoardConstants.GO_TO_JAIL / 2, BoardConstants.GO_TO_JAIL / 2, 4, 1);
            turnHandler.TakeTurn(player);

            Assert.AreEqual(BoardConstants.JAIL_OR_JUST_VISITING, boardHandler.PositionOf[player]);
            Assert.IsTrue(jailHandler.HasImprisoned(player));
        }
    }
}