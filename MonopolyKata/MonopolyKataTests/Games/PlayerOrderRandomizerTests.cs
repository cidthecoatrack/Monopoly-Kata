﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Games;
using Monopoly.Players;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Games
{
    [TestClass]
    public class PlayerOrderRandomizerTests
    {
        private List<Player> nonRandomizedPlayers;
        private IEnumerable<Player> randomizedPlayers;
        
        [TestInitialize]
        public void Setup()
        {
            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();
            
            nonRandomizedPlayers = new List<Player>();
            for (var i = 0; i < 8; i++)
                nonRandomizedPlayers.Add(new Player(Convert.ToString(i), strategies));

            var randomizer = new PlayerOrderRandomizer();
            randomizedPlayers = randomizer.Execute(nonRandomizedPlayers);
        }
        
        [TestMethod]
        public void PlayerOrderIsRandomized()
        {
            Assert.AreEqual(randomizedPlayers.Count(), nonRandomizedPlayers.Count());

            var notRandomized = randomizedPlayers.Where((x, i) => x.Equals(nonRandomizedPlayers[i]));
            Assert.AreNotEqual(nonRandomizedPlayers.Count(), notRandomized.Count());
        }

        [TestMethod]
        public void RandomizingPlayerOrderMaintainsPlayerIntegrity()
        {
            Assert.AreEqual(randomizedPlayers.Count(), nonRandomizedPlayers.Count());
            Assert.IsTrue(randomizedPlayers.All(x => nonRandomizedPlayers.Contains(x)));
            Assert.IsTrue(nonRandomizedPlayers.All(x => randomizedPlayers.Contains(x)));
        }
    }
}