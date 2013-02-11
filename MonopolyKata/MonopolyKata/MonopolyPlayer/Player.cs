using System;
using System.Collections.Generic;
using System.Linq;
using MonopolyKata.MonopolyBoard;
using MonopolyKata.MonopolyBoard.Spaces;
using MonopolyKata.Strategies;

namespace MonopolyKata.MonopolyPlayer
{
    public class Player
    {
        public Int32 Money { get; private set; }
        public String Name { get; private set; }
        public Int32 Position { get; private set; }
        public Boolean LostTheGame { get { return (Money < 0); } }

        private IJailStrategy jailStrategy;
        private IMortgageStrategy mortgageStrategy;
        private List<RealEstate> ownedRealEstate;

        public Player(String name, IMortgageStrategy mortgageStrategy, IJailStrategy jailStrategy)
        {
            Name = name;
            Position = 0;
            Money = 1500;
            ownedRealEstate = new List<RealEstate>();
            this.mortgageStrategy = mortgageStrategy;
            this.jailStrategy = jailStrategy;
        }

        public void Move(Int32 amountToMove)
        {
            Position += amountToMove;
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

        public Boolean Owns(RealEstate realEstate)
        {
            return ownedRealEstate.Contains(realEstate);
        }

        public void Buy(RealEstate realEstate)
        {
            Pay(realEstate.Price);
            ownedRealEstate.Add(realEstate);
        }

        public void HandleMortgages()
        {
            MortgageProperties();
            PayOffMortgages();
        }

        private void MortgageProperties()
        {
            var propertiesToMortgage = ownedRealEstate.Where(x => !x.Mortgaged);
            foreach (var property in propertiesToMortgage)
                if (mortgageStrategy.ShouldMortgage(Money))
                    property.Mortgage();
        }

        private void PayOffMortgages()
        {
            var propertiesToPayOff = ownedRealEstate.Where(x => x.Mortgaged);
            foreach(var property in propertiesToPayOff)
                if (mortgageStrategy.ShouldPayOffMortgage(Money, property))
                    property.PayOffMortgage();
        }

        public Boolean WillPayToGetOutOfJail()
        {
            return jailStrategy.SaysIShouldPayToGetOutOfJail(Money) && CanAfford(GameConstants.COST_TO_GET_OUT_OF_JAIL);
        }
    }
}