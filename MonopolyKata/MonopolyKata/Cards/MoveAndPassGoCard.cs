using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Board;
using Monopoly.Board.Spaces;
using Monopoly.Handlers;

namespace Monopoly.Cards
{
    public class MoveAndPassGoCard : ICard
    {
        public readonly String Name;
        public Boolean Held { get; private set; }

        private readonly Int32 position;
        private ISpace destination;

        public MoveAndPassGoCard(String name, Int32 position, ISpace destination)
        {
            Name = name;
            this.position = position;
            this.destination = destination;
        }

        public void Execute(Player player)
        {
            if (position < player.Position)
                MovePlayerToGo(player);

            player.Move(position);
            destination.LandOn(player);
        }

        private void MovePlayerToGo(Player player)
        {
            player.Move(BoardConstants.BOARD_SIZE - player.Position);
            
            var goHandler = new GoHandler(player);
            goHandler.HandleGo();
        }
    }
}