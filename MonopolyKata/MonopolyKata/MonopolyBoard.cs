using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyKata
{
    public static class MonopolyBoard
    {
        public const Int16 GO = 0;
        public const Int16 BOARD_SIZE = 40;
        public const Int16 JAIL_OR_JUST_VISITING = 10;
        public const Int16 GO_TO_JAIL = 30;
        public const Int16 INCOME_TAX = 4;
        public const Int16 LUXURY_TAX = 38;

        public static string Location(Int32 Position)
        {
            switch (Position)
            {
                case 0: return "GO";
                case 1: return "Mediteranean Avenue";
                case 33:
                case 17:
                case 2: return "Community Chest";
                case 3: return "Baltic Avenue";
                case 4: return "Income Tax";
                case 5: return "Reading Railroad";
                case 6: return "Oriental Avenue";
                case 22:
                case 36:
                case 7: return "Chance";
                case 8: return "Vermont Avenue";
                case 9: return "Connecticut Avenue";
                case 10: return "Jail/Just Visiting";
                case 11: return "St. Charles Place";
                case 12: return "Electric Company";
                case 13: return "States Avenue";
                case 14: return "Virginia Avenue";
                case 15: return "Pennsylvania Railroad";
                case 16: return "St. James Place";
                case 18: return "Tennessee Avenue";
                case 19: return "New York Avenue";
                case 20: return "Free Parking";
                case 21: return "Kentucky Avenue";
                case 23: return "Indiana Avenue";
                case 24: return "Illinois Avenue";
                case 25: return "B&O Railroad";
                case 26: return "Atlantic Avenue";
                case 27: return "Ventnor Avenue";
                case 28: return "Water Works";
                case 29: return "Marvin Gardens";
                case 30: return "Go To Jail";
                case 31: return "Pacific Avenue";
                case 32: return "North Carolina Avenue";
                case 34: return "Pennsylvania Avenue";
                case 35: return "Short Line";
                case 37: return "Park Place";
                case 38: return "Luxury Tax";
                case 39: return "Boardwalk";
                default: throw new IndexOutOfRangeException();
            }
        }
    }
}
