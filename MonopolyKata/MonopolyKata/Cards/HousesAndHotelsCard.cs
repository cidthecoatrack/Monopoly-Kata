using System;
using Monopoly.Handlers;
using Monopoly.Players;

namespace Monopoly.Cards
{
    public class HousesAndHotelsCard : ICard
    {
        public Boolean Held { get; private set; }

        private readonly String name; 
        private readonly Int32 houseCost;
        private readonly Int32 hotelPremium;
        private OwnableHandler realEstateHandler;
        private Banker banker;

        public HousesAndHotelsCard(String name, Int32 houseCost, Int32 hotelCost, OwnableHandler realEstateHandler, Banker banker)
        {
            this.name = name;
            this.houseCost = houseCost;
            hotelPremium = hotelCost - houseCost;
            this.realEstateHandler = realEstateHandler;
            this.banker = banker;
        }

        public void Execute(Player player)
        {
            var houses = realEstateHandler.GetNumberOfHouses(player);
            var hotels = realEstateHandler.GetNumberOfHotels(player);

            var payment = houses * houseCost + hotels * hotelPremium;
            banker.Pay(player, payment, ToString());
        }

        public override String ToString()
        {
            return name;
        }
    }
}