using System;
using Monopoly.Players.Strategies;

namespace Monopoly.Tests.Players.Strategies.JailStrategies
{
    public class AlwaysPay : IJailStrategy
    {
        public Boolean ShouldPay(Int32 moneyOnHand)
        {
            return true;
        }

        public Boolean UseCard()
        {
            return true;
        }
    }
}