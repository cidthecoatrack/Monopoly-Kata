using System;
using Monopoly.Dice;
using Monopoly.Players;

namespace Monopoly.Handlers
{
    public class TurnHandler : ITurnHandler
    {
        private IDice dice;
        private Int32 doublesCount;
        private IBoardHandler boardHandler;
        private IJailHandler jailHandler;
        private IOwnableHandler realEstateHandler;
        private IBanker banker;

        public TurnHandler(IDice dice, IBoardHandler boardHandler, IJailHandler jailHandler, IOwnableHandler realEstateHandler, IBanker banker)
        {
            this.dice = dice;
            this.boardHandler = boardHandler;
            this.jailHandler = jailHandler;
            this.realEstateHandler = realEstateHandler;
            this.banker = banker;
        }

        public void TakeTurn(IPlayer player)
        {
            doublesCount = 0;

            realEstateHandler.HandleMortgages(player);

            do RollAndMove(player);
            while (CanGoAgain(player));

            if (!banker.IsBankrupt(player))
                realEstateHandler.HandleMortgages(player);
        }

        private void RollAndMove(IPlayer player)
        {
            dice.RollTwoDice();
            if (dice.Doubles)
                doublesCount++;

            jailHandler.HandleJail(doublesCount, player);

            if (!jailHandler.HasImprisoned(player) && !banker.IsBankrupt(player))
                boardHandler.Move(player, dice.Value);
        }

        private Boolean CanGoAgain(IPlayer player)
        {
            return dice.Doubles && !banker.IsBankrupt(player) && !jailHandler.HasImprisoned(player);
        }
    }
}