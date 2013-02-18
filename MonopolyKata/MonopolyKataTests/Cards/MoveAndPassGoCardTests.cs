using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board;
using Monopoly.Board.Spaces;
using Monopoly.Cards;
using Monopoly.Handlers;
using Monopoly.Tests.Board;
using Monopoly.Tests.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class MoveAndPassGoCardTests
    {
        private MoveAndPassGoCard card;
        private Player player;
        private BoardHandler boardHandler;

        [TestInitialize]
        public void Setup()
        {
            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();
            player = new Player("name", strategies);
            var players = new[] { player };
            var board = FakeBoardFactory.CreateBoardOfNormalSpaces();
            boardHandler = new BoardHandler(players, board);

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
            var playerMoney = player.Money;
            boardHandler.MoveTo(player, BoardConstants.ATLANTIC_AVENUE + 1);
            card.Execute(player);

            Assert.AreEqual(BoardConstants.ATLANTIC_AVENUE, boardHandler.PositionOf[player]);
            Assert.AreEqual(playerMoney + GameConstants.PASS_GO_PAYMENT, player.Money);
        }
    }
}