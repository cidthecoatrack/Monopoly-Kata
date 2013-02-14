using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
    public class HousesAndHotelsCard : ICard
    {
        public Boolean Held {get; private set;}
        public readonly String Name;

        private readonly Int32 houseCost;
        private readonly Int32 hotelPremium;

        public HousesAndHotelsCard(Int32 houseCost, Int32 hotelCost)
        {
            Name = "You Are Assessed For Street Repairs";
            this.houseCost = houseCost;
            hotelPremium = hotelCost - houseCost;
        }

        public void Execute(Player player)
        {
            var houses = player.GetNumberOfHouses();
            var hotels = player.GetNumberOfHotels();

            var payment = houses * houseCost + hotels * hotelPremium;
            player.Pay(payment);
        }
    }
}