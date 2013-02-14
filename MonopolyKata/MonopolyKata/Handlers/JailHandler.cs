using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Board;
using Monopoly.Dice;
using Monopoly;
using Monopoly.Cards;

namespace Monopoly.Handlers
{
    public class JailHandler
    {
        private IDice dice;
        private Dictionary<Player, Int16> turnsInJail;
        private Player player;
        private Dictionary<Player, GetOutOfJailFreeCard> cards;

        public JailHandler(IDice dice)
        {
            this.dice = dice;
            turnsInJail = new Dictionary<Player, Int16>();
            cards = new Dictionary<Player, GetOutOfJailFreeCard>(2);
        }

        public void AddCardHolder(Player player, GetOutOfJailFreeCard card)
        {
            cards.Add(player, card);
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
            turnsInJail.Remove(playerToLiberate);
        }

        private void DoTime()
        {
            turnsInJail[player]++;

            if (dice.Doubles)
                turnsInJail.Remove(player);
            else if (cards.ContainsKey(player) && player.WillUseGetOutOfJailCard())
                UseGetOutOfJailCard();
            else if (turnsInJail[player] >= GameConstants.TURNS_IN_JAIL_LIMIT || player.WillPayToGetOutOfJail())
                PayToLiberate(player);
        }

        private void UseGetOutOfJailCard()
        {
            turnsInJail.Remove(player);
            cards[player].Use();
        }
    }
}