using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Players;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Board.Spaces
{
    [TestClass]
    public class RealEstateTests
    {
        [TestMethod]
        public void Constructor()
        {
            var realEstate = new TestRealEstate("real estate", 50);

            Assert.AreEqual("real estate", realEstate.ToString());
            Assert.AreEqual(50, realEstate.Price);
            Assert.IsFalse(realEstate.Mortgaged);
        }
    }
}