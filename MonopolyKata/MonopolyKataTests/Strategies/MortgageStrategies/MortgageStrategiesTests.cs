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

        [TestInitialize]
        public void Setup()
        {
            firstRealEstate = new TestRealEstate("first", 50);
            secondRealEstate = new TestRealEstate("second", 50);
            thirdRealEstate = new TestRealEstate("third", 50);
        }

        private Player CreatePlayer(IMortgageStrategy mortgageStrategy)
        {
            var player = new Player("name", mortgageStrategy, new RandomlyPay());

            firstRealEstate.LandOn(player);
            secondRealEstate.LandOn(player);
            thirdRealEstate.LandOn(player);

            return player;
        }

        [TestMethod]
        public void NeverMortgage()
        {
            var player = CreatePlayer(new NeverMortgage());

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
            var player = CreatePlayer(new MortgageIfMoneyLessThanFiveHundred());

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
            var player = CreatePlayer(new AlwaysMortgage());

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