using System;
using System.Collections.Generic;
using MonopolyKata.MonopolyPlayer;

namespace MonopolyKata.MonopolyBoard
{
    public enum GROUPING { PURPLE, LIGHT_BLUE, PINK, GOLD, RED, YELLOW, GREEN, DARK_BLUE }

    public class Property : ISpace
    {
        private Int32 baseRent;
        public GROUPING Grouping { get; private set; }
        public IEnumerable<Property> PropertiesInGroup { get; private set; }
        public Boolean Mortgaged { get; protected set; }
        public String Name { get; protected set; }
        public Boolean Owned { get { return Owner != null && !Owner.LostTheGame; } }
        public Player Owner { get; protected set; }
        public Int32 Price { get; protected set; }

        protected Property() { }

        public Property(String name, Int32 price, Int32 baseRent, GROUPING grouping)
        {
            Name = name;
            Price = price;
            this.baseRent = baseRent;
            this.Grouping = grouping;
            Mortgaged = false;
        }

        public virtual void SetPropertiesInGroup(IEnumerable<Property> propertiesInGroup)
        {
            this.PropertiesInGroup = propertiesInGroup;
        }

        public void Reset()
        {
            Mortgaged = false;
            Owner = null;
        }

        public void LandOn(Player player)
        {
            if (!Owned)
                SeeIfPlayerCanBuyMe(player);
            else if (!Owner.Equals(player))
                MakePlayerPayRent(player);
        }

        protected virtual void MakePlayerPayRent(Player player)
        {
            var rent = GetRent();
            player.Pay(rent);
            Owner.ReceiveMoney(rent);
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

        public virtual Int32 GetRent()
        {
            var ownerOwnsAllInGroup = true;
            foreach (var property in PropertiesInGroup)
                if (!Owner.Owns(property))
                    ownerOwnsAllInGroup = false;

            if (ownerOwnsAllInGroup)
                return baseRent * 2;
            return baseRent;
        }

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