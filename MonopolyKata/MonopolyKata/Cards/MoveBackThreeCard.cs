using System;
using Monopoly.Board;
using Monopoly.Handlers;
using Monopoly.Players;

namespace Monopoly.Cards
{
    public class MoveBackThreeCard : ICard
    {
        public Boolean Held { get; private set; }

        private IBoardHandler boardHandler;

        public MoveBackThreeCard(IBoardHandler boardHandler)
        {
            this.boardHandler = boardHandler;
        }

        public void Execute(IPlayer player)
        {
            var newPosition = (boardHandler.PositionOf[player] - 3 + BoardConstants.BOARD_SIZE) % BoardConstants.BOARD_SIZE;
            boardHandler.MoveToAndDontPassGo(player, newPosition);
        }

        public override String ToString()
        {
            return "Go Back 3 Spaces";
        }
    }
}