﻿using System;
using Monopoly.Players;

namespace Monopoly.Cards
{
    public class FlatPayCard : ICard
    {
        public Boolean Held { get; private set; }

        private readonly String Name; 
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

        public override String ToString()
        {
            return Name;
        }
    }
}