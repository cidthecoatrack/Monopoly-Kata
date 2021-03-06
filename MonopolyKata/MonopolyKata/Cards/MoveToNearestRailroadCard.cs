﻿using System;
using Monopoly.Board;
using Monopoly.Handlers;
using Monopoly.Players;

namespace Monopoly.Cards
{
    public class MoveToNearestRailroadCard : ICard
    {
        public Boolean Held { get; private set; }

        private IBoardHandler boardHandler;

        public MoveToNearestRailroadCard(IBoardHandler boardHandler)
        {
            this.boardHandler = boardHandler;
        }

        public void Execute(IPlayer player)
        {
            var location = boardHandler.PositionOf[player];

            if (location < BoardConstants.READING_RAILROAD || location >= BoardConstants.SHORT_LINE)
                MoveTo(player, BoardConstants.READING_RAILROAD);
            else if (location < BoardConstants.PENNSYLVANIA_RAILROAD)
                MoveTo(player, BoardConstants.PENNSYLVANIA_RAILROAD);
            else if (location < BoardConstants.BandO_RAILROAD)
                MoveTo(player, BoardConstants.BandO_RAILROAD);
            else
                MoveTo(player, BoardConstants.SHORT_LINE);
        }

        private void MoveTo(IPlayer player, Int32 position)
        {
            boardHandler.MoveToRailroadAndPayDoubleRent(player, position);
        }

        public override String ToString()
        {
            return "Advance to the nearest Railroad and pay the owner twice the normal rent";
        }
    }
}