using System;
using Monopoly.Players;

namespace Monopoly.Board.Spaces
{
    public abstract class UnownableSpace
    {
        private readonly String name;

        public UnownableSpace(String name)
        {
            this.name = name;
        }

        public abstract void LandOn(IPlayer player);

        public override String ToString()
        {
            return name;
        }
    }
}