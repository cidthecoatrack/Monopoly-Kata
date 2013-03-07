using System;
using Monopoly.Players;
namespace Monopoly.Handlers
{
    public interface IUnownableHandler
    {
        void Land(IPlayer player, Int32 position);
    }
}
