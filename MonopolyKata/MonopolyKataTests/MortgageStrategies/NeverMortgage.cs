using System;
using MonopolyKata.MonopolyBoard;
using MonopolyKata.MonopolyPlayer;

namespace MonopolyKataTests
{
    public class NeverMortgage : IMortgageStrategy
    {
        public Boolean SaysIShouldMortgage(Int32 moneyOnHand)
        {
            return false;
        }

        public Boolean SaysIShouldPayOffMortgage(Int32 moneyOnHand, Property property)
        {
            return true;
        }
    }
}
