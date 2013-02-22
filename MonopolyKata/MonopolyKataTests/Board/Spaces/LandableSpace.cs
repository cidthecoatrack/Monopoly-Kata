using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Board.Spaces;
using Monopoly.Players;

namespace Monopoly.Tests.Board.Spaces
{
    public class LandableSpace : ISpace
    {
        public Boolean LandedOn { get; private set; }

        public void LandOn(Player player)
        {
            LandedOn = true;
        }
    }
}