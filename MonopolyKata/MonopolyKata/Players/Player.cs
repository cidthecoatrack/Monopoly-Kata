using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Board.Spaces;
using Monopoly.Games;
using Monopoly.Players.Strategies;

namespace Monopoly.Players
{
    public class Player
    {
        public IJailStrategy JailStrategy { get; set; }
        public IOwnableStrategy OwnableStrategy { get; set; }

        private readonly String name;

        public Player(String name)
        {
            this.name = name;
        }

        public Boolean WillUseGetOutOfJailCard()
        {
            return JailStrategy.UseCard();
        }

        public override String ToString()
        {
            return name;
        }
    }
}