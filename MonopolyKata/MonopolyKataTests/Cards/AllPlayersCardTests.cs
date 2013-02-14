﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Cards;
using Monopoly.Tests.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class AllPlayersCardTests
    {
        AllPlayersCard card;
        Player player;

        [TestInitialize]
        public void Setup()
        {
            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();

            player = new Player("name", strategies);

            var players = new List<Player>();
            for (var i = 0; i < 8; i++)
                players.Add(new Player("player " + i, strategies));

            card = new AllPlayersCard("all players", 10, players);
        }

        [TestMethod]
        public void Initialize()
        {
            Assert.AreEqual("all players", card.Name);
        }

        [TestMethod]
        public void Pay()
        {
            var playerMoney = player.Money;

            card.Execute(player);

            Assert.AreEqual(playerMoney + 80, player.Money);
        }
    }
}