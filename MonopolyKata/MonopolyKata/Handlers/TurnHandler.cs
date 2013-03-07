using System;
using Monopoly.Dice;
using Monopoly.Players;

namespace Monopoly.Handlers
{
    public class TurnHandler
    {
        private IDice dice;
        private Int32 doublesCount;
        private BoardHandler boardHandler;
        private JailHandler jailHandler;
        private OwnableHandler realEstateHandler;
        private Banker banker;

        public TurnHandler(IDice dice, BoardHandler boardHandler, JailHandler jailHandler, OwnableHandler realEstateHandler, Banker banker)
        {
            this.dice = dice;
            this.boardHandler = boardHandler;
            this.jailHandler = jailHandler;
            this.realEstateHandler = realEstateHandler;
            this.banker = banker;
        }

        public void TakeTurn(Player player)
        {
            doublesCount = 0;

            realEstateHandler.HandleMortgages(player);

            do RollAndMove(player);
            while (CanGoAgain(player));

            if (!banker.IsBankrupt(player))
                realEstateHandler.HandleMortgages(player);
        }

        private void RollAndMove(Player player)
        {
            dice.RollTwoDice();
            if (dice.Doubles)
                doublesCount++;

            jailHandler.HandleJail(doublesCount, player);

            if (!jailHandler.HasImprisoned(player) && !banker.IsBankrupt(player))
                boardHandler.Move(player, dice.Value);
        }

        private Boolean CanGoAgain(Player player)
        {
            return dice.Doubles && !banker.IsBankrupt(player) && !jailHandler.HasImprisoned(player);
        }
    }
}