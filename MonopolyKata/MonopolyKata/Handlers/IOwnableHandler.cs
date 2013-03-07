using System;
using Monopoly.Players;
namespace Monopoly.Handlers
{
    public interface IOwnableHandler
    {
        Boolean Contains(Int32 position);
        void DevelopProperties(IPlayer player);
        Int32 GetNumberOfHotels(IPlayer player);
        Int32 GetNumberOfHouses(IPlayer player);
        void HandleMortgages(IPlayer player);
        void Land(IPlayer player, Int32 position);
        void LandAndForce10xUtilityRent(IPlayer player, Int32 utilityPosition);
        void LandAndPayDoubleRailroadRent(IPlayer player, Int32 railroadPosition);
    }
}
