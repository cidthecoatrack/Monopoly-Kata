using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Board;
using Monopoly.Board.Spaces;
using Monopoly.Handlers;
using Monopoly.Players;

namespace Monopoly.Tests.Handlers
{
    public class FakeHandlerFactory
    {
        public static BoardHandler CreateBoardHandlerForFakeBoard(IEnumerable<Player> players, RealEstateHandler realEstateHandler, Banker banker)
        {
            var normalSpaces = new Dictionary<Int32, ISpace>();
            for (var i = 0; i < BoardConstants.BOARD_SIZE; i++)
                normalSpaces.Add(i, new NormalSpace("space " + i));

            var spaceHandler = new SpaceHandler(normalSpaces);

            return new BoardHandler(players, realEstateHandler, spaceHandler, banker);
        }

        public static RealEstateHandler CreateEmptyRealEstateHandler(IEnumerable<Player> players)
        {
            return new RealEstateHandler(new Dictionary<Int32, RealEstate>(), players, new Banker(players));
        }

        public static RealEstateHandler CreateRealEstateHandler(IEnumerable<RealEstate> realEstate, IEnumerable<Player> players, Banker banker)
        {
            var dict = new Dictionary<Int32, RealEstate>();
            for (var i = 0; i < realEstate.Count(); i++)
                dict.Add(i, realEstate.ElementAt(i));

            return new RealEstateHandler(dict, players, banker);
        }
    }
}