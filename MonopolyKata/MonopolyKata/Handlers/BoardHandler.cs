using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Board;
using Monopoly.Board.Spaces;
using Monopoly.Games;
using Monopoly.Players;

namespace Monopoly.Handlers
{
    public class BoardHandler : IBoardHandler
    {
        public Dictionary<IPlayer, Int32> PositionOf { get; private set; }

        private IOwnableHandler realEstateHandler;
        private IUnownableHandler spaceHandler;
        private IBanker banker;

        public BoardHandler(IEnumerable<IPlayer> players, IOwnableHandler realEstateHandler, IUnownableHandler spaceHandler, IBanker banker)
        {
            this.realEstateHandler = realEstateHandler;
            this.spaceHandler = spaceHandler;
            this.banker = banker;

            PositionOf = new Dictionary<IPlayer, Int32>();
            foreach (var player in players)
                PositionOf.Add(player, 0);
        }

        public void Move(IPlayer player, Int32 amountToMove)
        {
            var newPosition = (PositionOf[player] + amountToMove) % BoardConstants.BOARD_SIZE;
            MoveTo(player, newPosition);
        }

        public void MoveTo(IPlayer player, Int32 newPosition)
        {
            if (PositionOf[player] > newPosition)
                banker.Collect(player, GameConstants.PASS_GO_PAYMENT);

            MoveToAndDontPassGo(player, newPosition);
        }

        private void Land(IPlayer player)
        {
            var position = PositionOf[player];

            if (realEstateHandler.Contains(position))
                realEstateHandler.Land(player, position);
            else
                spaceHandler.Land(player, position);
        }

        public void MoveToAndDontPassGo(IPlayer player, Int32 newPosition)
        {
            PositionOf[player] = newPosition;
            Land(player);
        }

        public void MoveToUtilityAndForce10xRent(IPlayer player, Int32 utilityPosition)
        {
            if (utilityPosition != BoardConstants.ELECTRIC_COMPANY && utilityPosition != BoardConstants.WATER_WORKS)
                return;

            if (PositionOf[player] > utilityPosition)
                banker.Collect(player, GameConstants.PASS_GO_PAYMENT);

            PositionOf[player] = utilityPosition;
            realEstateHandler.LandAndForce10xUtilityRent(player, utilityPosition);
        }

        public void MoveToRailroadAndPayDoubleRent(IPlayer player, Int32 railroadPosition)
        {
            if (railroadPosition != BoardConstants.READING_RAILROAD
                && railroadPosition != BoardConstants.PENNSYLVANIA_RAILROAD
                && railroadPosition != BoardConstants.BandO_RAILROAD
                && railroadPosition != BoardConstants.SHORT_LINE)
                return;

            if (PositionOf[player] > railroadPosition)
                banker.Collect(player, GameConstants.PASS_GO_PAYMENT);

            PositionOf[player] = railroadPosition;
            realEstateHandler.LandAndPayDoubleRailroadRent(player, railroadPosition);
        }
    }
}