using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Board.Spaces;

namespace Monopoly.Tests.Board.Spaces
{
    public class TestRealEstate : RealEstate
    {
        public TestRealEstate(String name, Int32 price) : base(name, price) { }
        protected override Int32 GetRent() { return 0; }
    }
}