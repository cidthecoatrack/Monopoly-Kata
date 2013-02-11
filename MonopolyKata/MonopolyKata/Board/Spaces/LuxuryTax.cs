using System;
using Monopoly;

namespace Monopoly.Board.Spaces
{
    public class LuxuryTax : ISpace
    {
        public String Name { get; private set; }
        public const Int16 LUXURY_TAX = 75;

        public LuxuryTax()
        {
            Name = "Luxury Tax";
        }

        public void LandOn(Player player)
        {
            player.Pay(LUXURY_TAX);
        }
    }
}