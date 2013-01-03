using System;

namespace MonopolyKata.MonopolyDice
{
    public interface IDice
    {
        Boolean Doubles { get; }
        Int32 DoublesCount { get; }
        Int32 Value { get; }

        Int32 RollSingleDie();
        void RollTwoDice();
        void Reset();
    }
}
