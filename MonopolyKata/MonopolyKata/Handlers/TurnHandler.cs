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
        private Int32 doublesCount;
        private JailHandler jailHandler;

        public TurnHandler(IDice dice, List<ISpace> board, JailHandler jailHandler)
        {
            this.dice = dice;
            this.board = board;
            this.jailHandler = jailHandler;
        }

        public void TakeTurn(Player p)
        {
            player = p;
            doublesCount = 0;

            player.HandleMortgages();

            do RollAndMove();
            while (CanGoAgain());

            player.HandleMortgages();
        }

        private void RollAndMove()
        {
            dice.RollTwoDice();
            if (dice.Doubles)
                doublesCount++;

            jailHandler.HandleJail(doublesCount, player);

            if (!jailHandler.HasImprisoned(player))
                MoveForward();
        }

        private Boolean CanGoAgain()
        {
            return dice.Doubles && !player.LostTheGame && !jailHandler.HasImprisoned(player);
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