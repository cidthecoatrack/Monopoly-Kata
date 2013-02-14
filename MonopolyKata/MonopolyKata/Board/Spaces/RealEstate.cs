using System;
using Monopoly;

namespace Monopoly.Board.Spaces
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
                TryToBuyMe(player);
            else
                MakePlayerPayRent(player);
        }

        protected void TryToBuyMe(Player player)
        {
            player.Buy(this);

            if (player.Owns(this))
            {
                Mortgaged = false;
                Owner = player;
            }
        }

        protected void MakePlayerPayRent(Player player)
        {
            var rent = GetRent();
            player.Pay(rent);
            Owner.Collect(rent);
        }

        protected abstract Int32 GetRent();

        public virtual void Mortgage()
        {
            if (Owned && !Mortgaged)
            {
                Mortgaged = true;
                Owner.Collect(Convert.ToInt32(Price * .9));
            }
        }

        public void PayOffMortgage()
        {
            if (Owned && Mortgaged)
            {
                Owner.Pay(Price);
                Mortgaged = false;
            }
        }
    }
}