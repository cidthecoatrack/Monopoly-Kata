using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopoly.Board.Spaces
{
    public enum GROUPING { PURPLE, LIGHT_BLUE, PINK, ORANGE, RED, YELLOW, GREEN, DARK_BLUE }

    public class Property : RealEstate
    {
        public readonly GROUPING Grouping;
        public readonly Int32 HousePrice;
        public Int32 Houses { get; private set; }
        public Boolean PartOfMonopoly { get; set; }

        private readonly Int32 baseRent;
        private readonly IEnumerable<Int32> houseRents;

        public Property(String name, Int32 price, Int32 baseRent, GROUPING grouping, Int32 housePrice, IEnumerable<Int32> houseRents)
            : base(name, price)
        {
            if (houseRents.Count() != 5)
                throw new ArgumentException("Incorrect number of house rents. Should be 5, got " + houseRents.Count());
            
            this.baseRent = baseRent;
            Grouping = grouping;
            HousePrice = housePrice;

            this.houseRents = houseRents.Union(new[] { baseRent * 2 }).OrderBy(r => r);
        }

        protected override Int32 GetRent()
        {
            if (PartOfMonopoly)
                return houseRents.ElementAt(Houses);
            
            return baseRent;
        }

        public void BuyHouse()
        {
            Houses++;
        }

        public void BuyHotel()
        {
            Houses++;
        }

        public override void Mortgage()
        {
            if (Houses > 0)
            {
                Houses--;
                Owner.Collect(HousePrice / 2);
            }
            else if (Houses == 0 && !Mortgaged)
            {
                Mortgaged = true;
                Owner.Collect(Convert.ToInt32(Price * .9));
            }
        }
    }
}