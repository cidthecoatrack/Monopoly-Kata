using System;
using Monopoly.Players.Strategies;

namespace Monopoly.Tests.Players.Strategies.OwnableStrategies
{
    public class NeverBuyOrMortgage : IOwnableStrategy
    {
        public Boolean ShouldBuy(Int32 moneyOnHand)
        {
            return true;
        }

        public Boolean ShouldDevelop(Int32 moneyOnHand)
        {
            return false;
        }

        public Boolean ShouldMortgage(int moneyOnHand)
        {
            throw new NotImplementedException();
        }

        public Boolean ShouldPayOffMortgage(int moneyOnHand, Monopoly.Board.Spaces.OwnableSpace property)
        {
            throw new NotImplementedException();
        }
    }
}