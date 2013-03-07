using System;
using Monopoly.Handlers;
using Monopoly.Players;

namespace Monopoly.Cards
{
    public class FlatPayCard : ICard
    {
        public Boolean Held { get; private set; }

        private readonly String Name; 
        private readonly Int32 payment;
        private Banker banker;

        public FlatPayCard(String name, Int32 payment, Banker banker)
        {
            Name = name;
            this.payment = payment;
            this.banker = banker;
        }

        public void Execute(Player player)
        {
            banker.Pay(player, payment, ToString());
        }

        public override String ToString()
        {
            return Name;
        }
    }
}