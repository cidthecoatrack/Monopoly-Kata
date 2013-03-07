using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Dice;

namespace Monopoly.Tests.Dice
{
    [TestClass]
    public class DiceTests
    {
        [TestMethod]
        public void RollBetweenOneAndTwelve()
        {
            var dice = new MonopolyDice();
            var rolls = new List<Int32>();

            for (var i = 0; i < 1000000; i++)
            {
                dice.RollTwoDice();
                rolls.Add(dice.Value);
            }

            var max = rolls.Max();
            var min = rolls.Min();

            Assert.IsTrue(min > 0);
            Assert.IsTrue(max <= 12);
        }

        [TestMethod]
        public void RollDoubles()
        {
            var controlledDice = new ControlledDice();
            controlledDice.SetPredeterminedDieValues(3);
            controlledDice.RollTwoDice();

            Assert.AreEqual(6, controlledDice.Value);
            Assert.IsTrue(controlledDice.Doubles);
        }
    }
}