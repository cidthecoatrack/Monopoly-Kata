using System;
using Monopoly;

namespace Monopoly.Board.Spaces
{
    public class LuxuryTax : ISpace
    {
        public const Int16 LUXURY_TAX = 75;

        public void LandOn(Player player)
        {
            player.Pay(LUXURY_TAX);
        }

        public override String ToString()
        {
            return "Luxury Tax";
        }
    }
}