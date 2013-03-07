using System;
using Monopoly.Players.Strategies;

namespace Monopoly.Players
{
    public interface IPlayer
    {
        IJailStrategy JailStrategy { get; set; }
        IOwnableStrategy OwnableStrategy { get; set; }
    }
}