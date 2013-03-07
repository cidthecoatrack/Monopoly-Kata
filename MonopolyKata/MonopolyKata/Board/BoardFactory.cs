using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Board.Spaces;
using Monopoly.Dice;
using Monopoly.Handlers;

namespace Monopoly.Board
{
    public class BoardFactory
    {
        public static Dictionary<Int32, OwnableSpace> CreateRealEstate(IDice dice)
        {
            var realEstate = new Dictionary<Int32, OwnableSpace>();

            realEstate.Add(BoardConstants.MEDITERANEAN_AVENUE, new Property("Mediteranean Avenue", 60, 2, GROUPING.PURPLE, 50, new[] { 10, 30, 90, 160, 250 }));
            realEstate.Add(BoardConstants.BALTIC_AVENUE, new Property("Baltic Avenue", 60, 4, GROUPING.PURPLE, 50, new[] { 20, 60, 180, 320, 450 }));
            realEstate.Add(BoardConstants.ORIENTAL_AVENUE, new Property("Oriental Avenue", 100, 6, GROUPING.LIGHT_BLUE, 50, new[] { 30, 90, 270, 400, 550 }));
            realEstate.Add(BoardConstants.VERMONT_AVENUE, new Property("Vermont Avenue", 100, 6, GROUPING.LIGHT_BLUE, 50, new[] { 30, 90, 270, 400, 550 }));
            realEstate.Add(BoardConstants.CONNECTICUT_AVENUE, new Property("Connecticut Avenue", 120, 8, GROUPING.LIGHT_BLUE, 50, new[] { 30, 90, 270, 400, 550 }));
            realEstate.Add(BoardConstants.ST_CHARLES_PLACE, new Property("St. Charles Place", 140, 10, GROUPING.PINK, 100, new[] { 50, 150, 450, 625, 750 }));
            realEstate.Add(BoardConstants.STATES_AVENUE, new Property("States Avenue", 140, 10, GROUPING.PINK, 100, new[] { 50, 150, 450, 625, 750 }));
            realEstate.Add(BoardConstants.VIRGINIA_AVENUE, new Property("Virginia Avenue", 160, 12, GROUPING.PINK, 100, new[] { 60, 180, 500, 700, 900 }));
            realEstate.Add(BoardConstants.ST_JAMES_PLACE, new Property("St. James Place", 180, 14, GROUPING.ORANGE, 100, new[] { 70, 200, 550, 750, 950 }));
            realEstate.Add(BoardConstants.TENNESSEE_AVENUE, new Property("Tennessee Avenue", 180, 14, GROUPING.ORANGE, 100, new[] { 70, 200, 550, 750, 950 }));
            realEstate.Add(BoardConstants.NEW_YORK_AVENUE, new Property("New York Avenue", 200, 16, GROUPING.ORANGE, 100, new[] { 80, 220, 600, 800, 1000 }));
            realEstate.Add(BoardConstants.KENTUCKY_AVENUE, new Property("Kentucky Avenue", 220, 18, GROUPING.RED, 150, new[] { 90, 250, 700, 875, 1050 }));
            realEstate.Add(BoardConstants.INDIANA_AVENUE, new Property("Indiana Avenue", 220, 18, GROUPING.RED, 150, new[] { 90, 250, 700, 875, 1050 }));
            realEstate.Add(BoardConstants.ILLINOIS_AVENUE, new Property("Illinois Avenue", 240, 20, GROUPING.RED, 150, new[] { 100, 300, 750, 925, 1100 }));
            realEstate.Add(BoardConstants.ATLANTIC_AVENUE, new Property("Atlantic Avenue", 260, 22, GROUPING.YELLOW, 150, new[] { 110, 330, 800, 975, 1150 }));
            realEstate.Add(BoardConstants.VENTNOR_AVENUE, new Property("Ventnor Avenue", 260, 22, GROUPING.YELLOW, 150, new[] { 110, 330, 800, 975, 1150 }));
            realEstate.Add(BoardConstants.MARVIN_GARDENS, new Property("Marvin Gardens", 280, 24, GROUPING.YELLOW, 150, new[] { 120, 360, 850, 1025, 1200 }));
            realEstate.Add(BoardConstants.PACIFIC_AVENUE, new Property("Pacific Avenue", 300, 26, GROUPING.GREEN, 200, new[] { 130, 390, 900, 1100, 1275 }));
            realEstate.Add(BoardConstants.NORTH_CAROLINA_AVENUE, new Property("North Carolina Avenue", 300, 26, GROUPING.GREEN, 200, new[] { 130, 390, 900, 1100, 1275 }));
            realEstate.Add(BoardConstants.PENNSYLVANIA_AVENUE, new Property("Pennsylvania Avenue", 320, 28, GROUPING.GREEN, 200, new[] { 150, 450, 1000, 1200, 1400 }));
            realEstate.Add(BoardConstants.PARK_PLACE, new Property("Park Place", 350, 35, GROUPING.DARK_BLUE, 200, new[] { 175, 500, 1100, 1300, 1500 }));
            realEstate.Add(BoardConstants.BOARDWALK, new Property("Boardwalk", 400, 50, GROUPING.DARK_BLUE, 200, new[] { 200, 600, 1400, 1700, 2000 }));
            realEstate.Add(BoardConstants.SHORT_LINE, new Railroad("Short Line"));
            realEstate.Add(BoardConstants.BandO_RAILROAD, new Railroad("B&O Railroad"));
            realEstate.Add(BoardConstants.PENNSYLVANIA_RAILROAD, new Railroad("Pennsylvania Railroad"));
            realEstate.Add(BoardConstants.READING_RAILROAD, new Railroad("Reading Railroad"));
            realEstate.Add(BoardConstants.WATER_WORKS, new Utility("Water Works", dice));
            realEstate.Add(BoardConstants.ELECTRIC_COMPANY, new Utility("Electric Company", dice));

            return realEstate;
        }

        public static Dictionary<Int32, UnownableSpace> CreateNonRealEstateSpaces(IBanker banker)
        {
            var spaces = new Dictionary<Int32, UnownableSpace>();
            
            spaces.Add(BoardConstants.GO, new NormalSpace("GO"));
            spaces.Add(2, new DrawCard("Community Chest"));
            spaces.Add(BoardConstants.INCOME_TAX, new IncomeTax(banker));
            spaces.Add(7, new DrawCard("Chance"));
            spaces.Add(BoardConstants.JAIL_OR_JUST_VISITING, new NormalSpace("Jail/Just Visiting"));
            spaces.Add(17, new DrawCard("Community Chest"));
            spaces.Add(BoardConstants.FREE_PARKING, new NormalSpace("Free Parking"));
            spaces.Add(22, new DrawCard("Chance"));
            spaces.Add(BoardConstants.GO_TO_JAIL, new NormalSpace("Go To Jail"));
            spaces.Add(33, new DrawCard("Community Chest"));
            spaces.Add(36, new DrawCard("Chance"));
            spaces.Add(BoardConstants.LUXURY_TAX, new LuxuryTax(banker));

            return spaces;
        }
    }
}