using System;
using MonopolyKata.Strategies;

namespace MonopolyKataTests.Strategies.JailStrategies
{
    public class NeverPay : IJailStrategy
    {
        public Boolean SaysIShouldPayToGetOutOfJail(Int32 moneyOnHand)
        {
            return false;
        }
    }
}
