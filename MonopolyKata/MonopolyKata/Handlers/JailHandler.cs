using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Board;
using Monopoly.Cards;
using Monopoly.Dice;
using Monopoly.Games;
using Monopoly.Players;

namespace Monopoly.Handlers
{
    public class JailHandler : IJailHandler
    {
        private IDice dice;
        private Dictionary<IPlayer, Int16> turnsInJail;
        private Dictionary<GetOutOfJailFreeCard, IPlayer> cards;
        private IBoardHandler boardHandler;
        private IBanker banker;

        public JailHandler(IDice dice, IBoardHandler boardHandler, IBanker banker)
        {
            this.dice = dice;
            this.boardHandler = boardHandler;
            this.banker = banker;

            turnsInJail = new Dictionary<IPlayer, Int16>();
            cards = new Dictionary<GetOutOfJailFreeCard, IPlayer>(2);
        }

        public void AddCardHolder(IPlayer player, GetOutOfJailFreeCard card)
        {
            cards.Add(card, player);
        }

        public Boolean HasImprisoned(IPlayer player)
        {
            return turnsInJail.ContainsKey(player);
        }

        public void HandleJail(Int32 doublesCount, IPlayer player)
        {
            if (HasImprisoned(player))
                DoTime(player);
            else if (doublesCount >= GameConstants.DOUBLES_LIMIT || boardHandler.PositionOf[player] == BoardConstants.GO_TO_JAIL)
                Imprison(player);
        }

        public void Imprison(IPlayer player)
        {
            boardHandler.MoveToAndDontPassGo(player, BoardConstants.JAIL_OR_JUST_VISITING);
            turnsInJail.Add(player, 0);
        }

        private void Bail(IPlayer player)
        {
            banker.Pay(player, GameConstants.COST_TO_GET_OUT_OF_JAIL);
            turnsInJail.Remove(player);
        }

        private void DoTime(IPlayer player)
        {
            turnsInJail[player]++;

            if (dice.Doubles)
                turnsInJail.Remove(player);
            else if (cards.ContainsValue(player) && player.JailStrategy.UseCard())
                UseGetOutOfJailCard(player);
            else if (turnsInJail[player] >= GameConstants.TURNS_IN_JAIL_LIMIT || PlayerWillPayToGetOutOfJail(player))
                Bail(player);
        }

        public Boolean PlayerWillPayToGetOutOfJail(IPlayer player)
        {
            var money = banker.Money[player];
            return player.JailStrategy.ShouldPay(money) && banker.CanAfford(player, GameConstants.COST_TO_GET_OUT_OF_JAIL);
        }

        private void UseGetOutOfJailCard(IPlayer player)
        {
            turnsInJail.Remove(player);
            var card = cards.Keys.First(x => cards[x] == player);
            card.Use();
            cards.Remove(card);
        }
    }
}