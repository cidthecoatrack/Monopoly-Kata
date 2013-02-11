﻿using System;
using Monopoly.Board.Spaces;

namespace Monopoly.Strategies
{
    public interface IMortgageStrategy
    {
        Boolean ShouldMortgage(Int32 moneyOnHand);
        Boolean ShouldPayOffMortgage(Int32 moneyOnHand, RealEstate property);
    }
}