using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;
using MonopolyKata.MonopolyPlayer;
using MonopolyKataTests.MortgageStrategies;

namespace MonopolyKataTests
{
    [TestClass]
    public class PlayerOrderRandomizerTests
    {
        List<Player> nonRandomizedPlayers;
        
        [TestInitialize]
        public void Setup()
        {
            nonRandomizedPlayers = new List<Player>();
            for (var i = 0; i < 8; i++)
                nonRandomizedPlayers.Add(new Player(i.ToString(), new RandomlyMortgage()));
        }
        
        [TestMethod]
        public void PlayerOrderIsRandomized()
        {
            var randomizedPlayers = PlayerOrderRandomizer.Execute(nonRandomizedPlayers);
            Assert.AreEqual(randomizedPlayers.Count(), nonRandomizedPlayers.Count());

            var randomized = false;
            for (var i = 0; i < randomizedPlayers.Count(); i++)
                if (!randomizedPlayers.ElementAt(i).Equals(nonRandomizedPlayers.ElementAt(i)))
                    randomized = true;

            Assert.IsTrue(randomized);
        }

        [TestMethod]
        public void RandomizingPlayerOrderMaintainsPlayerIntegrity()
        {
            var randomized = PlayerOrderRandomizer.Execute(nonRandomizedPlayers);
            var nonrandomized = nonRandomizedPlayers;
            Assert.AreEqual(randomized.Count(), nonrandomized.Count());
            TwoSidedUnion(randomized, nonrandomized);
        }

        private void TwoSidedUnion(IEnumerable<Player> firstCollection, IEnumerable<Player> secondCollection)
        {
            OneSidedUnion(firstCollection, secondCollection);
            OneSidedUnion(secondCollection, firstCollection);
        }

        private void OneSidedUnion(IEnumerable<Player> left, IEnumerable<Player> right)
        {
            foreach (var element in left)
                Assert.IsTrue(left.Contains(element));
        }
    }
}