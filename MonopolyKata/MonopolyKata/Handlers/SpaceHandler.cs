using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Board.Spaces;
using Monopoly.Players;

namespace Monopoly.Handlers
{
    public class SpaceHandler
    {
        private Dictionary<Int32, ISpace> spaces;
        private Dictionary<int, ISpace> dictionary;
        private IEnumerable<Player> players;

        public SpaceHandler(Dictionary<Int32, ISpace> spaces)
        {
            this.spaces = spaces;
        }

        public void Land(Player player, Int32 position)
        {
            spaces[position].LandOn(player);
        }
    }
}