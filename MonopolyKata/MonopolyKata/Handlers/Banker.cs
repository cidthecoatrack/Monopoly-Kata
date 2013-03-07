using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Players;

namespace Monopoly.Handlers
{
    public class Banker
    {
        private Dictionary<Player, Int32> money;

        public Banker(IEnumerable<Player> players)
        {
            money = new Dictionary<Player, Int32>();
            foreach (var player in players)
                money.Add(player, 1500);
        }

        public Player GetWinner()
        {
            var ordered = money.OrderByDescending(p => p.Value);
            return ordered.First().Key;
        }

        public Int32 GetMoney(Player player)
        {
            return money[player];
        }

        public Boolean IsBankrupt(Player player)
        {
            return !money.ContainsKey(player);
        }

        public IEnumerable<Player> GetBankrupcies(IEnumerable<Player> players)
        {
            return players.Where(p => IsBankrupt(p)).ToList();
        }

        public Boolean CanAfford(Player player, Int32 amount)
        {
            return money[player] >= amount;
        }

        public void Pay(Player player, Int32 amountToPay)
        {
            money[player] -= amountToPay;

            if (money[player] < 0)
                money.Remove(player);
        }

        public void Collect(Player player, Int32 amountToCollect)
        {
            money[player] += amountToCollect;
        }

        public void Transact(Player payer, Player collector, Int32 amount)
        {
            Collect(collector, amount);
            Pay(payer, amount);
        }
    }
}