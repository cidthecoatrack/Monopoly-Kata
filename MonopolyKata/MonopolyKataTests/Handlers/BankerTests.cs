using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Handlers
{
    [TestClass]
    public class BankerTests
    {
        private IPlayer player;
        private IPlayer collector;
        private IBanker banker;

        [TestInitialize]
        public void Setup()
        {
            player = new Player("name");
            collector = new Player("collector");
            banker = new Banker(new[] { player, collector });
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual(1500, banker.Money[player]);
            Assert.IsFalse(banker.IsBankrupt(player));
        }
        
        [TestMethod]
        public void CollectTwoHundred()
        {
            banker.Collect(player, 200);
            Assert.AreEqual(1700, banker.Money[player]);
        }

        [TestMethod]
        public void PayOneHundred()
        {
            banker.Pay(player, 100);
            Assert.AreEqual(1400, banker.Money[player]);
        }

        [TestMethod]
        public void Bankruptcy()
        {
            banker.Pay(player, banker.Money[player] + 1);
            Assert.IsTrue(banker.IsBankrupt(player));
        }

        [TestMethod]
        public void Transact()
        {
            var playerMoney = banker.Money[player];
            var collectorMoney = banker.Money[collector];
            banker.Transact(player, collector, 50);

            Assert.AreEqual(playerMoney - 50, banker.Money[player]);
            Assert.AreEqual(collectorMoney + 50, banker.Money[collector]);
        }
    }
}