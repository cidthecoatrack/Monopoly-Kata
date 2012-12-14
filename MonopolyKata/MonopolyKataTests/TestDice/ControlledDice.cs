using System;
using MonopolyKata.MonopolyDice;

namespace MonopolyKataTests
{
    public class ControlledDice : IDice
    {
        private Int32[] predeterminedRollValues;
        private Int32[] predeterminedDieValues;
        private Int32 rIndex;
        private Int32 dIndex;

        public Boolean Doubles { get; private set; }
        public Int32 DoublesCount { get; private set; }

        private Int32 rollIndex
        {
            get { return rIndex; }
            set
            {
                if (predeterminedRollValues.Length > 0)
                {
                    if (value >= predeterminedRollValues.Length)
                        value %= predeterminedRollValues.Length;
                    rIndex = value;
                }
            }
        }

        private Int32 dieIndex
        {
            get { return dIndex; }
            set
            {
                if (predeterminedDieValues.Length > 0)
                {
                    if (value >= predeterminedDieValues.Length)
                        value %= predeterminedDieValues.Length;
                    dIndex = value;
                }
            }
        }

        public ControlledDice()
        {
            predeterminedDieValues = new Int32[0];
            predeterminedRollValues = new Int32[0];
            DoublesCount = 0;
        }

        public void SetPredeterminedRollValue(Int32 rollValue)
        {
            predeterminedRollValues = new Int32[] { rollValue };
            rollIndex = 0;
        }

        public void SetPredeterminedDieValue(Int32 dieValue)
        {
            predeterminedDieValues = new Int32[] { dieValue };
            dieIndex = 0;
        }

        public void SetPredeterminedDieValue(params Int32[] dieValues)
        {
            predeterminedDieValues = dieValues;
            dieIndex = 0;
        }

        public Int32 RollSingleDie()
        {
            if (predeterminedDieValues.Length > 0)
                return predeterminedDieValues[dieIndex++];
            return new Dice().RollSingleDie();
        }

        public Int32 RollTwoDice()
        {
            if (predeterminedRollValues.Length > 0)
                return predeterminedRollValues[rollIndex++];

            var Die1 = RollSingleDie();
            var Die2 = RollSingleDie();

            if (Die1 == Die2)
            {
                Doubles = true;
                DoublesCount++;
            }
            else
            {
                Doubles = false;
                DoublesCount = 0;
            }

            return Die1 + Die2;
        }
    }
}
