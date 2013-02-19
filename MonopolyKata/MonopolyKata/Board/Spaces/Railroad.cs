using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopoly.Board.Spaces
{
    public class Railroad : RealEstate
    {
        public Int32 ownedRailroads { get; set; }

        public Railroad(String name) : base(name, 200) { }

        protected override Int32 GetRent()
        {
            return 25 * Convert.ToInt32(Math.Pow(2, ownedRailroads - 1));
        }
    }
}