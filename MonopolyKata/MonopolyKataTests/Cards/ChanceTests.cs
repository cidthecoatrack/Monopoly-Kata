using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Cards;
using Monopoly.Handlers;
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
            var boardwalk = new NormalSpace("boardwalk");
            var stCharles = new NormalSpace("St. Charles");
            var illinois = new NormalSpace("Illinois");

            var railroads = new[]
                {
                    new Railroad("Reading Railroad"),
                    new Railroad("Pennsylvania Railroad"),
                    new Railroad("B&O Railroad"),
                    new Railroad("Short Line")
                };

            var utilities = new[]
                {
                    new Utility("Electric Company", dice),
                    new Utility("Water Works", dice)
                };

            deck = deckFactory.BuildChanceDeck(boardwalk, illinois, railroads, stCharles, utilities);
        }

        [TestMethod]
        public void SixteenCards()
        {
            Assert.AreEqual(16, deck.Count);
        }
    }
}