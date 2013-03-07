using System;
using Monopoly.Handlers;
using Monopoly.Players;

namespace Monopoly.Cards
{
    public class FlatCollectCard : ICard
    {
        public Boolean Held { get; private set; }

        private readonly String name; 
        private readonly Int32 payment;
        private IBanker banker;
        
        public FlatCollectCard(String name, Int32 payment, IBanker banker)
        {
            this.name = name;
            this.payment = payment;
            this.banker = banker;
        }

        public void Execute(IPlayer player)
        {
            banker.Collect(player, payment);
        }

        public override String ToString()
        {
            return name;
        }
    }
}