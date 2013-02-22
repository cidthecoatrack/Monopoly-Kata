using System;
using Monopoly.Handlers;
using Monopoly.Players;

namespace Monopoly.Board.Spaces
{
    public class LuxuryTax : ISpace
    {
        public const Int16 LUXURY_TAX = 75;

        private Banker banker;

        public LuxuryTax(Banker banker)
        {
            this.banker = banker;
        }

        public void LandOn(Player player)
        {
            banker.Pay(player, LUXURY_TAX);
        }

        public override String ToString()
        {
            return "Luxury Tax";
        }
    }
}