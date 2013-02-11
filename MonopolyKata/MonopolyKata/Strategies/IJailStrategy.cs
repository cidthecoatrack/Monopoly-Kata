using System;

namespace Monopoly.Strategies
{
    public interface IJailStrategy
    {
        Boolean SaysIShouldPayToGetOutOfJail(Int32 moneyOnHand);
    }
}