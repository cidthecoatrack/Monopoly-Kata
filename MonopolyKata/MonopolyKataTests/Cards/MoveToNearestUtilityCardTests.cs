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
using Monopoly.Tests.Players.Strategies.OwnableStrategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class MoveToNearestUtilityCardTests
    {
        private ICard utilityCard;
        private IPlayer player;
        private IPlayer owner;
        private IBoardHandler boardHandler;
        private const Int32 ROLL = 10;
        private const Int32 UTILITY_PRICE = 150;
        private IBanker banker;

        [TestInitialize]
        public void Setup()
        {
            player = new Player("name");
            owner = new Player("owner");
            player.OwnableStrategy = new RandomlyBuyOrMortgage();
            owner.OwnableStrategy = new RandomlyBuyOrMortgage();
            var players = new[] { player, owner };

            banker = new Banker(players);
            var dice = new ControlledDice();
            dice.SetPredeterminedRollValue(ROLL);
            var realEstate = BoardFactory.CreateRealEstate(dice);
            var realEstateHandler = new OwnableHandler(realEstate, banker);
            boardHandler = FakeHandlerFactory.CreateBoardHandlerForFakeBoard(players, realEstateHandler, banker);

            utilityCard = new MoveToNearestUtilityCard(boardHandler, dice);
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("Advance to Nearest Utility and pay 10x a die roll", utilityCard.ToString());
            Assert.IsFalse(utilityCard.Held);
        }

        [TestMethod]
        public void Move()
        {
            utilityCard.Execute(player);
            Assert.AreEqual(BoardConstants.ELECTRIC_COMPANY, boardHandler.PositionOf[player]);

            boardHandler.Move(player, 1);
            utilityCard.Execute(player);
            Assert.AreEqual(BoardConstants.WATER_WORKS, boardHandler.PositionOf[player]);
        }

        [TestMethod]
        public void MoveAndPassGo()
        {
            boardHandler.MoveTo(player, BoardConstants.WATER_WORKS + 1);

            var expectedMoney = banker.Money[player] + GameConstants.PASS_GO_PAYMENT - UTILITY_PRICE;
            utilityCard.Execute(player);

            Assert.AreEqual(BoardConstants.ELECTRIC_COMPANY, boardHandler.PositionOf[player]);
            Assert.AreEqual(expectedMoney, banker.Money[player]);
        }

        [TestMethod]
        public void Buy()
        {
            var ownerMoney = banker.Money[owner];
            utilityCard.Execute(owner);

            Assert.AreEqual(ownerMoney - UTILITY_PRICE, banker.Money[owner]);
        }

        [TestMethod]
        public void Pay10xDieRoll()
        {
            Buy();

            var playerMoney = banker.Money[player];
            utilityCard.Execute(player);

            Assert.AreEqual(playerMoney - ROLL * 10, banker.Money[player]);
        }
    }
}