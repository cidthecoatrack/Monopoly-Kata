using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata.MonopolyBoard.Spaces;
using MonopolyKata.MonopolyPlayer;
using MonopolyKataTests.Strategies.JailStrategies;
using MonopolyKataTests.Strategies.MortgageStrategies;

namespace MonopolyKataTests.Strategies
{
    [TestClass]
    public class StrategyTests
    {
        [TestMethod]
        public void PlayerImplementsMortgageStrategy()
        {
            PlayerAlwaysMortgages();
            PlayerNeverMortgages();
            PlayerMortgagesWhenSheHasLessThan500();
        }

        private void PlayerNeverMortgages()
        {
            var player = new Player("name", new NeverMortgage(), new RandomlyPay());
            var firstProperty = new Property("first", 50, 5, GROUPING.YELLOW);
            var secondProperty = new Property("second", 50, 5, GROUPING.YELLOW);
            var thirdProperty = new Property("third", 50, 5, GROUPING.RED);

            player.ReceiveMoney(150);
            firstProperty.LandOn(player);
            secondProperty.LandOn(player);
            thirdProperty.LandOn(player);
            player.HandleMortgages();

            Assert.IsFalse(firstProperty.Mortgaged);
            Assert.IsFalse(secondProperty.Mortgaged);
            Assert.IsFalse(thirdProperty.Mortgaged);

            firstProperty.Mortgage();
            player.ReceiveMoney(50);
            player.HandleMortgages();

            Assert.IsFalse(firstProperty.Mortgaged);
            Assert.IsFalse(secondProperty.Mortgaged);
            Assert.IsFalse(thirdProperty.Mortgaged);
        }

        private void PlayerMortgagesWhenSheHasLessThan500()
        {
            var player = new Player("name", new MortgageIfMoneyLessThanFiveHundred(), new RandomlyPay());
            var firstProperty = new Property("first", 50, 5, GROUPING.YELLOW);
            var secondProperty = new Property("second", 50, 5, GROUPING.YELLOW);
            var thirdProperty = new Property("third", 50, 5, GROUPING.RED);

            player.ReceiveMoney(600);
            firstProperty.LandOn(player);
            secondProperty.LandOn(player);
            thirdProperty.LandOn(player);
            player.HandleMortgages();

            Assert.IsTrue(firstProperty.Mortgaged);
            Assert.IsTrue(secondProperty.Mortgaged);
            Assert.IsFalse(thirdProperty.Mortgaged);
            player.ReceiveMoney(10);
            player.HandleMortgages();

            Assert.IsFalse(firstProperty.Mortgaged);
            Assert.IsTrue(secondProperty.Mortgaged);
            Assert.IsFalse(thirdProperty.Mortgaged);
        }

        private void PlayerAlwaysMortgages()
        {
            var player = new Player("name", new AlwaysMortgage(), new RandomlyPay());
            var firstProperty = new Property("first", 50, 5, GROUPING.YELLOW);
            var secondProperty = new Property("second", 50, 5, GROUPING.YELLOW);
            var thirdProperty = new Property("third", 50, 5, GROUPING.RED);

            player.ReceiveMoney(150);
            firstProperty.LandOn(player);
            secondProperty.LandOn(player);
            thirdProperty.LandOn(player);
            player.HandleMortgages();

            Assert.IsTrue(firstProperty.Mortgaged);
            Assert.IsTrue(secondProperty.Mortgaged);
            Assert.IsTrue(thirdProperty.Mortgaged);

            player.HandleMortgages();

            Assert.IsTrue(firstProperty.Mortgaged);
            Assert.IsTrue(secondProperty.Mortgaged);
            Assert.IsTrue(thirdProperty.Mortgaged);
        }

        [TestMethod]
        public void PlayerImplementsJailStrategy()
        {
            PlayerAlwaysPays();
            PlayerNeverPays();
        }

        private void PlayerNeverPays()
        {
            var player = new Player("name", new RandomlyMortgage(), new NeverPay());
            var goToJail = new GoToJail();
            player.ReceiveMoney(50);
            goToJail.LandOn(player);
            player.PreTurnChecks();
            Assert.AreEqual(50, player.Money);
            Assert.IsTrue(player.IsInJail);
        }

        private void PlayerAlwaysPays()
        {
            var player = new Player("name", new RandomlyMortgage(), new AlwaysPay());
            var goToJail = new GoToJail();
            player.ReceiveMoney(50);
            goToJail.LandOn(player);
            player.PreTurnChecks();
            Assert.AreEqual(0, player.Money);
            Assert.IsFalse(player.IsInJail);
        }
    }
}