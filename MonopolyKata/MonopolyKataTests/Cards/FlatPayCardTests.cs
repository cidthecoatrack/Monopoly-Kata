using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Cards;
using Monopoly.Tests.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class FlatPayCardTests
    {
        FlatPayCard card;
        Player player;
        
        [TestInitialize]
        public void Setup()
        {
            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();

            player = new Player("name", strategies);

            card = new FlatPayCard("pay", 10);
        }
        
        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("pay", card.ToString());
        }
        
        [TestMethod]
        public void Pay()
        {
            var playerMoney = player.Money;

            card.Execute(player);

            Assert.AreEqual(playerMoney - 10, player.Money);
        }
    }
}