using System;
using Monopoly.Cards;
using Monopoly.Players;

namespace Monopoly.Handlers
{
    public interface IJailHandler
    {
        void AddCardHolder(IPlayer player, GetOutOfJailFreeCard card);
        void HandleJail(Int32 doublesCount, IPlayer player);
        Boolean HasImprisoned(IPlayer player);
        void Imprison(IPlayer player);
        Boolean PlayerWillPayToGetOutOfJail(IPlayer player);
    }
}