using System;
using Monopoly.Players.Strategies;

namespace Monopoly.Tests.Players.Strategies.RealEstateStrategies
{
    public class NeverBuy : IRealEstateStrategy
    {
        public Boolean ShouldBuy(Int32 moneyOnHand)
        {
            return true;
        }

        public Boolean ShouldDevelop(Int32 moneyOnHand)
        {
            return false;
        }
    }
}