using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board;
using Monopoly.Cards;
using Monopoly.Games;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Board;
using Monopoly.Tests.Handlers;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class MoveAndPassGoCardTests
    {
        private ICard passGoCard;
        private IPlayer player;
        private IBoardHandler boardHandler;
        private IBanker banker;

        [TestInitialize]
        public void Setup()
        {
            player = new Player("name");
            var players = new[] { player };
            banker = new Banker(players);
            var realEstateHandler = FakeHandlerFactory.CreateEmptyRealEstateHandler(players);
            boardHandler = FakeHandlerFactory.CreateBoardHandlerForFakeBoard(players, realEstateHandler, banker);

            passGoCard = new MoveAndPassGoCard("move", BoardConstants.ATLANTIC_AVENUE, boardHandler);
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("move", passGoCard.ToString());
            Assert.IsFalse(passGoCard.Held);
        }

        [TestMethod]
        public void Move()
        {
            passGoCard.Execute(player);

            Assert.AreEqual(BoardConstants.ATLANTIC_AVENUE, boardHandler.PositionOf[player]);
        }

        [TestMethod]
        public void MoveAndPassGo()
        {
            var playerMoney = banker.Money[player];
            boardHandler.MoveTo(player, BoardConstants.ATLANTIC_AVENUE + 1);
            passGoCard.Execute(player);

            Assert.AreEqual(BoardConstants.ATLANTIC_AVENUE, boardHandler.PositionOf[player]);
            Assert.AreEqual(playerMoney + GameConstants.PASS_GO_PAYMENT, banker.Money[player]);
        }
    }
}