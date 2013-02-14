using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Handlers;

namespace Monopoly.Cards
{
    public class GoToJailCard : ICard
    {
        public readonly String Name;
        public Boolean Held { get; private set; }

        private JailHandler jailHandler;

        public GoToJailCard(JailHandler jailHandler)
        {
            Name = "Go To Jail";
            this.jailHandler = jailHandler;
        }

        public void Execute(Player player)
        {
            jailHandler.Imprison(player);
        }
    }
}