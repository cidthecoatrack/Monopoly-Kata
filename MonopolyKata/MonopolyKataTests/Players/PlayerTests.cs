using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Players;
using Monopoly.Tests.Players.Strategies;
using Monopoly.Tests.Players.Strategies.JailStrategies;

namespace Monopoly.Tests.Players
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void Constructor()
        {
            var player = new Player("Name");
            Assert.AreEqual("Name", player.ToString());
        }
    }
}