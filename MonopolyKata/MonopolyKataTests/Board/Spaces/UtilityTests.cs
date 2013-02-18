using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Players;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Board.Spaces
{
    [TestClass]
    public class UtilityTests
    {
        private Utility utility;
        private Utility otherUtility;
        private Player owner;
        private Player otherOwner;
        private Player renter;
        private const Int32 ROLL = 4;

        [TestInitialize]
        public void Setup()
        {
            var dice = new ControlledDice();
            dice.SetPredeterminedRollValue(ROLL);
            utility = new Utility("utility", dice);
            otherUtility = new Utility("other utility", dice);
            var utilities = new Utility[] { utility, otherUtility };

            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();

            owner = new Player("owner", strategies);
            renter = new Player("renter", strategies);
            otherOwner = new Player("other owner", strategies);

            utility.SetUtilities(utilities);
            otherUtility.SetUtilities(utilities);

            dice.RollTwoDice();
            utility.LandOn(owner);
        }
        
        [TestMethod]
        public void LandOnOwnedUtility_PlayerPays4xDieRoll()
        {
            var ownerMoney = owner.Money;
            var renterMoney = renter.Money;
            utility.LandOn(renter);

            Assert.AreEqual(renterMoney - ROLL * 4, renter.Money);
            Assert.AreEqual(ownerMoney + ROLL * 4, owner.Money);
        }

        [TestMethod]
        public void LandOnOwnedUtilityx2_PlayerPays10xDieRoll()
        {
            otherUtility.LandOn(otherOwner);

            var ownerMoney = owner.Money;
            var renterMoney = renter.Money;
            utility.LandOn(renter);

            Assert.AreEqual(renterMoney - ROLL * 10, renter.Money);
            Assert.AreEqual(ownerMoney + ROLL * 10, owner.Money);
        }

        [TestMethod]
        public void ForceFlag()
        {
            utility.Force10xRent = true;

            var ownerMoney = owner.Money;
            var renterMoney = renter.Money;
            utility.LandOn(renter);

            Assert.AreEqual(renterMoney - ROLL * 10, renter.Money);
            Assert.AreEqual(ownerMoney + ROLL * 10, owner.Money);
        }
    }
}