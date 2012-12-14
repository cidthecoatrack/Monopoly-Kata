using System;
using MonopolyKata.MonopolyBoard;
using MonopolyKata.MonopolyPlayer;

namespace MonopolyKataTests
{
    public class MortgageIfMoneyLessThanFiveHundred : IMortgageStrategy
    {
        public Boolean SaysIShouldMortgage(Int32 moneyOnHand)
        {
            return moneyOnHand < 500;
        }

        public Boolean SaysIShouldPayOffMortgage(Int32 moneyOnHand, Property property)
        {
            return moneyOnHand - property.Price >= 500;
        }
    }
}
