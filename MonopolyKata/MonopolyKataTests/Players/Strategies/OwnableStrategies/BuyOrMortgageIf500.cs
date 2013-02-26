using System;
using Monopoly.Board.Spaces;
using Monopoly.Players.Strategies;

namespace Monopoly.Tests.Players.Strategies.OwnableStrategies
{
    public class BuyOrMortgageIf500 : IOwnableStrategy
    {
        public Boolean ShouldBuy(Int32 moneyOnHand)
        {
            return moneyOnHand >= 500;
        }

        public Boolean ShouldDevelop(Int32 moneyOnHand)
        {
            return moneyOnHand >= 500;
        }

        public Boolean ShouldMortgage(Int32 moneyOnHand)
        {
            return moneyOnHand < 500;
        }

        public Boolean ShouldPayOffMortgage(Int32 moneyOnHand, OwnableSpace property)
        {
            return moneyOnHand - property.Price >= 500;
        }
    }
}