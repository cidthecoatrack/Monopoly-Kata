using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Strategies;
using Monopoly.Tests.Strategies.JailStrategies;
using Monopoly.Tests.Strategies.MortgageStrategies;
using Monopoly.Tests.Strategies.RealEstateStrategies;

namespace Monopoly.Tests.Strategies
{
    public class StrategyCollection : IStrategyCollection
    {
        public IJailStrategy JailStrategy { get; set; }
        public IMortgageStrategy MortgageStrategy { get; set; }
        public IRealEstateStrategy RealEstateStrategy { get; set; }

        public void CreateRandomStrategyCollection()
        {
            JailStrategy = new RandomlyPay();
            MortgageStrategy = new RandomlyMortgage();
            RealEstateStrategy = new RandomlyBuy();
        }

        public void CreateNeverStrategyCollection()
        {
            JailStrategy = new NeverPay();
            MortgageStrategy = new NeverMortgage();
            RealEstateStrategy = new NeverBuy();
        }
    }
}