﻿using System;
using System.Collections.Generic;
using Monopoly.Cards;
using Monopoly.Players;

namespace Monopoly.Board.Spaces
{
    public class DrawCard : UnownableSpace
    {
        private Queue<ICard> deck;

        public DrawCard(String name) : base(name) { }

        public void AddDeck(Queue<ICard> deck)
        {
            this.deck = deck;
        }
        
        public override void LandOn(IPlayer player)
        {
            while (deck.Peek().Held)
                deck.Enqueue(deck.Dequeue());

            var card = deck.Dequeue();
            card.Execute(player);
            deck.Enqueue(card);
        }
    }
}