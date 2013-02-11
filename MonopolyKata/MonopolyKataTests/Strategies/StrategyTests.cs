using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly;
using Monopoly.Handlers;
using Monopoly.Board.Spaces;
using Monopoly.Tests.Strategies.JailStrategies;
using Monopoly.Tests.Strategies.MortgageStrategies;
using Monopoly.Tests.Dice;

namespace Monopoly.Tests.Strategies
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
            var firstProperty = new Property("first", 50, 5, GROUPING.YELLOW, 30);
            var secondProperty = new Property("second", 50, 5, GROUPING.YELLOW, 30);
            var thirdProperty = new Property("third", 50, 5, GROUPING.RED, 30);

            firstProperty.LandOn(player);
            secondProperty.LandOn(player);
            thirdProperty.LandOn(player);
            player.HandleMortgages();

            Assert.IsFalse(firstProperty.Mortgaged);
            Assert.IsFalse(secondProperty.Mortgaged);
            Assert.IsFalse(thirdProperty.Mortgaged);

            firstProperty.Mortgage();
            player.HandleMortgages();

            Assert.IsFalse(firstProperty.Mortgaged);
            Assert.IsFalse(secondProperty.Mortgaged);
            Assert.IsFalse(thirdProperty.Mortgaged);
        }

        private void PlayerMortgagesWhenSheHasLessThan500()
        {
            var player = new Player("name", new MortgageIfMoneyLessThanFiveHundred(), new RandomlyPay());
            var firstProperty = new Property("first", 50, 5, GROUPING.YELLOW, 30);
            var secondProperty = new Property("second", 50, 5, GROUPING.YELLOW, 30);
            var thirdProperty = new Property("third", 50, 5, GROUPING.RED, 30);

            firstProperty.LandOn(player);
            secondProperty.LandOn(player);
            thirdProperty.LandOn(player);
            player.Pay(player.Money - 440);
            player.HandleMortgages();

            Assert.IsTrue(firstProperty.Mortgaged);
            Assert.IsTrue(secondProperty.Mortgaged);
            Assert.IsFalse(thirdProperty.Mortgaged);

            player.ReceiveMoney(551 - player.Money);
            player.HandleMortgages();

            Assert.IsFalse(firstProperty.Mortgaged);
            Assert.IsTrue(secondProperty.Mortgaged);
            Assert.IsFalse(thirdProperty.Mortgaged);
        }

        private void PlayerAlwaysMortgages()
        {
            var player = new Player("name", new AlwaysMortgage(), new RandomlyPay());
            var firstProperty = new Property("first", 50, 5, GROUPING.YELLOW, 30);
            var secondProperty = new Property("second", 50, 5, GROUPING.YELLOW, 30);
            var thirdProperty = new Property("third", 50, 5, GROUPING.RED, 30);

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
            var dice = new ControlledDice();
            var jailHandler = new JailHandler(dice);
            var goToJail = new GoToJail(jailHandler);

            var playerMoney = player.Money;
            dice.RollTwoDice();
            goToJail.LandOn(player);
            jailHandler.HandleJail(0, player);

            Assert.AreEqual(playerMoney, player.Money);
            Assert.IsTrue(jailHandler.HasImprisoned(player));
        }

        private void PlayerAlwaysPays()
        {
            var player = new Player("name", new RandomlyMortgage(), new AlwaysPay());
            var dice = new ControlledDice();
            var jailHandler = new JailHandler(dice);
            var goToJail = new GoToJail(jailHandler);

            var playerMoney = player.Money;
            dice.RollTwoDice();
            goToJail.LandOn(player);
            jailHandler.HandleJail(0, player);

            Assert.AreEqual(playerMoney - GameConstants.COST_TO_GET_OUT_OF_JAIL, player.Money);
            Assert.IsFalse(jailHandler.HasImprisoned(player));
        }
    }
}