using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Board.Spaces;
using Monopoly.Tests.Handlers;

namespace Monopoly.Tests.Players.Strategies.MortgageStrategies
{
    [TestClass]
    public class MortgageStrategiesTests
    {
        private OwnableSpace firstRealEstate;
        private OwnableSpace secondRealEstate;
        private OwnableSpace thirdRealEstate;
        private Player player;
        private RealEstateHandler realEstateHandler;
        private Banker banker;

        [TestInitialize]
        public void Setup()
        {
            firstRealEstate = new TestRealEstate("first", 50);
            secondRealEstate = new TestRealEstate("second", 50);
            thirdRealEstate = new TestRealEstate("third", 50);

            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();
            player = new Player("name", strategies);
            var players = new[] { player };
            banker = new Banker(players);
            var estates = new[]
                {
                    firstRealEstate,
                    secondRealEstate,
                    thirdRealEstate
                };

            realEstateHandler = FakeHandlerFactory.CreateRealEstateHandler(estates, players, banker);

            realEstateHandler.Land(player, 0);
            realEstateHandler.Land(player, 1);
            realEstateHandler.Land(player, 2);
        }

        [TestMethod]
        public void NeverMortgage()
        {
            player.MortgageStrategy = new NeverMortgageAlwaysPay();
            realEstateHandler.HandleMortgages(player);

            Assert.IsFalse(firstRealEstate.Mortgaged);
            Assert.IsFalse(secondRealEstate.Mortgaged);
            Assert.IsFalse(thirdRealEstate.Mortgaged);

            firstRealEstate.Mortgaged = true;
            realEstateHandler.HandleMortgages(player);

            Assert.IsFalse(firstRealEstate.Mortgaged);
            Assert.IsFalse(secondRealEstate.Mortgaged);
            Assert.IsFalse(thirdRealEstate.Mortgaged);
        }

        [TestMethod]
        public void MortgageWhenSheHasLessThan500()
        {
            player.MortgageStrategy = new MortgageIfMoneyLessThanFiveHundred();
            banker.Pay(player, banker.GetMoney(player) - 440);
            realEstateHandler.HandleMortgages(player);

            Assert.IsTrue(firstRealEstate.Mortgaged, "first, mortgage");
            Assert.IsTrue(secondRealEstate.Mortgaged, "second, mortgage");
            Assert.IsFalse(thirdRealEstate.Mortgaged, "third, mortgage");

            banker.Collect(player, 551 - banker.GetMoney(player));
            realEstateHandler.HandleMortgages(player);

            Assert.IsFalse(firstRealEstate.Mortgaged, "first, pay off");
            Assert.IsTrue(secondRealEstate.Mortgaged, "second, pay off");
            Assert.IsFalse(thirdRealEstate.Mortgaged, "third, pay off");
        }

        [TestMethod]
        public void AlwaysMortgage()
        {
            player.MortgageStrategy = new AlwaysMortgageNeverPay();
            realEstateHandler.HandleMortgages(player);

            Assert.IsTrue(firstRealEstate.Mortgaged);
            Assert.IsTrue(secondRealEstate.Mortgaged);
            Assert.IsTrue(thirdRealEstate.Mortgaged);

            realEstateHandler.HandleMortgages(player);

            Assert.IsTrue(firstRealEstate.Mortgaged);
            Assert.IsTrue(secondRealEstate.Mortgaged);
            Assert.IsTrue(thirdRealEstate.Mortgaged);
        }
    }
}