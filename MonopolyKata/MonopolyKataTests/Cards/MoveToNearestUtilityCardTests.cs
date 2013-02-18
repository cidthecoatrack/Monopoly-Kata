using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board;
using Monopoly.Board.Spaces;
using Monopoly.Cards;
using Monopoly.Dice;
using Monopoly.Handlers;
using Monopoly.Tests.Board;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class MoveToNearestUtilityCardTests
    {
        private MoveToNearestUtilityCard card;
        private Player player;
        private BoardHandler boardHandler;

        [TestInitialize]
        public void Setup()
        {
            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();
            player = new Player("name", strategies);
            var owner = new Player("owner", strategies);

            var dice = new ControlledDice();
            dice.SetPredeterminedRollValue(10);
            var board = BoardFactory.CreateMonopolyBoard(dice);
            boardHandler = new BoardHandler(new[] { player }, board);

            var utilities = board.OfType<Utility>();
            utilities.First().LandOn(owner);

            card = new MoveToNearestUtilityCard(boardHandler, dice);
        }

        [TestMethod]
        public void Name()
        {
            Assert.AreEqual("Advance to Nearest Utility and pay 10x a die roll", card.ToString());
        }

        [TestMethod]
        public void Move()
        {
            card.Execute(player);
            Assert.AreEqual(BoardConstants.ELECTRIC_COMPANY, boardHandler.PositionOf[player]);

            boardHandler.Move(player, 1);
            card.Execute(player);
            Assert.AreEqual(BoardConstants.WATER_WORKS, boardHandler.PositionOf[player]);
        }

        [TestMethod]
        public void MoveAndPassGo()
        {
            boardHandler.MoveTo(player, BoardConstants.WATER_WORKS + 1);

            var x10Rent = 100;
            var expectedMoney = player.Money + GameConstants.PASS_GO_PAYMENT - x10Rent;

            card.Execute(player);

            Assert.AreEqual(BoardConstants.ELECTRIC_COMPANY, boardHandler.PositionOf[player]);
            Assert.AreEqual(expectedMoney, player.Money);
        }

        [TestMethod]
        public void Pay10xDieRoll()
        {
            var playerMoney = player.Money;
            card.Execute(player);
            Assert.AreEqual(playerMoney - 100, player.Money);
        }
    }
}