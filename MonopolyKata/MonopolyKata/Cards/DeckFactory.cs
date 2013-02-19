using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Board;
using Monopoly.Dice;
using Monopoly.Handlers;
using Monopoly.Players;

namespace Monopoly.Cards
{
    public class DeckFactory
    {
        private Random random;
        private JailHandler jailHandler;
        private IEnumerable<Player> players;
        private BoardHandler boardHandler;

        public DeckFactory(JailHandler jailHandler, IEnumerable<Player> players, BoardHandler boardHandler)
        {
            random = new Random();
            this.jailHandler = jailHandler;
            this.players = players;
            this.boardHandler = boardHandler;
        }
        
        public Queue<ICard> BuildCommunityChestDeck()
        {
            var deck = new List<ICard>();

            deck.Add(new FlatPayCard("Doctor's Fee", 50));
            deck.Add(new FlatCollectCard("Christmas Fund Matures", 100));
            deck.Add(new GetOutOfJailFreeCard(jailHandler));
            deck.Add(new CollectFromAllPlayersCard(players));
            deck.Add(new FlatCollectCard("You Inherit", 100));
            deck.Add(new FlatCollectCard("Receive Services Fees", 25));
            deck.Add(new FlatCollectCard("Income Tax Refund", 20));
            deck.Add(new FlatCollectCard("Sell Stock", 45));
            deck.Add(new FlatPayCard("Pay School Tax", 150));
            deck.Add(new HousesAndHotelsCard("You Are Assessed For Street Repairs", 40, 115));
            deck.Add(new FlatCollectCard("Bank Error In Your Favor", 200));
            deck.Add(new MoveAndPassGoCard("Advance To Go", BoardConstants.GO, boardHandler));
            deck.Add(new FlatCollectCard("Life Insurance Matures", 50));
            deck.Add(new FlatPayCard("Pay Hospital", 50));
            deck.Add(new FlatCollectCard("You Have Won Second Prize in a Beauty Contest", 10));
            deck.Add(new GoToJailCard(jailHandler));

            var shuffledDeck = deck.OrderBy(x => random.Next());
            return new Queue<ICard>(shuffledDeck);
        }

        public Queue<ICard> BuildChanceDeck(IDice dice)
        {
            var deck = new List<ICard>();

            deck.Add(new MoveAndPassGoCard("Advance To Go", BoardConstants.GO, boardHandler));
            deck.Add(new FlatCollectCard("Bank Pays You Dividend", 50));
            deck.Add(new MoveBackThreeCard(boardHandler));
            deck.Add(new MoveToNearestUtilityCard(boardHandler, dice));
            deck.Add(new GoToJailCard(jailHandler));
            deck.Add(new FlatPayCard("Pay Poor Tax", 15));
            deck.Add(new MoveAndPassGoCard("Advance To St. Charles Place", BoardConstants.ST_CHARLES_PLACE, boardHandler));
            deck.Add(new PayAllPlayersCard(players));
            deck.Add(new MoveToNearestRailroadCard(boardHandler));
            deck.Add(new MoveToNearestRailroadCard(boardHandler));
            deck.Add(new MoveAndPassGoCard("Take a Ride on the Reading", BoardConstants.READING_RAILROAD, boardHandler));
            deck.Add(new MoveAndPassGoCard("Take a walk on the Boardwalk", BoardConstants.BOARDWALK, boardHandler));
            deck.Add(new FlatCollectCard("Your Building And Loan Matures", 150));
            deck.Add(new MoveAndPassGoCard("Advance to Illinois Avenue", BoardConstants.ILLINOIS_AVENUE, boardHandler));
            deck.Add(new GetOutOfJailFreeCard(jailHandler));
            deck.Add(new HousesAndHotelsCard("Make General Repairs On All Your Property", 25, 100));

            var shuffledDeck = deck.OrderBy(x => random.Next());
            return new Queue<ICard>(shuffledDeck);
        }
    }
}