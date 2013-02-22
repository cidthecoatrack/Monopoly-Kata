using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board;
using Monopoly.Board.Spaces;
using Monopoly.Games;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Board;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Handlers
{
    [TestClass]
    public class BoardHandlerTests
    {
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
            var realEstateHandler = new RealEstateHandler(new Dictionary<Int32, RealEstate>(), players, banker);
            
            var normalSpaces = new Dictionary<Int32, ISpace>();
            for(var i = 0; i < BoardConstants.BOARD_SIZE; i++)
                normalSpaces.Add(i, new NormalSpace("space " + i));

            var spaceHandler = new SpaceHandler(normalSpaces);

            boardHandler = new BoardHandler(players, realEstateHandler, spaceHandler, banker);
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
        public void PassGo()
        {
            var money = banker.GetMoney(player);
            boardHandler.MoveTo(player, 39);
            boardHandler.Move(player, 1);
            Assert.AreEqual(money + GameConstants.PASS_GO_PAYMENT, banker.GetMoney(player));
        }

        [TestMethod]
        public void MoveToAndDontPassGo()
        {
            var money = banker.GetMoney(player);
            boardHandler.MoveTo(player, 2);
            boardHandler.MoveToAndDontPassGo(player, 1);
            Assert.AreEqual(money, banker.GetMoney(player));
        }

        [TestMethod]
        public void MoveToAndPassGo()
        {
            var money = banker.GetMoney(player);
            boardHandler.MoveTo(player, 39);
            boardHandler.MoveTo(player, 0);
            Assert.AreEqual(money + GameConstants.PASS_GO_PAYMENT, banker.GetMoney(player));
        }

        [TestMethod]
        public void Move()
        {
            boardHandler.Move(player, 2);
            boardHandler.Move(player, 1);
            Assert.AreEqual(3, boardHandler.PositionOf[player]);
        }
    }
}