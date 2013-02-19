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
        public static Game CreateGame(IEnumerable<Player> players)
        {
            var dice = new MonopolyDice();
            var board = BoardFactory.CreateMonopolyBoard(dice);
            var boardHandler = new BoardHandler(players, board);

            var jailHandler = new JailHandler(dice, boardHandler);

            var realEstate = board.OfType<RealEstate>();
            var realEstateHandler = new RealEstateHandler(realEstate, players);

            var deckFactory = new DeckFactory(jailHandler, players, boardHandler, realEstateHandler);
            var communityChest = deckFactory.BuildCommunityChestDeck();
            var chance = deckFactory.BuildChanceDeck(dice);

            foreach (var space in board.OfType<DrawCard>())
            {
                if (space.ToString() == "Community Chest")
                    space.AddDeck(communityChest);
                else
                    space.AddDeck(chance);
            }

            var turnHandler = new TurnHandler(dice, boardHandler, jailHandler);

            return new Game(players, turnHandler);
        }
    }
}