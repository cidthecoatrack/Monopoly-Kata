using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Players;

namespace Monopoly.Handlers
{
    public class Banker : IBanker
    {
        public Dictionary<IPlayer, Int32> Money { get; private set; }

        public Banker(IEnumerable<IPlayer> players)
        {
            Money = new Dictionary<IPlayer, Int32>();
            foreach (var player in players)
                Money.Add(player, 1500);
        }

        public IPlayer GetWinner()
        {
            var ordered = Money.OrderByDescending(p => p.Value);
            return ordered.First().Key;
        }

        public Boolean IsBankrupt(IPlayer player)
        {
            return !Money.ContainsKey(player);
        }

        public IEnumerable<IPlayer> GetBankrupcies(IEnumerable<IPlayer> players)
        {
            return players.Where(p => IsBankrupt(p)).ToList();
        }

        public Boolean CanAfford(IPlayer player, Int32 amount)
        {
            return Money[player] >= amount;
        }

        public void Pay(IPlayer player, Int32 amountToPay)
        {
            Money[player] -= amountToPay;

            if (Money[player] < 0)
                Money.Remove(player);
        }

        public void Collect(IPlayer player, Int32 amountToCollect)
        {
            Money[player] += amountToCollect;
        }

        public void Transact(IPlayer payer, IPlayer collector, Int32 amount)
        {
            Collect(collector, amount);
            Pay(payer, amount);
        }
    }
}