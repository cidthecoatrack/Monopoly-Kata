using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board;
using Monopoly.Board.Spaces;
using Monopoly.Cards;
using Monopoly.Games;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Handlers;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class MoveToNearestUtilityCardTests
    {
        private MoveToNearestUtilityCard card;
        private Player player;
        private Player owner;
        private BoardHandler boardHandler;
        private const Int32 ROLL = 10;
        private const Int32 UTILITY_PRICE = 150;
        private Banker banker;

        [TestInitialize]
        public void Setup()
        {
            player = new Player("name");
            owner = new Player("owner");
            var players = new[] { player, owner };

            banker = new Banker(players);
            var dice = new ControlledDice();
            dice.SetPredeterminedRollValue(ROLL);
            var realEstate = BoardFactory.CreateRealEstate(dice);
            var realEstateHandler = new OwnableHandler(realEstate, banker);
            boardHandler = FakeHandlerFactory.CreateBoardHandlerForFakeBoard(players, realEstateHandler, banker);

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

            var expectedMoney = banker.GetMoney(player) + GameConstants.PASS_GO_PAYMENT - UTILITY_PRICE;
            card.Execute(player);

            Assert.AreEqual(BoardConstants.ELECTRIC_COMPANY, boardHandler.PositionOf[player]);
            Assert.AreEqual(expectedMoney, banker.GetMoney(player));
        }

        [TestMethod]
        public void Buy()
        {
            var ownerMoney = banker.GetMoney(owner);
            card.Execute(owner);

            Assert.AreEqual(ownerMoney - UTILITY_PRICE, banker.GetMoney(owner));
        }

        [TestMethod]
        public void Pay10xDieRoll()
        {
            Buy();

            var playerMoney = banker.GetMoney(player);
            card.Execute(player);

            Assert.AreEqual(playerMoney - ROLL * 10, banker.GetMoney(player));
        }
    }
}