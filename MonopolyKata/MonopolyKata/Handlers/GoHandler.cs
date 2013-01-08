using System;
using MonopolyKata.MonopolyBoard;
using MonopolyKata.MonopolyPlayer;

namespace MonopolyKata.Handlers
{
    public class GoHandler
    {
        private Player player;

        public GoHandler(Player player)
        {
            this.player = player;
        }

        public void HandleGo()
        {
            var timesPassGo = (player.Position - (player.Position % BoardConstants.BOARD_SIZE)) / BoardConstants.BOARD_SIZE;
            player.Move(-BoardConstants.BOARD_SIZE * timesPassGo);
            player.ReceiveMoney(GameConstants.PASS_GO_PAYMENT * timesPassGo);
        }
    }
}