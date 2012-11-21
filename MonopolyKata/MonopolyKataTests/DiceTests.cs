using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;

namespace MonopolyKataTests
{
    [TestClass]
    public class DiceTests
    {
        Dice dice;
        
        [TestInitialize]
        public void Setup()
        {
            dice = new Dice();
        }
        
        [TestMethod]
        public void CanMakeDice()
        {
            Assert.IsNotNull(dice);
        }

        [TestMethod]
        public void RollOneDie_ResultBetweenOneAndSix()
        {
            Int32 roll = dice.RollSingleDie();
            Assert.IsTrue(0 < roll);
            Assert.IsTrue(roll <= 6);
        }

        [TestMethod]
        public void RollTwoDice_ResultBetweenOneAndTwelve()
        {

            Int32 roll = dice.RollTwoDice();
            Assert.IsTrue(0 < roll);
            Assert.IsTrue(roll <= 12);
        }

        [TestMethod]
        public void MultipleDiceClasses_ShouldRollDifferent()
        {
            for (Int32 i = 0; i < 100; i++)
                Assert.AreNotEqual(new Dice().RollUnboundedRandomNumber(), new Dice().RollUnboundedRandomNumber());
        }
    }
}