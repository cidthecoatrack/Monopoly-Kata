using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata.Handlers;
using MonopolyKata.MonopolyBoard;
using MonopolyKata.MonopolyBoard.Spaces;
using MonopolyKata.MonopolyPlayer;
using MonopolyKataTests.Strategies.JailStrategies;
using MonopolyKataTests.Strategies.MortgageStrategies;

namespace MonopolyKataTests.Handlers
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
            turnTaker = new TurnHandler(dice, boardFactory.CreateBoardOfNormalSpaces());
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
            var board = boardFactory.CreateMonopolyBoard(dice);
            turnTaker = new TurnHandler(dice, board);
            dice.SetPredeterminedDieValues(3, 3, 3, 1);

            player.ReceiveMoney(9266);
            turnTaker.TakeTurn(player);
            Assert.AreEqual(10, player.Position);
            var realEstate = board[6] as RealEstate;
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
            var board = boardFactory.CreateMonopolyBoard(dice);
            turnTaker = new TurnHandler(dice, board);
            dice.SetPredeterminedDieValues(3, 3, 4, 4, 4, 2);
            player.ReceiveMoney(9266);

            turnTaker.TakeTurn(player);
            Assert.AreEqual(20, player.Position);
            var realEstate = board[6] as RealEstate;
            var otherRealEstate = board[14] as RealEstate;
            Assert.IsTrue(player.Owns(realEstate));
            Assert.IsTrue(player.Owns(otherRealEstate));
        }

        [TestMethod]
        public void IfPlayerLandsOnGoToJail_DoesNotGoAgain()
        {
            var board = boardFactory.CreateMonopolyBoard(dice);
            turnTaker = new TurnHandler(dice, board);
            dice.SetPredeterminedDieValues(BoardConstants.GO_TO_JAIL / 2, BoardConstants.GO_TO_JAIL / 2, 4, 1);
            turnTaker.TakeTurn(player);
            Assert.AreEqual(BoardConstants.JAIL_OR_JUST_VISITING, player.Position);
            Assert.IsTrue(player.IsInJail);
        }

        [TestMethod]
        public void LandOnGoToJail_DoubleCountResets()
        {
            dice.SetPredeterminedDieValues(BoardConstants.GO_TO_JAIL / 2, BoardConstants.GO_TO_JAIL / 2, 4, 4, 3, 3, 2, 1);
            turnTaker.TakeTurn(player);
            Assert.IsFalse(dice.Doubles);
            Assert.AreEqual(0, dice.DoublesCount);
        }
    }
}