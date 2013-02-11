using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Dice;

namespace Monopoly.Tests.Dice
{
    [TestClass]
    public class DiceTests
    {
        [TestMethod]
        public void RollTwoDice_ResultBetweenOneAndTwelve()
        {
            var dice = new MonopolyDice();
            for (var i = 0; i < 1000; i++)
            {
                dice.RollTwoDice();
                Assert.IsTrue(0 < dice.Value);
                Assert.IsTrue(dice.Value <= 12);
            }
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