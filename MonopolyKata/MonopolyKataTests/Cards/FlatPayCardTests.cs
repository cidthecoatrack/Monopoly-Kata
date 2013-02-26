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
        private FlatPayCard card;
        private Player player;
        private Banker banker;
        
        [TestInitialize]
        public void Setup()
        {
            player = new Player("name");
            banker = new Banker(new[] { player });

            card = new FlatPayCard("pay", 10, banker);
        }
        
        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("pay", card.ToString());
        }
        
        [TestMethod]
        public void Pay()
        {
            var playerMoney = banker.GetMoney(player);
            card.Execute(player);
            Assert.AreEqual(playerMoney - 10, banker.GetMoney(player));
        }
    }
}