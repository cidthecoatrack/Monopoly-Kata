using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Dice;

namespace Monopoly.Board.Spaces
{
    public class Utility : RealEstate
    {
        private IEnumerable<Utility> utilities;
        private IDice dice;

        public Utility(String name, IDice dice) : base(name, 150)
        {
            this.dice = dice;
        }

        public void SetUtilities(IEnumerable<Utility> utilities)
        {
            this.utilities = utilities;
        }

        protected override Int32 GetRent()
        {
            if (TwoUtilitiesAreOwned())
                return 10 * dice.Value;
            return 4 * dice.Value;
        }

        private Boolean TwoUtilitiesAreOwned()
        {
            return utilities.Count() == 2 && utilities.All(x => x.Owned);
        }
    }
}