using System;
using MonopolyKata.MonopolyPlayer;

namespace MonopolyKata.MonopolyBoard
{
    public class LuxuryTax : ISpace
    {
        public String Name { get; private set; }

        public LuxuryTax()
        {
            Name = "Luxury Tax";
        }

        public void LandOn(Player player)
        {
            player.Pay(75);
        }
    }
}