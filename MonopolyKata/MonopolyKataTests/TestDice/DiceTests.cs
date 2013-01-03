using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata.MonopolyDice;

namespace MonopolyKataTests
{
    [TestClass]
    public class DiceTests
    {
        [TestMethod]
        public void MultipleDiceClasses_ShouldRollDifferent()
        {
            for (var i = 0; i < 1000; i++)
                Assert.AreNotEqual(new DiceForTesting().RollUnboundedRandomNumber(), new DiceForTesting().RollUnboundedRandomNumber());
        }

        [TestMethod]
        public void RollOneDie_ResultBetweenOneAndSix()
        {
            var dice = new Dice();
            for (var i = 0; i < 1000; i++)
            {
                var roll = dice.RollSingleDie();
                Assert.IsTrue(0 < roll);
                Assert.IsTrue(roll <= 6);
            }
        }

        [TestMethod]
        public void RollTwoDice_ResultBetweenOneAndTwelve()
        {
            var dice = new Dice();
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
            ControlledDice controlledDice = new ControlledDice();
            controlledDice.SetPredeterminedDieValues(3);
            controlledDice.RollTwoDice();
            Assert.AreEqual(6, controlledDice.Value);
            Assert.IsTrue(controlledDice.Doubles);
        }
    }
}