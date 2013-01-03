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
            while (PlayerHasPassedGo())
            {
                player.Move(-BoardConstants.BOARD_SIZE);
                player.ReceiveMoney(GameConstants.PASS_GO_PAYMENT);
            }
        }

        private Boolean PlayerHasPassedGo()
        {
            return player.Position >= BoardConstants.BOARD_SIZE;
        }
    }
}