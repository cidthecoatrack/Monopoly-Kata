using System;
using Monopoly.Handlers;
using Monopoly.Players;

namespace Monopoly.Board.Spaces
{
    public class LuxuryTax : UnownableSpace
    {
        public const Int16 LUXURY_TAX = 75;

        private Banker banker;

        public LuxuryTax(Banker banker) : base("Luxury Tax")
        {
            this.banker = banker;
        }

        public override void LandOn(Player player)
        {
            banker.Pay(player, LUXURY_TAX, ToString());
        }
    }
}