using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Board;
using Monopoly.Board.Spaces;
using Monopoly.Cards;
using Monopoly.Strategies;

namespace Monopoly
{
    public class Player
    {
        public Int32 Money { get; private set; }
        public Boolean LostTheGame { get { return (Money < 0); } }

        private readonly String Name;
        private IJailStrategy jailStrategy;
        private IMortgageStrategy mortgageStrategy;
        private IRealEstateStrategy realEstateStrategy;
        private List<RealEstate> ownedRealEstate;

        public Player(String name, IStrategyCollection strategies)
        {
            Name = name;
            Money = 1500;
            ownedRealEstate = new List<RealEstate>();

            mortgageStrategy = strategies.MortgageStrategy;
            jailStrategy = strategies.JailStrategy;
            realEstateStrategy = strategies.RealEstateStrategy;
        }

        public void Pay(Int32 amountToPay)
        {
            Money -= amountToPay;
        }

        public Boolean CanAfford(Int32 amountToPay)
        {
            return Money >= amountToPay;
        }

        public void Collect(Int32 amountToCollect)
        {
            Money += amountToCollect;
        }

        public Boolean Owns(RealEstate realEstate)
        {
            return ownedRealEstate.Contains(realEstate);
        }

        public void Buy(RealEstate realEstate)
        {
            if (CanAfford(realEstate.Price) && realEstateStrategy.ShouldBuy(Money))
            {
                Pay(realEstate.Price);
                ownedRealEstate.Add(realEstate);
            }
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
                if (CanAfford(property.Price) && mortgageStrategy.ShouldPayOffMortgage(Money, property))
                    property.PayOffMortgage();
        }

        public Boolean WillPayToGetOutOfJail()
        {
            return jailStrategy.ShouldPay(Money) && CanAfford(GameConstants.COST_TO_GET_OUT_OF_JAIL);
        }

        public Boolean WillUseGetOutOfJailCard()
        {
            return jailStrategy.UseCard();
        }

        public void DevelopProperties()
        {
            var propertiesToDevelop = ownedRealEstate.OfType<Property>().Where(x => x.CanBuyHouseOrHotel());
            foreach (var property in propertiesToDevelop)
                if (CanAfford(property.HousePrice) && realEstateStrategy.ShouldDevelop(Money))
                    property.BuyHouse();
        }

        public Int32 GetNumberOfHouses()
        {
            return ownedRealEstate.OfType<Property>().Sum(x => x.Houses);
        }

        public Int32 GetNumberOfHotels()
        {
            return ownedRealEstate.OfType<Property>().Where(x => x.Houses == 5).Count();
        }

        public override String ToString()
        {
            return Name;
        }
    }
}