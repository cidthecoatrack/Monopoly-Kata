using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board;
using Monopoly.Board.Spaces;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Handlers
{
    [TestClass]
    public class RealEstateHandlerTests
    {
        [TestMethod]
        public void Constructor()
        {
            var dice = new ControlledDice();
            var board = BoardFactory.CreateMonopolyBoard(dice);
            var realEstate = board.OfType<RealEstate>();

            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();
            var players = new[]
                {
                    new Player("Player 1", strategies),
                    new Player("Player 2", strategies),
                };

            var realEstateHandler = new RealEstateHandler(realEstate, players);
        }
    }
}