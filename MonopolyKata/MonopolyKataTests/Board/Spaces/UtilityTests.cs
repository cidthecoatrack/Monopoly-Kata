using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly;
using Monopoly.Tests.Strategies.JailStrategies;
using Monopoly.Tests.Strategies.MortgageStrategies;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Strategies;

namespace Monopoly.Tests.Board.Spaces
{
    [TestClass]
    public class UtilityTests
    {
        private Utility utility;
        private Utility otherUtility;
        private Player owner;
        private Player otherOwner;
        private Player renter;

        [TestInitialize]
        public void Setup()
        {
            var dice = new ControlledDice();
            dice.SetPredeterminedRollValue(4);
            utility = new Utility("utility", dice);
            otherUtility = new Utility("other utility", dice);
            var utilities = new Utility[] { utility, otherUtility };

            var strategies = new StrategyCollection();
            strategies.CreateRandomStrategyCollection();

            owner = new Player("owner", strategies);
            renter = new Player("renter", strategies);
            otherOwner = new Player("other owner", strategies);

            utility.SetUtilities(utilities);
            otherUtility.SetUtilities(utilities);

            dice.RollTwoDice();
            utility.LandOn(owner);
            renter.Move(dice.Value);
        }
        
        [TestMethod]
        public void LandOnOwnedUtility_PlayerPays4xDieRoll()
        {
            var ownerMoney = owner.Money;
            var renterMoney = renter.Money;
            utility.LandOn(renter);

            Assert.AreEqual(renterMoney - 16, renter.Money);
            Assert.AreEqual(ownerMoney + 16, owner.Money);
        }

        [TestMethod]
        public void LandOnOwnedUtilityx2_PlayerPays10xDieRoll()
        {
            otherUtility.LandOn(otherOwner);

            var ownerMoney = owner.Money;
            var renterMoney = renter.Money;
            utility.LandOn(renter);

            Assert.AreEqual(renterMoney - 40, renter.Money);
            Assert.AreEqual(ownerMoney + 40, owner.Money);
        }
    }
}