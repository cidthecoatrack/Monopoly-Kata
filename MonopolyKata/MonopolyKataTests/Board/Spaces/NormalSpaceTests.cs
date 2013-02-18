using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;

namespace Monopoly.Tests.Board.Spaces
{
    [TestClass]
    public class NormalSpaceTests
    {
        [TestMethod]
        public void Constructor()
        {
            var space = new NormalSpace("space");
            Assert.AreEqual("space", space.ToString());
        }
    }
}