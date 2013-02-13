using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly;
using Monopoly.Handlers;
using Monopoly.Board;
using Monopoly.Tests.Strategies.JailStrategies;
using Monopoly.Tests.Strategies.MortgageStrategies;

namespace Monopoly.Tests.Handlers
{
    [TestClass]
    public class GoHandlerTests
    {
        Player player;
        GoHandler goHandler;

        [TestInitialize]
        public void Setup()
        {
            player = new Player("name", new NeverMortgage(), new NeverPay());
            goHandler = new GoHandler(player);
        }
        
        [TestMethod]
        public void CannotMoveOffOfBoard()
        {
            player.Move(BoardConstants.BOARD_SIZE + 5);
            goHandler.HandleGo();
            Assert.AreEqual(5, player.Position);
        }

        [TestMethod]
        public void Receive200ForLandingOnGo()
        {
            var previousMoney = player.Money;
            player.Move(BoardConstants.BOARD_SIZE);
            goHandler.HandleGo();
            Assert.AreEqual(previousMoney + GameConstants.PASS_GO_PAYMENT, player.Money);
        }

        [TestMethod]
        public void Receive200ForPassingGo()
        {
            var previousMoney = player.Money;
            player.Move(BoardConstants.BOARD_SIZE + 1);
            goHandler.HandleGo();
            Assert.AreEqual(previousMoney + GameConstants.PASS_GO_PAYMENT, player.Money);
        }

        [TestMethod]
        public void DoNotReceiveMoneyFromLeavingGo()
        {
            var previousMoney = player.Money;
            Assert.AreEqual(0, player.Position);
            player.Move(1);
            goHandler.HandleGo();
            Assert.AreNotEqual(0, player.Position);
            Assert.AreEqual(previousMoney, player.Money);
        }

        [TestMethod]
        public void PassGoMultipleTimes_PlayerGetsMultiplePaymentsOfTwoHundred()
        {
            var previousMoney = player.Money;
            player.Move(BoardConstants.BOARD_SIZE * 2);
            goHandler.HandleGo();
            Assert.AreEqual(previousMoney + GameConstants.PASS_GO_PAYMENT * 2, player.Money);
        }
    }
}