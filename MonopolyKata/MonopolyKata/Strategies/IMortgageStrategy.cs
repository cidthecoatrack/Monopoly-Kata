using System;
using MonopolyKata.MonopolyBoard.Spaces;

namespace MonopolyKata.Strategies
{
    public interface IMortgageStrategy
    {
        Boolean ShouldMortgage(Int32 moneyOnHand);
        Boolean ShouldPayOffMortgage(Int32 moneyOnHand, RealEstate property);
    }
}