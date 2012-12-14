using System;
using MonopolyKata.MonopolyPlayer;

namespace MonopolyKata.MonopolyBoard
{
    public interface ISpace
    { 
        String Name { get; }

        void LandOn(Player player);
    }
}