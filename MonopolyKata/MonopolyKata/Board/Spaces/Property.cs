using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopoly.Board.Spaces
{
    public enum GROUPING { PURPLE, LIGHT_BLUE, PINK, ORANGE, RED, YELLOW, GREEN, DARK_BLUE }

    public class Property : RealEstate
    {
        public readonly GROUPING Grouping;

        private Int32 baseRent;
        private List<Int32> houseRents;
        private Int32 houses;
        private IEnumerable<Property> propertiesInGroup;
        private Int32 housePrice;

        public Property(String name, Int32 price, Int32 baseRent, GROUPING grouping, Int32 housePrice, IEnumerable<Int32> houseRents)
            : base(name, price)
        {
            if (houseRents.Count() != 5)
                throw new ArgumentException("Incorrect number of house rents. Should be 5, got " + houseRents.Count());
            
            this.baseRent = baseRent;
            Grouping = grouping;
            this.housePrice = housePrice;

            this.houseRents = new List<Int32>();
            this.houseRents.Add(baseRent * 2);
            this.houseRents.AddRange(houseRents);
        }

        public void SetPropertiesInGroup(IEnumerable<Property> propertiesInGroup)
        {
            this.propertiesInGroup = propertiesInGroup;
        }

        protected override Int32 GetRent()
        {
            if (OwnerOwnsAllInGroup())
                return houseRents[houses];
            
            return baseRent;
        }

        public void BuyHouse()
        {
            if (OwnerOwnsAllInGroup() && Owner.CanAfford(housePrice) && 
                !AnyPropertiesInGroupAreMortgaged() && EvenBuildAllowsANewHouseHere() && 
                houses < 4)
            {
                Owner.Pay(housePrice);
                houses++;
            }
        }

        private Boolean AnyPropertiesInGroupAreMortgaged()
        {
            return propertiesInGroup.Any(x => x.Mortgaged);
        }

        private Boolean EvenBuildAllowsANewHouseHere()
        {
            return houses == propertiesInGroup.Min(x => x.houses);
        }

        private Boolean OwnerOwnsAllInGroup()
        {
            return Owned && propertiesInGroup.All(x => Owner.Owns(x));
        }

        public void BuyHotel()
        {
            if (houses == 4 && Owner.CanAfford(housePrice) && EvenBuildAllowsANewHouseHere())
            {
                Owner.Pay(housePrice);
                houses++;
            }
        }

        public override void Mortgage()
        {
            if (houses > 0 && EvenBuildAllowsSellingHouse())
            {
                houses--;
                Owner.ReceiveMoney(housePrice / 2);
            }
            else if (houses == 0 && Owned && !Mortgaged && !AnyPropertiesInGroupHaveHouses())
            {
                Mortgaged = true;
                Owner.ReceiveMoney(Convert.ToInt32(Price * .9));
            }
        }

        private Boolean AnyPropertiesInGroupHaveHouses()
        {
            return propertiesInGroup.Any(x => x.houses > 0);
        }

        private Boolean EvenBuildAllowsSellingHouse()
        {
            return houses == propertiesInGroup.Max(x => x.houses);
        }
    }
}