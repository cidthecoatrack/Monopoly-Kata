using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Board.Spaces;
using Monopoly.Tests.Players.Strategies;
using Monopoly.Tests.Players.Strategies.MortgageStrategies;

namespace Monopoly.Tests.Handlers
{
    [TestClass]
    public class RealEstateHandlerTests
    {
        private RealEstateHandler realEstateHandler;
        private Player player;
        private Player renter;
        private Property property;
        private Banker banker;

        [TestInitialize]
        public void Setup()
        {
            var strategies = new StrategyCollection();
            strategies.CreateAlwaysStrategyCollection();
            player = new Player("name", strategies);
            renter = new Player("renter", strategies);
            var players = new[] { player, renter };
            banker = new Banker(players);
            property = new Property("name", 10, 1, GROUPING.DARK_BLUE, 2, new[] { 4, 5, 6, 7, 8 });
            realEstateHandler = FakeHandlerFactory.CreateRealEstateHandler(new[] { property }, players, banker);
        }

        [TestMethod]
        public void Contains()
        {
            Assert.IsTrue(realEstateHandler.Contains(0));
            Assert.IsFalse(realEstateHandler.Contains(1));
        }

        [TestMethod]
        public void BuyProperty()
        {
            var playerMoney = banker.GetMoney(player);
            realEstateHandler.Land(player, 0);

            Assert.AreEqual(playerMoney - property.Price, banker.GetMoney(player));
        }

        [TestMethod]
        public void PayRent()
        {
            BuyProperty();

            var rent = property.GetRent();
            var renterMoney = banker.GetMoney(renter);
            var ownerMoney = banker.GetMoney(player);
            realEstateHandler.Land(renter, 0);

            Assert.AreEqual(renterMoney - rent, banker.GetMoney(renter));
            Assert.AreEqual(ownerMoney + rent, banker.GetMoney(player));
        }

        [TestMethod]
        public void BuyHouses()
        {
            property.PartOfMonopoly = true;
            BuyProperty();

            realEstateHandler.DevelopProperties(player);

            Assert.AreEqual(1, property.Houses);
        }

        [TestMethod]
        public void CantBuyHousesIfAnyInGroupIsMortgaged()
        {
            BuyProperty();
            property.Mortgaged = true;
            realEstateHandler.DevelopProperties(player);

            Assert.AreEqual(0, property.Houses);
        }

        [TestMethod]
        public void SellHouseAtHalfPrice()
        {
            BuyHouses();
            var money = banker.GetMoney(player);
            realEstateHandler.HandleMortgages(player);

            Assert.AreEqual(money + property.HousePrice / 2, banker.GetMoney(player));
        }
        
        [TestMethod]
        public void OwnerLandsOnTheirRealEstate_NothingHappens()
        {
            BuyProperty();

            var money = banker.GetMoney(player);
            realEstateHandler.Land(player, 0);

            Assert.AreEqual(money, banker.GetMoney(player));
        }

        [TestMethod]
        public void DoNotBuyUnaffordableRealEstate()
        {
            banker.Pay(player, banker.GetMoney(player) - property.Price + 1);
            var money = banker.GetMoney(player);
            realEstateHandler.Land(player, 0);

            Assert.AreEqual(money, banker.GetMoney(player));
        }

        [TestMethod]
        public void MortgagePropertyFor90PercentPurchasePrice()
        {
            BuyProperty();

            var previousMoney = banker.GetMoney(player);
            player.MortgageStrategy = new AlwaysMortgageNeverPay();
            realEstateHandler.HandleMortgages(player);

            Assert.AreEqual(previousMoney + property.Price * .9, banker.GetMoney(player));
            Assert.IsTrue(property.Mortgaged);
        }

        [TestMethod]
        public void CannotMortgageAlreadyMortgagedProperty()
        {
            MortgagePropertyFor90PercentPurchasePrice();

            var previousMoney = banker.GetMoney(player);
            realEstateHandler.HandleMortgages(player);

            Assert.AreEqual(previousMoney, banker.GetMoney(player));
        }

        [TestMethod]
        public void PayOffMortgage()
        {
            MortgagePropertyFor90PercentPurchasePrice();
            player.MortgageStrategy = new NeverMortgageAlwaysPay();

            var previousMoney = banker.GetMoney(player);
            realEstateHandler.HandleMortgages(player);

            Assert.AreEqual(previousMoney - property.Price, banker.GetMoney(player));
            Assert.IsFalse(property.Mortgaged);
        }

        [TestMethod]
        public void CantPayOffUnaffordableMortgage()
        {
            MortgagePropertyFor90PercentPurchasePrice();
            player.MortgageStrategy = new NeverMortgageAlwaysPay();
            banker.Pay(player, banker.GetMoney(player) - property.Price + 1);

            var previousMoney = banker.GetMoney(player);
            realEstateHandler.HandleMortgages(player);

            Assert.AreEqual(previousMoney, banker.GetMoney(player));
            Assert.IsTrue(property.Mortgaged);
        }

        [TestMethod]
        public void CantPayOffUnmortgagedProperty()
        {
            BuyProperty();
            player.MortgageStrategy = new NeverMortgageAlwaysPay();

            var previousMoney = banker.GetMoney(player);
            realEstateHandler.HandleMortgages(player);

            Assert.AreEqual(previousMoney, banker.GetMoney(player));
        }

        [TestMethod]
        public void LosingPlayerDoesNotOwnPropertyAnymore()
        {
            BuyProperty();

            banker.Pay(player, banker.GetMoney(player) + 1);
            var money = banker.GetMoney(renter);
            var rent = property.GetRent();
            realEstateHandler.Land(renter, 0);

            Assert.IsTrue(banker.IsBankrupt(player));
            Assert.AreNotEqual(money - rent, banker.GetMoney(renter));
            Assert.AreEqual(money - property.Price, banker.GetMoney(renter));
        }
    }
}