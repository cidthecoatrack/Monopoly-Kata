using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board;
using Monopoly.Board.Spaces;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Board
{
    [TestClass]
    public class BoardFactoryTests
    {
        private Dictionary<Int32, RealEstate> realEstate;
        private Dictionary<Int32, ISpace> spaces;
        
        [TestInitialize]
        public void Setup()
        {
            var dice = new ControlledDice();

            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();
            var players = new[]
                {
                    new Player("player 1", strategies),
                    new Player("player 2", strategies)
                };

            var banker = new Banker(players);
            realEstate = BoardFactory.CreateRealEstate(dice);
            spaces = BoardFactory.CreateNonRealEstateSpaces(banker);
        }

        [TestMethod]
        public void TotalSpaces()
        {
            Assert.AreEqual(BoardConstants.BOARD_SIZE, spaces.Count() + realEstate.Count());
        }

        [TestMethod]
        public void NumberOfSpaces()
        {
            Assert.AreEqual(12, spaces.Count());
        }

        [TestMethod]
        public void AmountOfRealEstate()
        {
            Assert.AreEqual(28, realEstate.Count());
        }

        [TestMethod]
        public void SpacesNames()
        {
            Assert.AreEqual("GO", spaces[BoardConstants.GO].ToString());
            Assert.AreEqual("Community Chest", spaces[2].ToString());
            Assert.AreEqual("Income Tax", spaces[BoardConstants.INCOME_TAX].ToString());
            Assert.AreEqual("Chance", spaces[7].ToString());
            Assert.AreEqual("Jail/Just Visiting", spaces[BoardConstants.JAIL_OR_JUST_VISITING].ToString());
            Assert.AreEqual("Community Chest", spaces[17].ToString());
            Assert.AreEqual("Free Parking", spaces[BoardConstants.FREE_PARKING].ToString());
            Assert.AreEqual("Chance", spaces[22].ToString());
            Assert.AreEqual("Go To Jail", spaces[BoardConstants.GO_TO_JAIL].ToString());
            Assert.AreEqual("Community Chest", spaces[33].ToString());
            Assert.AreEqual("Chance", spaces[36].ToString());
            Assert.AreEqual("Luxury Tax", spaces[BoardConstants.LUXURY_TAX].ToString());
        }

        [TestMethod]
        public void RealEstateNames()
        {
            Assert.AreEqual("Mediteranean Avenue", realEstate[BoardConstants.MEDITERANEAN_AVENUE].ToString());
            Assert.AreEqual("Baltic Avenue", realEstate[BoardConstants.BALTIC_AVENUE].ToString());
            Assert.AreEqual("Reading Railroad", realEstate[BoardConstants.READING_RAILROAD].ToString());
            Assert.AreEqual("Oriental Avenue", realEstate[BoardConstants.ORIENTAL_AVENUE].ToString());
            Assert.AreEqual("Vermont Avenue", realEstate[BoardConstants.VERMONT_AVENUE].ToString());
            Assert.AreEqual("Connecticut Avenue", realEstate[BoardConstants.CONNECTICUT_AVENUE].ToString());
            Assert.AreEqual("St. Charles Place", realEstate[BoardConstants.ST_CHARLES_PLACE].ToString());
            Assert.AreEqual("Electric Company", realEstate[BoardConstants.ELECTRIC_COMPANY].ToString());
            Assert.AreEqual("States Avenue", realEstate[BoardConstants.STATES_AVENUE].ToString());
            Assert.AreEqual("Virginia Avenue", realEstate[BoardConstants.VIRGINIA_AVENUE].ToString());
            Assert.AreEqual("Pennsylvania Railroad", realEstate[BoardConstants.PENNSYLVANIA_RAILROAD].ToString());
            Assert.AreEqual("St. James Place", realEstate[BoardConstants.ST_JAMES_PLACE].ToString());
            Assert.AreEqual("Tennessee Avenue", realEstate[BoardConstants.TENNESSEE_AVENUE].ToString());
            Assert.AreEqual("New York Avenue", realEstate[BoardConstants.NEW_YORK_AVENUE].ToString());
            Assert.AreEqual("Kentucky Avenue", realEstate[BoardConstants.KENTUCKY_AVENUE].ToString());
            Assert.AreEqual("Indiana Avenue", realEstate[BoardConstants.INDIANA_AVENUE].ToString());
            Assert.AreEqual("Illinois Avenue", realEstate[BoardConstants.ILLINOIS_AVENUE].ToString());
            Assert.AreEqual("B&O Railroad", realEstate[BoardConstants.BandO_RAILROAD].ToString());
            Assert.AreEqual("Atlantic Avenue", realEstate[BoardConstants.ATLANTIC_AVENUE].ToString());
            Assert.AreEqual("Ventnor Avenue", realEstate[BoardConstants.VENTNOR_AVENUE].ToString());
            Assert.AreEqual("Water Works", realEstate[BoardConstants.WATER_WORKS].ToString());
            Assert.AreEqual("Marvin Gardens", realEstate[BoardConstants.MARVIN_GARDENS].ToString());
            Assert.AreEqual("Pacific Avenue", realEstate[BoardConstants.PACIFIC_AVENUE].ToString());
            Assert.AreEqual("North Carolina Avenue", realEstate[BoardConstants.NORTH_CAROLINA_AVENUE].ToString());
            Assert.AreEqual("Pennsylvania Avenue", realEstate[BoardConstants.PENNSYLVANIA_AVENUE].ToString());
            Assert.AreEqual("Short Line", realEstate[BoardConstants.SHORT_LINE].ToString());
            Assert.AreEqual("Park Place", realEstate[BoardConstants.PARK_PLACE].ToString());
            Assert.AreEqual("Boardwalk", realEstate[BoardConstants.BOARDWALK].ToString());
        }

        [TestMethod]
        public void PurpleGroup()
        {
            var purple = realEstate.Values.OfType<Property>().Where(x => x.Grouping == GROUPING.PURPLE);
            Assert.AreEqual(2, purple.Count());

            Assert.AreEqual(GROUPING.PURPLE, (realEstate[BoardConstants.MEDITERANEAN_AVENUE] as Property).Grouping);
            Assert.AreEqual(GROUPING.PURPLE, (realEstate[BoardConstants.BALTIC_AVENUE] as Property).Grouping);
        }

        [TestMethod]
        public void LightBlueGroup()
        {
            var lightBlue = realEstate.Values.OfType<Property>().Where(x => x.Grouping == GROUPING.LIGHT_BLUE);
            Assert.AreEqual(3, lightBlue.Count());

            Assert.AreEqual(GROUPING.LIGHT_BLUE, (realEstate[BoardConstants.ORIENTAL_AVENUE] as Property).Grouping);
            Assert.AreEqual(GROUPING.LIGHT_BLUE, (realEstate[BoardConstants.VERMONT_AVENUE] as Property).Grouping);
            Assert.AreEqual(GROUPING.LIGHT_BLUE, (realEstate[BoardConstants.CONNECTICUT_AVENUE] as Property).Grouping);
        }

        [TestMethod]
        public void PinkGroup()
        {
            var pink = realEstate.Values.OfType<Property>().Where(x => x.Grouping == GROUPING.PINK);
            Assert.AreEqual(3, pink.Count());

            Assert.AreEqual(GROUPING.PINK, (realEstate[BoardConstants.ST_CHARLES_PLACE] as Property).Grouping);
            Assert.AreEqual(GROUPING.PINK, (realEstate[BoardConstants.STATES_AVENUE] as Property).Grouping);
            Assert.AreEqual(GROUPING.PINK, (realEstate[BoardConstants.VIRGINIA_AVENUE] as Property).Grouping);
        }

        [TestMethod]
        public void OrangeGroup()
        {
            var orange = realEstate.Values.OfType<Property>().Where(x => x.Grouping == GROUPING.ORANGE);
            Assert.AreEqual(3, orange.Count());

            Assert.AreEqual(GROUPING.ORANGE, (realEstate[BoardConstants.ST_JAMES_PLACE] as Property).Grouping);
            Assert.AreEqual(GROUPING.ORANGE, (realEstate[BoardConstants.TENNESSEE_AVENUE] as Property).Grouping);
            Assert.AreEqual(GROUPING.ORANGE, (realEstate[BoardConstants.NEW_YORK_AVENUE] as Property).Grouping);
        }

        [TestMethod]
        public void RedGroup()
        {
            var red = realEstate.Values.OfType<Property>().Where(x => x.Grouping == GROUPING.RED);
            Assert.AreEqual(3, red.Count());

            Assert.AreEqual(GROUPING.RED, (realEstate[BoardConstants.KENTUCKY_AVENUE] as Property).Grouping);
            Assert.AreEqual(GROUPING.RED, (realEstate[BoardConstants.INDIANA_AVENUE] as Property).Grouping);
            Assert.AreEqual(GROUPING.RED, (realEstate[BoardConstants.ILLINOIS_AVENUE] as Property).Grouping);
        }

        [TestMethod]
        public void YellowGroup()
        {
            var yellow = realEstate.Values.OfType<Property>().Where(x => x.Grouping == GROUPING.YELLOW);
            Assert.AreEqual(3, yellow.Count());

            Assert.AreEqual(GROUPING.YELLOW, (realEstate[BoardConstants.ATLANTIC_AVENUE] as Property).Grouping);
            Assert.AreEqual(GROUPING.YELLOW, (realEstate[BoardConstants.VENTNOR_AVENUE] as Property).Grouping);
            Assert.AreEqual(GROUPING.YELLOW, (realEstate[BoardConstants.MARVIN_GARDENS] as Property).Grouping);
        }

        [TestMethod]
        public void GreenGroup()
        {
            var green = realEstate.Values.OfType<Property>().Where(x => x.Grouping == GROUPING.GREEN);
            Assert.AreEqual(3, green.Count());

            Assert.AreEqual(GROUPING.GREEN, (realEstate[BoardConstants.PACIFIC_AVENUE] as Property).Grouping);
            Assert.AreEqual(GROUPING.GREEN, (realEstate[BoardConstants.NORTH_CAROLINA_AVENUE] as Property).Grouping);
            Assert.AreEqual(GROUPING.GREEN, (realEstate[BoardConstants.PENNSYLVANIA_AVENUE] as Property).Grouping);
        }

        [TestMethod]
        public void DarkBlueGroup()
        {
            var darkBlue = realEstate.Values.OfType<Property>().Where(x => x.Grouping == GROUPING.DARK_BLUE);
            Assert.AreEqual(2, darkBlue.Count());

            Assert.AreEqual(GROUPING.DARK_BLUE, (realEstate[BoardConstants.PARK_PLACE] as Property).Grouping);
            Assert.AreEqual(GROUPING.DARK_BLUE, (realEstate[BoardConstants.BOARDWALK] as Property).Grouping);
        }

        [TestMethod]
        public void TwentyTwoProperties()
        {
            var properties = realEstate.Values.OfType<Property>().Count();
            Assert.AreEqual(22, properties);
        }

        [TestMethod]
        public void FourRailroads()
        {
            var railroads = realEstate.Values.OfType<Railroad>().Count();
            Assert.AreEqual(4, railroads);
        }

        [TestMethod]
        public void TwoUtilities()
        {
            var utilities = realEstate.Values.OfType<Utility>().Count();
            Assert.AreEqual(2, utilities);
        }

        [TestMethod]
        public void FourNormalSpaces()
        {
            var normals = spaces.Values.OfType<NormalSpace>().Count();
            Assert.AreEqual(4, normals);
        }

        [TestMethod]
        public void SixDrawCardSpaces()
        {
            var drawCards = spaces.Values.OfType<DrawCard>().Count();
            Assert.AreEqual(6, drawCards);
        }

        [TestMethod]
        public void OneIncomeTax()
        {
            var income = spaces.Values.OfType<IncomeTax>().Count();
            Assert.AreEqual(1, income);
        }

        [TestMethod]
        public void OneLuxuryTax()
        {
            var luxury = spaces.Values.OfType<LuxuryTax>().Count();
            Assert.AreEqual(1, luxury);
        }
    }
}