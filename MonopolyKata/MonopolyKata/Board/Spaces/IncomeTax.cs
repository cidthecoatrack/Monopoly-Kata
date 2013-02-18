﻿using System;
using Monopoly;

namespace Monopoly.Board.Spaces
{
    public class IncomeTax : ISpace
    {
        public const Int16 INCOME_TAX_PERCENTAGE_DIVISOR = 10;
        public const Int16 INCOME_TAX_FLAT_RATE = 200;

        public void LandOn(Player player)
        {
            var amountToPay = Math.Min(player.Money / INCOME_TAX_PERCENTAGE_DIVISOR, INCOME_TAX_FLAT_RATE);
            player.Pay(amountToPay);
        }

        public override String ToString()
        {
            return "Income Tax";
        }
    }
}