using System;
using System.Collections.Generic;
using System.Linq;
using MonopolyKata.MonopolyPlayer;

namespace MonopolyKata.MonopolyBoard
{
    public class Utility : Property
    {
        private IEnumerable<Utility> utilities;

        public Utility(String name)
        {
            base.Name = name;
            base.Price = 150;
            base.Mortgaged = false;
        }

        public override void SetPropertiesInGroup(IEnumerable<Property> utilities)
        {
            this.utilities = utilities as IEnumerable<Utility>;
        }

        protected override void MakePlayerPayRent(Player player)
        {
            var rent = GetRent(player.Roll);
            player.Pay(rent);
            Owner.ReceiveMoney(rent);
        }

        public Int32 GetRent(Int32 roll)
        {
            if (TwoUtilitiesAreOwned())
                return 10 * roll;
            return 4 * roll;
        }

        private Boolean TwoUtilitiesAreOwned()
        {
            return utilities.First().Owned && utilities.Last().Owned;
        }
    }
}