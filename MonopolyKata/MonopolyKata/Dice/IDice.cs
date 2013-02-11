using System;

namespace Monopoly.Dice
{
    public interface IDice
    {
        Boolean Doubles { get; }
        Int32 Value { get; }

        void RollTwoDice();
    }
}
