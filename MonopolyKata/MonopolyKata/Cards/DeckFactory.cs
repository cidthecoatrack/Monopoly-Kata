using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Board.Spaces;
using Monopoly.Handlers;

namespace Monopoly.Cards
{
    public class DeckFactory
    {
        private Random random;

        public DeckFactory()
        {
            random = new Random();
        }
        
        public Queue<ICard> BuildCommunityChestDeck(JailHandler jailHandler, IEnumerable<Player> players, ISpace go)
        {
            var deck = new List<ICard>();

            deck.Add(new FlatPayCard("Doctor's Fee", 50));
            deck.Add(new FlatCollectCard("Christmas Fund Matures", 100));
            deck.Add(new GetOutOfJailFreeCard(jailHandler));
            deck.Add(new AllPlayersCard("Grand Opera Opening: Every Player Pays For Opening Night Seats", 50, players));
            deck.Add(new FlatCollectCard("You Inherit", 100));
            deck.Add(new FlatCollectCard("Receive Services Fees", 25));
            deck.Add(new FlatCollectCard("Income Tax Refund", 20));
            deck.Add(new FlatCollectCard("Sell Stock", 45));
            deck.Add(new FlatPayCard("Pay School Tax", 150));
            deck.Add(new HousesAndHotelsCard());
            deck.Add(new FlatCollectCard("Bank Error In Your Favor", 200));
            deck.Add(new MoveAndPassGoCard("Advance To Go", 0, go));
            deck.Add(new FlatCollectCard("Life Insurance Matures", 50));
            deck.Add(new FlatPayCard("Pay Hospital", 50));
            deck.Add(new FlatCollectCard("You Have Won Second Prize in a Beauty Contest", 10));
            deck.Add(new GoToJailCard(jailHandler));

            var shuffledDeck = deck.OrderBy(x => random.Next());
            return new Queue<ICard>(shuffledDeck);
        }
    }
}