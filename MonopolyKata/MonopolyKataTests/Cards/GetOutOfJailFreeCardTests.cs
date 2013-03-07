using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Cards;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Handlers;
using Monopoly.Tests.Players.Strategies;
using Monopoly.Tests.Players.Strategies.JailStrategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class GetOutOfJailFreeCardTests
    {
        private ICard getOutOfJailCard;
        private IJailHandler jailHandler;
        private IPlayer player;
        private IBanker banker;

        [TestInitialize]
        public void Setup()
        {
            player = new Player("name");
            var players = new[] { player };

            var dice = new ControlledDice();
            banker = new Banker(players);
            var realEstateHandler = FakeHandlerFactory.CreateEmptyRealEstateHandler(players);
            var boardHandler = FakeHandlerFactory.CreateBoardHandlerForFakeBoard(players, realEstateHandler, banker);
            jailHandler = new JailHandler(dice, boardHandler, banker);
            getOutOfJailCard = new GetOutOfJailFreeCard(jailHandler);
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("Get Out Of Jail, Free", getOutOfJailCard.ToString());
            Assert.IsFalse(getOutOfJailCard.Held);
        }

        [TestMethod]
        public void GetOutOfJail()
        {
            getOutOfJailCard.Execute(player);

            Assert.IsTrue(getOutOfJailCard.Held);
        }

        [TestMethod]
        public void GetOutOfJailFree()
        {
            player.JailStrategy = new AlwaysPay();
            var money = banker.Money[player];

            jailHandler.Imprison(player);
            jailHandler.HandleJail(0, player);

            Assert.IsFalse(getOutOfJailCard.Held);
        }

        [TestMethod]
        public void CardAvailableAfterUse()
        {
            player.JailStrategy = new AlwaysPay();
            jailHandler.Imprison(player);
            jailHandler.HandleJail(0, player);

            Assert.IsFalse(getOutOfJailCard.Held);
        }
    }
}