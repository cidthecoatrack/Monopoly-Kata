using Monopoly.Players.Strategies;
using Monopoly.Tests.Players.Strategies.JailStrategies;
using Monopoly.Tests.Players.Strategies.MortgageStrategies;
using Monopoly.Tests.Players.Strategies.RealEstateStrategies;

namespace Monopoly.Tests.Players.Strategies
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
            MortgageStrategy = new NeverMortgageAlwaysPay();
            RealEstateStrategy = new NeverBuy();
        }

        public void CreateAlwaysStrategyCollection()
        {
            JailStrategy = new AlwaysPay();
            MortgageStrategy = new AlwaysMortgageNeverPay();
            RealEstateStrategy = new AlwaysBuy();
        }
    }
}