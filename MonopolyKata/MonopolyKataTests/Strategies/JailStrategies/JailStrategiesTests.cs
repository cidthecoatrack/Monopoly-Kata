using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Handlers;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Strategies.MortgageStrategies;

namespace Monopoly.Tests.Strategies.JailStrategies
{
    [TestClass]
    public class JailStrategiesTests
    {
        private ControlledDice dice;
        private JailHandler jailHandler;
        private GoToJail goToJail;

        [TestInitialize]
        public void Setup()
        {
            dice = new ControlledDice();
            jailHandler = new JailHandler(dice);
            goToJail = new GoToJail(jailHandler);
        }
        
        [TestMethod]
        private void NeverPay()
        {
            var player = new Player("name", new RandomlyMortgage(), new NeverPay());

            var playerMoney = player.Money;
            dice.RollTwoDice();
            goToJail.LandOn(player);
            jailHandler.HandleJail(0, player);

            Assert.AreEqual(playerMoney, player.Money);
            Assert.IsTrue(jailHandler.HasImprisoned(player));
        }

        [TestMethod]
        private void AlwaysPay()
        {
            var player = new Player("name", new RandomlyMortgage(), new AlwaysPay());

            var playerMoney = player.Money;
            dice.RollTwoDice();
            goToJail.LandOn(player);
            jailHandler.HandleJail(0, player);

            Assert.AreEqual(playerMoney - GameConstants.COST_TO_GET_OUT_OF_JAIL, player.Money);
            Assert.IsFalse(jailHandler.HasImprisoned(player));
        }
    }
}