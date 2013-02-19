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
        public Int32 Money { get; private set; }
        public Boolean LostTheGame { get { return (Money < 0); } }
        public IJailStrategy JailStrategy { get; set; }
        public IMortgageStrategy MortgageStrategy { get; set; }
        public IRealEstateStrategy RealEstateStrategy { get; set; }

        private readonly String name;

        public Player(String name, IStrategyCollection strategies)
        {
            this.name = name;
            Money = 1500;

            MortgageStrategy = strategies.MortgageStrategy;
            JailStrategy = strategies.JailStrategy;
            RealEstateStrategy = strategies.RealEstateStrategy;
        }

        public void Pay(Int32 amountToPay)
        {
            Money -= amountToPay;
        }

        public Boolean CanAfford(Int32 amountToPay)
        {
            return Money >= amountToPay;
        }

        public void Collect(Int32 amountToCollect)
        {
            Money += amountToCollect;
        }

        public Boolean WillPayToGetOutOfJail()
        {
            return JailStrategy.ShouldPay(Money) && CanAfford(GameConstants.COST_TO_GET_OUT_OF_JAIL);
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