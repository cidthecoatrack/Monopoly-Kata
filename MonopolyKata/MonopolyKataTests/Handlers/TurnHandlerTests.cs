using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board;
using Monopoly.Board.Spaces;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Board.Spaces;
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
        private Dictionary<Int32, UnownableSpace> landableSpaces;
        private LandableSpace space6;
        private LandableSpace space10;

        [TestInitialize]
        public void Setup()
        {
            player = new Player("Player");
            var players = new[] { player };

            dice = new ControlledDice();
            var realEstateHandler = FakeHandlerFactory.CreateEmptyRealEstateHandler(players);
            
            landableSpaces = new Dictionary<Int32, UnownableSpace>();

            for (var i = 0; i < BoardConstants.BOARD_SIZE; i++)
                landableSpaces.Add(i, new LandableSpace());

            space6 = landableSpaces[6] as LandableSpace;
            space10 = landableSpaces[10] as LandableSpace;

            var spaceHandler = new UnownableHandler(landableSpaces);
            var banker = new Banker(players);
            boardHandler = new BoardHandler(players, realEstateHandler, spaceHandler, banker);
            jailHandler = new JailHandler(dice, boardHandler, banker);
            turnHandler = new TurnHandler(dice, boardHandler, jailHandler, realEstateHandler, banker);
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
            Assert.IsTrue(space6.LandedOn);
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
            dice.SetPredeterminedDieValues(3, 3, 2, 2, 4, 2);
            turnHandler.TakeTurn(player);

            Assert.AreEqual(16, boardHandler.PositionOf[player]);
            Assert.IsTrue(space6.LandedOn);
            Assert.IsTrue(space10.LandedOn);
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