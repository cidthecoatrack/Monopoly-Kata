using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
    public class MoveBackThreeCard : ICard
    {
        public Boolean Held { get; private set; }
        public readonly String Name;

        public MoveBackThreeCard()
        {
            Name = "Go Back 3 Spaces";
        }

        public void Execute(Player player)
        {
            throw new NotImplementedException();
        }
    }
}