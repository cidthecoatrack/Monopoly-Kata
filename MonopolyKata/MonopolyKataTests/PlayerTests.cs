using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;

namespace MonopolyKataTests
{
    [TestClass]
    public class PlayerTests
    {
        Player player;
        
        [TestInitialize]
        public void Setup()
        {
            player = new Player("Name");
        }

        [TestMethod]
        public void PlayersNamePassedInCorrectly()
        {
            Assert.AreEqual("Name", player.Name);
        }

        [TestMethod]
        public void PlayersInitializedToStartingPosition()
        {
            Assert.AreEqual(0, player.Position);
        }

        [TestMethod]
        public void PlayerMoneyInitializedToZero()
        {
            Assert.AreEqual(0, player.Money);
        }

        [TestMethod]
        public void TellPlayerToMoveSevenSpaces_PlayerMovesSevenSpaces()
        {
            player.Move(7);
            Assert.AreEqual(7, player.Position);
        }

        [TestMethod]
        public void GivePlayerTwoHundred_PlayerReceivesTwoHundred()
        {
            player.ReceiveMoney(200);
            Assert.AreEqual(200, player.Money);
        }

        [TestMethod]
        public void TellPlayerToPayOneHundred_PlayerPaysOneHundred()
        {
            player.ReceiveMoney(200);
            player.Pay(100);
            Assert.AreEqual(100, player.Money);
        }

        [TestMethod]
        public void PlayerHasNegativeMoney_PlayerLoses()
        {
            player.ReceiveMoney(-1);
            Assert.IsTrue(player.LostTheGame);
        }

        [TestMethod]
        public void PlayerPaysUnaffordableAmount_PlayerLoses()
        {
            player.ReceiveMoney(200);
            player.Pay(9266);
            Assert.IsTrue(player.LostTheGame);
        }

        [TestMethod]
        public void PlayerHasNotLostTheGameAtStart()
        {
            Assert.IsFalse(player.LostTheGame);
        }

        [TestMethod]
        public void EqualPlayers_AreAssessedAsEqual()
        {
            Player playerClone = player;
            Assert.IsTrue(playerClone.Equals(player));
            Assert.IsTrue(player.Equals(playerClone));
        }

        [TestMethod]
        public void UnequalPlayers_AreAssessedAsUnequal()
        {
            Player differentPlayer = new Player("Other Name");
            AssertPlayersAreDifferent(player, differentPlayer);

            differentPlayer = new Player(player.Name);
            differentPlayer.ReceiveMoney(1);
            AssertPlayersAreDifferent(player, differentPlayer);

            differentPlayer = new Player(player.Name);
            differentPlayer.SetPosition(1);
            AssertPlayersAreDifferent(player, differentPlayer);

            differentPlayer = new Player(player.Name);
            player.ReceiveMoney(1);
            AssertPlayersAreDifferent(player, differentPlayer);

            player.Pay(1);
            Assert.IsTrue(player.Equals(differentPlayer));
            player.SetPosition(1);
            AssertPlayersAreDifferent(player, differentPlayer);
        }

        private void AssertPlayersAreDifferent(Player basePlayer, Player comparePlayer)
        {
            Assert.IsFalse(basePlayer.Equals(comparePlayer));
            Assert.IsFalse(comparePlayer.Equals(basePlayer));
        }

        [TestMethod]
        public void ListContainsPlayer_PlayerSaysSoWhenAsked()
        {
            List<Player> playerList = new List<Player>();
            playerList.Add(player);

            Assert.IsTrue(player.IsContainedIn(playerList));
        }

        [TestMethod]
        public void ListDoesNotContainPlayer_PlayerSaysSoWhenAsked()
        {
            List<Player> playerList = new List<Player>();
            playerList.Add(new Player("Other Name"));

            Assert.IsFalse(player.IsContainedIn(playerList));
        }
    }
}