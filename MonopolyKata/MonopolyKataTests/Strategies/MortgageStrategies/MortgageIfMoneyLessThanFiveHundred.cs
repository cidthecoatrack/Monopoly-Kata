using System;
using MonopolyKata.MonopolyBoard.Spaces;
using MonopolyKata.Strategies;

namespace MonopolyKataTests.Strategies.MortgageStrategies
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
