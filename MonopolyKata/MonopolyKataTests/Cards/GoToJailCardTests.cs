﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board;
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
    public class GoToJailCardTests
    {
        private GoToJailCard card;
        private Player player;
        private JailHandler jailHandler;
        private BoardHandler boardHandler;

        [TestInitialize]
        public void Setup()
        {
            player = new Player("name");

            var dice = new ControlledDice();
            var players = new[] { player };
            var banker = new Banker(players);
            var realEstateHandler = FakeHandlerFactory.CreateEmptyRealEstateHandler(players);
            boardHandler = FakeHandlerFactory.CreateBoardHandlerForFakeBoard(players, realEstateHandler, banker);
            jailHandler = new JailHandler(dice, boardHandler, banker);

            card = new GoToJailCard(jailHandler);
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("Go To Jail", card.ToString());
        }

        [TestMethod]
        public void GoToJail()
        {
            card.Execute(player);
            Assert.AreEqual(BoardConstants.JAIL_OR_JUST_VISITING, boardHandler.PositionOf[player]);
            Assert.IsTrue(jailHandler.HasImprisoned(player));
        }
    }
}