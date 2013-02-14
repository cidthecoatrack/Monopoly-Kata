using System;
using Monopoly.Strategies;

namespace Monopoly.Tests.Strategies.JailStrategies
{
    public class RandomlyPay : IJailStrategy
    {
        Random random = new Random();
        
        public Boolean ShouldPay(Int32 moneyOnHand)
        {
            return Convert.ToBoolean(random.Next());
        }

        public Boolean UseCard()
        {
            return Convert.ToBoolean(random.Next());
        }
    }
}