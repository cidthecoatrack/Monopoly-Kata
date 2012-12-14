using System;
using MonopolyKata.MonopolyPlayer;

namespace MonopolyKata.MonopolyBoard
{
    public class IncomeTax : ISpace
    {
        public String Name { get; private set; }

        public IncomeTax()
        {
            Name = "Income Tax";
        }

        public void LandOn(Player player)
        {
            var amountToPay = Math.Min(player.Money / 10, 200);
            player.Pay(amountToPay);
        }
    }
}