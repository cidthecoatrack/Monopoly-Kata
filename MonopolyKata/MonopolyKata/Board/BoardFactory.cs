using System.Collections.Generic;
using System.Linq;
using Monopoly.Handlers;
using Monopoly.Board.Spaces;
using Monopoly.Dice;

namespace Monopoly.Board
{
    public class BoardFactory
    {
        public List<ISpace> CreateMonopolyBoard(IDice dice, JailHandler jailHandler)
        {
            var board = new List<ISpace>();
            board.Capacity = BoardConstants.BOARD_SIZE;

            var mediteranean = new Property("Mediteranean Avenue", 60, 2, GROUPING.PURPLE);
            var baltic = new Property("Baltic Avenue", 60, 4, GROUPING.PURPLE);
            var oriental = new Property("Oriental Avenue", 100, 6, GROUPING.LIGHT_BLUE);
            var vermont = new Property("Vermont Avenue", 100, 6, GROUPING.LIGHT_BLUE);
            var connecticut = new Property("Connecticut Avenue", 120, 8, GROUPING.LIGHT_BLUE);
            var stCharles = new Property("St. Charles Place", 140, 10, GROUPING.PINK);
            var states = new Property("States Avenue", 140, 10, GROUPING.PINK);
            var virginia = new Property("Virginia Avenue", 160, 12, GROUPING.PINK);
            var stJames = new Property("St. James Place", 180, 14, GROUPING.GOLD);
            var tennessee = new Property("Tennessee Avenue", 180, 14, GROUPING.GOLD);
            var newYork = new Property("New York Avenue", 200, 16, GROUPING.GOLD);
            var kentucky = new Property("Kentucky Avenue", 220, 18, GROUPING.RED);
            var indiana = new Property("Indiana Avenue", 220, 18, GROUPING.RED);
            var illinois = new Property("Illinois Avenue", 240, 20, GROUPING.RED);
            var atlantic = new Property("Atlantic Avenue", 260, 22, GROUPING.YELLOW);
            var ventnor = new Property("Ventnor Avenue", 260, 22, GROUPING.YELLOW);
            var marvin = new Property("Marvin Gardens", 280, 22, GROUPING.YELLOW);
            var pacific = new Property("Pacific Avenue", 300, 26, GROUPING.GREEN);
            var northCarolina = new Property("North Carolina Avenue", 300, 26, GROUPING.GREEN);
            var pennsylvaniaAve = new Property("Pennsylvania Avenue", 320, 28, GROUPING.GREEN);
            var park = new Property("Park Place", 350, 35, GROUPING.DARK_BLUE);
            var boardwalk = new Property("Boardwalk", 400, 50, GROUPING.DARK_BLUE);

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
            board.Add(new GoToJail(jailHandler));
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

        public List<ISpace> CreateBoardOfNormalSpaces()
        {
            var board = new List<ISpace>();

            for (var i = 0; i < BoardConstants.BOARD_SIZE; i++)
                board.Add(new NormalSpace(i.ToString()));

            return board;
        }
    }
}