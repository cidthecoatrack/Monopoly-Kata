using System;
using Monopoly.Board;
using Monopoly.Dice;
using Monopoly.Handlers;
using Monopoly.Players;

namespace Monopoly.Cards
{
    public class MoveToNearestUtilityCard : ICard
    {
        public Boolean Held { get; private set; }

        private IBoardHandler boardHandler;
        private IDice dice;

        public MoveToNearestUtilityCard(IBoardHandler boardHandler, IDice dice)
        {
            this.boardHandler = boardHandler;
            this.dice = dice;
        }

        public void Execute(IPlayer player)
        {
            var location = boardHandler.PositionOf[player];

            if (location <= BoardConstants.ELECTRIC_COMPANY || location > BoardConstants.WATER_WORKS)
                MoveTo(player, BoardConstants.ELECTRIC_COMPANY);
            else
                MoveTo(player, BoardConstants.WATER_WORKS);
        }

        private void MoveTo(IPlayer player, Int32 position)
        {
            dice.RollTwoDice();
            boardHandler.MoveToUtilityAndForce10xRent(player, position);
        }

        public override String ToString()
        {
            return "Advance to Nearest Utility and pay 10x a die roll";
        }
    }
}