using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata.MonopolyBoard.Spaces
{
    public enum GROUPING { PURPLE, LIGHT_BLUE, PINK, GOLD, RED, YELLOW, GREEN, DARK_BLUE }

    public class Property : RealEstate
    {
        private Int32 baseRent;
        public GROUPING Grouping { get; private set; }
        public IEnumerable<Property> PropertiesInGroup { get; private set; }

        public Property(String name, Int32 price, Int32 baseRent, GROUPING grouping) : base(name, price)
        {
            this.baseRent = baseRent;
            Grouping = grouping;
        }

        public void SetPropertiesInGroup(IEnumerable<Property> propertiesInGroup)
        {
            this.PropertiesInGroup = propertiesInGroup;
        }

        protected override Int32 GetRent()
        {
            var ownerOwnsAllInGroup = PropertiesInGroup.All(x => Owner.Owns(x));
            if (ownerOwnsAllInGroup)
                return baseRent * 2;
            return baseRent;
        }
    }
}