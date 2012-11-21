using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyKata
{
    public class Player
    {
        public Int32 Position;
        public String Name;

        public Player(String Name)
        {
            this.Name = Name;
            Position = 0;
        }
    }
}
