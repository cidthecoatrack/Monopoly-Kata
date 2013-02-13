using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Board.Spaces;
using Monopoly.Strategies;

namespace Monopoly.Tests.Strategies.RealEstateStrategies
{
    public class BuyIfAtLeast500OnHand : IRealEstateStrategy
    {
        public Boolean ShouldBuy(Int32 moneyOnHand)
        {
            return moneyOnHand >= 500;
        }

        public Boolean ShouldDevelop(Int32 moneyOnHand)
        {
            return moneyOnHand >= 500;
        }
    }
}