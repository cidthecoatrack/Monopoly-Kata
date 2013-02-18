using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board;
using Monopoly.Handlers;
using Monopoly.Tests.Board;
using Monopoly.Tests.Strategies;

namespace Monopoly.Tests.Handlers
{
    [TestClass]
    public class BoardHandlerTests
    {
        private Player player;
        private BoardHandler boardHandler;

        [TestInitialize]
        public void Setup()
        {
            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();
            player = new Player("name", strategies);
            var players = new[] { player };

            boardHandler = new BoardHandler(players, FakeBoardFactory.CreateBoardOfNormalSpaces());
        }
        
        [TestMethod]
        public void InitialPosition()
        {
            Assert.AreEqual(0, boardHandler.PositionOf[player]);
        }

        [TestMethod]
        public void MoveTo()
        {
            boardHandler.MoveTo(player, 2);
            boardHandler.MoveTo(player, 1);
            Assert.AreEqual(1, boardHandler.PositionOf[player]);
        }

        [TestMethod]
        public void MoveAndPassGo()
        {
            var money = player.Money;
            boardHandler.MoveTo(player, 39);
            boardHandler.Move(player, 1);
            Assert.AreEqual(money + GameConstants.PASS_GO_PAYMENT, player.Money);
        }

        [TestMethod]
        public void MoveToAndDontPassGo()
        {
            var money = player.Money;
            boardHandler.MoveTo(player, 2);
            boardHandler.MoveToAndDontPassGo(player, 1);
            Assert.AreEqual(money, player.Money);
        }

        [TestMethod]
        public void MoveToAndPassGo()
        {
            var money = player.Money;
            boardHandler.MoveTo(player, 39);
            boardHandler.MoveTo(player, 0);
            Assert.AreEqual(money + GameConstants.PASS_GO_PAYMENT, player.Money);
        }

        [TestMethod]
        public void Move()
        {
            var money = player.Money;
            boardHandler.Move(player, 2);
            boardHandler.Move(player, 1);
            Assert.AreEqual(3, boardHandler.PositionOf[player]);
        }
    }
}