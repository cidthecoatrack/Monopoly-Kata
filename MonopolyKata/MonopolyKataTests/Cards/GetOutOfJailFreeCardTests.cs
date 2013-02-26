using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Cards;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Handlers;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class GetOutOfJailFreeCardTests
    {
        GetOutOfJailFreeCard card;

        [TestInitialize]
        public void Setup()
        {
            var player = new Player("name");

            var dice = new ControlledDice();
            var empty = Enumerable.Empty<Player>();
            var banker = new Banker(empty);
            var realEstateHandler = FakeHandlerFactory.CreateEmptyRealEstateHandler(empty);
            var boardHandler = FakeHandlerFactory.CreateBoardHandlerForFakeBoard(empty, realEstateHandler, banker);
            var jailHandler = new JailHandler(dice, boardHandler, banker);
            card = new GetOutOfJailFreeCard(jailHandler);

            card.Execute(player);
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("Get Out Of Jail, Free", card.ToString());
        }

        [TestMethod]
        public void GetOutOfJail()
        {
            Assert.IsTrue(card.Held);
        }
    }
}