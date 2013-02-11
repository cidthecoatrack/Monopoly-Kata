using System;
using Monopoly.Strategies;

namespace Monopoly.Tests.Strategies.JailStrategies
{
    public class RandomlyPay : IJailStrategy
    {
        public Boolean SaysIShouldPayToGetOutOfJail(Int32 moneyOnHand)
        {
            return Convert.ToBoolean(new Random().Next());
        }
    }
}
