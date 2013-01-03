using System;
using System.Collections.Generic;
using MonopolyKata.MonopolyBoard.Spaces;
using MonopolyKata.MonopolyDice;
using MonopolyKata.MonopolyPlayer;

namespace MonopolyKata.Handlers
{
    public class TurnHandler
    {
        private IDice dice;
        private List<ISpace> board;
        private Player player;

        public TurnHandler(IDice dice, List<ISpace> board)
        {
            this.dice = dice;
            this.board = board;
        }

        public void TakeTurn(Player p)
        {
            player = p;

            player.PreTurnChecks();

            do RollAndMove();
            while (CanGoAgain());

            dice.Reset();
            player.PostTurnChecks();
        }

        private void RollAndMove()
        {
            dice.RollTwoDice();

            var jailHandler = new JailHandler(dice, player);
            jailHandler.HandleJail();

            if (!player.IsInJail)
                MoveForward();
        }

        private Boolean CanGoAgain()
        {
            return dice.Doubles
                   && dice.DoublesCount < GameConstants.DOUBLES_LIMIT
                   && !player.LostTheGame
                   && !player.IsInJail;
        }

        private void MoveForward()
        {
            player.Move(dice.Value);
            var goHandler = new GoHandler(player);
            goHandler.HandleGo();
            board[player.Position].LandOn(player);
        }
    }
}