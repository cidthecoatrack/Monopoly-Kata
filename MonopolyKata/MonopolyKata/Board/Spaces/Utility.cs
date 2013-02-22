using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Dice;

namespace Monopoly.Board.Spaces
{
    public class Utility : RealEstate
    {
        public Boolean Force10xRent { get; set; }
        public Boolean BothUtilitiesOwned { get; set; }

        private IEnumerable<Utility> utilities;
        private IDice dice;

        public Utility(String name, IDice dice) : base(name, 150)
        {
            this.dice = dice;
        }

        public override Int32 GetRent()
        {
            if (BothUtilitiesOwned || Force10xRent)
                return 10 * dice.Value;
            return 4 * dice.Value;
        }
    }
}