using System;
using MonopolyKata.Strategies;

namespace MonopolyKataTests.Strategies.JailStrategies
{
    public class AlwaysPay : IJailStrategy
    {
        public Boolean SaysIShouldPayToGetOutOfJail(Int32 moneyOnHand)
        {
            return true;
        }
    }
}
