using System;
using Monopoly.Handlers;
using Monopoly;

namespace Monopoly.Board.Spaces
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