﻿using System;
using Monopoly.Board.Spaces;
using Monopoly.Strategies;

namespace Monopoly.Tests.Strategies.MortgageStrategies
{
    public class RandomlyMortgage : IMortgageStrategy
    {
        Random random = new Random();
        
        public Boolean ShouldMortgage(Int32 moneyOnHand)
        {
            return Convert.ToBoolean(random.Next());
        }

        public Boolean ShouldPayOffMortgage(Int32 moneyOnHand, RealEstate property)
        {
            return Convert.ToBoolean(random.Next());
        }
    }
}