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
        private GoToJail goToJail;
        private JailHandler jailHandler;
        private Player player;
        private StrategyCollection strategies;
        private Int32 playerMoney;

        [TestInitialize]
        public void Setup()
        {
            dice = new ControlledDice();
            jailHandler = new JailHandler(dice);
            goToJail = new GoToJail(jailHandler);

            strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();
        }
        
        [TestMethod]
        public void NeverPay()
        {
            strategies.JailStrategy = new NeverPay();

            player = new Player("name", strategies);
            playerMoney = player.Money;

            dice.RollTwoDice();
            goToJail.LandOn(player);

            jailHandler.HandleJail(0, player);

            Assert.AreEqual(playerMoney, player.Money);
            Assert.IsTrue(jailHandler.HasImprisoned(player));
        }

        [TestMethod]
        public void AlwaysPay()
        {
            strategies.JailStrategy = new AlwaysPay();

            player = new Player("name", strategies);
            playerMoney = player.Money;

            dice.RollTwoDice();
            goToJail.LandOn(player);

            jailHandler.HandleJail(0, player);

            Assert.AreEqual(playerMoney - GameConstants.COST_TO_GET_OUT_OF_JAIL, player.Money);
            Assert.IsFalse(jailHandler.HasImprisoned(player));
        }
    }
}