using System;
using System.Collections.Generic;
using Monopoly.Players;
namespace Monopoly.Handlers
{
    public interface IBoardHandler
    {
        Dictionary<IPlayer, Int32> PositionOf { get; }

        void Move(IPlayer player, Int32 amountToMove);
        void MoveTo(IPlayer player, Int32 newPosition);
        void MoveToAndDontPassGo(IPlayer player, Int32 newPosition);
        void MoveToRailroadAndPayDoubleRent(IPlayer player, Int32 railroadPosition);
        void MoveToUtilityAndForce10xRent(IPlayer player, Int32 utilityPosition);
    }
}