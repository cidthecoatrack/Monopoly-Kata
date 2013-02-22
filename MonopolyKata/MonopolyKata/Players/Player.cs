using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Board.Spaces;
using Monopoly.Games;
using Monopoly.Players.Strategies;

namespace Monopoly.Players
{
    public class Player
    {
        public IJailStrategy JailStrategy { get; set; }
        public IMortgageStrategy MortgageStrategy { get; set; }
        public IRealEstateStrategy RealEstateStrategy { get; set; }

        private readonly String name;

        public Player(String name, IStrategyCollection strategies)
        {
            this.name = name;
            MortgageStrategy = strategies.MortgageStrategy;
            JailStrategy = strategies.JailStrategy;
            RealEstateStrategy = strategies.RealEstateStrategy;
        }

        public Boolean WillUseGetOutOfJailCard()
        {
            return JailStrategy.UseCard();
        }

        public override String ToString()
        {
            return name;
        }
    }
}