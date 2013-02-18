using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Board;
using Monopoly.Board.Spaces;

namespace Monopoly.Handlers
{
    public class BoardHandler
    {
        public Dictionary<Player, Int32> PositionOf { get; private set; }
        private IEnumerable<ISpace> board;

        public BoardHandler(IEnumerable<Player> players, IEnumerable<ISpace> board)
        {
            this.board = board;

            PositionOf = new Dictionary<Player, Int32>();
            foreach (var player in players)
                PositionOf.Add(player, 0);
        }

        public void Move(Player player, Int32 amountToMove)
        {
            var newPosition = (PositionOf[player] + amountToMove) % BoardConstants.BOARD_SIZE;
            MoveTo(player, newPosition);
        }

        public void MoveTo(Player player, Int32 newPosition)
        {
            if (PositionOf[player] > newPosition)
                player.Collect(GameConstants.PASS_GO_PAYMENT);

            PositionOf[player] = newPosition;
            board.ElementAt(newPosition).LandOn(player);
        }

        public void MoveToUtilityAndForce10xRent(Player player, Int32 newPosition)
        {
            if (PositionOf[player] > newPosition)
                player.Collect(GameConstants.PASS_GO_PAYMENT);

            PositionOf[player] = newPosition;

            var utility = board.ElementAt(newPosition) as Utility;
            utility.Force10xRent = true;
            utility.LandOn(player);
            utility.Force10xRent = false;
        }

        public void MoveToAndDontPassGo(Player player, Int32 newPosition)
        {
            PositionOf[player] = newPosition;
            board.ElementAt(newPosition).LandOn(player);
        }
    }
}