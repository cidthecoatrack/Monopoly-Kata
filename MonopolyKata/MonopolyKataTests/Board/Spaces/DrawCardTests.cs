using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Cards;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Handlers;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Board.Spaces
{
    [TestClass]
    public class DrawCardTests
    {
        private Queue<ICard> deck;
        private DrawCard drawCard;
        private IPlayer player;
        private IBanker banker;

        [TestInitialize]
        public void Setup()
        {
            player = new Player("name");
            var players = new[] { player };
            var realEstateHandler = FakeHandlerFactory.CreateEmptyRealEstateHandler(players);
            var boardHandler = FakeHandlerFactory.CreateBoardHandlerForFakeBoard(players, realEstateHandler, banker);
            var dice = new ControlledDice();
            banker = new Banker(players);
            var jailHandler = new JailHandler(dice, boardHandler, banker);
            var deckFactory = new DeckFactory(players, jailHandler, boardHandler, realEstateHandler, banker);

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

            var money = banker.Money[player];
            drawCard.LandOn(player);
            Assert.IsTrue(money > banker.Money[player]);
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