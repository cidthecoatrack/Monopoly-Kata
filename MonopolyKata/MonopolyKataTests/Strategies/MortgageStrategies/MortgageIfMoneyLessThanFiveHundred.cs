using System;
using Monopoly.Board.Spaces;
using Monopoly.Strategies;

namespace Monopoly.Tests.Strategies.MortgageStrategies
{
    public class MortgageIfMoneyLessThanFiveHundred : IMortgageStrategy
    {
        public Boolean ShouldMortgage(Int32 moneyOnHand)
        {
            return moneyOnHand < 500;
        }

        public Boolean ShouldPayOffMortgage(Int32 moneyOnHand, RealEstate property)
        {
            return moneyOnHand - property.Price >= 500;
        }
    }
}