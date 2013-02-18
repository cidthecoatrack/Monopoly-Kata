using System;

namespace Monopoly.Cards
{
    public class HousesAndHotelsCard : ICard
    {
        public Boolean Held { get; private set; }

        private readonly String Name; 
        private readonly Int32 houseCost;
        private readonly Int32 hotelPremium;

        public HousesAndHotelsCard(String name, Int32 houseCost, Int32 hotelCost)
        {
            Name = name;
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

        public override String ToString()
        {
            return Name;
        }
    }
}