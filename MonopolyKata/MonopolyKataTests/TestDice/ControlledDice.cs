using System;
using System.Collections.Generic;
using System.Linq;
using MonopolyKata.MonopolyDice;

namespace MonopolyKataTests
{
    public class ControlledDice : IDice
    {
        private Int32 predeterminedRollValue;
        private Queue<Int32> predeterminedDieValues;

        public Boolean Doubles { get; private set; }
        public Int32 DoublesCount { get; private set; }
        public Int32 Value { get; private set; }

        public ControlledDice()
        {
            predeterminedDieValues = new Queue<Int32>();
            DoublesCount = 0;
        }

        public void SetPredeterminedRollValue(Int32 rollValue)
        {
            predeterminedRollValue = rollValue;
        }

        public void SetPredeterminedDieValues(params Int32[] dieValues)
        {
            foreach (var dieValue in dieValues)
                predeterminedDieValues.Enqueue(dieValue);
        }

        public Int32 RollSingleDie()
        {
            Int32 roll;

            if (predeterminedDieValues.Any())
            {
                roll = predeterminedDieValues.Dequeue();
                predeterminedDieValues.Enqueue(roll);
            }
            else
            {
                var dice = new Dice();
                roll = dice.RollSingleDie();
            }

            return roll;
        }

        public void RollTwoDice()
        {
            if (predeterminedRollValue != 0)
            {
                Value = predeterminedRollValue;
                return;
            }

            var Die1 = RollSingleDie();
            var Die2 = RollSingleDie();

            if (Die1 == Die2)
            {
                Doubles = true;
                DoublesCount++;
            }
            else
            {
                Reset();
            }

            Value = Die1 + Die2;
        }

        public void Reset()
        {
            Doubles = false;
            DoublesCount = 0;
        }
    }
}