using System;
using Monopoly;

namespace Monopoly.Board.Spaces
{
    public class NormalSpace : ISpace
    {
        private readonly String Name;

        public NormalSpace(String name) { Name = name; }

        public void LandOn(Player player) { }

        public override String ToString()
        {
            return Name;
        }
    }
}