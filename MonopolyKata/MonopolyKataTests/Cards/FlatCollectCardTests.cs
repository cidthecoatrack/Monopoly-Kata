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
        private ICard collectCard;
        private IPlayer player;
        private IBanker banker;
        
        [TestInitialize]
        public void Setup()
        {
            player = new Player("name");
            banker = new Banker(new[] { player });
            
            collectCard = new FlatCollectCard("collect", 10, banker);
        }
        
        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("collect", collectCard.ToString());
            Assert.IsFalse(collectCard.Held);
        }
        
        [TestMethod]
        public void Collect()
        {
            var playerMoney = banker.Money[player];
            collectCard.Execute(player);
            Assert.AreEqual(playerMoney + 10, banker.Money[player]);
        }
    }
}