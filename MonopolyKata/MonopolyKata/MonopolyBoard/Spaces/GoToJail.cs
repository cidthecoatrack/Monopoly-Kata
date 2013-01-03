using System;
using MonopolyKata.MonopolyPlayer;

namespace MonopolyKata.MonopolyBoard.Spaces
{
    public class GoToJail : ISpace
    {
        public String Name { get; private set; }

        public GoToJail()
        {
            Name = "Go To Jail";
        }

        public void LandOn(Player player)
        {
            player.GoToJail();
        }
    }
}