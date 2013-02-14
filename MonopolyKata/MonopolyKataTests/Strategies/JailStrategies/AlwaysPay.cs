using System;
using Monopoly.Strategies;

namespace Monopoly.Tests.Strategies.JailStrategies
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