using System;
using Monopoly.Players;

namespace Monopoly.Cards
{
    public interface ICard
    {
        Boolean Held { get; }

        void Execute(Player player);
    }
}