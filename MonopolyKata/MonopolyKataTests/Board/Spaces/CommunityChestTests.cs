using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Cards;
using Monopoly.Handlers;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Strategies;

namespace Monopoly.Tests.Board.Spaces
{
    [TestClass]
    public class CommunityChestTests
    {
        private Queue<ICard> deck;
        private CommunityChest communityChest;
        private Player player;
        
        [TestMethod]
        public void Constructor()
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
            communityChest = new CommunityChest(deck);

            Assert.AreEqual("Community Chest", communityChest.ToString());
        }

        [TestMethod]
        public void CardsAreRequeued()
        {
            communityChest.LandOn(player);
            Assert.AreEqual(16, deck.Count);
        }
    }
}