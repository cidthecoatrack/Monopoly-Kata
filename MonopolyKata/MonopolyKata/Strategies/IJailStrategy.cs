using System;

namespace Monopoly.Strategies
{
    public interface IJailStrategy
    {
        Boolean ShouldPay(Int32 moneyOnHand);
    }
}