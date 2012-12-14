using System;
using System.Collections.Generic;

namespace MonopolyKata.MonopolyBoard
{
    public class Railroad : Property
    {
        private IEnumerable<Railroad> railroads;

        public Railroad(String name)
        {
            Name = name;
            Price = 200;
            Mortgaged = false;
        }

        public override void SetPropertiesInGroup(IEnumerable<Property> railroads)
        {
            this.railroads = railroads as IEnumerable<Railroad>;
        }

        public override Int32 GetRent()
        {
            var RailroadCount = -1;

            foreach (var railroad in railroads)
                if (Owner.Owns(railroad))
                    RailroadCount++;

            return 25 * (Int32)Math.Pow(2, RailroadCount);
        }
    }
}