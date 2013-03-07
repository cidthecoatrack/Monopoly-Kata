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
        private IBoardHandler boardHandler;

        public MoveAndPassGoCard(String name, Int32 position, IBoardHandler boardHandler)
        {
            this.name = name;
            this.position = position;
            this.boardHandler = boardHandler;
        }

        public void Execute(IPlayer player)
        {
            boardHandler.MoveTo(player, position);
        }

        public override String ToString()
        {
            return name;
        }
    }
}