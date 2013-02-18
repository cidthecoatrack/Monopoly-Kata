using System;
using Monopoly.Dice;

namespace Monopoly.Handlers
{
    public class TurnHandler
    {
        private IDice dice;
        private Int32 doublesCount;
        private BoardHandler boardHandler;
        private JailHandler jailHandler;

        public TurnHandler(IDice dice, BoardHandler boardHandler, JailHandler jailHandler)
        {
            this.dice = dice;
            this.boardHandler = boardHandler;
            this.jailHandler = jailHandler;
        }

        public void TakeTurn(Player player)
        {
            doublesCount = 0;

            player.HandleMortgages();

            do RollAndMove(player);
            while (CanGoAgain(player));

            player.HandleMortgages();
        }

        private void RollAndMove(Player player)
        {
            dice.RollTwoDice();
            if (dice.Doubles)
                doublesCount++;

            jailHandler.HandleJail(doublesCount, player);

            if (!jailHandler.HasImprisoned(player))
                boardHandler.Move(player, dice.Value);
        }

        private Boolean CanGoAgain(Player player)
        {
            return dice.Doubles && !player.LostTheGame && !jailHandler.HasImprisoned(player);
        }
    }
}