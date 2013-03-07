using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Cards;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class PayAllPlayersCardTests
    {
        private ICard payCard;
        private IPlayer player;
        private IPlayer loser;
        private IBanker banker;

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

            payCard = new PayAllPlayersCard(players, banker);
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("You Have Been Elected Chairman Of The Board", payCard.ToString());
            Assert.IsFalse(payCard.Held);
        }

        [TestMethod]
        public void Pay()
        {
            var playerMoney = banker.Money[player];
            payCard.Execute(player);
            Assert.AreEqual(playerMoney - 450, banker.Money[player]);
        }

        [TestMethod]
        public void LosersDontCollect()
        {
            banker.Pay(loser, banker.Money[loser] + 1);
            var playerMoney = banker.Money[player];
            payCard.Execute(player);

            Assert.IsTrue(banker.IsBankrupt(loser));
            Assert.AreEqual(playerMoney - 400, banker.Money[player]);
        }

        [TestMethod]
        public void BankruptWhilePaying()
        {
            banker.Pay(player, banker.Money[player] - 300);
            payCard.Execute(player);

            Assert.IsTrue(banker.IsBankrupt(player));
        }
    }
}