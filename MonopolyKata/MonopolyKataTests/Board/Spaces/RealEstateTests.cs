using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly;
using Monopoly.Tests.Strategies.JailStrategies;
using Monopoly.Tests.Strategies.MortgageStrategies;

namespace Monopoly.Tests.Board.Spaces
{
    [TestClass]
    public class RealEstateTests
    {
        RealEstate realEstate;
        Player player;
        private const Int32 PRICE = 50;

        private class TestRealEstate : RealEstate
        {
            public TestRealEstate(String name, Int32 price) : base(name, price) { }
            protected override Int32 GetRent() { return 0; }
        }
        
        [TestInitialize]
        public void Setup()
        {
            realEstate = new TestRealEstate("real estate", PRICE);
            player = new Player("player", new RandomlyMortgage(), new RandomlyPay());
        }
        
        [TestMethod]
        public void OwnerLandsOnTheirRealEstate_NothingHappens()
        {
            player.ReceiveMoney(PRICE);
            realEstate.LandOn(player);
            Assert.IsTrue(player.Owns(realEstate));
            var previousPlayer = player;
            realEstate.LandOn(player);
            Assert.AreEqual(player, previousPlayer);
        }

        [TestMethod]
        public void PlayerDoesNotBuyUnaffordableRealEstate()
        {
            player.Pay(player.Money - realEstate.Price + 1);
            realEstate.LandOn(player);

            Assert.IsFalse(player.Owns(realEstate));
            Assert.IsFalse(realEstate.Owned);
            Assert.IsNull(realEstate.Owner);
        }

        [TestMethod]
        public void PlayerMortgagesPropertyFor90PercentPurchasePrice()
        {
            realEstate.LandOn(player);
            var previousMoney = player.Money;

            realEstate.Mortgage();

            Assert.AreEqual(previousMoney + PRICE * .9, player.Money);
        }

        [TestMethod]
        public void CannotMortgageAlreadyMortgagedProperty()
        {
            realEstate.LandOn(player);
            var previousMoney = player.Money;
            realEstate.Mortgage();

            Assert.AreEqual(previousMoney + PRICE * .9, player.Money);
            Assert.IsTrue(realEstate.Mortgaged);

            realEstate.Mortgage();

            Assert.AreEqual(previousMoney + PRICE * .9, player.Money);
        }

        [TestMethod]
        public void PlayerCanPayOffMortgage()
        {
            player.ReceiveMoney(PRICE);
            realEstate.LandOn(player);
            realEstate.Mortgage();
            player.ReceiveMoney(PRICE - player.Money);
            realEstate.PayOffMortgage();

            Assert.AreEqual(0, player.Money);
            Assert.IsFalse(realEstate.Mortgaged);
        }

        [TestMethod]
        public void PlayerCantPayOffUnmortgagedProperty()
        {
            realEstate.LandOn(player);
            var playerMoney = player.Money;
            realEstate.PayOffMortgage();

            Assert.AreEqual(playerMoney, player.Money);
            Assert.IsFalse(realEstate.Mortgaged);
        }

        [TestMethod]
        public void LosingPlayerDoesNotOwnPropertyAnymore()
        {
            player.ReceiveMoney(PRICE);
            realEstate.LandOn(player);

            Assert.IsTrue(realEstate.Owned);

            player.Pay(player.Money + 1);

            Assert.IsTrue(player.LostTheGame);
            Assert.IsFalse(realEstate.Owned);
        }
    }
}