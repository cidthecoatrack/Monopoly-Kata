using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata.MonopolyDice;

namespace MonopolyKataTests
{
    [TestClass]
    public class DiceTests
    {
        IDice dice;
        
        [TestInitialize]
        public void Setup()
        {
            dice = new Dice();
        }

        [TestMethod]
        public void MultipleDiceClasses_ShouldRollDifferent()
        {
            for (Int32 i = 0; i < 100; i++)
                Assert.AreNotEqual(new DiceForTesting().RollUnboundedRandomNumber(), new DiceForTesting().RollUnboundedRandomNumber());
        }

        [TestMethod]
        public void RollOneDie_ResultBetweenOneAndSix()
        {
            Int32 roll;

            for (Int32 i = 0; i < 100; i++)
            {
                roll = dice.RollSingleDie();
                Assert.IsTrue(0 < roll);
                Assert.IsTrue(roll <= 6);
            }
        }

        [TestMethod]
        public void RollTwoDice_ResultBetweenOneAndTwelve()
        {
            Int32 roll;

            for (Int32 i = 0; i < 100; i++)
            {
                roll = dice.RollTwoDice();
                Assert.IsTrue(0 < roll);
                Assert.IsTrue(roll <= 12);
            }
        }

        [TestMethod]
        public void RollDoubles()
        {
            ControlledDice controlledDice = new ControlledDice();
            controlledDice.SetPredeterminedDieValue(3);
            var roll = controlledDice.RollTwoDice();
            Assert.AreEqual(6, roll);
            Assert.IsTrue(controlledDice.Doubles);
        }

        [TestMethod]
        public void ControlledDiceOnlyRollSetValue()
        {
            ControlledDice controlledDice = new ControlledDice();
            controlledDice.SetPredeterminedRollValue(7);

            for (int i = 0; i < 100; i++)
                Assert.AreEqual(7, controlledDice.RollTwoDice());
        }
    }
}