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
    public class MoveToNearestRailroadCardTests
    {
        private MoveToNearestRailroadCard card;
        private BoardHandler boardHandler;
        private Player player;
        private Banker banker;

        [TestInitialize]
        public void Setup()
        {
            player = new Player("name");
            player.OwnableStrategy = new RandomlyBuyOrMortgage();
            var owner = new Player("owner");
            owner.OwnableStrategy = new RandomlyBuyOrMortgage();

            var players = new[] { player, owner };

            var dice = new ControlledDice();
            var realEstate = BoardFactory.CreateRealEstate(dice);
            banker = new Banker(players);
            var realEstateHandler = new OwnableHandler(realEstate, banker);
            boardHandler = FakeHandlerFactory.CreateBoardHandlerForFakeBoard(players, realEstateHandler, banker);

            foreach (var rxr in realEstate.Values.OfType<Railroad>())
                realEstateHandler.Land(owner, realEstate.Keys.First(k => realEstate[k] == rxr));

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

            card.Execute(player);
            Assert.AreEqual(BoardConstants.PENNSYLVANIA_RAILROAD, boardHandler.PositionOf[player]);

            card.Execute(player);
            Assert.AreEqual(BoardConstants.BandO_RAILROAD, boardHandler.PositionOf[player]);

            card.Execute(player);
            Assert.AreEqual(BoardConstants.SHORT_LINE, boardHandler.PositionOf[player]);
        }

        [TestMethod]
        public void MoveAndPassGo()
        {
            boardHandler.MoveTo(player, BoardConstants.SHORT_LINE + 2);

            var expectedMoney = banker.GetMoney(player) + GameConstants.PASS_GO_PAYMENT - 400;
            card.Execute(player);

            Assert.AreEqual(BoardConstants.READING_RAILROAD, boardHandler.PositionOf[player]);
            Assert.AreEqual(expectedMoney, banker.GetMoney(player));
        }

        [TestMethod]
        public void PayTwiceNormalRent()
        {
            var playerMoney = banker.GetMoney(player);
            card.Execute(player);
            Assert.AreEqual(playerMoney - 400, banker.GetMoney(player));
        }

        [TestMethod]
        public void BankruptWhilePaying()
        {
            banker.Pay(player, banker.GetMoney(player) - 1, ToString());
            card.Execute(player);
            Assert.IsTrue(banker.IsBankrupt(player));
        }
    }
}