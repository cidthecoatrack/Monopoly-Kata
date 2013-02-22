using System;
using Monopoly.Players;

namespace Monopoly.Board.Spaces
{
    public abstract class OwnableSpace
    {
        public Boolean Mortgaged { get; set; }
        public Int32 Price { get; protected set; }

        private readonly String name;

        public OwnableSpace(String name, Int32 price)
        {
            this.name = name;
            Price = price;
            Mortgaged = false;
        }

        public abstract Int32 GetRent();

        public override String ToString()
        {
            return name;
        }
    }
}