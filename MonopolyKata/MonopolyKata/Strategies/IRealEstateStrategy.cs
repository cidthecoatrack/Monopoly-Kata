using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Board.Spaces;

namespace Monopoly.Strategies
{
    public interface IRealEstateStrategy
    {
        Boolean ShouldBuy(Int32 moneyOnHand);
        Boolean ShouldDevelop(Int32 moneyOnHand);
    }
}