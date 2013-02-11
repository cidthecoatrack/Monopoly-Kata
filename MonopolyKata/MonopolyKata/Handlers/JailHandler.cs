using System;
using System.Collections.Generic;
using Monopoly.Board;
using Monopoly.Dice;
using Monopoly;

namespace Monopoly.Handlers
{
    public class JailHandler
    {
        private IDice dice;
        private Dictionary<Player, Int16> turnsInJail;
        private Player player;

        public JailHandler(IDice dice)
        {
            this.dice = dice;
            turnsInJail = new Dictionary<Player, Int16>();
        }

        public Boolean HasImprisoned(Player player)
        {
            return turnsInJail.ContainsKey(player);
        }

        public void HandleJail(Int32 doublesCount, Player p)
        {
            player = p;

            if (HasImprisoned(player))
                DoTime();
            else if (doublesCount >= GameConstants.DOUBLES_LIMIT)
                Imprison(player);
        }

        public void Imprison(Player playerToImprison)
        {
            playerToImprison.Move(BoardConstants.JAIL_OR_JUST_VISITING - playerToImprison.Position);
            turnsInJail.Add(playerToImprison, 0);
        }

        private void PayToLiberate(Player playerToLiberate)
        {
            player.Pay(GameConstants.COST_TO_GET_OUT_OF_JAIL);
            turnsInJail.Remove(player);
        }

        public void Liberate(Player playerToLiberate)
        {
            turnsInJail.Remove(player);
        }

        private void DoTime()
        {
            turnsInJail[player]++;
            
            if (dice.Doubles)
                turnsInJail.Remove(player);
            else if (turnsInJail[player] >= GameConstants.TURNS_IN_JAIL_LIMIT || player.WillPayToGetOutOfJail())
                PayToLiberate(player); 
        }
    }
}