using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Handlers;
using Monopoly.Board;
using Monopoly.Board.Spaces;
using Monopoly.Tests.Dice;
using System;

namespace Monopoly.Tests.Board
{
    [TestClass]
    public class BoardFactoryTests
    {
        IEnumerable<ISpace> board;
        
        [TestInitialize]
        public void Setup()
        {
            var dice = new ControlledDice();
            board = BoardFactory.CreateMonopolyBoard(dice);
        }

        [TestMethod]
        public void NumberOfSpaces()
        {
            Assert.AreEqual(BoardConstants.BOARD_SIZE, board.Count());
        }

        [TestMethod]
        public void Names()
        {
            Assert.AreEqual("GO", board.ElementAt(BoardConstants.GO).ToString());
            Assert.AreEqual("Mediteranean Avenue", board.ElementAt(BoardConstants.MEDITERANEAN_AVENUE).ToString());
            Assert.AreEqual("Community Chest", board.ElementAt(2).ToString());
            Assert.AreEqual("Baltic Avenue", board.ElementAt(BoardConstants.BALTIC_AVENUE).ToString());
            Assert.AreEqual("Income Tax", board.ElementAt(BoardConstants.INCOME_TAX).ToString());
            Assert.AreEqual("Reading Railroad", board.ElementAt(BoardConstants.READING_RAILROAD).ToString());
            Assert.AreEqual("Oriental Avenue", board.ElementAt(BoardConstants.ORIENTAL_AVENUE).ToString());
            Assert.AreEqual("Chance", board.ElementAt(7).ToString());
            Assert.AreEqual("Vermont Avenue", board.ElementAt(BoardConstants.VERMONT_AVENUE).ToString());
            Assert.AreEqual("Connecticut Avenue", board.ElementAt(BoardConstants.CONNECTICUT_AVENUE).ToString());
            Assert.AreEqual("Jail/Just Visiting", board.ElementAt(BoardConstants.JAIL_OR_JUST_VISITING).ToString());
            Assert.AreEqual("St. Charles Place", board.ElementAt(BoardConstants.ST_CHARLES_PLACE).ToString());
            Assert.AreEqual("Electric Company", board.ElementAt(BoardConstants.ELECTRIC_COMPANY).ToString());
            Assert.AreEqual("States Avenue", board.ElementAt(BoardConstants.STATES_AVENUE).ToString());
            Assert.AreEqual("Virginia Avenue", board.ElementAt(BoardConstants.VIRGINIA_AVENUE).ToString());
            Assert.AreEqual("Pennsylvania Railroad", board.ElementAt(BoardConstants.PENNSYLVANIA_RAILROAD).ToString());
            Assert.AreEqual("St. James Place", board.ElementAt(BoardConstants.ST_JAMES_PLACE).ToString());
            Assert.AreEqual("Community Chest", board.ElementAt(17).ToString());
            Assert.AreEqual("Tennessee Avenue", board.ElementAt(BoardConstants.TENNESSEE_AVENUE).ToString());
            Assert.AreEqual("New York Avenue", board.ElementAt(BoardConstants.NEW_YORK_AVENUE).ToString());
            Assert.AreEqual("Free Parking", board.ElementAt(BoardConstants.FREE_PARKING).ToString());
            Assert.AreEqual("Kentucky Avenue", board.ElementAt(BoardConstants.KENTUCKY_AVENUE).ToString());
            Assert.AreEqual("Chance", board.ElementAt(22).ToString());
            Assert.AreEqual("Indiana Avenue", board.ElementAt(BoardConstants.INDIANA_AVENUE).ToString());
            Assert.AreEqual("Illinois Avenue", board.ElementAt(BoardConstants.ILLINOIS_AVENUE).ToString());
            Assert.AreEqual("B&O Railroad", board.ElementAt(BoardConstants.BandO_RAILROAD).ToString());
            Assert.AreEqual("Atlantic Avenue", board.ElementAt(BoardConstants.ATLANTIC_AVENUE).ToString());
            Assert.AreEqual("Ventnor Avenue", board.ElementAt(BoardConstants.VENTNOR_AVENUE).ToString());
            Assert.AreEqual("Water Works", board.ElementAt(BoardConstants.WATER_WORKS).ToString());
            Assert.AreEqual("Marvin Gardens", board.ElementAt(BoardConstants.MARVIN_GARDENS).ToString());
            Assert.AreEqual("Go To Jail", board.ElementAt(BoardConstants.GO_TO_JAIL).ToString());
            Assert.AreEqual("Pacific Avenue", board.ElementAt(BoardConstants.PACIFIC_AVENUE).ToString());
            Assert.AreEqual("North Carolina Avenue", board.ElementAt(BoardConstants.NORTH_CAROLINA_AVENUE).ToString());
            Assert.AreEqual("Community Chest", board.ElementAt(33).ToString());
            Assert.AreEqual("Pennsylvania Avenue", board.ElementAt(BoardConstants.PENNSYLVANIA_AVENUE).ToString());
            Assert.AreEqual("Short Line", board.ElementAt(BoardConstants.SHORT_LINE).ToString());
            Assert.AreEqual("Chance", board.ElementAt(36).ToString());
            Assert.AreEqual("Park Place", board.ElementAt(BoardConstants.PARK_PLACE).ToString());
            Assert.AreEqual("Luxury Tax", board.ElementAt(BoardConstants.LUXURY_TAX).ToString());
            Assert.AreEqual("Boardwalk", board.ElementAt(BoardConstants.BOARDWALK).ToString());
        }

        [TestMethod]
        public void PurpleGroup()
        {
            var purple = board.OfType<Property>().Where(x => x.Grouping == GROUPING.PURPLE);
            Assert.AreEqual(2, purple.Count());

            Assert.AreEqual(GROUPING.PURPLE, (board.ElementAt(BoardConstants.MEDITERANEAN_AVENUE) as Property).Grouping);
            Assert.AreEqual(GROUPING.PURPLE, (board.ElementAt(BoardConstants.BALTIC_AVENUE) as Property).Grouping);
        }

        [TestMethod]
        public void LightBlueGroup()
        {
            var lightBlue = board.OfType<Property>().Where(x => x.Grouping == GROUPING.LIGHT_BLUE);
            Assert.AreEqual(3, lightBlue.Count());

            Assert.AreEqual(GROUPING.LIGHT_BLUE, (board.ElementAt(BoardConstants.ORIENTAL_AVENUE) as Property).Grouping);
            Assert.AreEqual(GROUPING.LIGHT_BLUE, (board.ElementAt(BoardConstants.VERMONT_AVENUE) as Property).Grouping);
            Assert.AreEqual(GROUPING.LIGHT_BLUE, (board.ElementAt(BoardConstants.CONNECTICUT_AVENUE) as Property).Grouping);
        }

        [TestMethod]
        public void PinkGroup()
        {
            var pink = board.OfType<Property>().Where(x => x.Grouping == GROUPING.PINK);
            Assert.AreEqual(3, pink.Count());

            Assert.AreEqual(GROUPING.PINK, (board.ElementAt(BoardConstants.ST_CHARLES_PLACE) as Property).Grouping);
            Assert.AreEqual(GROUPING.PINK, (board.ElementAt(BoardConstants.STATES_AVENUE) as Property).Grouping);
            Assert.AreEqual(GROUPING.PINK, (board.ElementAt(BoardConstants.VIRGINIA_AVENUE) as Property).Grouping);
        }

        [TestMethod]
        public void OrangeGroup()
        {
            var orange = board.OfType<Property>().Where(x => x.Grouping == GROUPING.ORANGE);
            Assert.AreEqual(3, orange.Count());

            Assert.AreEqual(GROUPING.ORANGE, (board.ElementAt(BoardConstants.ST_JAMES_PLACE) as Property).Grouping);
            Assert.AreEqual(GROUPING.ORANGE, (board.ElementAt(BoardConstants.TENNESSEE_AVENUE) as Property).Grouping);
            Assert.AreEqual(GROUPING.ORANGE, (board.ElementAt(BoardConstants.NEW_YORK_AVENUE) as Property).Grouping);
        }

        [TestMethod]
        public void RedGroup()
        {
            var red = board.OfType<Property>().Where(x => x.Grouping == GROUPING.RED);
            Assert.AreEqual(3, red.Count());

            Assert.AreEqual(GROUPING.RED, (board.ElementAt(BoardConstants.KENTUCKY_AVENUE) as Property).Grouping);
            Assert.AreEqual(GROUPING.RED, (board.ElementAt(BoardConstants.INDIANA_AVENUE) as Property).Grouping);
            Assert.AreEqual(GROUPING.RED, (board.ElementAt(BoardConstants.ILLINOIS_AVENUE) as Property).Grouping);
        }

        [TestMethod]
        public void YellowGroup()
        {
            var yellow = board.OfType<Property>().Where(x => x.Grouping == GROUPING.YELLOW);
            Assert.AreEqual(3, yellow.Count());

            Assert.AreEqual(GROUPING.YELLOW, (board.ElementAt(BoardConstants.ATLANTIC_AVENUE) as Property).Grouping);
            Assert.AreEqual(GROUPING.YELLOW, (board.ElementAt(BoardConstants.VENTNOR_AVENUE) as Property).Grouping);
            Assert.AreEqual(GROUPING.YELLOW, (board.ElementAt(BoardConstants.MARVIN_GARDENS) as Property).Grouping);
        }

        [TestMethod]
        public void GreenGroup()
        {
            var green = board.OfType<Property>().Where(x => x.Grouping == GROUPING.GREEN);
            Assert.AreEqual(3, green.Count());

            Assert.AreEqual(GROUPING.GREEN, (board.ElementAt(BoardConstants.PACIFIC_AVENUE) as Property).Grouping);
            Assert.AreEqual(GROUPING.GREEN, (board.ElementAt(BoardConstants.NORTH_CAROLINA_AVENUE) as Property).Grouping);
            Assert.AreEqual(GROUPING.GREEN, (board.ElementAt(BoardConstants.PENNSYLVANIA_AVENUE) as Property).Grouping);
        }

        [TestMethod]
        public void DarkBlueGroup()
        {
            var darkBlue = board.OfType<Property>().Where(x => x.Grouping == GROUPING.DARK_BLUE);
            Assert.AreEqual(2, darkBlue.Count());

            Assert.AreEqual(GROUPING.DARK_BLUE, (board.ElementAt(BoardConstants.PARK_PLACE) as Property).Grouping);
            Assert.AreEqual(GROUPING.DARK_BLUE, (board.ElementAt(BoardConstants.BOARDWALK) as Property).Grouping);
        }

        [TestMethod]
        public void TwentyTwoProperties()
        {
            var properties = board.Count(x => x is Property);
            Assert.AreEqual(22, properties);
        }

        [TestMethod]
        public void FourRailroads()
        {
            var railroads = board.Count(x => x is Railroad);
            Assert.AreEqual(4, railroads);
        }

        [TestMethod]
        public void TwoUtilities()
        {
            var utilities = board.Count(x => x is Utility);
            Assert.AreEqual(2, utilities);
        }

        [TestMethod]
        public void NineNormalSpaces()
        {
            var normals = board.Count(x => x is NormalSpace);
            Assert.AreEqual(10, normals);
        }

        [TestMethod]
        public void OneIncomeTax()
        {
            var income = board.Count(x => x is IncomeTax);
            Assert.AreEqual(1, income);
        }

        [TestMethod]
        public void OneLuxuryTax()
        {
            var luxury = board.Count(x => x is LuxuryTax);
            Assert.AreEqual(1, luxury);
        }
    }
}