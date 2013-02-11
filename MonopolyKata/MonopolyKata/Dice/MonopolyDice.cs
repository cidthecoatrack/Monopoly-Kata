using System;

namespace Monopoly.Dice
{
    public class MonopolyDice : IDice
    {
        protected Random random;
        protected Int32 firstDie;
        protected Int32 secondDie;

        public Boolean Doubles { get { return firstDie == secondDie && firstDie > 0; } }
        public Int32 Value { get { return firstDie + secondDie; } }

        public MonopolyDice()
        {
            random = new Random();
        }
        
        private Int32 RollSingleDie()
        {
            return random.Next(1, 7);
        }

        public void RollTwoDice()
        {
            firstDie = RollSingleDie();
            secondDie = RollSingleDie();
        }
    }
}