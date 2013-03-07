using System;
using Monopoly.Players;

namespace Monopoly.Board.Spaces
{
    public class NormalSpace : UnownableSpace
    {
        public NormalSpace(String name) : base(name) { }

        public override void LandOn(IPlayer player) { }
    }
}