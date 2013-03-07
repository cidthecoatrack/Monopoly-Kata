using System;
using Monopoly.Handlers;
using Monopoly.Players;

namespace Monopoly.Cards
{
    public class GoToJailCard : ICard
    {
        public Boolean Held { get; private set; }

        private IJailHandler jailHandler;

        public GoToJailCard(IJailHandler jailHandler)
        {
            this.jailHandler = jailHandler;
        }

        public void Execute(IPlayer player)
        {
            jailHandler.Imprison(player);
        }

        public override String ToString()
        {
            return "Go To Jail";
        }
    }
}