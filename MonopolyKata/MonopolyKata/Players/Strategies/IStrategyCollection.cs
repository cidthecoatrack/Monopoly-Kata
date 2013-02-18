
namespace Monopoly.Players.Strategies
{
    public interface IStrategyCollection
    {
        IJailStrategy JailStrategy { get; }
        IMortgageStrategy MortgageStrategy { get; }
        IRealEstateStrategy RealEstateStrategy { get; }
    }
}