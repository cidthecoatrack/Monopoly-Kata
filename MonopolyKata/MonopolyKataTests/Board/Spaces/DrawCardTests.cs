using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Cards;
using Monopoly.Handlers;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Strategies;

namespace Monopoly.Tests.Board.Spaces
{
    [TestClass]
    public class DrawCardTests
    {
        private Queue<ICard> deck;
        private DrawCard drawCard;
        private Player player;

        [TestInitialize]
        public void Setup()
        {
            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();
            player = new Player("name", strategies);
            var players = new[] { player };
            var board = FakeBoardFactory.CreateBoardOfNormalSpaces();
            var boardHandler = new BoardHandler(players, board);
            var dice = new ControlledDice();
            var jailHandler = new JailHandler(dice, boardHandler);
            var deckFactory = new DeckFactory(jailHandler, players, boardHandler);

            deck = deckFactory.BuildCommunityChestDeck();
            drawCard = new DrawCard("draw card");
            drawCard.AddDeck(deck);
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("draw card", drawCard.ToString());
        }

        [TestMethod]
        public void CardsExecute()
        {
            while (!(deck.Peek() is FlatPayCard))
                deck.Enqueue(deck.Dequeue());

            var money = player.Money;
            drawCard.LandOn(player);
            Assert.IsTrue(money > player.Money);
        }

        [TestMethod]
        public void CardsAreRequeued()
        {
            drawCard.LandOn(player);
            Assert.AreEqual(16, deck.Count);
        }

        [TestMethod]
        public void HeldCards()
        {
            while (!(deck.Peek() is GetOutOfJailFreeCard))
                deck.Enqueue(deck.Dequeue());

            drawCard.LandOn(player);

            while (!(deck.Peek() is GetOutOfJailFreeCard))
                deck.Enqueue(deck.Dequeue());

            drawCard.LandOn(player);

            Assert.IsInstanceOfType(deck.ElementAt(14), typeof(GetOutOfJailFreeCard));
        }
    }
}