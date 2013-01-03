using System;
using MonopolyKata.MonopolyBoard.Spaces;
using MonopolyKata.Strategies;

namespace MonopolyKataTests.Strategies.MortgageStrategies
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
