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
        private Dictionary<Player, String> removeReasons;

        public Banker(IEnumerable<Player> players)
        {
            removeReasons = new Dictionary<Player, String>();

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
            try { return money[player]; }
            catch (KeyNotFoundException)
            {
                ThrowKeyNotFoundException(player, "get money", 0);
            }

            return -9266;
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

        public void Pay(Player player, Int32 amountToPay, String payReason)
        {
            try { money[player] -= amountToPay; }
            catch (KeyNotFoundException)
            {
                ThrowKeyNotFoundException(player, "pay", amountToPay);
            }

            if (money[player] < 0)
            {
                money.Remove(player);
                removeReasons.Add(player, payReason);
            }
        }

        private void ThrowKeyNotFoundException(Player player, String action, Int32 amount)
        {
            var message = String.Format("{0} cannot {1} {2} because they are bankrupt.", player.ToString(), action, amount);
            message += "\nBankrupt because " + removeReasons[player];
            throw new KeyNotFoundException(message);
        }

        public void Collect(Player player, Int32 amountToCollect)
        {
            try { money[player] += amountToCollect; }
            catch (KeyNotFoundException)
            {
                ThrowKeyNotFoundException(player, "COLLECT", amountToCollect);
            }
        }

        public void Transact(Player payer, Player collector, Int32 amount, String payReason)
        {
            Collect(collector, amount);
            Pay(payer, amount, payReason);
        }
    }
}