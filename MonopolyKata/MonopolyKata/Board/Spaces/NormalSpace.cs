using System;
using Monopoly.Players;

namespace Monopoly.Board.Spaces
{
    public class NormalSpace : UnownableSpace
    {
        private readonly String Name;

        public NormalSpace(String name) { Name = name; }

        public override void LandOn(Player player) { }

        public override String ToString()
        {
            return Name;
        }
    }
}