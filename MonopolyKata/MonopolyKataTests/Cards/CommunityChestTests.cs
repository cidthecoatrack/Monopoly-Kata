using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Cards;
using Monopoly.Handlers;
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
            var dice = new ControlledDice();
            var jailHandler = new JailHandler(dice);

            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();

            var players = new[]
                {
                    new Player("Player 1", strategies),
                    new Player("Player 2", strategies),
                    new Player("Player 3", strategies),
                    new Player("Player 4", strategies)
                };

            var go = new NormalSpace("go");

            var deckFactory = new DeckFactory(jailHandler, players, go);
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
            var getOutOfJailFreeCards = deck.OfType<GoToJailCard>();
            Assert.AreEqual(1, getOutOfJailFreeCards.Count());
        }

        [TestMethod]
        public void OneMoveAndPassGoCard()
        {
            var getOutOfJailFreeCards = deck.OfType<MoveAndPassGoCard>();
            Assert.AreEqual(1, getOutOfJailFreeCards.Count());
        }

        [TestMethod]
        public void OneAllPlayersCard()
        {
            var getOutOfJailFreeCards = deck.OfType<CollectFromAllPlayersCard>();
            Assert.AreEqual(1, getOutOfJailFreeCards.Count());
        }

        [TestMethod]
        public void OneHousesAndHotelCard()
        {
            var getOutOfJailFreeCards = deck.OfType<HousesAndHotelsCard>();
            Assert.AreEqual(1, getOutOfJailFreeCards.Count());
        }

        [TestMethod]
        public void OneFlatPayCard()
        {
            var getOutOfJailFreeCards = deck.OfType<FlatPayCard>();
            Assert.AreEqual(3, getOutOfJailFreeCards.Count());
        }

        [TestMethod]
        public void OneFlatCollectCard()
        {
            var getOutOfJailFreeCards = deck.OfType<FlatCollectCard>();
            Assert.AreEqual(8, getOutOfJailFreeCards.Count());
        }
    }
}