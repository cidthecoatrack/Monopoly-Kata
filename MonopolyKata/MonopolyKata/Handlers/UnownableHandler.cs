using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Board.Spaces;
using Monopoly.Players;

namespace Monopoly.Handlers
{
    public class UnownableHandler : IUnownableHandler
    {
        private Dictionary<Int32, UnownableSpace> spaces;

        public UnownableHandler(Dictionary<Int32, UnownableSpace> spaces)
        {
            this.spaces = spaces;
        }

        public void Land(IPlayer player, Int32 position)
        {
            spaces[position].LandOn(player);
        }
    }
}