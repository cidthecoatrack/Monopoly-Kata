using System;
using Monopoly.Strategies;

namespace Monopoly.Tests.Strategies.RealEstateStrategies
{
    public class RandomlyBuy : IRealEstateStrategy
    {
        Random random = new Random();
        
        public Boolean ShouldBuy(Int32 moneyOnHand)
        {
            return Convert.ToBoolean(random.Next());
        }

        public Boolean ShouldDevelop(Int32 moneyOnHand)
        {
            return Convert.ToBoolean(random.Next());
        }
    }
}