using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopoly.Board.Spaces
{
    public enum GROUPING { PURPLE, LIGHT_BLUE, PINK, GOLD, RED, YELLOW, GREEN, DARK_BLUE }

    public class Property : RealEstate
    {
        private Int32 baseRent;
        private List<Int32> houseRents;

        public GROUPING Grouping { get; private set; }
        public IEnumerable<Property> PropertiesInGroup { get; private set; }
        public Int32 HousePrice { get; private set; }
        public Int32 HouseCount { get; private set; }

        public Property(String name, Int32 price, Int32 baseRent, GROUPING grouping, Int32 housePrice) : base(name, price)
        {
            this.baseRent = baseRent;
            Grouping = grouping;
            HousePrice = housePrice;
        }

        public void SetPropertiesInGroup(IEnumerable<Property> propertiesInGroup)
        {
            this.PropertiesInGroup = propertiesInGroup;
        }

        public void SetHouseRents(IEnumerable<Int32> houseRents)
        {
            houseRents.Add(baseRent * 2);
            foreach (var rent in houseRents)
                this.houseRents.Add(rent);
        }

        protected override Int32 GetRent()
        {
            if (OwnerOwnsAllInGroup())
                return houseRents[HouseCount];
            
            return baseRent;
        }

        public void BuyHouse()
        {
            if (OwnerOwnsAllInGroup() && EvenBuildAllowsANewHouseHere())
            {
                Owner.Pay(HousePrice);
                HouseCount++;
            }
        }

        private Boolean EvenBuildAllowsANewHouseHere()
        {
            var minimumHouses = PropertiesInGroup.Min(x => x.HouseCount);
            return HouseCount == minimumHouses;
        }

        private Boolean OwnerOwnsAllInGroup()
        {
            return PropertiesInGroup.All(x => Owner.Owns(x));
        }
    }
}