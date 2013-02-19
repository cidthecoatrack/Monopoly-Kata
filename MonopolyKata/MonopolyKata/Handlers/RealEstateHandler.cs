using System;
using System.Collections;
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
        private IEnumerable<RealEstate> realEstate;
        private Dictionary<Player, List<RealEstate>> ownedRealEstate;

        public RealEstateHandler(IEnumerable<RealEstate> realEstate, IEnumerable<Player> players)
        {
            this.realEstate = realEstate;
            
            ownedRealEstate = new Dictionary<Player,List<RealEstate>>();
            foreach (var player in players)
                ownedRealEstate.Add(player, new List<RealEstate>());
        }

        public Boolean Owns(Player player, RealEstate realEstate)
        {
            return ownedRealEstate[player].Contains(realEstate);
        }

        public void Buy(Player player, RealEstate realEstate)
        {
            if (player.CanAfford(realEstate.Price) && player.RealEstateStrategy.ShouldBuy(player.Money))
            {
                player.Pay(realEstate.Price);
                ownedRealEstate[player].Add(realEstate);

                if (realEstate is Property)
                {
                    var property = realEstate as Property;
                    SetMonopolyFlags(player, property.Grouping);
                }
            }
        }

        private void SetMonopolyFlags(Player player, GROUPING group)
        {
            var groupProperties = realEstate.OfType<Property>().Where(p => p.Grouping == group);

            if (groupProperties.Any(p => !p.PartOfMonopoly) && groupProperties.All(p => ownedRealEstate[player].Contains(p)))
                foreach (var property in groupProperties)
                    property.PartOfMonopoly = true;
        }

        public void HandleMortgages(Player player)
        {
            MortgageProperties(player);
            PayOffMortgages(player);
        }

        private void MortgageProperties(Player player)
        {
            var realEstateToMortgage = ownedRealEstate[player].Where(x => !x.Mortgaged);
            foreach (var property in realEstateToMortgage)
                if (player.MortgageStrategy.ShouldMortgage(player.Money))
                    property.Mortgage();
        }

        private void PayOffMortgages(Player player)
        {
            var mortgagesToPayOff = ownedRealEstate[player].Where(x => x.Mortgaged);
            foreach (var mortgage in mortgagesToPayOff)
                if (player.CanAfford(mortgage.Price) && player.MortgageStrategy.ShouldPayOffMortgage(player.Money, mortgage))
                    mortgage.PayOffMortgage();
        }

        public void DevelopProperties(Player player)
        {
            var propertiesToDevelop = ownedRealEstate[player].OfType<Property>().Where(p => CanBuyHouseOrHotel(p));
            foreach (var property in propertiesToDevelop)
                if (player.CanAfford(property.HousePrice) && player.RealEstateStrategy.ShouldDevelop(player.Money))
                {
                    player.Pay(property.HousePrice);
                    property.BuyHouse();
                }
        }

        public Int32 GetHouses(Player player)
        {
            return ownedRealEstate[player].OfType<Property>().Sum(x => x.Houses);
        }

        public Int32 GetHotels(Player player)
        {
            return ownedRealEstate[player].OfType<Property>().Where(x => x.Houses == 5).Count();
        }

        public Boolean CanBuyHouseOrHotel(Property property)
        {
            return property.PartOfMonopoly && !AnyPropertiesInGroupAreMortgaged(property.Grouping) && EvenBuildAllowsANewHouseHere(property);
        }

        private Boolean AnyPropertiesInGroupAreMortgaged(GROUPING group)
        {
            return realEstate.OfType<Property>().Where(p => p.Grouping == group).Any(p => p.Mortgaged);
        }

        private Boolean EvenBuildAllowsANewHouseHere(Property property)
        {
            return property.Houses == realEstate.OfType<Property>().Where(p => p.Grouping == property.Grouping).Min(p => p.Houses);
        }

        private Boolean AnyPropertiesInGroupHaveHouses(GROUPING group)
        {
            return realEstate.OfType<Property>().Where(p => p.Grouping == group).Any(p => p.Houses > 0);
        }

        private Boolean EvenBuildAllowsSellingHouse(Property property)
        {
            return property.Houses == realEstate.OfType<Property>().Where(p => p.Grouping == property.Grouping).Max(p => p.Houses);
        }
    }
}