using System;
using Monopoly.Players.Strategies;

namespace Monopoly.Tests.Players.Strategies.RealEstateStrategies
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