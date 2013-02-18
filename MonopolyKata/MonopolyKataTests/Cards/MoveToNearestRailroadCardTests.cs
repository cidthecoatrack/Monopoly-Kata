using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board;
using Monopoly.Board.Spaces;
using Monopoly.Cards;
using Monopoly.Handlers;
using Monopoly.Tests.Board;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class MoveToNearestRailroadCardTests
    {
        private MoveToNearestRailroadCard card;
        private BoardHandler boardHandler;
        private Player player;

        [TestInitialize]
        public void Setup()
        {
            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();
            player = new Player("name", strategies);
            var owner = new Player("owner", strategies);

            var players = new[]
                {
                    player,
                    new Player("other player", strategies)
                };

            var dice = new ControlledDice();
            var board = BoardFactory.CreateMonopolyBoard(dice);
            boardHandler = new BoardHandler(players, board);

            foreach (var rxr in board.OfType<Railroad>())
                rxr.LandOn(owner);

            card = new MoveToNearestRailroadCard(boardHandler);
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("Advance to the nearest Railroad and pay the owner twice the normal rent", card.ToString());
        }

        [TestMethod]
        public void Move()
        {
            card.Execute(player);
            Assert.AreEqual(BoardConstants.READING_RAILROAD, boardHandler.PositionOf[player]);

            boardHandler.Move(player, 1);
            card.Execute(player);
            Assert.AreEqual(BoardConstants.PENNSYLVANIA_RAILROAD, boardHandler.PositionOf[player]);

            boardHandler.Move(player, 1);
            card.Execute(player);
            Assert.AreEqual(BoardConstants.BandO_RAILROAD, boardHandler.PositionOf[player]);

            boardHandler.Move(player, 1);
            card.Execute(player);
            Assert.AreEqual(BoardConstants.SHORT_LINE, boardHandler.PositionOf[player]);
        }

        [TestMethod]
        public void MoveAndPassGo()
        {
            boardHandler.MoveTo(player, BoardConstants.SHORT_LINE + 2);

            var expectedMoney = player.Money + GameConstants.PASS_GO_PAYMENT - 400;
            card.Execute(player);

            Assert.AreEqual(BoardConstants.READING_RAILROAD, boardHandler.PositionOf[player]);
            Assert.AreEqual(expectedMoney, player.Money);
        }

        [TestMethod]
        public void PayTwiceNormalRent()
        {
            var playerMoney = player.Money;
            card.Execute(player);
            Assert.AreEqual(playerMoney - 400, player.Money);
        }
    }
}