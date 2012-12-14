using System;
using MonopolyKata.MonopolyBoard;
using MonopolyKata.MonopolyPlayer;

namespace MonopolyKataTests
{
    public class AlwaysMortgage : IMortgageStrategy
    {
        public Boolean SaysIShouldMortgage(Int32 moneyOnHand)
        {
            return true;
        }

        public Boolean SaysIShouldPayOffMortgage(Int32 moneyOnHand, Property property)
        {
            return false;
        }
    }
}
