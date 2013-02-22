using System;
using Monopoly.Handlers;
using Monopoly.Players;

namespace Monopoly.Cards
{
    public class FlatCollectCard : ICard
    {
        public Boolean Held { get; private set; }

        private readonly String Name; 
        private readonly Int32 payment;
        private Banker banker;
        
        public FlatCollectCard(String name, Int32 payment, Banker banker)
        {
            Name = name;
            this.payment = payment;
            this.banker = banker;
        }

        public void Execute(Player player)
        {
            banker.Collect(player, payment);
        }

        public override String ToString()
        {
            return Name;
        }
    }
}