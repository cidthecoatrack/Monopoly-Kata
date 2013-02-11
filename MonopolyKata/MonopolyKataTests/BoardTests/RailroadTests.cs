﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata.MonopolyBoard.Spaces;
using MonopolyKata.MonopolyPlayer;
using MonopolyKataTests.Strategies.JailStrategies;
using MonopolyKataTests.Strategies.MortgageStrategies;

namespace MonopolyKataTests.BoardTests
{
    [TestClass]
    public class RailroadTests
    {
        Railroad[] railroads;
        Player owner;
        Player renter;

        [TestInitialize]
        public void Setup()
        {
            BuildRailroads();

            owner = new Player("owner", new RandomlyMortgage(), new RandomlyPay());
            renter = new Player("renter", new RandomlyMortgage(), new RandomlyPay());

            railroads[0].LandOn(owner);
        }

        private void BuildRailroads()
        {
            var firstRxR = new Railroad("first railroad");
            var secondRxR = new Railroad("second railroad");
            var thirdRxR = new Railroad("third railroad");
            var fourthRxR = new Railroad("fourth railroad");

            railroads = new Railroad[] { firstRxR, secondRxR, thirdRxR, fourthRxR };

            foreach (var railroad in railroads)
                railroad.SetRailroads(railroads);
        }
        
        [TestMethod]
        public void PlayerLandsOnOwnedRailroadx1_Pays25()
        {
            var renterMoney = renter.Money;
            var ownerMoney = owner.Money;
            railroads[0].LandOn(renter);

            Assert.AreEqual(renterMoney - 25, renter.Money);
            Assert.AreEqual(ownerMoney + 25, owner.Money);
        }

        [TestMethod]
        public void PlayerLandsOnOwnedRailroadx2_Pays50()
        {
            railroads[1].LandOn(owner);

            var renterMoney = renter.Money;
            var ownerMoney = owner.Money;
            railroads[0].LandOn(renter);

            Assert.AreEqual(renterMoney - 50, renter.Money);
            Assert.AreEqual(ownerMoney + 50, owner.Money);
        }

        [TestMethod]
        public void PlayerLandsOnOwnedRailroadx3_Pays100()
        {
            railroads[1].LandOn(owner);
            railroads[2].LandOn(owner);

            var renterMoney = renter.Money;
            var ownerMoney = owner.Money;
            railroads[0].LandOn(renter);

            Assert.AreEqual(renterMoney - 100, renter.Money);
            Assert.AreEqual(ownerMoney + 100, owner.Money);
        }

        [TestMethod]
        public void PlayerLandsOnOwnedRailroadx4_Pays200()
        {
            railroads[1].LandOn(owner);
            railroads[2].LandOn(owner);
            railroads[3].LandOn(owner);

            var renterMoney = renter.Money;
            var ownerMoney = owner.Money;
            railroads[0].LandOn(renter);

            Assert.AreEqual(renterMoney - 200, renter.Money);
            Assert.AreEqual(ownerMoney + 200, owner.Money);
        }
    }
}