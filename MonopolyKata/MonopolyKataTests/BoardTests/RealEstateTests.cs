using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata.MonopolyBoard.Spaces;
using MonopolyKata.MonopolyPlayer;
using MonopolyKataTests.Strategies.JailStrategies;
using MonopolyKataTests.Strategies.MortgageStrategies;

namespace MonopolyKataTests.BoardTests
{
    [TestClass]
    public class RealEstateTests
    {
        RealEstate realEstate;
        Player player;
        private const Int32 PRICE = 50;
        
        [TestInitialize]
        public void Setup()
        {
            realEstate = new RealEstate("real estate", PRICE);
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
            var previousPlayer = player;
            realEstate.LandOn(player);
            Assert.AreEqual(player, previousPlayer);
            Assert.IsFalse(player.Owns(realEstate));
            Assert.IsFalse(realEstate.Owned);
            Assert.IsNull(realEstate.Owner);
        }

        [TestMethod]
        public void PlayerMortgagesPropertyFor90PercentPurchasePrice()
        {
            player.ReceiveMoney(PRICE);
            realEstate.LandOn(player);
            realEstate.Mortgage();
            Assert.AreEqual(PRICE * .9, player.Money);
        }

        [TestMethod]
        public void CannotMortgageAlreadyMortgagedProperty()
        {
            player.ReceiveMoney(PRICE);
            realEstate.LandOn(player);
            realEstate.Mortgage();
            Assert.AreEqual(PRICE * .9, player.Money);
            Assert.IsTrue(realEstate.Mortgaged);
            realEstate.Mortgage();
            Assert.AreEqual(PRICE * .9, player.Money);
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
            player.ReceiveMoney(PRICE);
            realEstate.LandOn(player);
            player.ReceiveMoney(PRICE);
            realEstate.PayOffMortgage();
            Assert.AreEqual(PRICE, player.Money);
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