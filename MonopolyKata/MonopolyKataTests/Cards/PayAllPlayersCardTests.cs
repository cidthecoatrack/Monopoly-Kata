﻿using System.Collections.Generic;
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
        private PayAllPlayersCard card;
        private Player player;
        private Player loser;
        private Banker banker;

        [TestInitialize]
        public void Setup()
        {
            player = new Player("name");
            loser = new Player("loser");

            var players = new List<Player>();
            for (var i = 0; i < 8; i++)
                players.Add(new Player("player " + i));
            players.Add(player);
            players.Add(loser);

            banker = new Banker(players);

            card = new PayAllPlayersCard(players, banker);
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("You Have Been Elected Chairman Of The Board", card.ToString());
        }

        [TestMethod]
        public void Pay()
        {
            var playerMoney = banker.GetMoney(player);
            card.Execute(player);
            Assert.AreEqual(playerMoney - 450, banker.GetMoney(player));
        }

        [TestMethod]
        public void LosersDontCollect()
        {
            banker.Pay(loser, banker.GetMoney(loser) + 1, ToString());
            var playerMoney = banker.GetMoney(player);
            card.Execute(player);

            Assert.IsTrue(banker.IsBankrupt(loser));
            Assert.AreEqual(playerMoney - 400, banker.GetMoney(player));
        }

        [TestMethod]
        public void BankruptWhilePaying()
        {
            banker.Pay(player, banker.GetMoney(player) - 300, ToString());
            card.Execute(player);

            Assert.IsTrue(banker.IsBankrupt(player));
        }
    }
}