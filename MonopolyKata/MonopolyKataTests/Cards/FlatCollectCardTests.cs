using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Cards;
using Monopoly.Tests.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class FlatCollectCardTests
    {
        FlatCollectCard card;
        Player player;
        
        [TestInitialize]
        public void Setup()
        {
            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();

            player = new Player("name", strategies); 
            
            card = new FlatCollectCard("collect", 10);
        }
        
        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("collect", card.ToString());
        }
        
        [TestMethod]
        public void Collect()
        {
            var playerMoney = player.Money;

            card.Execute(player);

            Assert.AreEqual(playerMoney + 10, player.Money);
        }
    }
}