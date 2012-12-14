using System;
using MonopolyKata.MonopolyDice;

namespace MonopolyKataTests
{
    public class DiceForTesting : Dice
    {
        public Int32 RollUnboundedRandomNumber()
        {
            return random.Next();
        }
    }
}
