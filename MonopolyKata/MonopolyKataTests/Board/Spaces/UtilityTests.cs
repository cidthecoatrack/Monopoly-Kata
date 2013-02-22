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
        private const Int32 ROLL = 4;

        [TestInitialize]
        public void Setup()
        {
            var dice = new ControlledDice();
            dice.SetPredeterminedRollValue(ROLL);
            utility = new Utility("utility", dice);
            dice.RollTwoDice();
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("utility", utility.ToString());
            Assert.IsFalse(utility.Force10xRent);
            Assert.IsFalse(utility.Mortgaged);
            Assert.AreEqual(150, utility.Price);
            Assert.IsFalse(utility.BothUtilitiesOwned);
        }
        
        [TestMethod]
        public void GetRent()
        {
            Assert.AreEqual(ROLL * 4, utility.GetRent());
        }

        [TestMethod]
        public void BothUtilitiesOwnedGetRent()
        {
            utility.BothUtilitiesOwned = true;
            Assert.AreEqual(ROLL * 10, utility.GetRent());
        }

        [TestMethod]
        public void ForceFlag()
        {
            utility.Force10xRent = true;
            Assert.AreEqual(ROLL * 10, utility.GetRent());
        }
    }
}