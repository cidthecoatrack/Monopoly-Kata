using System;
using Monopoly.Handlers;
using Monopoly.Players;

namespace Monopoly.Cards
{
    public class MoveAndPassGoCard : ICard
    {
        public Boolean Held { get; private set; }

        private readonly String name;
        private readonly Int32 position;
        private BoardHandler boardHandler;

        public MoveAndPassGoCard(String name, Int32 position, BoardHandler boardHandler)
        {
            this.name = name;
            this.position = position;
            this.boardHandler = boardHandler;
        }

        public void Execute(Player player)
        {
            boardHandler.MoveTo(player, position);
        }

        public override String ToString()
        {
            return name;
        }
    }
}