using System;
using Monopoly;

namespace Monopoly.Board.Spaces
{
    public class NormalSpace : ISpace
    {
        public String Name { get; private set; }

        public NormalSpace(String name) { Name = name; }

        public void LandOn(Player player) { }
    }
}