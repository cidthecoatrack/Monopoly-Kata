using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata.MonopolyBoard.Spaces;
using MonopolyKata.MonopolyPlayer;
using MonopolyKataTests.Strategies.JailStrategies;
using MonopolyKataTests.Strategies.MortgageStrategies;

namespace MonopolyKataTests.BoardTests
{
    [TestClass]
    public class IncomeTaxTests
    {
        [TestMethod]
        public void IncomeTaxTest()
        {
            TestIncomeTaxWith(1800);
            TestIncomeTaxWith(2200);
            TestIncomeTaxWith(0);
            TestIncomeTaxWith(2000);
        }

        private void TestIncomeTaxWith(Int32 thisMuchMoney)
        {
            var incomeTax = new IncomeTax();
            var player = new Player("name", new RandomlyMortgage(), new RandomlyPay());

            player.ReceiveMoney(thisMuchMoney);
            incomeTax.LandOn(player);

            var paid = Math.Min(thisMuchMoney * IncomeTax.INCOME_TAX_PERCENTAGE, IncomeTax.INCOME_TAX_FLAT_RATE);
            Assert.AreEqual(paid, thisMuchMoney - player.Money);
        }
    }
}