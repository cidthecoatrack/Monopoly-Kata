using System;
using MonopolyKata.MonopolyPlayer;

namespace MonopolyKata.MonopolyBoard.Spaces
{
    public abstract class RealEstate : ISpace
    {
        public Boolean Mortgaged { get; protected set; }
        public String Name { get; protected set; }
        public Boolean Owned { get { return Owner != null && !Owner.LostTheGame; } }
        public Player Owner { get; protected set; }
        public Int32 Price { get; protected set; }

        public RealEstate(String name, Int32 price)
        {
            Name = name;
            Price = price;
            Mortgaged = false;
        }

        public void LandOn(Player player)
        {
            if (!Owned)
                SeeIfPlayerCanBuyMe(player);
            else
                MakePlayerPayRent(player);
        }

        protected void SeeIfPlayerCanBuyMe(Player player)
        {
            if (player.CanAfford(Price))
                BuyMe(player);
        }

        protected void BuyMe(Player player)
        {
            player.Buy(this);
            Mortgaged = false;
            Owner = player;
        }

        protected void MakePlayerPayRent(Player player)
        {
            var rent = GetRent();
            player.Pay(rent);
            Owner.ReceiveMoney(rent);
        }

        protected abstract Int32 GetRent();

        public void Mortgage()
        {
            if (Owned && !Mortgaged)
            {
                Mortgaged = true;
                Owner.ReceiveMoney(Convert.ToInt32(Price * .9));
            }
        }

        public void PayOffMortgage()
        {
            if (Owned && Mortgaged && Owner.CanAfford(Price))
            {
                Owner.Pay(Price);
                Mortgaged = false;
            }
        }
    }
}