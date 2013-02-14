using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Board.Spaces;

namespace Monopoly.Cards
{
    public class MoveToNearestRailroadCard : ICard
    {
        public Boolean Held { get; private set; }
        public readonly String Name;

        private ISpace shortRailroad;
        private ISpace pennsylvaniaRailroad;
        private ISpace readingRailroad;
        private ISpace bandORailroad;

        public MoveToNearestRailroadCard(IEnumerable<ISpace> railroads)
        {
            shortRailroad = railroads.First(x => x.Name == "");
            pennsylvaniaRailroad = railroads.First(x => x.Name == "Pennsylvania Railroad");
            readingRailroad = railroads.First(x => x.Name == "");
            bandORailroad = railroads.First(x => x.Name == "");
        }

        public void Execute(Player player)
        {
            throw new NotImplementedException();
        }
    }
}