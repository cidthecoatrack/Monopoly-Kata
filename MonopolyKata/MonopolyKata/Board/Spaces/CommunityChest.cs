using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Cards;

namespace Monopoly.Board.Spaces
{
    public class CommunityChest : ISpace
    {
        private Queue<ICard> communityChestDeck;

        public CommunityChest(Queue<ICard> communityChestDeck)
        {
            this.communityChestDeck = communityChestDeck;
        }
        
        public void LandOn(Player player)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "Community Chest";
        }
    }
}