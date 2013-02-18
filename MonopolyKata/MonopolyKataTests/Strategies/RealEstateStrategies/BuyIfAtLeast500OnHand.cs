using System;
using Monopoly.Strategies;

namespace Monopoly.Tests.Strategies.RealEstateStrategies
{
    public class BuyIfAtLeast500OnHand : IRealEstateStrategy
    {
        public Boolean ShouldBuy(Int32 moneyOnHand)
        {
            return moneyOnHand >= 500;
        }

        public Boolean ShouldDevelop(Int32 moneyOnHand)
        {
            return moneyOnHand >= 500;
        }
    }
}