using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Handlers;

namespace Monopoly.Tests.Players.Strategies.OwnableStrategies
{
    [TestClass]
    public class OwnableStrategiesTests
    {
        private IPlayer player;
        private IPlayer renter;
        private IOwnableHandler realEstateHandler;
        private IBanker banker;

        [TestInitialize]
        public void Setup()
        {
            player = new Player("name");
            renter = new Player("renter");
            var players = new[] { player, renter };

            var property = new Property("property", 1, 0, GROUPING.DARK_BLUE, 1, new[] { 1, 2, 3, 4, 5 });
            banker = new Banker(players);
            realEstateHandler = FakeHandlerFactory.CreateRealEstateHandler(new[] { property }, players, banker);
        }
        
        [TestMethod]
        public void AlwaysBuy()
        {
            player.OwnableStrategy = new AlwaysBuyOrMortgage();
            renter.OwnableStrategy = new AlwaysBuyOrMortgage();

            var money = banker.Money[player];
            realEstateHandler.Land(player, 0);

            Assert.AreEqual(money - 1, banker.Money[player], "player");

            realEstateHandler.DevelopProperties(player);
            money = banker.Money[renter];
            realEstateHandler.Land(renter, 0);

            Assert.AreEqual(money - 1, banker.Money[player], "renter");
        }

        [TestMethod]
        public void BuyIfAtLeast500OnHand()
        {
            player.OwnableStrategy = new BuyOrMortgageIf500();
            renter.OwnableStrategy = new BuyOrMortgageIf500();

            banker.Pay(player, banker.Money[player] - 499);
            var money = banker.Money[player];
            realEstateHandler.Land(player, 0);

            Assert.AreEqual(money, banker.Money[player]);

            banker.Collect(player, 1);
            money = banker.Money[player];
            realEstateHandler.Land(player, 0);

            Assert.AreEqual(money - 1, banker.Money[player]);

            realEstateHandler.DevelopProperties(player);
            money = banker.Money[renter];
            realEstateHandler.Land(renter, 0);

            Assert.AreEqual(money, banker.Money[renter]);

            banker.Collect(player, 1);
            realEstateHandler.DevelopProperties(player);
            money = banker.Money[renter];
            realEstateHandler.Land(renter, 0);

            Assert.AreEqual(money - 1, banker.Money[renter]);
        }

        [TestMethod]
        public void NeverBuy()
        {
            player.OwnableStrategy = new NeverBuyOrMortgage();
            renter.OwnableStrategy = new NeverBuyOrMortgage();

            realEstateHandler.Land(player, 0);
            realEstateHandler.DevelopProperties(player);

            var money = banker.Money[renter];
            realEstateHandler.Land(renter, 0);

            Assert.AreEqual(money, banker.Money[renter]);
        }
    }
}