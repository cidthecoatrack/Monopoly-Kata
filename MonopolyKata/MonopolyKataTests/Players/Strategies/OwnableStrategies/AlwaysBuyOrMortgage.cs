using System;
using Monopoly.Board.Spaces;
using Monopoly.Players.Strategies;

namespace Monopoly.Tests.Players.Strategies.OwnableStrategies
{
    public class AlwaysBuyOrMortgage : IOwnableStrategy
    {
        public Boolean ShouldBuy(Int32 moneyOnHand) { return true; }

        public Boolean ShouldDevelop(Int32 moneyOnHand) { return true; }

        public Boolean ShouldMortgage(Int32 moneyOnHand) { return true; }

        public Boolean ShouldPayOffMortgage(Int32 moneyOnHand, OwnableSpace property) { return false; }
    }
}