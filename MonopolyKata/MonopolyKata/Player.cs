using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyKata
{
    public class Player
    {
        private Int32 position;
        private String name;
        private Int32 money;

        public Int32 Position
        {
            get { return position; }
        }

        public String Name
        {
            get { return name; }
        }

        public Int32 Money
        {
            get { return money; }
        }

        public Boolean LostTheGame
        {
            get { return (money < 0); }
        }

        public Player(String Name)
        {
            this.name = Name;
            position = 0;
            money = 0;
        }

        public void Move(Int32 AmountToMove)
        {
            position += AmountToMove;
        }

        public void SetPosition(Int32 NewPosition)
        {
            position = NewPosition;
        }

        public void Pay(Int32 AmountToPay)
        {
            money -= AmountToPay;
        }

        public Boolean CanAfford(Int32 AmountToPay)
        {
            return (money >= AmountToPay);
        }

        public void ReceiveMoney(Int32 AmountToReceive)
        {
            money += AmountToReceive;
        }

        public Boolean Equals(Player PlayerToCompare)
        {
            if (this.position != PlayerToCompare.Position)
                return false;
            if (this.name != PlayerToCompare.Name)
                return false;
            if (this.money != PlayerToCompare.Money)
                return false;

            return true;
        }

        public Boolean IsContainedIn(IEnumerable<Player> Players)
        {
            foreach (Player player in Players)
                if (this.Equals(player))
                    return true;

            return false;
        }
    }
}