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
        private Player player;
        private Player collector;
        private Banker banker;

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
            Assert.AreEqual(1500, banker.GetMoney(player));
            Assert.IsFalse(banker.IsBankrupt(player));
        }
        
        [TestMethod]
        public void CollectTwoHundred()
        {
            banker.Collect(player, 200);
            Assert.AreEqual(1700, banker.GetMoney(player));
        }

        [TestMethod]
        public void PayOneHundred()
        {
            banker.Pay(player, 100, ToString());
            Assert.AreEqual(1400, banker.GetMoney(player));
        }

        [TestMethod]
        public void Bankruptcy()
        {
            banker.Pay(player, banker.GetMoney(player) + 1, ToString());
            Assert.IsTrue(banker.IsBankrupt(player));
        }

        [TestMethod]
        public void Transact()
        {
            var playerMoney = banker.GetMoney(player);
            var collectorMoney = banker.GetMoney(collector);
            banker.Transact(player, collector, 50, ToString());

            Assert.AreEqual(playerMoney - 50, banker.GetMoney(player));
            Assert.AreEqual(collectorMoney + 50, banker.GetMoney(collector));
        }
    }
}