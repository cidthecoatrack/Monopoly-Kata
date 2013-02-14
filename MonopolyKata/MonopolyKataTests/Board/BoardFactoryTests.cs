﻿using System.Collections.Generic;
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
        List<ISpace> board;
        
        [TestInitialize]
        public void Setup()
        {
            var dice = new ControlledDice();
            var boardFactory = new BoardFactory();
            board = boardFactory.CreateMonopolyBoard(dice, new JailHandler(dice));
        }

        [TestMethod]
        public void NumberOfSpaces()
        {
            Assert.AreEqual(BoardConstants.BOARD_SIZE, board.Capacity);
            Assert.AreEqual(BoardConstants.BOARD_SIZE, board.Count);
        }

        [TestMethod]
        public void Names()
        {
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
            Assert.AreEqual("Pennsylvania Railroad", board[BoardConstants.PENNSYLVANIA_RAILROAD].Name);
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
        public void PurpleGroup()
        {
            var purple = board.OfType<Property>().Where(x => x.Grouping == GROUPING.PURPLE);
            Assert.AreEqual(2, purple.Count());

            Assert.AreEqual(GROUPING.PURPLE, (board[BoardConstants.MEDITERANEAN_AVENUE] as Property).Grouping);
            Assert.AreEqual(GROUPING.PURPLE, (board[BoardConstants.BALTIC_AVENUE] as Property).Grouping);
        }

        [TestMethod]
        public void LightBlueGroup()
        {
            var lightBlue = board.OfType<Property>().Where(x => x.Grouping == GROUPING.LIGHT_BLUE);
            Assert.AreEqual(3, lightBlue.Count());

            Assert.AreEqual(GROUPING.LIGHT_BLUE, (board[BoardConstants.ORIENTAL_AVENUE] as Property).Grouping);
            Assert.AreEqual(GROUPING.LIGHT_BLUE, (board[BoardConstants.VERMONT_AVENUE] as Property).Grouping);
            Assert.AreEqual(GROUPING.LIGHT_BLUE, (board[BoardConstants.CONNECTICUT_AVENUE] as Property).Grouping);
        }

        [TestMethod]
        public void PinkGroup()
        {
            var pink = board.OfType<Property>().Where(x => x.Grouping == GROUPING.PINK);
            Assert.AreEqual(3, pink.Count());

            Assert.AreEqual(GROUPING.PINK, (board[BoardConstants.ST_CHARLES_PLACE] as Property).Grouping);
            Assert.AreEqual(GROUPING.PINK, (board[BoardConstants.STATES_AVENUE] as Property).Grouping);
            Assert.AreEqual(GROUPING.PINK, (board[BoardConstants.VIRGINIA_AVENUE] as Property).Grouping);
        }

        [TestMethod]
        public void OrangeGroup()
        {
            var orange = board.OfType<Property>().Where(x => x.Grouping == GROUPING.ORANGE);
            Assert.AreEqual(3, orange.Count());

            Assert.AreEqual(GROUPING.ORANGE, (board[BoardConstants.ST_JAMES_PLACE] as Property).Grouping);
            Assert.AreEqual(GROUPING.ORANGE, (board[BoardConstants.TENNESSEE_AVENUE] as Property).Grouping);
            Assert.AreEqual(GROUPING.ORANGE, (board[BoardConstants.NEW_YORK_AVENUE] as Property).Grouping);
        }

        [TestMethod]
        public void RedGroup()
        {
            var red = board.OfType<Property>().Where(x => x.Grouping == GROUPING.RED);
            Assert.AreEqual(3, red.Count());

            Assert.AreEqual(GROUPING.RED, (board[BoardConstants.KENTUCKY_AVENUE] as Property).Grouping);
            Assert.AreEqual(GROUPING.RED, (board[BoardConstants.INDIANA_AVENUE] as Property).Grouping);
            Assert.AreEqual(GROUPING.RED, (board[BoardConstants.ILLINOIS_AVENUE] as Property).Grouping);
        }

        [TestMethod]
        public void YellowGroup()
        {
            var yellow = board.OfType<Property>().Where(x => x.Grouping == GROUPING.YELLOW);
            Assert.AreEqual(3, yellow.Count());

            Assert.AreEqual(GROUPING.YELLOW, (board[BoardConstants.ATLANTIC_AVENUE] as Property).Grouping);
            Assert.AreEqual(GROUPING.YELLOW, (board[BoardConstants.VENTNOR_AVENUE] as Property).Grouping);
            Assert.AreEqual(GROUPING.YELLOW, (board[BoardConstants.MARVIN_GARDENS] as Property).Grouping);
        }

        [TestMethod]
        public void GreenGroup()
        {
            var green = board.OfType<Property>().Where(x => x.Grouping == GROUPING.GREEN);
            Assert.AreEqual(3, green.Count());

            Assert.AreEqual(GROUPING.GREEN, (board[BoardConstants.PACIFIC_AVENUE] as Property).Grouping);
            Assert.AreEqual(GROUPING.GREEN, (board[BoardConstants.NORTH_CAROLINA_AVENUE] as Property).Grouping);
            Assert.AreEqual(GROUPING.GREEN, (board[BoardConstants.PENNSYLVANIA_AVENUE] as Property).Grouping);
        }

        [TestMethod]
        public void DarkBlueGroup()
        {
            var darkBlue = board.OfType<Property>().Where(x => x.Grouping == GROUPING.DARK_BLUE);
            Assert.AreEqual(2, darkBlue.Count());

            Assert.AreEqual(GROUPING.DARK_BLUE, (board[BoardConstants.PARK_PLACE] as Property).Grouping);
            Assert.AreEqual(GROUPING.DARK_BLUE, (board[BoardConstants.BOARDWALK] as Property).Grouping);
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
            Assert.AreEqual(9, normals);
        }

        [TestMethod]
        public void OneGoToJail()
        {
            var gotojail = board.Count(x => x is GoToJail);
            Assert.AreEqual(1, gotojail);
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