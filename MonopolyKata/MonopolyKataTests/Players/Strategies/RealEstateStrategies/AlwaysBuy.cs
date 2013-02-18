using System;
using Monopoly.Players.Strategies;

namespace Monopoly.Tests.Players.Strategies.RealEstateStrategies
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