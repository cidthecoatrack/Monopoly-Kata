using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata.MonopolyBoard;

namespace MonopolyKataTests.BoardTests
{
    [TestClass]
    public class BoardTests
    {
        ISpace[] board;
        
        [TestInitialize]
        public void Setup()
        {
            board = Board.GetMonopolyBoard();
        }

        [TestMethod]
        public void CanGrabPropertiesFromBoard()
        {
            var properties = board.Count(x => x.GetType() == typeof(Property));
            Assert.AreEqual(22, properties);
            var railroads = board.Count(x => x.GetType() == typeof(Railroad));
            Assert.AreEqual(4, railroads);
            var utilities = board.Count(x => x.GetType() == typeof(Utility));
            Assert.AreEqual(2, utilities);
        }

        [TestMethod]
        public void PropertiesInGroupsAreAccuratelyAssociated()
        {
            var properties = board.Where(x => x.GetType() == typeof(Property)).Cast<Property>();
            var yellowGroup = properties.Where(x => x.Grouping == GROUPING.YELLOW);

            foreach (var y in yellowGroup)
            {
                Assert.IsNotNull(y.PropertiesInGroup);
                foreach (var ay in yellowGroup)
                    Assert.IsTrue(y.PropertiesInGroup.Contains(ay));
            }
        }
    }
}