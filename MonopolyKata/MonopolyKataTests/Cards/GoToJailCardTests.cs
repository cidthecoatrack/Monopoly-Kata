using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board;
using Monopoly.Cards;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Board;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Cards
{
    [TestClass]
    public class GoToJailCardTests
    {
        private GoToJailCard card;
        private Player player;
        private JailHandler jailHandler;
        private BoardHandler boardHandler;

        [TestInitialize]
        public void Setup()
        {
            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();

            player = new Player("name", strategies);

            var dice = new ControlledDice();
            boardHandler = new BoardHandler(new[] { player }, FakeBoardFactory.CreateBoardOfNormalSpaces());
            jailHandler = new JailHandler(dice, boardHandler);

            card = new GoToJailCard(jailHandler);
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("Go To Jail", card.ToString());
        }

        [TestMethod]
        public void GoToJail()
        {
            card.Execute(player);
            Assert.AreEqual(BoardConstants.JAIL_OR_JUST_VISITING, boardHandler.PositionOf[player]);
            Assert.IsTrue(jailHandler.HasImprisoned(player));
        }
    }
}