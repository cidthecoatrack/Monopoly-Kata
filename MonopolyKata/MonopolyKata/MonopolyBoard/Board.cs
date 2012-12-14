using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata.MonopolyBoard
{
    public static class Board
    {
        public const Int16 GO = 0;
        public const Int16 MEDITERANEAN_AVENUE = 1;
        public const Int16 BALTIC_AVENUE = 3;
        public const Int16 INCOME_TAX = 4;
        public const Int16 READING_RAILROAD = 5;
        public const Int16 ORIENTAL_AVENUE = 6;
        public const Int16 VERMONT_AVENUE = 8;
        public const Int16 CONNECTICUT_AVENUE = 9;
        public const Int16 JAIL_OR_JUST_VISITING = 10;
        public const Int16 ST_CHARLES_PLACE = 11;
        public const Int16 ELECTRIC_COMPANY = 12;
        public const Int16 STATES_AVENUE = 13;
        public const Int16 VIRGINIA_AVENUE = 14;
        public const Int16 PENNSYLVANIA_RAILROAD = 15;
        public const Int16 ST_JAMES_PLACE = 16;
        public const Int16 TENNESSEE_AVENUE = 18;
        public const Int16 NEW_YORK_AVENUE = 19;
        public const Int16 FREE_PARKING = 20;
        public const Int16 KENTUCKY_AVENUE = 21;
        public const Int16 INDIANA_AVENUE = 23;
        public const Int16 ILLINOIS_AVENUE = 24;
        public const Int16 BandO_RAILROAD = 25;
        public const Int16 ATLANTIC_AVENUE = 26;
        public const Int16 VENTNOR_AVENUE = 27;
        public const Int16 WATER_WORKS = 28;
        public const Int16 MARVIN_GARDENS = 29;
        public const Int16 GO_TO_JAIL = 30;
        public const Int16 PACIFIC_AVENUE = 31;
        public const Int16 NORTH_CAROLINA_AVENUE = 32;
        public const Int16 PENNSYLVANIA_AVENUE = 34;
        public const Int16 SHORT_LINE = 35;
        public const Int16 PARK_PLACE = 37;
        public const Int16 LUXURY_TAX = 38;
        public const Int16 BOARDWALK = 39;
        public const Int16 BOARD_SIZE = 40;

        public static ISpace[] GetMonopolyBoard()
        {
            var board = new ISpace[BOARD_SIZE];
            InitializeSpaces(board);
            AssociateGroups(board);
            return board;
        }

        private static void InitializeSpaces(ISpace[] board)
        {
            board[GO] = new NormalSpace("GO");
            board[MEDITERANEAN_AVENUE] = new Property("Mediteranean Avenue", 60, 2, GROUPING.PURPLE);
            board[2] = new NormalSpace("Community Chest");
            board[BALTIC_AVENUE] = new Property("Baltic Avenue", 60, 4, GROUPING.PURPLE);
            board[INCOME_TAX] = new IncomeTax();
            board[READING_RAILROAD] = new Railroad("Reading Railroad");
            board[ORIENTAL_AVENUE] = new Property("Oriental Avenue", 100, 6, GROUPING.LIGHT_BLUE);
            board[7] = new NormalSpace("Chance");
            board[VERMONT_AVENUE] = new Property("Vermont Avenue", 100, 6, GROUPING.LIGHT_BLUE);
            board[CONNECTICUT_AVENUE] = new Property("Connecticut Avenue", 120, 8, GROUPING.LIGHT_BLUE);
            board[JAIL_OR_JUST_VISITING] = new NormalSpace("Jail/Just Visiting");
            board[ST_CHARLES_PLACE] = new Property("St. Charles Place", 140, 10, GROUPING.PINK);
            board[ELECTRIC_COMPANY] = new Utility("Electric Company");
            board[STATES_AVENUE] = new Property("States Avenue", 140, 10, GROUPING.PINK);
            board[VIRGINIA_AVENUE] = new Property("Virginia Avenue", 160, 12, GROUPING.PINK);
            board[PENNSYLVANIA_RAILROAD] = new Railroad("Pennsylvania Railroad");
            board[ST_JAMES_PLACE] = new Property("St. James Place", 180, 14, GROUPING.GOLD);
            board[17] = new NormalSpace("Community Chest");
            board[TENNESSEE_AVENUE] = new Property("Tennessee Avenue", 180, 14, GROUPING.GOLD);
            board[NEW_YORK_AVENUE] = new Property("New York Avenue", 200, 16, GROUPING.GOLD);
            board[FREE_PARKING] = new NormalSpace("Free Parking");
            board[KENTUCKY_AVENUE] = new Property("Kentucky Avenue", 220, 18, GROUPING.RED);
            board[22] = new NormalSpace("Chance");
            board[INDIANA_AVENUE] = new Property("Indiana Avenue", 220, 18, GROUPING.RED);
            board[ILLINOIS_AVENUE] = new Property("Illinois Avenue", 240, 20, GROUPING.RED);
            board[BandO_RAILROAD] = new Railroad("B&O Railroad");
            board[ATLANTIC_AVENUE] = new Property("Atlantic Avenue", 260, 22, GROUPING.YELLOW);
            board[VENTNOR_AVENUE] = new Property("Ventnor Avenue", 260, 22, GROUPING.YELLOW);
            board[WATER_WORKS] = new Utility("Water Works");
            board[MARVIN_GARDENS] = new Property("Marvin Gardens", 280, 22, GROUPING.YELLOW);
            board[GO_TO_JAIL] = new GoToJail();
            board[PACIFIC_AVENUE] = new Property("Pacific Avenue", 300, 26, GROUPING.GREEN);
            board[NORTH_CAROLINA_AVENUE] = new Property("North Caroline Avenue", 300, 26, GROUPING.GREEN);
            board[33] = new NormalSpace("Community Chest");
            board[PENNSYLVANIA_AVENUE] = new Property("Pennsylvania Avenue", 320, 28, GROUPING.GREEN);
            board[SHORT_LINE] = new Railroad("Short Line");
            board[36] = new NormalSpace("Chance");
            board[PARK_PLACE] = new Property("Park Place", 350, 35, GROUPING.DARK_BLUE);
            board[LUXURY_TAX] = new LuxuryTax();
            board[BOARDWALK] = new Property("Boardwalk", 400, 50, GROUPING.DARK_BLUE);
        }

        private static void AssociateGroups(IEnumerable<ISpace> board)
        {
            var properties = board.Where(x => x.GetType() == typeof(Property)).Cast<Property>();
            foreach (var property in properties)
            {
                var groupProperties = properties.Where(x => x.Grouping == property.Grouping);
                property.SetPropertiesInGroup(groupProperties);
            }

            var railroads = board.Where(x => x.GetType() == typeof(Railroad)).Cast<Railroad>();
            foreach (var railroad in railroads)
                railroad.SetPropertiesInGroup(railroads);

            var utilities = board.Where(x => x.GetType() == typeof(Utility)).Cast<Utility>();
            foreach (var utility in utilities)
                utility.SetPropertiesInGroup(utilities);
        }
    }
}