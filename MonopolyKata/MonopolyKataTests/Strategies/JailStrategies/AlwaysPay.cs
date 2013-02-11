using System;
using Monopoly.Strategies;

namespace Monopoly.Tests.Strategies.JailStrategies
{
    public class AlwaysPay : IJailStrategy
    {
        public Boolean SaysIShouldPayToGetOutOfJail(Int32 moneyOnHand)
        {
            return true;
        }
    }
}
