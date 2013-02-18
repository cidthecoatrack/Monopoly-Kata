using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Cards;
using Monopoly.Handlers;
using Monopoly.Tests.Board;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class ChanceTests
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

            var board = FakeBoardFactory.CreateBoardOfNormalSpaces();
            var boardHandler = new BoardHandler(players, board);
            var dice = new ControlledDice();
            var jailHandler = new JailHandler(dice, boardHandler);
            var deckFactory = new DeckFactory(jailHandler, players, boardHandler);

            deck = deckFactory.BuildChanceDeck(dice);
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
        public void OneMoveBackThreeCard()
        {
            var moveBackThreeCards = deck.OfType<MoveBackThreeCard>();
            Assert.AreEqual(1, moveBackThreeCards.Count());
        }

        [TestMethod]
        public void FiveMoveAndPassGoCards()
        {
            var moveAndPassGoCards = deck.OfType<MoveAndPassGoCard>();
            Assert.AreEqual(5, moveAndPassGoCards.Count());
        }

        [TestMethod]
        public void TwoFlatCollectCards()
        {
            var flatCollectCards = deck.OfType<FlatCollectCard>();
            Assert.AreEqual(2, flatCollectCards.Count());
        }

        [TestMethod]
        public void OneMoveToNearestUtilityCard()
        {
            var moveToNearestUtilityCards = deck.OfType<MoveToNearestUtilityCard>();
            Assert.AreEqual(1, moveToNearestUtilityCards.Count());
        }

        [TestMethod]
        public void TwoMoveToNearestRailroadCards()
        {
            var moveToNearestRailroadCards = deck.OfType<MoveToNearestRailroadCard>();
            Assert.AreEqual(2, moveToNearestRailroadCards.Count());
        }

        [TestMethod]
        public void OnePayAllPlayersCard()
        {
            var payAllPlayersCards = deck.OfType<PayAllPlayersCard>();
            Assert.AreEqual(1, payAllPlayersCards.Count());
        }

        [TestMethod]
        public void OneHousesAndHotelsCard()
        {
            var housesAndHotelsCards = deck.OfType<HousesAndHotelsCard>();
            Assert.AreEqual(1, housesAndHotelsCards.Count());
        }

        [TestMethod]
        public void OneFlatPayCard()
        {
            var flatPayCards = deck.OfType<FlatPayCard>();
            Assert.AreEqual(1, flatPayCards.Count());
        }
    }
}