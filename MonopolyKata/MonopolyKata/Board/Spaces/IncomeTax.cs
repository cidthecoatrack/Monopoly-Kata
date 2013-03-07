using System;
using Monopoly.Handlers;
using Monopoly.Players;

namespace Monopoly.Board.Spaces
{
    public class IncomeTax : UnownableSpace
    {
        public const Int16 INCOME_TAX_PERCENTAGE_DIVISOR = 10;
        public const Int16 INCOME_TAX_FLAT_RATE = 200;

        private IBanker banker;

        public IncomeTax(IBanker banker) : base("Income Tax")
        {
            this.banker = banker;
        }

        public override void LandOn(IPlayer player)
        {
            var amountToPay = Math.Min(banker.Money[player] / INCOME_TAX_PERCENTAGE_DIVISOR, INCOME_TAX_FLAT_RATE);
            banker.Pay(player, amountToPay);
        }
    }
}