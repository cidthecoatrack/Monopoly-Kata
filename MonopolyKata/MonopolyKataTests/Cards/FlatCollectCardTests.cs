using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Cards;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class FlatCollectCardTests
    {
        private FlatCollectCard card;
        private Player player;
        private Banker banker;
        
        [TestInitialize]
        public void Setup()
        {
            player = new Player("name");
            banker = new Banker(new[] { player });
            
            card = new FlatCollectCard("collect", 10, banker);
        }
        
        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("collect", card.ToString());
        }
        
        [TestMethod]
        public void Collect()
        {
            var playerMoney = banker.GetMoney(player);
            card.Execute(player);
            Assert.AreEqual(playerMoney + 10, banker.GetMoney(player));
        }
    }
}