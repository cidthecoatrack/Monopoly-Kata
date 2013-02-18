﻿using System;
using Monopoly.Players;

namespace Monopoly.Cards
{
    public class FlatCollectCard : ICard
    {
        public Boolean Held { get; private set; }

        private readonly String Name; 
        private readonly Int32 payment;
        
        public FlatCollectCard(String name, Int32 payment)
        {
            Name = name;
            this.payment = payment;
        }

        public void Execute(Player player)
        {
            player.Collect(payment);
        }

        public override String ToString()
        {
            return Name;
        }
    }
}