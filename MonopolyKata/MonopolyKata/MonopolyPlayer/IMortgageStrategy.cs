using System;
using MonopolyKata.MonopolyBoard;

namespace MonopolyKata.MonopolyPlayer
{
    public interface IMortgageStrategy
    {
        Boolean SaysIShouldMortgage(Int32 moneyOnHand);
        Boolean SaysIShouldPayOffMortgage(Int32 moneyOnHand, Property property);
    }
}
