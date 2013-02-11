using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata.Handlers;
using MonopolyKata.MonopolyBoard;
using MonopolyKata.MonopolyBoard.Spaces;

namespace MonopolyKataTests.BoardTests
{
    [TestClass]
    public class BoardFactoryTests
    {
        List<ISpace> board;
        
        [TestInitialize]
        public void Setup()
        {
            var dice = new DiceForTesting();
            var boardFactory = new BoardFactory();
            board = boardFactory.CreateMonopolyBoard(dice, new JailHandler(dice));
        }

        [TestMethod]
        public void FactoryBuildsCorrectBoard()
        {
            Assert.AreEqual(40, board.Capacity);
            Assert.AreEqual(40, board.Count);
            Assert.AreEqual("GO", board[BoardConstants.GO].Name);
            Assert.AreEqual("Mediteranean Avenue", board[BoardConstants.MEDITERANEAN_AVENUE].Name);
            Assert.AreEqual("Community Chest", board[2].Name);
            Assert.AreEqual("Baltic Avenue", board[BoardConstants.BALTIC_AVENUE].Name);
            Assert.AreEqual("Income Tax", board[BoardConstants.INCOME_TAX].Name);
            Assert.AreEqual("Reading Railroad", board[BoardConstants.READING_RAILROAD].Name);
            Assert.AreEqual("Oriental Avenue", board[BoardConstants.ORIENTAL_AVENUE].Name);
            Assert.AreEqual("Chance", board[7].Name);
            Assert.AreEqual("Vermont Avenue", board[BoardConstants.VERMONT_AVENUE].Name);
            Assert.AreEqual("Connecticut Avenue", board[BoardConstants.CONNECTICUT_AVENUE].Name);
            Assert.AreEqual("Jail/Just Visiting", board[BoardConstants.JAIL_OR_JUST_VISITING].Name);
            Assert.AreEqual("St. Charles Place", board[BoardConstants.ST_CHARLES_PLACE].Name);
            Assert.AreEqual("Electric Company", board[BoardConstants.ELECTRIC_COMPANY].Name);
            Assert.AreEqual("States Avenue", board[BoardConstants.STATES_AVENUE].Name);
            Assert.AreEqual("Virginia Avenue", board[BoardConstants.VIRGINIA_AVENUE].Name);
            Assert.AreEqual("Pennsylvania Avenue", board[BoardConstants.PENNSYLVANIA_AVENUE].Name);
            Assert.AreEqual("St. James Place", board[BoardConstants.ST_JAMES_PLACE].Name);
            Assert.AreEqual("Community Chest", board[17].Name);
            Assert.AreEqual("Tennessee Avenue", board[BoardConstants.TENNESSEE_AVENUE].Name);
            Assert.AreEqual("New York Avenue", board[BoardConstants.NEW_YORK_AVENUE].Name);
            Assert.AreEqual("Free Parking", board[BoardConstants.FREE_PARKING].Name);
            Assert.AreEqual("Kentucky Avenue", board[BoardConstants.KENTUCKY_AVENUE].Name);
            Assert.AreEqual("Chance", board[22].Name);
            Assert.AreEqual("Indiana Avenue", board[BoardConstants.INDIANA_AVENUE].Name);
            Assert.AreEqual("Illinois Avenue", board[BoardConstants.ILLINOIS_AVENUE].Name);
            Assert.AreEqual("B&O Railroad", board[BoardConstants.BandO_RAILROAD].Name);
            Assert.AreEqual("Atlantic Avenue", board[BoardConstants.ATLANTIC_AVENUE].Name);
            Assert.AreEqual("Ventnor Avenue", board[BoardConstants.VENTNOR_AVENUE].Name);
            Assert.AreEqual("Water Works", board[BoardConstants.WATER_WORKS].Name);
            Assert.AreEqual("Marvin Gardens", board[BoardConstants.MARVIN_GARDENS].Name);
            Assert.AreEqual("Go To Jail", board[BoardConstants.GO_TO_JAIL].Name);
            Assert.AreEqual("Pacific Avenue", board[BoardConstants.PACIFIC_AVENUE].Name);
            Assert.AreEqual("North Carolina Avenue", board[BoardConstants.NORTH_CAROLINA_AVENUE].Name);
            Assert.AreEqual("Community Chest", board[33].Name);
            Assert.AreEqual("Pennsylvania Avenue", board[BoardConstants.PENNSYLVANIA_AVENUE].Name);
            Assert.AreEqual("Short Line", board[BoardConstants.SHORT_LINE].Name);
            Assert.AreEqual("Chance", board[36].Name);
            Assert.AreEqual("Park Place", board[BoardConstants.PARK_PLACE].Name);
            Assert.AreEqual("Luxury Tax", board[BoardConstants.LUXURY_TAX].Name);
            Assert.AreEqual("Boardwalk", board[BoardConstants.BOARDWALK].Name);
        }

        [TestMethod]
        public void CanGrabSpecificSpaceTypesFromBoard()
        {
            var properties = board.Count(x => x is Property);
            Assert.AreEqual(22, properties);

            var railroads = board.Count(x => x is Railroad);
            Assert.AreEqual(4, railroads);

            var utilities = board.Count(x => x is Utility);
            Assert.AreEqual(2, utilities);

            var normals = board.Count(x => x is NormalSpace);
            Assert.AreEqual(9, normals);

            var gotojail = board.Count(x => x is GoToJail);
            Assert.AreEqual(1, gotojail);

            var income = board.Count(x => x is IncomeTax);
            Assert.AreEqual(1, income);

            var luxury = board.Count(x => x is LuxuryTax);
            Assert.AreEqual(1, luxury);
        }

        [TestMethod]
        public void PropertiesInGroupsAreAccuratelyAssociated()
        {
            var properties = board.Where(x => x is Property).Cast<Property>();
            var yellowGroup = properties.Where(x => x.Grouping == GROUPING.YELLOW);

            foreach (var y in yellowGroup)
            {
                Assert.IsNotNull(y.PropertiesInGroup);
                Assert.IsTrue(yellowGroup.All(x => y.PropertiesInGroup.Contains(x)));
            }
        }
    }
}