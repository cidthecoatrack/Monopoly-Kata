using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
    public class FlatPayCard : ICard
    {
        public readonly String Name;
        public Boolean Held { get; private set; }

        private readonly Int32 payment;

        public FlatPayCard(String name, Int32 payment)
        {
            Name = name;
            this.payment = payment;
        }

        public void Execute(Player player)
        {
            player.Pay(payment);
        }
    }
}