using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Board;
using Monopoly.Board.Spaces;
using Monopoly.Games;
using Monopoly.Players;

namespace Monopoly.Handlers
{
    public class BoardHandler
    {
        public Dictionary<Player, Int32> PositionOf { get; private set; }

        private OwnableHandler realEstateHandler;
        private UnownableHandler spaceHandler;
        private Banker banker;

        public BoardHandler(IEnumerable<Player> players, OwnableHandler realEstateHandler, UnownableHandler spaceHandler, Banker banker)
        {
            this.realEstateHandler = realEstateHandler;
            this.spaceHandler = spaceHandler;
            this.banker = banker;

            PositionOf = new Dictionary<Player, Int32>();
            foreach (var player in players)
                PositionOf.Add(player, 0);
        }

        public void Move(Player player, Int32 amountToMove)
        {
            var newPosition = (PositionOf[player] + amountToMove) % BoardConstants.BOARD_SIZE;
            MoveTo(player, newPosition);
        }

        public void MoveTo(Player player, Int32 newPosition)
        {
            if (PositionOf[player] > newPosition)
                banker.Collect(player, GameConstants.PASS_GO_PAYMENT);

            MoveToAndDontPassGo(player, newPosition);
        }

        private void Land(Player player)
        {
            var position = PositionOf[player];

            if (realEstateHandler.Contains(position))
                realEstateHandler.Land(player, position);
            else
                spaceHandler.Land(player, position);
        }

        public void MoveToUtilityAndForce10xRent(Player player, Int32 utilityPosition)
        {
            if (utilityPosition != BoardConstants.ELECTRIC_COMPANY && utilityPosition != BoardConstants.WATER_WORKS)
                return;

            if (PositionOf[player] > utilityPosition)
                banker.Collect(player, GameConstants.PASS_GO_PAYMENT);

            PositionOf[player] = utilityPosition;
            realEstateHandler.LandAndForce10xUtilityRent(player, utilityPosition);
        }

        public void MoveToAndDontPassGo(Player player, Int32 newPosition)
        {
            PositionOf[player] = newPosition;
            Land(player);
        }

        public void MoveToRailroadAndPayDoubleRent(Player player, Int32 railroadPosition)
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