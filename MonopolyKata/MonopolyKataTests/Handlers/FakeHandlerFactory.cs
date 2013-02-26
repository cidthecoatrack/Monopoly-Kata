﻿using System;
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
        public static BoardHandler CreateBoardHandlerForFakeBoard(IEnumerable<Player> players, OwnableHandler realEstateHandler, Banker banker)
        {
            var normalSpaces = new Dictionary<Int32, UnownableSpace>();
            for (var i = 0; i < BoardConstants.BOARD_SIZE; i++)
                normalSpaces.Add(i, new NormalSpace("space " + i));

            var spaceHandler = new UnownableHandler(normalSpaces);

            return new BoardHandler(players, realEstateHandler, spaceHandler, banker);
        }

        public static OwnableHandler CreateEmptyRealEstateHandler(IEnumerable<Player> players)
        {
            return new OwnableHandler(new Dictionary<Int32, OwnableSpace>(), new Banker(players));
        }

        public static OwnableHandler CreateRealEstateHandler(IEnumerable<OwnableSpace> realEstate, IEnumerable<Player> players, Banker banker)
        {
            var dict = new Dictionary<Int32, OwnableSpace>();
            for (var i = 0; i < realEstate.Count(); i++)
                dict.Add(i, realEstate.ElementAt(i));

            return new OwnableHandler(dict, banker);
        }
    }
}