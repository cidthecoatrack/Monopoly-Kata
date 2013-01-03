using System;
using MonopolyKata.MonopolyBoard.Spaces;
using MonopolyKata.Strategies;

namespace MonopolyKataTests.Strategies.MortgageStrategies
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
