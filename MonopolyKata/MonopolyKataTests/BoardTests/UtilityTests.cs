using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata.MonopolyBoard;
using MonopolyKata.MonopolyPlayer;
using MonopolyKataTests.MortgageStrategies;

namespace MonopolyKataTests.BoardTests
{
    [TestClass]
    public class UtilityTests
    {
        Utility utility;
        Utility otherUtility;
        Player owner;
        Player renter;
        Int32 roll;

        [TestInitialize]
        public void Setup()
        {
            utility = new Utility("utility");
            otherUtility = new Utility("other utility");
            var utilities = new Utility[] { utility, otherUtility };
            owner = new Player("owner", new RandomlyMortgage());
            renter = new Player("renter", new RandomlyMortgage());
            roll = 4;

            utility.SetPropertiesInGroup(utilities);
            otherUtility.SetPropertiesInGroup(utilities);
            owner.ReceiveMoney(utility.Price);
            utility.LandOn(owner);
            renter.Move(roll);
        }
        
        [TestMethod]
        public void PlayerLandsOnOwnedUtility_PlayerPays4xDieRoll()
        {
            renter.ReceiveMoney(16);
            utility.LandOn(renter);
            Assert.AreEqual(0, renter.Money);
            Assert.AreEqual(16, owner.Money);
        }

        [TestMethod]
        public void PlayerLandsOnOwnedUtilityx2_PlayerPays10xDieRoll()
        {
            var otherOwner = new Player("other owner", new RandomlyMortgage());
            otherOwner.ReceiveMoney(otherUtility.Price);
            otherUtility.LandOn(otherOwner);
            
            renter.ReceiveMoney(40);
            utility.LandOn(renter);
            Assert.AreEqual(0, renter.Money);
            Assert.AreEqual(40, owner.Money);
        }
    }
}