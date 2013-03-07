using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board;
using Monopoly.Cards;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Board;
using Monopoly.Tests.Handlers;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class MoveBackThreeCardTests
    {
        private ICard moveBackCard;
        private IPlayer player;
        private IBoardHandler boardHandler;

        [TestInitialize]
        public void Setup()
        {
            player = new Player("name");
            var players = new[] { player };
            var realEstateHandler = FakeHandlerFactory.CreateEmptyRealEstateHandler(players);
            var banker = new Banker(players);
            boardHandler = FakeHandlerFactory.CreateBoardHandlerForFakeBoard(players, realEstateHandler, banker);

            moveBackCard = new MoveBackThreeCard(boardHandler);
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("Go Back 3 Spaces", moveBackCard.ToString());
            Assert.IsFalse(moveBackCard.Held);
        }

        [TestMethod]
        public void GoBackThreeSpaces()
        {
            var expectedPosition = (boardHandler.PositionOf[player] - 3 + BoardConstants.BOARD_SIZE) % BoardConstants.BOARD_SIZE;
            moveBackCard.Execute(player);

            Assert.AreEqual(expectedPosition, boardHandler.PositionOf[player]);
        }
    }
}