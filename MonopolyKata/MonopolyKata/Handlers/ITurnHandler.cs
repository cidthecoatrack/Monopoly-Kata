using System;
using Monopoly.Players;

namespace Monopoly.Handlers
{
    public interface ITurnHandler
    {
        void TakeTurn(IPlayer player);
    }
}