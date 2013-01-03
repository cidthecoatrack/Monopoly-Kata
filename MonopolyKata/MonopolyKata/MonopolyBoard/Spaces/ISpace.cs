using System;
using MonopolyKata.MonopolyPlayer;

namespace MonopolyKata.MonopolyBoard.Spaces
{
    public interface ISpace
    { 
        String Name { get; }

        void LandOn(Player player);
    }
}