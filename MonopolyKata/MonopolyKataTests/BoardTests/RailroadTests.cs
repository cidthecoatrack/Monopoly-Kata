using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata.MonopolyBoard.Spaces;
using MonopolyKata.MonopolyPlayer;
using MonopolyKataTests.Strategies.JailStrategies;
using MonopolyKataTests.Strategies.MortgageStrategies;

namespace MonopolyKataTests.BoardTests
{
    [TestClass]
    public class RailroadTests
    {
        Railroad firstRxR;
        Railroad secondRxR;
        Railroad thirdRxR;
        Railroad fourthRxR;
        Player owner;
        Player renter;

        [TestInitialize]
        public void Setup()
        {
            firstRxR = new Railroad("first railroad");
            secondRxR = new Railroad("second railroad");
            thirdRxR = new Railroad("third railroad");
            fourthRxR = new Railroad("fourth railroad");

            var railroads = new Railroad[] { firstRxR, secondRxR, thirdRxR, fourthRxR };

            foreach (var railroad in railroads)
                railroad.SetRailroads(railroads);

            owner = new Player("owner", new RandomlyMortgage(), new RandomlyPay());
            renter = new Player("renter", new RandomlyMortgage(), new RandomlyPay());

            owner.ReceiveMoney(firstRxR.Price);
            firstRxR.LandOn(owner);
        }
        
        [TestMethod]
        public void PlayerLandsOnOwnedRailroadx1_Pays25()
        {
            renter.ReceiveMoney(25);
            firstRxR.LandOn(renter);

            Assert.AreEqual(0, renter.Money);
            Assert.AreEqual(25, owner.Money);
        }

        [TestMethod]
        public void PlayerLandsOnOwnedRailroadx2_Pays50()
        {
            owner.ReceiveMoney(secondRxR.Price);
            secondRxR.LandOn(owner);

            renter.ReceiveMoney(50);
            firstRxR.LandOn(renter);

            Assert.AreEqual(0, renter.Money);
            Assert.AreEqual(50, owner.Money);
        }

        [TestMethod]
        public void PlayerLandsOnOwnedRailroadx3_Pays100()
        {
            owner.ReceiveMoney(secondRxR.Price);
            secondRxR.LandOn(owner);
            owner.ReceiveMoney(thirdRxR.Price);
            thirdRxR.LandOn(owner);

            renter.ReceiveMoney(100);
            firstRxR.LandOn(renter);

            Assert.AreEqual(0, renter.Money);
            Assert.AreEqual(100, owner.Money);
        }

        [TestMethod]
        public void PlayerLandsOnOwnedRailroadx4_Pays200()
        {
            owner.ReceiveMoney(secondRxR.Price);
            secondRxR.LandOn(owner);
            owner.ReceiveMoney(thirdRxR.Price);
            thirdRxR.LandOn(owner);
            owner.ReceiveMoney(fourthRxR.Price);
            fourthRxR.LandOn(owner);

            renter.ReceiveMoney(200);
            firstRxR.LandOn(renter);

            Assert.AreEqual(0, renter.Money);
            Assert.AreEqual(200, owner.Money);
        }
    }
}