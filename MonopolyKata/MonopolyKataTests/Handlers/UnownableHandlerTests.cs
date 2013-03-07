using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Board.Spaces;

namespace Monopoly.Tests.Handlers
{
    [TestClass]
    public class UnownableHandlerTests
    {
        [TestMethod]
        public void Land()
        {
            var space = new LandableSpace();
            var spaces = new Dictionary<Int32, UnownableSpace>();
            spaces.Add(0, space);

            var handler = new UnownableHandler(spaces);
            var player = new Player("name");

            handler.Land(player, 0);

            Assert.IsTrue(space.LandedOn);
        }
    }
}