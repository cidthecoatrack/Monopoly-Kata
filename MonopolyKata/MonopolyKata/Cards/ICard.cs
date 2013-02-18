using System;

namespace Monopoly.Cards
{
    public interface ICard
    {
        Boolean Held { get; }

        void Execute(Player player);
    }
}