﻿using System;
using Monopoly.Board.Spaces;

namespace Monopoly.Players.Strategies
{
    public interface IOwnableStrategy
    {
        Boolean ShouldBuy(Int32 moneyOnHand);
        Boolean ShouldDevelop(Int32 moneyOnHand); 
        Boolean ShouldMortgage(Int32 moneyOnHand);
        Boolean ShouldPayOffMortgage(Int32 moneyOnHand, OwnableSpace property);
    }
}