using System;
using Monopoly.Strategies;

namespace Monopoly.Tests.Strategies.RealEstateStrategies
{
    public class AlwaysBuy : IRealEstateStrategy
    {
        public Boolean ShouldBuy(Int32 moneyOnHand)
        {
            return true;
        }

        public Boolean ShouldDevelop(Int32 moneyOnHand)
        {
            return true;
        }
    }
}