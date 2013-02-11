using System;
using Monopoly.Strategies;

namespace Monopoly.Tests.Strategies.JailStrategies
{
    public class NeverPay : IJailStrategy
    {
        public Boolean SaysIShouldPayToGetOutOfJail(Int32 moneyOnHand)
        {
            return false;
        }
    }
}
