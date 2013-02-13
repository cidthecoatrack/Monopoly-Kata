using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Board.Spaces;
using Monopoly.Strategies;

namespace Monopoly.Tests.Strategies.RealEstateStrategies
{
    public class RandomlyBuy : IRealEstateStrategy
    {
        Random random = new Random();
        
        public Boolean ShouldBuy(Int32 moneyOnHand)
        {
            return Convert.ToBoolean(random.Next());
        }

        public Boolean ShouldDevelop(Int32 moneyOnHand)
        {
            return Convert.ToBoolean(random.Next());
        }
    }
}