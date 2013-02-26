using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Cards;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Board;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Handlers;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class CommunityChestTests
    {
        Queue<ICard> deck;
        
        [TestInitialize]
        public void Setup()
        {
            var players = new[]
                {
                    new Player("Player 1"),
                    new Player("Player 2"),
                    new Player("Player 3"),
                    new Player("Player 4")
                };

            var dice = new ControlledDice();
            var realEstateHandler = FakeHandlerFactory.CreateEmptyRealEstateHandler(players);
            var banker = new Banker(players);
            var boardHandler = FakeHandlerFactory.CreateBoardHandlerForFakeBoard(players, realEstateHandler, banker);
            var jailHandler = new JailHandler(dice, boardHandler, banker);
            var deckFactory = new DeckFactory(players, jailHandler, boardHandler, realEstateHandler, banker);
            deck = deckFactory.BuildCommunityChestDeck();
        }

        [TestMethod]
        public void SixteenCards()
        {
            Assert.AreEqual(16, deck.Count);
        }
        
        [TestMethod]
        public void OneGetOutOfJailFreeCard()
        {
            var getOutOfJailFreeCards = deck.OfType<GetOutOfJailFreeCard>();
            Assert.AreEqual(1, getOutOfJailFreeCards.Count());
        }

        [TestMethod]
        public void OneGoToJailCard()
        {
            var goToJailCards = deck.OfType<GoToJailCard>();
            Assert.AreEqual(1, goToJailCards.Count());
        }

        [TestMethod]
        public void OneMoveAndPassGoCard()
        {
            var moveAndPassGoCards = deck.OfType<MoveAndPassGoCard>();
            Assert.AreEqual(1, moveAndPassGoCards.Count());
        }

        [TestMethod]
        public void OneAllPlayersCard()
        {
            var collectFromAllPlayersCards = deck.OfType<CollectFromAllPlayersCard>();
            Assert.AreEqual(1, collectFromAllPlayersCards.Count());
        }

        [TestMethod]
        public void OneHousesAndHotelCard()
        {
            var housesAndHotelsCards = deck.OfType<HousesAndHotelsCard>();
            Assert.AreEqual(1, housesAndHotelsCards.Count());
        }

        [TestMethod]
        public void OneFlatPayCard()
        {
            var flatPayCards = deck.OfType<FlatPayCard>();
            Assert.AreEqual(3, flatPayCards.Count());
        }

        [TestMethod]
        public void OneFlatCollectCard()
        {
            var flatCollectCards = deck.OfType<FlatCollectCard>();
            Assert.AreEqual(8, flatCollectCards.Count());
        }
    }
}