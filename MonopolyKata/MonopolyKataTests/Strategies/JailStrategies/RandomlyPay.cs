using System;
using MonopolyKata.Strategies;

namespace MonopolyKataTests.Strategies.JailStrategies
{
    public class RandomlyPay : IJailStrategy
    {
        public Boolean SaysIShouldPayToGetOutOfJail(Int32 moneyOnHand)
        {
            return Convert.ToBoolean(new DiceForTesting().RollUnboundedRandomNumber());
        }
    }
}
