﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
    public interface ICard
    {
        Boolean Held { get; }

        void Execute(Player player);
    }
}