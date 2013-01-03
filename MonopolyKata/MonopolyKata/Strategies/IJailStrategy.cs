using System;

namespace MonopolyKata.Strategies
{
    public interface IJailStrategy
    {
        Boolean SaysIShouldPayToGetOutOfJail(Int32 moneyOnHand);
    }
}