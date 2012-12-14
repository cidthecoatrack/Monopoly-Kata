using System;
using MonopolyKata.MonopolyPlayer;

namespace MonopolyKata.MonopolyBoard
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
            player.SetPosition(Board.JAIL_OR_JUST_VISITING);
        }
    }
}