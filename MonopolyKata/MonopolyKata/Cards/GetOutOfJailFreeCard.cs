using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Board.Spaces;
using Monopoly.Handlers;

namespace Monopoly.Cards
{
    public class GetOutOfJailFreeCard : ICard
    {
        public readonly String Name;
        public Player Owner { get; private set; }
        public Boolean Held { get; private set; }

        private JailHandler jailHandler;

        public GetOutOfJailFreeCard(JailHandler jailHandler)
        {
            Name = "Get Out Of Jail, Free";
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
    }
}