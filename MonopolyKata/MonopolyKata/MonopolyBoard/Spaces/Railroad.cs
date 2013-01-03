using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata.MonopolyBoard.Spaces
{
    public class Railroad : RealEstate
    {
        private IEnumerable<Railroad> railroads;

        public Railroad(String name) : base(name, 200) { }

        public void SetRailroads(IEnumerable<Railroad> railroads)
        {
            this.railroads = railroads;
        }

        protected override Int32 GetRent()
        {
            var RailroadCount = railroads.Count(x => Owner.Owns(x));
            return 25 * Convert.ToInt32(Math.Pow(2, RailroadCount - 1));
        }
    }
}