using System;
using Monopoly.Handlers;

namespace Monopoly.Cards
{
    public class GetOutOfJailFreeCard : ICard
    {
        public Player Owner { get; private set; }
        public Boolean Held { get; private set; }

        private JailHandler jailHandler;

        public GetOutOfJailFreeCard(JailHandler jailHandler)
        {
            this.jailHandler = jailHandler;
        }

        public void Execute(Player player)
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