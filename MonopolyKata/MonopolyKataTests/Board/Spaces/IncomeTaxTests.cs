using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly;
using Monopoly.Tests.Strategies.JailStrategies;
using Monopoly.Tests.Strategies.MortgageStrategies;

namespace Monopoly.Tests.Board.Spaces
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

            player.Pay(player.Money - thisMuchMoney);
            incomeTax.LandOn(player);
            var paid = Math.Min(thisMuchMoney / IncomeTax.INCOME_TAX_PERCENTAGE_DIVISOR, IncomeTax.INCOME_TAX_FLAT_RATE);

            Assert.AreEqual(thisMuchMoney - paid, player.Money);
        }
    }
}