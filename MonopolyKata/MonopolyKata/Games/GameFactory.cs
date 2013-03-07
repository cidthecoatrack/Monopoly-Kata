using System.Collections.Generic;
using System.Linq;
using Monopoly.Board;
using Monopoly.Board.Spaces;
using Monopoly.Cards;
using Monopoly.Dice;
using Monopoly.Handlers;
using Monopoly.Players;

namespace Monopoly.Games
{
    public class GameFactory
    {
        public static Game CreateGame(IEnumerable<IPlayer> players)
        {
            var dice = new MonopolyDice();

            var banker = new Banker(players);
            var realEstateHandler = new OwnableHandler(BoardFactory.CreateRealEstate(dice), banker);
            var spaces = BoardFactory.CreateNonRealEstateSpaces(banker);
            var spaceHandler = new UnownableHandler(spaces);

            var boardHandler = new BoardHandler(players, realEstateHandler, spaceHandler, banker);
            var jailHandler = new JailHandler(dice, boardHandler, banker);
            var turnHandler = new TurnHandler(dice, boardHandler, jailHandler, realEstateHandler, banker);

            var deckFactory = new DeckFactory(players, jailHandler, boardHandler, realEstateHandler, banker);
            var communityChest = deckFactory.BuildCommunityChestDeck();
            var chance = deckFactory.BuildChanceDeck(dice);

            foreach (var space in spaces.Values.OfType<DrawCard>())
            {
                if (space.ToString() == "Community Chest")
                    space.AddDeck(communityChest);
                else
                    space.AddDeck(chance);
            }

            return new Game(players, turnHandler, banker);
        }
    }
}