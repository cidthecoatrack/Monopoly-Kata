using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Cards;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Dice;
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
            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();
            var player = new Player("name", strategies);

            var dice = new ControlledDice();
            var boardHandler = new BoardHandler(Enumerable.Empty<Player>(), Enumerable.Empty<ISpace>());
            var jailHandler = new JailHandler(dice, boardHandler);
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