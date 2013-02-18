using System;
using Monopoly.Handlers;

namespace Monopoly.Cards
{
    public class GoToJailCard : ICard
    {
        public Boolean Held { get; private set; }

        private JailHandler jailHandler;

        public GoToJailCard(JailHandler jailHandler)
        {
            this.jailHandler = jailHandler;
        }

        public void Execute(Player player)
        {
            jailHandler.Imprison(player);
        }

        public override String ToString()
        {
            return "Go To Jail";
        }
    }
}