using System.Collections.Generic;
using System.Linq;
using Monopoly.Handlers;
using Monopoly.Board.Spaces;
using Monopoly.Dice;
using System;

namespace Monopoly.Board
{
    public class BoardFactory
    {        
        public static IEnumerable<ISpace> CreateMonopolyBoard(IDice dice)
        {
            var board = new List<ISpace>();

            var mediteranean = new Property("Mediteranean Avenue", 60, 2, GROUPING.PURPLE, 50, new[] { 10, 30, 90, 160, 250 });
            var baltic = new Property("Baltic Avenue", 60, 4, GROUPING.PURPLE, 50, new[] { 20, 60, 180, 320, 450 });
            var oriental = new Property("Oriental Avenue", 100, 6, GROUPING.LIGHT_BLUE, 50, new[] { 30, 90, 270, 400, 550 });
            var vermont = new Property("Vermont Avenue", 100, 6, GROUPING.LIGHT_BLUE, 50, new[] { 30, 90, 270, 400, 550 });
            var connecticut = new Property("Connecticut Avenue", 120, 8, GROUPING.LIGHT_BLUE, 50, new[] { 30, 90, 270, 400, 550 });
            var stCharles = new Property("St. Charles Place", 140, 10, GROUPING.PINK, 100, new[] { 50, 150, 450, 625, 750 });
            var states = new Property("States Avenue", 140, 10, GROUPING.PINK, 100, new[] { 50, 150, 450, 625, 750 });
            var virginia = new Property("Virginia Avenue", 160, 12, GROUPING.PINK, 100, new[] { 60, 180, 500, 700, 900 });
            var stJames = new Property("St. James Place", 180, 14, GROUPING.ORANGE, 100, new[] { 70, 200, 550, 750, 950 });
            var tennessee = new Property("Tennessee Avenue", 180, 14, GROUPING.ORANGE, 100, new[] { 70, 200, 550, 750, 950 });
            var newYork = new Property("New York Avenue", 200, 16, GROUPING.ORANGE, 100, new[] { 80, 220, 600, 800, 1000 });
            var kentucky = new Property("Kentucky Avenue", 220, 18, GROUPING.RED, 150, new[] { 90, 250, 700, 875, 1050 });
            var indiana = new Property("Indiana Avenue", 220, 18, GROUPING.RED, 150, new[] { 90, 250, 700, 875, 1050 });
            var illinois = new Property("Illinois Avenue", 240, 20, GROUPING.RED, 150, new[] { 100, 300, 750, 925, 1100 });
            var atlantic = new Property("Atlantic Avenue", 260, 22, GROUPING.YELLOW, 150, new[] { 110, 330, 800, 975, 1150 });
            var ventnor = new Property("Ventnor Avenue", 260, 22, GROUPING.YELLOW, 150, new[] { 110, 330, 800, 975, 1150 });
            var marvin = new Property("Marvin Gardens", 280, 24, GROUPING.YELLOW, 150, new[] { 120, 360, 850, 1025, 1200 });
            var pacific = new Property("Pacific Avenue", 300, 26, GROUPING.GREEN, 200, new[] { 130, 390, 900, 1100, 1275 });
            var northCarolina = new Property("North Carolina Avenue", 300, 26, GROUPING.GREEN, 200, new[] { 130, 390, 900, 1100, 1275 });
            var pennsylvaniaAve = new Property("Pennsylvania Avenue", 320, 28, GROUPING.GREEN, 200, new[] { 150, 450, 1000, 1200, 1400 });
            var park = new Property("Park Place", 350, 35, GROUPING.DARK_BLUE, 200, new[] { 175, 500, 1100, 1300, 1500 });
            var boardwalk = new Property("Boardwalk", 400, 50, GROUPING.DARK_BLUE, 200, new[] { 200, 600, 1400, 1700, 2000 });

            var properties = new[] { mediteranean, baltic, oriental, vermont, connecticut, stCharles, states, virginia, stJames, tennessee,
                                     newYork, kentucky, indiana, illinois, atlantic, ventnor, marvin, pacific, northCarolina, pennsylvaniaAve,
                                     park, boardwalk };
            foreach (var property in properties)
            {
                var groupProperties = properties.Where(x => x.Grouping == property.Grouping);
                property.SetPropertiesInGroup(groupProperties);
            }

            var shortLine = new Railroad("Short Line");
            var bAndO = new Railroad("B&O Railroad");
            var pennsylvania = new Railroad("Pennsylvania Railroad");
            var reading = new Railroad("Reading Railroad");

            var railroads = new[] { shortLine, bAndO, pennsylvania, reading };
            foreach (var railroad in railroads)
                railroad.SetRailroads(railroads);


            var water = new Utility("Water Works", dice);
            var electric = new Utility("Electric Company", dice);

            var utilities = new[] { water, electric };
            foreach (var utility in utilities)
                utility.SetUtilities(utilities);

            board.Add(new NormalSpace("GO"));
            board.Add(mediteranean);
            board.Add(new NormalSpace("Community Chest"));
            board.Add(baltic);
            board.Add(new IncomeTax());
            board.Add(reading);
            board.Add(oriental);
            board.Add(new NormalSpace("Chance"));
            board.Add(vermont);
            board.Add(connecticut);
            board.Add(new NormalSpace("Jail/Just Visiting"));
            board.Add(stCharles);
            board.Add(electric);
            board.Add(states);
            board.Add(virginia);
            board.Add(pennsylvania);
            board.Add(stJames);
            board.Add(new NormalSpace("Community Chest"));
            board.Add(tennessee);
            board.Add(newYork);
            board.Add(new NormalSpace("Free Parking"));
            board.Add(kentucky);
            board.Add(new NormalSpace("Chance"));
            board.Add(indiana);
            board.Add(illinois);
            board.Add(bAndO);
            board.Add(atlantic);
            board.Add(ventnor);
            board.Add(water);
            board.Add(marvin);
            board.Add(new NormalSpace("Go To Jail"));
            board.Add(pacific);
            board.Add(northCarolina);
            board.Add(new NormalSpace("Community Chest"));
            board.Add(pennsylvaniaAve);
            board.Add(shortLine);
            board.Add(new NormalSpace("Chance"));
            board.Add(park);
            board.Add(new LuxuryTax());
            board.Add(boardwalk);

            return board;
        }
    }
}