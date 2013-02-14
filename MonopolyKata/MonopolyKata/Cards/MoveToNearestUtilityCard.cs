using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Board.Spaces;

namespace Monopoly.Cards
{
    public class MoveToNearestUtilityCard : ICard
    {
        public Boolean Held { get; private set; }
        public readonly String Name;

        private ISpace electric;
        private ISpace water;

        public MoveToNearestUtilityCard(IEnumerable<ISpace> utilities)
        {
            electric = utilities.First(x => x.Name == "Electric Company");
            water = utilities.First(x => x.Name == "Water Works");
        }

        public void Execute(Player player)
        {
            throw new NotImplementedException();
        }
    }
}