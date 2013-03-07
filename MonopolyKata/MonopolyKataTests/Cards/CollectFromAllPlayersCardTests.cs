using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Cards;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class CollectFromAllPlayersCardTests
    {
        private ICard collectCard;
        private IPlayer player;
        private IBanker banker;
        private IPlayer loser;

        [TestInitialize]
        public void Setup()
        {
            player = new Player("name");
            loser = new Player("loser");

            var players = new List<IPlayer>();
            for (var i = 0; i < 8; i++)
                players.Add(new Player("player " + i));
            players.Add(player);
            players.Add(loser);

            banker = new Banker(players);

            collectCard = new CollectFromAllPlayersCard(players, banker);
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("Grand Opera Opening: Every Player Pays For Opening Night Seats", collectCard.ToString());
            Assert.IsFalse(collectCard.Held);
        }

        [TestMethod]
        public void Pay()
        {
            var playerMoney = banker.Money[player];
            collectCard.Execute(player);
            Assert.AreEqual(playerMoney + 450, banker.Money[player]);
        }

        [TestMethod]
        public void LosersDontPay()
        {
            banker.Pay(loser, banker.Money[loser] + 1);
            var playerMoney = banker.Money[player];
            collectCard.Execute(player);

            Assert.AreEqual(playerMoney + 400, banker.Money[player]);
        }
    }
}