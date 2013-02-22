using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board;
using Monopoly.Cards;
using Monopoly.Games;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Board;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Handlers;
using Monopoly.Tests.Players.Strategies;
using Monopoly.Tests.Players.Strategies.JailStrategies;

namespace Monopoly.Tests.Hendlers
{
    [TestClass]
    public class JailHandlerTests
    {
        private ControlledDice dice;
        private JailHandler jailHandler;
        private BoardHandler boardHandler;
        private Player player;
        private Banker banker;

        [TestInitialize]
        public void Setup()
        {
            var strategies = new StrategyCollection();
            strategies.CreateNeverStrategyCollection();
            player = new Player("name", strategies);

            dice = new ControlledDice();
            var players = new[] { player };
            banker = new Banker(players);
            var realEstateHandler = FakeHandlerFactory.CreateEmptyRealEstateHandler(players);
            boardHandler = FakeHandlerFactory.CreateBoardHandlerForFakeBoard(players, realEstateHandler, banker);
            jailHandler = new JailHandler(dice, boardHandler, banker);
        }

        [TestMethod]
        public void RollThreeDoubles_GoToJail()
        {
            jailHandler.HandleJail(3, player);

            Assert.AreEqual(BoardConstants.JAIL_OR_JUST_VISITING, boardHandler.PositionOf[player]);
            Assert.IsTrue(jailHandler.HasImprisoned(player));
        }

        [TestMethod]
        public void RollDoublesInJail_GetOut()
        {
            dice.SetPredeterminedDieValues(3, 3, 3, 1);
            jailHandler.Imprison(player);

            dice.RollTwoDice();
            jailHandler.HandleJail(0, player);

            Assert.IsFalse(jailHandler.HasImprisoned(player));
        }

        [TestMethod]
        public void DontRollDoublesInJail_StillInJailAndNoTurn()
        {
            var playerMoney = banker.GetMoney(player);
            jailHandler.Imprison(player);
            dice.RollTwoDice();
            jailHandler.HandleJail(0, player);

            Assert.AreEqual(BoardConstants.JAIL_OR_JUST_VISITING, boardHandler.PositionOf[player]);
            Assert.AreEqual(playerMoney, banker.GetMoney(player));
            Assert.IsTrue(jailHandler.HasImprisoned(player), "first turn");

            dice.RollTwoDice();
            jailHandler.HandleJail(0, player);

            Assert.AreEqual(BoardConstants.JAIL_OR_JUST_VISITING, boardHandler.PositionOf[player]);
            Assert.AreEqual(playerMoney, banker.GetMoney(player));
            Assert.IsTrue(jailHandler.HasImprisoned(player), "second turn");
        }

        [TestMethod]
        public void InJailThreeTurnsAndNoDoubles_Pay50AndGetOut()
        {
            var playerMoney = banker.GetMoney(player);
            jailHandler.Imprison(player);
            
            dice.RollTwoDice();
            jailHandler.HandleJail(0, player);
            jailHandler.HandleJail(0, player);
            jailHandler.HandleJail(0, player);

            Assert.AreEqual(playerMoney - GameConstants.COST_TO_GET_OUT_OF_JAIL, banker.GetMoney(player));
            Assert.IsFalse(jailHandler.HasImprisoned(player));
        }

        [TestMethod]
        public void PayToGetOut()
        {
            player.JailStrategy = new AlwaysPay();

            var playerMoney = banker.GetMoney(player);
            jailHandler.Imprison(player);

            dice.RollTwoDice();
            jailHandler.HandleJail(0, player);

            Assert.AreEqual(playerMoney - GameConstants.COST_TO_GET_OUT_OF_JAIL, banker.GetMoney(player));
            Assert.IsFalse(jailHandler.HasImprisoned(player));
        }

        [TestMethod]
        public void UseGetOutOfJailCard()
        {
            player.JailStrategy = new AlwaysPay();

            var card = new GetOutOfJailFreeCard(jailHandler);
            card.Execute(player);

            var playerMoney = banker.GetMoney(player);
            jailHandler.Imprison(player);

            dice.RollTwoDice();
            jailHandler.HandleJail(0, player);

            Assert.AreEqual(playerMoney, banker.GetMoney(player));
            Assert.IsFalse(jailHandler.HasImprisoned(player));
        }

        [TestMethod]
        public void HoldMultipleGetOutOfJailFreeCards()
        {
            var card = new GetOutOfJailFreeCard(jailHandler);
            card.Execute(player);

            var secondCard = new GetOutOfJailFreeCard(jailHandler);
            secondCard.Execute(player);
        }
    }
}