using System;

namespace Monopoly.Players.Strategies
{
    public interface IRealEstateStrategy
    {
        Boolean ShouldBuy(Int32 moneyOnHand);
        Boolean ShouldDevelop(Int32 moneyOnHand);
    }
}