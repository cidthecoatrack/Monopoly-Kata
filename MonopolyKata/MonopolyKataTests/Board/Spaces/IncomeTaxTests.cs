﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Board.Spaces
{
    [TestClass]
    public class IncomeTaxTests
    {
        private Player player;
        private Banker banker;
        private IncomeTax incomeTax;

        [TestInitialize]
        public void Setup()
        {
            player = new Player("name");
            banker = new Banker(new[] { player });
            incomeTax = new IncomeTax(banker);
        }
        
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
            banker.Pay(player, banker.GetMoney(player) - thisMuchMoney, ToString());
            incomeTax.LandOn(player);
            var paid = Math.Min(thisMuchMoney / IncomeTax.INCOME_TAX_PERCENTAGE_DIVISOR, IncomeTax.INCOME_TAX_FLAT_RATE);

            Assert.AreEqual(thisMuchMoney - paid, banker.GetMoney(player), Convert.ToString(thisMuchMoney));
        }
    }
}