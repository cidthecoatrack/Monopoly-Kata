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
        private Player player;
        private Player renter;
        private OwnableHandler realEstateHandler;
        private Banker banker;

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

            var money = banker.GetMoney(player);
            realEstateHandler.Land(player, 0);

            Assert.AreEqual(money - 1, banker.GetMoney(player), "player");

            realEstateHandler.DevelopProperties(player);
            money = banker.GetMoney(renter);
            realEstateHandler.Land(renter, 0);

            Assert.AreEqual(money - 1, banker.GetMoney(renter), "renter");
        }

        [TestMethod]
        public void BuyIfAtLeast500OnHand()
        {
            player.OwnableStrategy = new BuyOrMortgageIf500();
            renter.OwnableStrategy = new BuyOrMortgageIf500();

            banker.Pay(player, banker.GetMoney(player) - 499);
            var money = banker.GetMoney(player);
            realEstateHandler.Land(player, 0);

            Assert.AreEqual(money, banker.GetMoney(player));

            banker.Collect(player, 1);
            money = banker.GetMoney(player);
            realEstateHandler.Land(player, 0);

            Assert.AreEqual(money - 1, banker.GetMoney(player));

            realEstateHandler.DevelopProperties(player);
            money = banker.GetMoney(renter);
            realEstateHandler.Land(renter, 0);

            Assert.AreEqual(money, banker.GetMoney(renter));

            banker.Collect(player, 1);
            realEstateHandler.DevelopProperties(player);
            money = banker.GetMoney(renter);
            realEstateHandler.Land(renter, 0);

            Assert.AreEqual(money - 1, banker.GetMoney(renter));
        }

        [TestMethod]
        public void NeverBuy()
        {
            player.OwnableStrategy = new NeverBuyOrMortgage();
            renter.OwnableStrategy = new NeverBuyOrMortgage();

            realEstateHandler.Land(player, 0);
            realEstateHandler.DevelopProperties(player);

            var money = banker.GetMoney(renter);
            realEstateHandler.Land(renter, 0);

            Assert.AreEqual(money, banker.GetMoney(renter));
        }
    }
}