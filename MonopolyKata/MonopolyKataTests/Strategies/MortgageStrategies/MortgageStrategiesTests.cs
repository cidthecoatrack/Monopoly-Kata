using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Strategies;
using Monopoly.Tests.Board.Spaces;
using Monopoly.Tests.Strategies.JailStrategies;

namespace Monopoly.Tests.Strategies.MortgageStrategies
{
    [TestClass]
    public class MortgageStrategiesTests
    {
        private RealEstate firstRealEstate;
        private RealEstate secondRealEstate;
        private RealEstate thirdRealEstate;
        private Player player;
        private StrategyCollection strategies;

        [TestInitialize]
        public void Setup()
        {
            firstRealEstate = new TestRealEstate("first", 50);
            secondRealEstate = new TestRealEstate("second", 50);
            thirdRealEstate = new TestRealEstate("third", 50);

            strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();  
        }

        [TestMethod]
        public void NeverMortgage()
        {
            strategies.MortgageStrategy = new NeverMortgage();
            player = new Player("name", strategies);

            firstRealEstate.LandOn(player);
            secondRealEstate.LandOn(player);
            thirdRealEstate.LandOn(player);

            player.HandleMortgages();

            Assert.IsFalse(firstRealEstate.Mortgaged);
            Assert.IsFalse(secondRealEstate.Mortgaged);
            Assert.IsFalse(thirdRealEstate.Mortgaged);

            firstRealEstate.Mortgage();
            player.HandleMortgages();

            Assert.IsFalse(firstRealEstate.Mortgaged);
            Assert.IsFalse(secondRealEstate.Mortgaged);
            Assert.IsFalse(thirdRealEstate.Mortgaged);
        }

        [TestMethod]
        public void MortgageWhenSheHasLessThan500()
        {
            strategies.MortgageStrategy = new MortgageIfMoneyLessThanFiveHundred();
            player = new Player("name", strategies);

            firstRealEstate.LandOn(player);
            secondRealEstate.LandOn(player);
            thirdRealEstate.LandOn(player);

            player.Pay(player.Money - 440);
            player.HandleMortgages();

            Assert.IsTrue(firstRealEstate.Mortgaged);
            Assert.IsTrue(secondRealEstate.Mortgaged);
            Assert.IsFalse(thirdRealEstate.Mortgaged);

            player.ReceiveMoney(551 - player.Money);
            player.HandleMortgages();

            Assert.IsFalse(firstRealEstate.Mortgaged);
            Assert.IsTrue(secondRealEstate.Mortgaged);
            Assert.IsFalse(thirdRealEstate.Mortgaged);
        }

        [TestMethod]
        public void AlwaysMortgage()
        {
            strategies.MortgageStrategy = new AlwaysMortgage();
            player = new Player("name", strategies);

            firstRealEstate.LandOn(player);
            secondRealEstate.LandOn(player);
            thirdRealEstate.LandOn(player);

            player.HandleMortgages();

            Assert.IsTrue(firstRealEstate.Mortgaged);
            Assert.IsTrue(secondRealEstate.Mortgaged);
            Assert.IsTrue(thirdRealEstate.Mortgaged);

            player.HandleMortgages();

            Assert.IsTrue(firstRealEstate.Mortgaged);
            Assert.IsTrue(secondRealEstate.Mortgaged);
            Assert.IsTrue(thirdRealEstate.Mortgaged);
        }
    }
}