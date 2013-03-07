using System;
using Monopoly.Handlers;
using Monopoly.Players;

namespace Monopoly.Cards
{
    public class GetOutOfJailFreeCard : ICard
    {
        public Boolean Held { get; private set; }

        private IJailHandler jailHandler;

        public GetOutOfJailFreeCard(IJailHandler jailHandler)
        {
            this.jailHandler = jailHandler;
        }

        public void Execute(IPlayer player)
        {
            Held = true;
            jailHandler.AddCardHolder(player, this);
        }

        public void Use()
        {
            Held = false;
        }

        public override String ToString()
        {
            return "Get Out Of Jail, Free";
        }
    }
}