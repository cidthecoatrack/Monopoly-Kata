using System;
using System.Collections.Generic;
using Monopoly.Cards;
using Monopoly.Players;

namespace Monopoly.Board.Spaces
{
    public class DrawCard : ISpace
    {
        private Queue<ICard> deck;

        private readonly String name;

        public DrawCard(String name)
        {
            this.name = name;
        }

        public void AddDeck(Queue<ICard> deck)
        {
            this.deck = deck;
        }
        
        public void LandOn(Player player)
        {
            while (deck.Peek().Held)
                deck.Enqueue(deck.Dequeue());

            var card = deck.Dequeue();
            card.Execute(player);
            deck.Enqueue(card);
        }

        public override String ToString()
        {
            return name;
        }
    }
}