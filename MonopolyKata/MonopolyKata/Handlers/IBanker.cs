using System;
using System.Collections.Generic;
using Monopoly.Players;

namespace Monopoly.Handlers
{
    public interface IBanker
    {
        Dictionary<IPlayer, Int32> Money { get; }

        Boolean CanAfford(IPlayer player, Int32 amount);
        void Collect(IPlayer player, Int32 amountToCollect);
        IEnumerable<IPlayer> GetBankrupcies(IEnumerable<IPlayer> players);
        IPlayer GetWinner();
        Boolean IsBankrupt(IPlayer player);
        void Pay(IPlayer player, Int32 amountToPay);
        void Transact(IPlayer payer, IPlayer collector, Int32 amount);
    }
}
