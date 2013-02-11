using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Handlers;
using Monopoly.Board;
using Monopoly.Board.Spaces;
using Monopoly.Tests.Strategies.JailStrategies;
using Monopoly.Tests.Strategies.MortgageStrategies;
using Monopoly.Tests.Dice;

namespace Monopoly.Tests.Handlers
{
    [TestClass]
    public class TurnHandlerTests
    {
        private TurnHandler turnTaker;
        private Player player;
        private BoardFactory boardFactory;
        private ControlledDice dice;

        [TestInitialize]
        public void Setup()
        {
            dice = new ControlledDice();
            boardFactory = new BoardFactory();
            turnTaker = new TurnHandler(dice, boardFactory.CreateBoardOfNormalSpaces(), new JailHandler(dice));
            player = new Player("Player", new NeverMortgage(), new NeverPay());
        }
        
        [TestMethod]
        public void OnTurn_PlayerMoves()
        {
            dice.SetPredeterminedRollValue(3);
            turnTaker.TakeTurn(player);

            Assert.AreEqual(3, player.Position);
        }

        [TestMethod]
        public void PlayerRollsDoubles_GoesAgain()
        {
            var jailHandler = new JailHandler(dice);
            var board = boardFactory.CreateMonopolyBoard(dice, jailHandler);
            var realEstate = board[6] as RealEstate;
            turnTaker = new TurnHandler(dice, board, jailHandler);
            dice.SetPredeterminedDieValues(3, 3, 3, 1);

            turnTaker.TakeTurn(player);

            Assert.AreEqual(10, player.Position);
            Assert.IsTrue(player.Owns(realEstate));
        }

        [TestMethod]
        public void PlayerDoesNotRollDoubles_OnlyMovesOneRoll()
        {
            dice.SetPredeterminedDieValues(4, 2, 1, 1);
            turnTaker.TakeTurn(player);

            Assert.AreEqual(6, player.Position);
        }

        [TestMethod]
        public void PlayerRolls2Doubles_Moves3Times()
        {
            var jailHandler = new JailHandler(dice);
            var board = boardFactory.CreateMonopolyBoard(dice, jailHandler);
            turnTaker = new TurnHandler(dice, board, jailHandler);
            dice.SetPredeterminedDieValues(3, 3, 4, 4, 4, 2);
            var realEstate = board[6] as RealEstate;
            var otherRealEstate = board[14] as RealEstate;

            turnTaker.TakeTurn(player);

            Assert.AreEqual(20, player.Position);
            Assert.IsTrue(player.Owns(realEstate));
            Assert.IsTrue(player.Owns(otherRealEstate));
        }

        [TestMethod]
        public void IfPlayerLandsOnGoToJail_DoesNotGoAgain()
        {
            var jailHandler = new JailHandler(dice);
            var board = boardFactory.CreateMonopolyBoard(dice, jailHandler);
            turnTaker = new TurnHandler(dice, board, jailHandler);
            dice.SetPredeterminedDieValues(BoardConstants.GO_TO_JAIL / 2, BoardConstants.GO_TO_JAIL / 2, 4, 1);

            turnTaker.TakeTurn(player);

            Assert.AreEqual(BoardConstants.JAIL_OR_JUST_VISITING, player.Position);
            Assert.IsTrue(jailHandler.HasImprisoned(player));
        }
    }
}