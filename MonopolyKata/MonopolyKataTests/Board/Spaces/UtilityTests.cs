using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Board.Spaces;
using Monopoly;
using Monopoly.Tests.Strategies.JailStrategies;
using Monopoly.Tests.Strategies.MortgageStrategies;
using Monopoly.Tests.Dice;

namespace Monopoly.Tests.Board.Spaces
{
    [TestClass]
    public class UtilityTests
    {
        Utility utility;
        Utility otherUtility;
        Player owner;
        Player renter;

        [TestInitialize]
        public void Setup()
        {
            var dice = new ControlledDice();
            dice.SetPredeterminedRollValue(4);
            utility = new Utility("utility", dice);
            otherUtility = new Utility("other utility", dice);
            var utilities = new Utility[] { utility, otherUtility };
            owner = new Player("owner", new RandomlyMortgage(), new RandomlyPay());
            renter = new Player("renter", new RandomlyMortgage(), new RandomlyPay());

            dice.RollTwoDice();
            utility.SetUtilities(utilities);
            otherUtility.SetUtilities(utilities);
            owner.ReceiveMoney(utility.Price);
            utility.LandOn(owner);
            renter.Move(dice.Value);
        }
        
        [TestMethod]
        public void PlayerLandsOnOwnedUtility_PlayerPays4xDieRoll()
        {
            var ownerMoney = owner.Money;
            var renterMoney = renter.Money;
            utility.LandOn(renter);

            Assert.AreEqual(renterMoney - 16, renter.Money);
            Assert.AreEqual(ownerMoney + 16, owner.Money);
        }

        [TestMethod]
        public void PlayerLandsOnOwnedUtilityx2_PlayerPays10xDieRoll()
        {
            var otherOwner = new Player("other owner", new RandomlyMortgage(), new RandomlyPay());
            otherUtility.LandOn(otherOwner);

            var ownerMoney = owner.Money;
            var renterMoney = renter.Money;
            utility.LandOn(renter);

            Assert.AreEqual(renterMoney - 40, renter.Money);
            Assert.AreEqual(ownerMoney + 40, owner.Money);
        }
    }
}