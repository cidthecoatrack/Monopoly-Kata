using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Players;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Board.Spaces
{
    [TestClass]
    public class RailroadTests
    {
        Railroad railroad;

        [TestInitialize]
        public void Setup()
        {
            railroad = new Railroad("RxR");
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual("RxR", railroad.ToString());
            Assert.AreEqual(0, railroad.RailroadCount);
            Assert.AreEqual(200, railroad.Price);
            Assert.IsFalse(railroad.Mortgaged);
        }
        
        [TestMethod]
        public void LandOnOwnedRailroadx1_Pays25()
        {
            railroad.RailroadCount = 1;
            Assert.AreEqual(25, railroad.GetRent());
        }

        [TestMethod]
        public void LandOnOwnedRailroadx2_Pays50()
        {
            railroad.RailroadCount = 2;
            Assert.AreEqual(50, railroad.GetRent());
        }

        [TestMethod]
        public void LandOnOwnedRailroadx3_Pays100()
        {
            railroad.RailroadCount = 3;
            Assert.AreEqual(100, railroad.GetRent());
        }

        [TestMethod]
        public void LandOnOwnedRailroadx4_Pays200()
        {
            railroad.RailroadCount = 4;
            Assert.AreEqual(200, railroad.GetRent());
        }
    }
}