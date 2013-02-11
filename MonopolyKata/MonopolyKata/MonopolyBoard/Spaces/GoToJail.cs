using System;
using MonopolyKata.Handlers;
using MonopolyKata.MonopolyPlayer;

namespace MonopolyKata.MonopolyBoard.Spaces
{
    public class GoToJail : ISpace
    {
        public String Name { get; private set; }

        private JailHandler jailHandler;

        public GoToJail(JailHandler jailHandler)
        {
            Name = "Go To Jail";
            this.jailHandler = jailHandler;
        }

        public void LandOn(Player player)
        {
            jailHandler.Imprison(player);
        }
    }
}