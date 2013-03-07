using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Board.Spaces;
using Monopoly.Tests.Players.Strategies;
using Monopoly.Tests.Players.Strategies.OwnableStrategies;

namespace Monopoly.Tests.Handlers
{
    [TestClass]
    public class OwnableHandlerTests
    {
        private IOwnableHandler ownableHandler;
        private IPlayer player;
        private IPlayer renter;
        private Property property;
        private IBanker banker;

        [TestInitialize]
        public void Setup()
        {
            player = new Player("name");
            player.OwnableStrategy = new AlwaysBuyOrMortgage();
            renter = new Player("renter");
            renter.OwnableStrategy = new RandomlyBuyOrMortgage();
            var players = new[] { player, renter };
            banker = new Banker(players);
            property = new Property("name", 10, 1, GROUPING.DARK_BLUE, 2, new[] { 4, 5, 6, 7, 8 });
            ownableHandler = FakeHandlerFactory.CreateRealEstateHandler(new[] { property }, players, banker);
        }

        [TestMethod]
        public void Contains()
        {
            Assert.IsTrue(ownableHandler.Contains(0));
            Assert.IsFalse(ownableHandler.Contains(1));
        }

        [TestMethod]
        public void BuyProperty()
        {
            var playerMoney = banker.Money[player];
            ownableHandler.Land(player, 0);

            Assert.AreEqual(playerMoney - property.Price, banker.Money[player]);
        }

        [TestMethod]
        public void PayRent()
        {
            BuyProperty();

            var rent = property.GetRent();
            var renterMoney = banker.Money[renter];
            var ownerMoney = banker.Money[player];
            ownableHandler.Land(renter, 0);

            Assert.AreEqual(renterMoney - rent, banker.Money[renter]);
            Assert.AreEqual(ownerMoney + rent, banker.Money[player]);
        }

        [TestMethod]
        public void BuyHouses()
        {
            property.PartOfMonopoly = true;
            BuyProperty();

            ownableHandler.DevelopProperties(player);

            Assert.AreEqual(1, property.Houses);
        }

        [TestMethod]
        public void CantBuyHousesIfAnyInGroupIsMortgaged()
        {
            BuyProperty();
            property.Mortgaged = true;
            ownableHandler.DevelopProperties(player);

            Assert.AreEqual(0, property.Houses);
        }

        [TestMethod]
        public void SellHouseAtHalfPrice()
        {
            BuyHouses();
            var money = banker.Money[player];
            ownableHandler.HandleMortgages(player);

            Assert.AreEqual(money + property.HousePrice / 2, banker.Money[player]);
        }
        
        [TestMethod]
        public void OwnerLandsOnTheirSpace_NothingHappens()
        {
            BuyProperty();

            var money = banker.Money[player];
            ownableHandler.Land(player, 0);

            Assert.AreEqual(money, banker.Money[player]);
        }

        [TestMethod]
        public void DoNotBuyUnaffordableRealEstate()
        {
            banker.Pay(player, banker.Money[player] - property.Price + 1);
            var money = banker.Money[player];
            ownableHandler.Land(player, 0);

            Assert.AreEqual(money, banker.Money[player]);
        }

        [TestMethod]
        public void MortgagePropertyFor90PercentPurchasePrice()
        {
            BuyProperty();

            var previousMoney = banker.Money[player];
            player.OwnableStrategy = new AlwaysBuyOrMortgage();
            ownableHandler.HandleMortgages(player);

            Assert.AreEqual(previousMoney + property.Price * .9, banker.Money[player]);
            Assert.IsTrue(property.Mortgaged);
        }

        [TestMethod]
        public void CannotMortgageAlreadyMortgagedProperty()
        {
            MortgagePropertyFor90PercentPurchasePrice();

            var previousMoney = banker.Money[player];
            ownableHandler.HandleMortgages(player);

            Assert.AreEqual(previousMoney, banker.Money[player]);
        }

        [TestMethod]
        public void PayOffMortgage()
        {
            MortgagePropertyFor90PercentPurchasePrice();
            player.OwnableStrategy = new NeverBuyOrMortgage();

            var previousMoney = banker.Money[player];
            ownableHandler.HandleMortgages(player);

            Assert.AreEqual(previousMoney - property.Price, banker.Money[player]);
            Assert.IsFalse(property.Mortgaged);
        }

        [TestMethod]
        public void CantPayOffUnaffordableMortgage()
        {
            MortgagePropertyFor90PercentPurchasePrice();
            player.OwnableStrategy = new NeverBuyOrMortgage();
            banker.Pay(player, banker.Money[player] - property.Price + 1);

            var previousMoney = banker.Money[player];
            ownableHandler.HandleMortgages(player);

            Assert.AreEqual(previousMoney, banker.Money[player]);
            Assert.IsTrue(property.Mortgaged);
        }

        [TestMethod]
        public void CantPayOffUnmortgagedProperty()
        {
            BuyProperty();
            player.OwnableStrategy = new NeverBuyOrMortgage();

            var previousMoney = banker.Money[player];
            ownableHandler.HandleMortgages(player);

            Assert.AreEqual(previousMoney, banker.Money[player]);
        }

        [TestMethod]
        public void BankruptPlayerDoesNotOwnSpaces()
        {
            BuyProperty();

            banker.Pay(player, banker.Money[player] + 1);
            var renterMoney = banker.Money[renter];
            ownableHandler.Land(renter, 0);

            Assert.IsTrue(banker.IsBankrupt(player));
            Assert.AreEqual(renterMoney - property.Price, banker.Money[renter]);
        }
    }
}