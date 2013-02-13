using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Strategies;
using Monopoly.Tests.Board.Spaces;

namespace Monopoly.Tests.Strategies.RealEstateStrategies
{
    [TestClass]
    public class RealEstateStrategiesTests
    {
        Property property;
        RealEstate realEstate;

        [TestInitialize]
        public void Setup()
        {
            property = new Property("property", 1, 1, GROUPING.DARK_BLUE, 1, new[] { 1, 2, 3, 4, 5 });
            realEstate = new TestRealEstate("real estate", 1);
        }
        
        [TestMethod]
        public void AlwaysBuy()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void BuyIfAtLeast500OnHand()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void NeverBuy()
        {
            Assert.Fail();
        }
    }
}