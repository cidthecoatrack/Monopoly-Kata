using System;
using System.Collections.Generic;
using System.Linq;
using MonopolyKata.MonopolyBoard;

namespace MonopolyKata.MonopolyPlayer
{
    public class Player
    {
        private List<Property> ownedProperty;

        public Int32 Position {get; private set;}
        public String Name {get; private set;}
        public Int32 Money {get; private set;}
        public Int32 Roll { get; private set; }
        public Boolean LostTheGame { get { return (Money < 0); } }
        private IMortgageStrategy mortgageStrategy;

        protected Player() { }

        public Player(String name, IMortgageStrategy mortgageStrategy)
        {
            Name = name;
            Position = 0;
            Money = 0;
            ownedProperty = new List<Property>();
            this.mortgageStrategy = mortgageStrategy;
        }

        public void Move(Int32 amountToMove)
        {
            Roll = amountToMove;
            Position += Roll;
        }

        public void SetPosition(Int32 newPosition)
        {
            Position = newPosition;
        }

        public void Pay(Int32 amountToPay)
        {
            Money -= amountToPay;
        }

        public Boolean CanAfford(Int32 amountToPay)
        {
            return Money >= amountToPay;
        }

        public void ReceiveMoney(Int32 AmountToReceive)
        {
            Money += AmountToReceive;
        }

        public Boolean Equals(Player playerToCompare)
        {
            var theSame = true;

            if (this.Position != playerToCompare.Position)
                theSame = false;
            if (this.Name != playerToCompare.Name)
                theSame = false;
            if (this.Money != playerToCompare.Money)
                theSame = false;

            foreach (var property in ownedProperty)
                if (!playerToCompare.Owns(property))
                    theSame = false;

            return theSame;
        }

        public Boolean Owns(Property property)
        {
            return ownedProperty.Contains(property);
        }

        public void Buy(Property property)
        {
            Pay(property.Price);
            ownedProperty.Add(property);
        }

        public void HandleMortgages()
        {
            MortgageProperties();
            PayOffMortgages();
        }

        private void PayOffMortgages()
        {
            var propertiesToPayOff = ownedProperty.Where(x => x.Mortgaged);
            foreach(var property in propertiesToPayOff)
                if (mortgageStrategy.SaysIShouldPayOffMortgage(Money, property))
                    property.PayOffMortgage();
        }

        private void MortgageProperties()
        {
            var propertiesToMortgage = ownedProperty.Where(x => !x.Mortgaged);
            foreach(var property in propertiesToMortgage)
                if (mortgageStrategy.SaysIShouldMortgage(Money))
                    property.Mortgage();
        }
    }
}