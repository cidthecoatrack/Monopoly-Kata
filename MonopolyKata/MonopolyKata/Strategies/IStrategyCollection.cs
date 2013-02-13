using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Strategies
{
    public interface IStrategyCollection
    {
        IJailStrategy JailStrategy { get; }
        IMortgageStrategy MortgageStrategy { get; }
        IRealEstateStrategy RealEstateStrategy { get; }
    }
}