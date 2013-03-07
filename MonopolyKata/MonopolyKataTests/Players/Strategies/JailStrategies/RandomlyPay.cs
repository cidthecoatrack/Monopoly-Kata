using System;
using Monopoly.Players.Strategies;

namespace Monopoly.Tests.Players.Strategies.JailStrategies
{
    public class RandomlyPay : IJailStrategy
    {
        Random random = new Random();

        private Boolean GetRandomBooleanValue()
        {
            return Convert.ToBoolean(random.Next());
        }
        
        public Boolean ShouldPay(Int32 moneyOnHand)
        {
            return GetRandomBooleanValue();
        }

        public Boolean UseCard()
        {
            return GetRandomBooleanValue();
        }
    }
}