﻿using System;
using Monopoly.Handlers;
using Monopoly.Players;

namespace Monopoly.Board.Spaces
{
    public class IncomeTax : ISpace
    {
        public const Int16 INCOME_TAX_PERCENTAGE_DIVISOR = 10;
        public const Int16 INCOME_TAX_FLAT_RATE = 200;

        private Banker banker;

        public IncomeTax(Banker banker)
        {
            this.banker = banker;
        }

        public void LandOn(Player player)
        {
            var amountToPay = Math.Min(banker.GetMoney(player) / INCOME_TAX_PERCENTAGE_DIVISOR, INCOME_TAX_FLAT_RATE);
            banker.Pay(player, amountToPay);
        }

        public override String ToString()
        {
            return "Income Tax";
        }
    }
}