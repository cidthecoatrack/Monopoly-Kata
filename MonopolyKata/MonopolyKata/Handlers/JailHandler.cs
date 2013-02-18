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
        private Dictionary<Player, GetOutOfJailFreeCard> cards;
        private BoardHandler boardHandler;

        public JailHandler(IDice dice, BoardHandler boardHandler)
        {
            this.dice = dice;
            this.boardHandler = boardHandler;

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

        public void HandleJail(Int32 doublesCount, Player player)
        {
            if (HasImprisoned(player))
                DoTime(player);
            else if (doublesCount >= GameConstants.DOUBLES_LIMIT || boardHandler.PositionOf[player] == BoardConstants.GO_TO_JAIL)
                Imprison(player);
        }

        public void Imprison(Player player)
        {
            boardHandler.MoveToAndDontPassGo(player, BoardConstants.JAIL_OR_JUST_VISITING);
            turnsInJail.Add(player, 0);
        }

        private void PayToLiberate(Player player)
        {
            player.Pay(GameConstants.COST_TO_GET_OUT_OF_JAIL);
            turnsInJail.Remove(player);
        }

        private void DoTime(Player player)
        {
            turnsInJail[player]++;

            if (dice.Doubles)
                turnsInJail.Remove(player);
            else if (cards.ContainsKey(player) && player.WillUseGetOutOfJailCard())
                UseGetOutOfJailCard(player);
            else if (turnsInJail[player] >= GameConstants.TURNS_IN_JAIL_LIMIT || player.WillPayToGetOutOfJail())
                PayToLiberate(player);
        }

        private void UseGetOutOfJailCard(Player player)
        {
            turnsInJail.Remove(player);
            cards[player].Use();
            cards.Remove(player);
        }
    }
}