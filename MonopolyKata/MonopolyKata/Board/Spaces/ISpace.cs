using System;

namespace Monopoly.Board.Spaces
{
    public interface ISpace
    { 
        String Name { get; }

        void LandOn(Player player);
    }
}