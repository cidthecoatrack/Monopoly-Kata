using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Cards;
using Monopoly.Handlers;
using Monopoly.Tests.Board;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class CommunityChestTests
    {
        Queue<ICard> deck;
        
        [TestInitialize]
        public void Setup()
        {

            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();

            var players = new[]
                {
                    new Player("Player 1", strategies),
                    new Player("Player 2", strategies),
                    new Player("Player 3", strategies),
                    new Player("Player 4", strategies)
                };

            var dice = new ControlledDice();
            var board = FakeBoardFactory.CreateBoardOfNormalSpaces();
            var boardHandler = new BoardHandler(players, board);
            var jailHandler = new JailHandler(dice, boardHandler);

            var deckFactory = new DeckFactory(jailHandler, players, boardHandler);
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