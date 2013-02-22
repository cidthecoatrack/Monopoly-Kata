using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Board.Spaces;
using Monopoly.Players;

namespace Monopoly.Handlers
{
    public class RealEstateHandler
    {
        private Dictionary<Int32, OwnableSpace> allRealEstate;
        private Dictionary<Player, List<OwnableSpace>> ownedRealEstate;
        private Banker banker;

        public RealEstateHandler(Dictionary<Int32, OwnableSpace> realEstate, IEnumerable<Player> players, Banker banker)
        {
            allRealEstate = realEstate;
            this.banker = banker;

            ownedRealEstate = new Dictionary<Player, List<OwnableSpace>>();
            foreach (var player in players)
                ownedRealEstate.Add(player, new List<OwnableSpace>());
        }

        public Boolean Contains(Int32 position)
        {
            return allRealEstate.ContainsKey(position);
        }

        public void Land(Player player, Int32 position)
        {
            var realEstate = allRealEstate[position];
            var money = banker.GetMoney(player);

            CheckForBankruptcies();

            if (Owned(realEstate))
                PayRent(player, realEstate);
            else if (banker.CanAfford(player, realEstate.Price) && player.RealEstateStrategy.ShouldBuy(money))
                Buy(player, realEstate);
        }

        private void Buy(Player player, OwnableSpace realEstate)
        {
            banker.Pay(player, realEstate.Price);
            ownedRealEstate[player].Add(realEstate);

            if (realEstate is Property)
                SetMonopolyFlags(player, realEstate as Property);
            else if (realEstate is Railroad)
                SetRailroadCount(player);
            else
                SetBothUtilitiesOwnedFlag();
        }

        private void SetBothUtilitiesOwnedFlag()
        {
            var utilities = allRealEstate.Values.OfType<Utility>();
            if (utilities.All(u => ownedRealEstate.Values.Any(l => l.Contains(u))))
                foreach (var utility in utilities)
                    utility.BothUtilitiesOwned = true;

        }

        private void SetRailroadCount(Player player)
        {
            var owners = ownedRealEstate.Keys.Where(o => ownedRealEstate[o].Any(r => r is Railroad));
            foreach (var owner in owners)
            {
                var railroads = ownedRealEstate[owner].OfType<Railroad>();
                foreach (var railroad in railroads)
                    railroad.RailroadCount = railroads.Count();
            }
        }

        private void SetMonopolyFlags(Player player, Property property)
        {
            var groupProperties = allRealEstate.Values.OfType<Property>().Where(p => p.Grouping == property.Grouping);
            if (groupProperties.All(p => ownedRealEstate[player].Contains(p)))
                foreach (var groupProperty in groupProperties)
                    groupProperty.PartOfMonopoly = true;
        }

        private void PayRent(Player player, OwnableSpace realEstate)
        {
            var rent = realEstate.GetRent();
            var owner = GetOwner(realEstate);

            CheckForBankruptcies();

            banker.Transact(player, owner, rent);
        }

        private void CheckForBankruptcies()
        {
            var bankruptcies = ownedRealEstate.Keys.Where(p => banker.IsBankrupt(p));
            var toRemove = new List<Player>(bankruptcies);
            foreach (var player in toRemove)
                ownedRealEstate.Remove(player);
        }

        private Boolean Owned(OwnableSpace realEstate)
        {
            return ownedRealEstate.Any(o => o.Value.Contains(realEstate));
        }

        public void HandleMortgages(Player player)
        {
            CheckForBankruptcies();

            var realEstateToMortgage = ownedRealEstate[player].Where(r => !r.Mortgaged);
            foreach (var realEstate in realEstateToMortgage)
                if (player.MortgageStrategy.ShouldMortgage(banker.GetMoney(player)))
                    CheckMortgage(realEstate);

            var realEstateToPayOff = ownedRealEstate[player].Where(r => r.Mortgaged);
            foreach (var realEstate in realEstateToPayOff)
                if (banker.CanAfford(player, realEstate.Price) && player.MortgageStrategy.ShouldPayOffMortgage(banker.GetMoney(player), realEstate))
                    PayOffMortgage(realEstate);
        }

        public Player GetOwner(OwnableSpace realEstate)
        {
            return ownedRealEstate.Keys.First(p => ownedRealEstate[p].Contains(realEstate));
        }

        private void CheckMortgage(OwnableSpace realEstate)
        {
            if (realEstate is Property)
            {
                var property = realEstate as Property;

                if (property.Houses > 0 && EvenBuildAllowsSellingOfHouse(property))
                    SellHouseOrHotel(property);
                else if (property.Houses == 0 && NoPropertiesInGroupHaveHouses(property.Grouping))
                    Mortgage(property);
            }
            else
            {
                Mortgage(realEstate);
            }
        }

        private Boolean NoPropertiesInGroupHaveHouses(GROUPING group)
        {
            var groupProperties = allRealEstate.Values.OfType<Property>().Where(p => p.Grouping == group);
            return groupProperties.All(p => p.Houses == 0);
        }

        private Boolean EvenBuildAllowsSellingOfHouse(Property property)
        {
            var groupProperties = allRealEstate.Values.OfType<Property>().Where(p => p.Grouping == property.Grouping);
            return property.Houses == groupProperties.Max(p => p.Houses);
        }

        private void SellHouseOrHotel(Property property)
        {
            var owner = GetOwner(property);
            property.SellHouseOrHotel();
            banker.Collect(owner, property.HousePrice / 2);
        }

        private void Mortgage(OwnableSpace realEstate)
        {
            var owner = GetOwner(realEstate);
            realEstate.Mortgaged = true;
            banker.Collect(owner, realEstate.Price / 10 * 9);
        }

        private void PayOffMortgage(OwnableSpace realEstate)
        {
            var owner = GetOwner(realEstate);
            banker.Pay(owner, realEstate.Price);
            realEstate.Mortgaged = false;
        }

        public void DevelopProperties(Player player)
        {
            CheckForBankruptcies();

            var money = banker.GetMoney(player);
            var propertiesToDevelop = ownedRealEstate[player].OfType<Property>().Where(p => CanBuyHouseOrHotel(p));
            foreach (var property in propertiesToDevelop)
            {
                if (banker.CanAfford(player, property.HousePrice) && player.RealEstateStrategy.ShouldDevelop(money))
                {
                    banker.Pay(player, property.HousePrice);
                    property.BuyHouseOrHotel();
                }
            }
        }

        private Boolean CanBuyHouseOrHotel(Property property)
        {
            return !AnyPropertiesInGroupAreMortgaged(property.Grouping) && EvenBuildAllowsANewHouseHere(property);
        }

        private Boolean AnyPropertiesInGroupAreMortgaged(GROUPING group)
        {
            var groupProperties = allRealEstate.Values.OfType<Property>().Where(p => p.Grouping == group);
            return groupProperties.Any(p => p.Mortgaged);
        }

        private Boolean EvenBuildAllowsANewHouseHere(Property property)
        {
            var groupProperties = allRealEstate.Values.OfType<Property>().Where(p => p.Grouping == property.Grouping);
            return property.Houses == groupProperties.Min(p => p.Houses);
        }

        public Int32 GetNumberOfHouses(Player player)
        {
            CheckForBankruptcies();

            return ownedRealEstate[player].OfType<Property>().Sum(x => x.Houses);
        }

        public Int32 GetNumberOfHotels(Player player)
        {
            CheckForBankruptcies();

            return ownedRealEstate[player].OfType<Property>().Where(x => x.Houses == 5).Count();
        }

        public void LandAndForce10xUtilityRent(Player player, Int32 utilityPosition)
        {
            var utility = allRealEstate[utilityPosition] as Utility;

            CheckForBankruptcies();

            utility.Force10xRent = true;
            Land(player, utilityPosition);
            utility.Force10xRent = false;
        }
    }
}