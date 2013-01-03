using System;
using MonopolyKata.MonopolyBoard.Spaces;
using MonopolyKata.Strategies;

namespace MonopolyKataTests.Strategies.MortgageStrategies
{
    public class RandomlyMortgage : IMortgageStrategy
    {
        public Boolean ShouldMortgage(Int32 moneyOnHand)
        {
            return Convert.ToBoolean(new DiceForTesting().RollUnboundedRandomNumber());
        }

        public Boolean ShouldPayOffMortgage(Int32 moneyOnHand, RealEstate property)
        {
            return Convert.ToBoolean(new DiceForTesting().RollUnboundedRandomNumber());
        }
    }
}
