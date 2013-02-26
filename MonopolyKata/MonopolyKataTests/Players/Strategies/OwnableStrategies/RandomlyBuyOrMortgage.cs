using System;
using Monopoly.Board.Spaces;
using Monopoly.Players.Strategies;

namespace Monopoly.Tests.Players.Strategies.OwnableStrategies
{
    public class RandomlyBuyOrMortgage : IOwnableStrategy
    {
        Random random = new Random();

        private Boolean GetRandomBooleanValue()
        {
            return Convert.ToBoolean(random.Next());
        }
        
        public Boolean ShouldBuy(Int32 moneyOnHand)
        {
            return GetRandomBooleanValue();
        }

        public Boolean ShouldDevelop(Int32 moneyOnHand)
        {
            return GetRandomBooleanValue();
        }

        public Boolean ShouldMortgage(Int32 moneyOnHand)
        {
            return GetRandomBooleanValue();
        }

        public Boolean ShouldPayOffMortgage(Int32 moneyOnHand, OwnableSpace property)
        {
            return GetRandomBooleanValue();
        }
    }
}