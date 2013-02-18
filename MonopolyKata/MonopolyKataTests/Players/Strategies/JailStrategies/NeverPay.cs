using System;
using Monopoly.Players.Strategies;

namespace Monopoly.Tests.Players.Strategies.JailStrategies
{
    public class NeverPay : IJailStrategy
    {
        public Boolean ShouldPay(Int32 moneyOnHand)
        {
            return false;
        }

        public Boolean UseCard()
        {
            return false;
        }
    }
}