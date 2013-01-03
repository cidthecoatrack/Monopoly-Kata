using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata.MonopolyBoard.Spaces;
using MonopolyKata.MonopolyPlayer;
using MonopolyKataTests.Strategies.JailStrategies;
using MonopolyKataTests.Strategies.MortgageStrategies;

namespace MonopolyKataTests
{
    [TestClass]
    public class PlayerTests
    {
        Player player;
        
        [TestInitialize]
        public void Setup()
        {
            player = new Player("Name", new RandomlyMortgage(), new RandomlyPay());
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
            player.ReceiveMoney(101);
            player.Pay(100);
            Assert.AreEqual(1, player.Money);
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
            player.ReceiveMoney(1);
            player.Pay(2);
            Assert.IsTrue(player.LostTheGame);
        }

        [TestMethod]
        public void PlayerHasNotLostTheGameAtStart()
        {
            Assert.IsFalse(player.LostTheGame);
        }

        [TestMethod]
        public void PlayerCanBuyProperty()
        {
            var property = new Property("property", 5, 1, GROUPING.DARK_BLUE);
            player.ReceiveMoney(5);
            property.LandOn(player);

            Assert.AreEqual(0, player.Money);
            Assert.IsTrue(player.Owns(property));
            Assert.IsTrue(property.Owned);
            Assert.IsTrue(property.Owner.Equals(player));
        }
    }
}