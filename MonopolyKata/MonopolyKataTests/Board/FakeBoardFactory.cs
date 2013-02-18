using System;
using System.Collections.Generic;
using Monopoly.Board;
using Monopoly.Board.Spaces;

namespace Monopoly.Tests.Board
{
    public class FakeBoardFactory
    {
        public static IEnumerable<ISpace> CreateBoardOfNormalSpaces()
        {
            var board = new List<ISpace>();

            for (var i = 0; i < BoardConstants.BOARD_SIZE; i++)
                board.Add(new NormalSpace(Convert.ToString(i)));

            return board;
        }
    }
}