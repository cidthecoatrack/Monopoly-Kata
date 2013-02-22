using System;
using Monopoly.Board.Spaces;

namespace Monopoly.Players.Strategies
{
    public interface IMortgageStrategy
    {
        Boolean ShouldMortgage(Int32 moneyOnHand);
        Boolean ShouldPayOffMortgage(Int32 moneyOnHand, OwnableSpace property);
    }
}