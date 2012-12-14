using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata.MonopolyBoard;
using MonopolyKata.MonopolyPlayer;
using MonopolyKataTests.MortgageStrategies;

namespace MonopolyKataTests.BoardTests
{
    [TestClass]
    public class IncomeTaxTests
    {
        IncomeTax incomeTax;
        Player player;

        [TestInitialize]
        public void Setup()
        {
            incomeTax = new IncomeTax();
            player = new Player("name", new RandomlyMortgage());
        }

        private void LandOnIncomeTaxWith(Int32 thisMuchMoney)
        {
            player.ReceiveMoney(thisMuchMoney);
            incomeTax.LandOn(player);
        }
        
        [TestMethod]
        public void PlayerLandsOnIncomeTaxWith1800_PlayerPays180()
        {
            LandOnIncomeTaxWith(1800);
            Assert.AreEqual(180, 1800 - player.Money);
        }

        [TestMethod]
        public void PlayerLandsOnIncomeTaxWith2200_PlayerPays200()
        {
            LandOnIncomeTaxWith(2200);
            Assert.AreEqual(200, 2200 - player.Money);
        }

        [TestMethod]
        public void PlayerLandsOnIncomeTaxWith0_PlayerPays0()
        {
            LandOnIncomeTaxWith(0);
            Assert.AreEqual(0, 0 - player.Money);
        }

        [TestMethod]
        public void PlayerLandsOnIncomeTaxWith2000_PlayerPays200()
        {
            LandOnIncomeTaxWith(2000);
            Assert.AreEqual(200, 2000 - player.Money);
        }
    }
}