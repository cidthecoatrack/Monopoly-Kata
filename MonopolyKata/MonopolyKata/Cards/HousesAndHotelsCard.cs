using System;
using Monopoly.Handlers;
using Monopoly.Players;

namespace Monopoly.Cards
{
    public class HousesAndHotelsCard : ICard
    {
        public Boolean Held { get; private set; }

        private readonly String Name; 
        private readonly Int32 houseCost;
        private readonly Int32 hotelPremium;
        private RealEstateHandler realEstateHandler;

        public HousesAndHotelsCard(String name, Int32 houseCost, Int32 hotelCost, RealEstateHandler realEstateHandler)
        {
            Name = name;
            this.houseCost = houseCost;
            hotelPremium = hotelCost - houseCost;
            this.realEstateHandler = realEstateHandler;
        }

        public void Execute(Player player)
        {
            var houses = realEstateHandler.GetHouses(player);
            var hotels = realEstateHandler.GetHotels(player);

            var payment = houses * houseCost + hotels * hotelPremium;
            player.Pay(payment);
        }

        public override String ToString()
        {
            return Name;
        }
    }
}