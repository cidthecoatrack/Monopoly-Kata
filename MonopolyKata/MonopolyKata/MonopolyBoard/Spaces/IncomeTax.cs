using System;
using MonopolyKata.MonopolyPlayer;

namespace MonopolyKata.MonopolyBoard.Spaces
{
    public class IncomeTax : ISpace
    {
        public String Name { get; private set; }

        public const Double INCOME_TAX_PERCENTAGE = .1;
        public const Int16 INCOME_TAX_FLAT_RATE = 200;

        public IncomeTax()
        {
            Name = "Income Tax";
        }

        public void LandOn(Player player)
        {
            var amountToPay = Convert.ToInt32(Math.Min(player.Money * INCOME_TAX_PERCENTAGE, INCOME_TAX_FLAT_RATE));
            player.Pay(amountToPay);
        }
    }
}