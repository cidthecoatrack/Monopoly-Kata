using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Board.Spaces;
using Monopoly.Players;

namespace Monopoly.Handlers
{
    public class OwnableHandler : IOwnableHandler
    {
        private Dictionary<Int32, OwnableSpace> allOwnableSpaces;
        private Dictionary<OwnableSpace, IPlayer> ownedSpaces;
        private readonly IPlayer NO_OWNER;
        private IBanker banker;

        public OwnableHandler(Dictionary<Int32, OwnableSpace> ownableSpaces, IBanker banker)
        {
            allOwnableSpaces = ownableSpaces;
            this.banker = banker;
            NO_OWNER = new Player("NOT AN OWNER");

            ownedSpaces = new Dictionary<OwnableSpace, IPlayer>();
            foreach (var ownableSpace in ownableSpaces.Values)
                ownedSpaces.Add(ownableSpace, NO_OWNER);
        }

        public Boolean Contains(Int32 position)
        {
            return allOwnableSpaces.ContainsKey(position);
        }

        public void Land(IPlayer player, Int32 position)
        {
            CheckForBankrupcies();

            var ownableSpace = allOwnableSpaces[position];
            var money = banker.Money[player];

            if (Owned(ownableSpace))
                PayRent(player, ownableSpace);
            else if (banker.CanAfford(player, ownableSpace.Price) && player.OwnableStrategy.ShouldBuy(money))
                Buy(player, ownableSpace);
        }

        private void Buy(IPlayer player, OwnableSpace ownableSpace)
        {
            banker.Pay(player, ownableSpace.Price);
            ownedSpaces[ownableSpace] = player;

            if (ownableSpace is Property)
                SetMonopolyFlags(player, ownableSpace as Property);
            else if (ownableSpace is Railroad)
                SetRailroadCount(player);
            else
                SetBothUtilitiesOwnedFlag();
        }

        private void SetBothUtilitiesOwnedFlag()
        {
            var utilities = allOwnableSpaces.Values.OfType<Utility>();
            if (utilities.All(u => Owned(u)))
                foreach (var utility in utilities)
                    utility.BothUtilitiesOwned = true;
        }

        private void SetRailroadCount(IPlayer player)
        {
            var ownedRailroads = ownedSpaces.Keys.Where(o => Owned(o)).OfType<Railroad>();
            foreach (var RxR in ownedRailroads)
                RxR.RailroadCount = ownedSpaces.Count(kvp => kvp.Value == ownedSpaces[RxR]);
        }

        private void SetMonopolyFlags(IPlayer player, Property property)
        {
            var groupProperties = allOwnableSpaces.Values.OfType<Property>().Where(prop => prop.Grouping == property.Grouping);
            if (groupProperties.All(prop => ownedSpaces[prop] == player))
                foreach (var groupProperty in groupProperties)
                    groupProperty.PartOfMonopoly = true;
        }

        private void PayRent(IPlayer player, OwnableSpace ownableSpace)
        {
            var rent = ownableSpace.GetRent();

            banker.Transact(player, ownedSpaces[ownableSpace], rent);
            CheckForBankrupcies();    
        }

        private void CheckForBankrupcies()
        {
            var reposessions = ownedSpaces.Keys.Where(o => Owned(o) && banker.IsBankrupt(ownedSpaces[o])).ToList();
            foreach (var repo in reposessions)
                ownedSpaces[repo] = NO_OWNER;
        }

        private Boolean Owned(OwnableSpace ownableSpace)
        {
            return ownedSpaces[ownableSpace] != NO_OWNER;
        }

        private IEnumerable<OwnableSpace> GetOwnedSpaces(IPlayer player)
        {
            return ownedSpaces.Keys.Where(k => ownedSpaces[k] == player);
        }

        public void HandleMortgages(IPlayer player)
        {
            var realEstateToMortgage = GetOwnedSpaces(player).Where(r => !r.Mortgaged);
            foreach (var realEstate in realEstateToMortgage)
                if (player.OwnableStrategy.ShouldMortgage(banker.Money[player]))
                    CheckMortgage(realEstate);

            var realEstateToPayOff = GetOwnedSpaces(player).Where(r => r.Mortgaged);
            foreach (var realEstate in realEstateToPayOff)
                if (banker.CanAfford(player, realEstate.Price) && player.OwnableStrategy.ShouldPayOffMortgage(banker.Money[player], realEstate))
                    PayOffMortgage(realEstate);
        }

        private void CheckMortgage(OwnableSpace ownableSpace)
        {
            if (ownableSpace is Property)
            {
                var property = ownableSpace as Property;

                if (property.Houses > 0 && EvenBuildAllowsSellingOfHouse(property))
                    SellHouseOrHotel(property);
                else if (property.Houses == 0 && NoPropertiesInGroupHaveHouses(property.Grouping))
                    Mortgage(property);
            }
            else
            {
                Mortgage(ownableSpace);
            }
        }

        private Boolean NoPropertiesInGroupHaveHouses(GROUPING group)
        {
            var groupProperties = allOwnableSpaces.Values.OfType<Property>().Where(p => p.Grouping == group);
            return groupProperties.All(p => p.Houses == 0);
        }

        private Boolean EvenBuildAllowsSellingOfHouse(Property property)
        {
            var groupProperties = allOwnableSpaces.Values.OfType<Property>().Where(p => p.Grouping == property.Grouping);
            return property.Houses == groupProperties.Max(p => p.Houses);
        }

        private void SellHouseOrHotel(Property property)
        {
            property.SellHouseOrHotel();
            banker.Collect(ownedSpaces[property], property.HousePrice / 2);
        }

        private void Mortgage(OwnableSpace ownableSpace)
        {
            ownableSpace.Mortgaged = true;
            banker.Collect(ownedSpaces[ownableSpace], ownableSpace.Price / 10 * 9);
        }

        private void PayOffMortgage(OwnableSpace ownableSpace)
        {
            banker.Pay(ownedSpaces[ownableSpace], ownableSpace.Price);
            ownableSpace.Mortgaged = false;
        }

        public void DevelopProperties(IPlayer player)
        {
            var money = banker.Money[player];
            var propertiesToDevelop = GetOwnedSpaces(player).OfType<Property>().Where(p => CanBuyHouseOrHotel(p));
            foreach (var property in propertiesToDevelop)
            {
                if (banker.CanAfford(player, property.HousePrice) && player.OwnableStrategy.ShouldDevelop(money))
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
            var groupProperties = allOwnableSpaces.Values.OfType<Property>().Where(p => p.Grouping == group);
            return groupProperties.Any(p => p.Mortgaged);
        }

        private Boolean EvenBuildAllowsANewHouseHere(Property property)
        {
            var groupProperties = allOwnableSpaces.Values.OfType<Property>().Where(p => p.Grouping == property.Grouping);
            return property.Houses == groupProperties.Min(p => p.Houses);
        }

        public Int32 GetNumberOfHouses(IPlayer player)
        {
            return GetOwnedSpaces(player).OfType<Property>().Sum(x => x.Houses);
        }

        public Int32 GetNumberOfHotels(IPlayer player)
        {
            return GetOwnedSpaces(player).OfType<Property>().Where(x => x.Houses == 5).Count();
        }

        public void LandAndForce10xUtilityRent(IPlayer player, Int32 utilityPosition)
        {
            var utility = allOwnableSpaces[utilityPosition] as Utility;

            utility.Force10xRent = true;
            Land(player, utilityPosition);
            utility.Force10xRent = false;
        }

        public void LandAndPayDoubleRailroadRent(IPlayer player, Int32 railroadPosition)
        {
            var rxr = allOwnableSpaces[railroadPosition] as Railroad;

            rxr.DoubleRent = true;
            Land(player, railroadPosition);
            rxr.DoubleRent = false;
        }
    }
}