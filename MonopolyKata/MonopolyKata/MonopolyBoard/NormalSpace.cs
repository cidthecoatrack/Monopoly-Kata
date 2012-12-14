using System;
using MonopolyKata.MonopolyPlayer;

namespace MonopolyKata.MonopolyBoard
{
    public class NormalSpace : ISpace
    {
        public String Name { get; private set; }

        public NormalSpace(String name) { Name = name; }

        public void LandOn(Player player) { }
    }
}