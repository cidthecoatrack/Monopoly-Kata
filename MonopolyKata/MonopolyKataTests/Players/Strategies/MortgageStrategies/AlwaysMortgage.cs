using System;
using Monopoly.Board.Spaces;
using Monopoly.Players.Strategies;

namespace Monopoly.Tests.Players.Strategies.MortgageStrategies
{
    public class AlwaysMortgage : IMortgageStrategy
    {
        public Boolean ShouldMortgage(Int32 moneyOnHand)
        {
            return true;
        }

        public Boolean ShouldPayOffMortgage(Int32 moneyOnHand, RealEstate property)
        {
            return false;
        }
    }
}