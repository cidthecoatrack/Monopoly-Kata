using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Dice;

namespace Monopoly.Tests.Dice
{
    public class ControlledDice : IDice
    {
        private Int32 predeterminedRollValue;
        private Queue<Int32> predeterminedDieValues;
        private Int32 firstDie;
        private Int32 secondDie;

        private Int32 loopStorage;
        private Int32 loopedRoll
        {
            get
            {
                loopStorage = loopStorage % 6 + 1;
                return loopStorage;
            }
        }

        public Boolean Doubles { get { return firstDie == secondDie && firstDie > 0; } }
        public Int32 Value
        {
            get
            {
                if (predeterminedRollValue > 0)
                    return predeterminedRollValue;

                return firstDie + secondDie;
            }
        }

        public ControlledDice()
        {
            predeterminedDieValues = new Queue<Int32>();
            loopStorage = 0;
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

        private Int32 RollSingleDie()
        {
            Int32 roll;

            if (predeterminedDieValues.Any())
            {
                roll = predeterminedDieValues.Dequeue();
                predeterminedDieValues.Enqueue(roll);
            }
            else
            {
                roll = loopedRoll;
            }

            return roll;
        }

        public void RollTwoDice()
        {
            firstDie = RollSingleDie();
            secondDie = RollSingleDie();
        }
    }
}