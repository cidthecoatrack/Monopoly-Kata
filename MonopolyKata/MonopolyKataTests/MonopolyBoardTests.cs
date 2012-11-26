using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;

namespace MonopolyKataTests
{
    [TestClass]
    public class MonopolyBoardTests
    {
        [TestMethod]
        public void MonopolyBoardSize_Is40()
        {
            Assert.AreEqual(40, MonopolyBoard.BOARD_SIZE);
        }

        [TestMethod]
        public void Jail_Position10()
        {
            Assert.AreEqual("Jail/Just Visiting", MonopolyBoard.Location(10));
        }

        [TestMethod]
        public void GoToJail_Position30()
        {
            Assert.AreEqual("Go To Jail", MonopolyBoard.Location(30));
        }

        [TestMethod]
        public void IncomeTax_Position4()
        {
            Assert.AreEqual("Income Tax", MonopolyBoard.Location(4));
        }

        [TestMethod]
        public void IncomeTax_Position38()
        {
            Assert.AreEqual("Luxury Tax", MonopolyBoard.Location(38));
        }
    }
}
