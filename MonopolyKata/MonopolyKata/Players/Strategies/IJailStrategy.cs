using System;

namespace Monopoly.Players.Strategies
{
    public interface IJailStrategy
    {
        Boolean ShouldPay(Int32 moneyOnHand);
        Boolean UseCard();
    }
}