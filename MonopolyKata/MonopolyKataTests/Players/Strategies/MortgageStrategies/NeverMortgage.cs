using System;
using Monopoly.Board.Spaces;
using Monopoly.Players.Strategies;

namespace Monopoly.Tests.Players.Strategies.MortgageStrategies
{
    public class NeverMortgage : IMortgageStrategy
    {
        public Boolean ShouldMortgage(Int32 moneyOnHand)
        {
            return false;
        }

        public Boolean ShouldPayOffMortgage(Int32 moneyOnHand, RealEstate property)
        {
            return true;
        }
    }
}