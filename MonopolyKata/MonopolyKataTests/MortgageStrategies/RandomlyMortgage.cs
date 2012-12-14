using System;
using MonopolyKata.MonopolyBoard;
using MonopolyKata.MonopolyPlayer;

namespace MonopolyKataTests.MortgageStrategies
{
    public class RandomlyMortgage : IMortgageStrategy
    {
        public Boolean SaysIShouldMortgage(Int32 moneyOnHand)
        {
            return Convert.ToBoolean(new DiceForTesting().RollUnboundedRandomNumber());
        }

        public Boolean SaysIShouldPayOffMortgage(Int32 moneyOnHand, Property property)
        {
            return Convert.ToBoolean(new DiceForTesting().RollUnboundedRandomNumber());
        }
    }
}
