using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Cards;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class FlatPayCardTests
    {
        private ICard payCard;
        private IPlayer player;
        private IBanker banker;
        
        [TestInitialize]
        public void Setup()
        {
            player = new Player("name");
            banker = new Banker(new[] { player });

            payCard = new FlatPayCard("pay", 10, banker);
        }
        
        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("pay", payCard.ToString());
            Assert.IsFalse(payCard.Held);
        }
        
        [TestMethod]
        public void Pay()
        {
            var playerMoney = banker.Money[player];
            payCard.Execute(player);
            Assert.AreEqual(playerMoney - 10, banker.Money[player]);
        }
    }
}