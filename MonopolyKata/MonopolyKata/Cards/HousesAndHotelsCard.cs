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

        public HousesAndHotelsCard()
        {
            Name = "You Are Assessed For Street Repairs";
        }

        public void Execute(Player player)
        {
            var houses = player.GetNumberOfHouses();
            var hotels = player.GetNumberOfHotels();

            var payment = houses * 40 + hotels * 75;
            player.Pay(payment);
        }
    }
}