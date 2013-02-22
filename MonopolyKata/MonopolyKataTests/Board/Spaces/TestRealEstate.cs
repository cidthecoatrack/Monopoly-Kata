using System;
using Monopoly.Board.Spaces;

namespace Monopoly.Tests.Board.Spaces
{
    public class TestRealEstate : RealEstate
    {
        public TestRealEstate(String name, Int32 price) : base(name, price) { }
        public override Int32 GetRent() { return 1; }
    }
}