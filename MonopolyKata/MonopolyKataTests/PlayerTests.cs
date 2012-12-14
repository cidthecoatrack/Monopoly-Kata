using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata.MonopolyBoard;
using MonopolyKata.MonopolyPlayer;
using MonopolyKataTests.MortgageStrategies;

namespace MonopolyKataTests
{
    [TestClass]
    public class PlayerTests
    {
        Player player;
        
        [TestInitialize]
        public void Setup()
        {
            player = new Player("Name", new RandomlyMortgage());
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
            Player differentPlayer = new Player("Other Name", new RandomlyMortgage());
            AssertPlayersAreDifferent(player, differentPlayer);

            differentPlayer = new Player(player.Name, new RandomlyMortgage());
            differentPlayer.ReceiveMoney(1);
            AssertPlayersAreDifferent(player, differentPlayer);

            differentPlayer = new Player(player.Name, new RandomlyMortgage());
            differentPlayer.SetPosition(1);
            AssertPlayersAreDifferent(player, differentPlayer);

            differentPlayer = new Player(player.Name, new RandomlyMortgage());
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

        [TestMethod]
        public void PlayerImplementsMortgageStrategy()
        {
            PlayerAlwaysMortgages();
            PlayerNeverMortgages();
            PlayerMortgagesWhenSheHasLessThan500();
        }

        private void PlayerNeverMortgages()
        {
            player = new Player("name", new NeverMortgage());
            var firstProperty = new Property("first", 50, 5, GROUPING.YELLOW);
            var secondProperty = new Property("second", 50, 5, GROUPING.YELLOW);
            var thirdProperty = new Property("third", 50, 5, GROUPING.RED);

            player.ReceiveMoney(150);
            firstProperty.LandOn(player);
            secondProperty.LandOn(player);
            thirdProperty.LandOn(player);
            player.HandleMortgages();

            Assert.IsFalse(firstProperty.Mortgaged);
            Assert.IsFalse(secondProperty.Mortgaged);
            Assert.IsFalse(thirdProperty.Mortgaged);

            firstProperty.Mortgage();
            player.ReceiveMoney(50);
            player.HandleMortgages();

            Assert.IsFalse(firstProperty.Mortgaged);
            Assert.IsFalse(secondProperty.Mortgaged);
            Assert.IsFalse(thirdProperty.Mortgaged);
        }

        private void PlayerMortgagesWhenSheHasLessThan500()
        {
            player = new Player("name", new MortgageIfMoneyLessThanFiveHundred());
            var firstProperty = new Property("first", 50, 5, GROUPING.YELLOW);
            var secondProperty = new Property("second", 50, 5, GROUPING.YELLOW);
            var thirdProperty = new Property("third", 50, 5, GROUPING.RED);

            player.ReceiveMoney(600);
            firstProperty.LandOn(player);
            secondProperty.LandOn(player);
            thirdProperty.LandOn(player);
            player.HandleMortgages();

            Assert.IsTrue(firstProperty.Mortgaged);
            Assert.IsTrue(secondProperty.Mortgaged);
            Assert.IsFalse(thirdProperty.Mortgaged);
            player.ReceiveMoney(10);
            player.HandleMortgages();

            Assert.IsFalse(firstProperty.Mortgaged);
            Assert.IsTrue(secondProperty.Mortgaged);
            Assert.IsFalse(thirdProperty.Mortgaged);
        }

        private void PlayerAlwaysMortgages()
        {
            player = new Player("name", new AlwaysMortgage());
            var firstProperty = new Property("first", 50, 5, GROUPING.YELLOW);
            var secondProperty = new Property("second", 50, 5, GROUPING.YELLOW);
            var thirdProperty = new Property("third", 50, 5, GROUPING.RED);

            player.ReceiveMoney(150);
            firstProperty.LandOn(player);
            secondProperty.LandOn(player);
            thirdProperty.LandOn(player);
            player.HandleMortgages();

            Assert.IsTrue(firstProperty.Mortgaged);
            Assert.IsTrue(secondProperty.Mortgaged);
            Assert.IsTrue(thirdProperty.Mortgaged);
            
            player.HandleMortgages();

            Assert.IsTrue(firstProperty.Mortgaged);
            Assert.IsTrue(secondProperty.Mortgaged);
            Assert.IsTrue(thirdProperty.Mortgaged);
        }
    }
}