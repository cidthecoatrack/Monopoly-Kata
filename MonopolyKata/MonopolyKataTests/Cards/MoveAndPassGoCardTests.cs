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
        private MoveAndPassGoCard card;
        private Player player;
        private BoardHandler boardHandler;
        private Banker banker;

        [TestInitialize]
        public void Setup()
        {
            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();
            player = new Player("name", strategies);
            var players = new[] { player };
            banker = new Banker(players);
            var realEstateHandler = FakeHandlerFactory.CreateEmptyRealEstateHandler(players);
            boardHandler = FakeHandlerFactory.CreateBoardHandlerForFakeBoard(players, realEstateHandler, banker);

            card = new MoveAndPassGoCard("move", BoardConstants.ATLANTIC_AVENUE, boardHandler);
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("move", card.ToString());
        }

        [TestMethod]
        public void Move()
        {
            card.Execute(player);

            Assert.AreEqual(BoardConstants.ATLANTIC_AVENUE, boardHandler.PositionOf[player]);
        }

        [TestMethod]
        public void MoveAndPassGo()
        {
            var playerMoney = banker.GetMoney(player);
            boardHandler.MoveTo(player, BoardConstants.ATLANTIC_AVENUE + 1);
            card.Execute(player);

            Assert.AreEqual(BoardConstants.ATLANTIC_AVENUE, boardHandler.PositionOf[player]);
            Assert.AreEqual(playerMoney + GameConstants.PASS_GO_PAYMENT, banker.GetMoney(player));
        }
    }
}