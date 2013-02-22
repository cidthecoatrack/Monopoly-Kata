using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopoly.Board.Spaces
{
    public class Railroad : OwnableSpace
    {
        public Int32 RailroadCount { get; set; }

        public Railroad(String name) : base(name, 200) { }

        public override Int32 GetRent()
        {
            return 25 * Convert.ToInt32(Math.Pow(2, RailroadCount - 1));
        }
    }
}