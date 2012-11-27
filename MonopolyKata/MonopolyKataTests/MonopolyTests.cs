using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;

namespace MonopolyKataTests
{
    [TestClass]
    public class MonopolyTests
    {
        Monopoly monopolyGame;
        String[] players = new String[8] { "Horse", "Car", "Karl", "Allen", "Elliot", "Tim", "Keith", "Ramey" };
        LinkedList<Player> playerList;
        
        [TestInitialize]
        public void Setup()
        {
            monopolyGame = new Monopoly(GeneratePlayerIEnumerable(players.Length));
        }

        private void CreateGameWithSpecifiedNumberOfPlayers(Int32 NumberOfPlayers)
        {
            monopolyGame = new Monopoly(GeneratePlayerIEnumerable(NumberOfPlayers));
        }

        private IEnumerable<Player> GeneratePlayerIEnumerable(Int32 NumberOfPlayers)
        {
            playerList = new LinkedList<Player>();
            Int32 CountLimit = GenerateCountLimit(NumberOfPlayers);

            for (Int32 i = 0; i < CountLimit; i++)
                playerList.AddLast(new Player(players[i]));

            for (NumberOfPlayers -= CountLimit; NumberOfPlayers > 0; NumberOfPlayers--)
                playerList.AddLast(new Player(NumberOfPlayers.ToString()));

            return playerList;
        }

        private Int32 GenerateCountLimit(Int32 NumberOfPlayers)
        {
            if (NumberOfPlayers > players.Length)
                return players.Length;
            return NumberOfPlayers;
        }

        [TestMethod]
        public void CreateTwoPlayerGame_GameHasTwoPlayers()
        {
            CreateGameWithSpecifiedNumberOfPlayers(2);
            AssertContent(playerList, monopolyGame.Players);
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void FewerThanTwoPlayers_MonopolyWillNotPlay()
        {
            CreateGameWithSpecifiedNumberOfPlayers(1);
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MoreThanEightPlayers_MonopolyWillNotPlay()
        {
            CreateGameWithSpecifiedNumberOfPlayers(9);
        }

        [TestMethod]
        public void PlayerOrderIsRandomizedAtStart()
        {
            LinkedList<Player> NonRandomizedPlayers = new LinkedList<Player>();

            foreach (String player in players)
                NonRandomizedPlayers.AddLast(new Player(player));
            
            Assert.IsTrue(AssertRandomization(NonRandomizedPlayers, monopolyGame.Players));
        }

        [TestMethod]
        public void RandomizingPlayerOrderMaintainsPlayerIntegrity()
        {
            LinkedList<Player> NonRandomizedPlayers = new LinkedList<Player>();

            foreach (String player in players)
                NonRandomizedPlayers.AddLast(new Player(player));

            AssertContent(NonRandomizedPlayers, monopolyGame.Players);
        }

        private static void AssertContent(IEnumerable<Player> expected, IEnumerable<Player> actual)
        {
            Assert.AreEqual(expected.Count(), actual.Count());
            AssertTwoSidedUnion(expected, actual);
        }

        private static void AssertTwoSidedUnion(IEnumerable<Player> left, IEnumerable<Player> right)
        {
            AssertOneSidedUnion(left, right);
            AssertOneSidedUnion(right, left);
        }

        private static void AssertOneSidedUnion(IEnumerable<Player> left, IEnumerable<Player> right)
        {
            foreach (Player player in left)
                Assert.IsTrue(player.IsContainedIn(right));
        }

        private static bool AssertRandomization(LinkedList<Player> expected, LinkedList<Player> actual)
        {
            return RecursiveRandomizationAssert(expected.First, actual.First);
        }

        private static bool RecursiveRandomizationAssert(LinkedListNode<Player> expectedNode, LinkedListNode<Player> actualNode)
        {
            if (AtEndOfList(expectedNode, actualNode))
                return false;
            else if (NamesAreDifferent(expectedNode, actualNode))
                return true;
            else
                return RecursiveRandomizationAssert(expectedNode.Next, actualNode.Next);
        }

        private static bool AtEndOfList(LinkedListNode<Player> expectedNode, LinkedListNode<Player> actualNode)
        {
            return expectedNode == null || actualNode == null;
        }

        private static bool NamesAreDifferent(LinkedListNode<Player> expectedNode, LinkedListNode<Player> actualNode)
        {
            return expectedNode.Value.Name != actualNode.Value.Name;
        }

        [TestMethod]
        public void AtBeginningOfGame_CurrentPlayerIsFirstPlayer()
        {
            Assert.AreEqual(monopolyGame.Players.First, monopolyGame.CurrentPlayerNode);
        }

        [TestMethod]
        public void OnTurn_PlayerMoves()
        {
            monopolyGame.TakeTurn();
            Assert.AreNotEqual(MonopolyBoard.GO, monopolyGame.Players.First.Value.Position);
        }

        [TestMethod]
        public void AfterFirstTurn_NextPlayerIsTheCurrentPlayer()
        {
            monopolyGame.TakeTurn();
            Assert.AreEqual(monopolyGame.Players.First.Next, monopolyGame.CurrentPlayerNode);
        }

        [TestMethod]
        public void AllPlayersTakeATurnInARound()
        {
            monopolyGame.TakeRound();

            foreach (Player player in monopolyGame.Players)
                Assert.AreNotEqual(MonopolyBoard.GO, player.Position);
        }

        [TestMethod]
        public void AfterEveryoneTakesATurn_BeginNewRound()
        {
            monopolyGame.TakeTurns(monopolyGame.NumberOfActivePlayers);
            Assert.AreEqual(2, monopolyGame.CurrentRound);
        }

        [TestMethod]
        public void AtNewRound_FirstPlayerHasNewTurn()
        {
            monopolyGame.TakeRound();
            Assert.AreEqual(monopolyGame.Players.First, monopolyGame.CurrentPlayerNode);
        }

        [TestMethod]
        public void AfterTwentyRounds_GameEnds()
        {
            monopolyGame.PlayFullGame();
            Assert.IsTrue(monopolyGame.GameOver);
        }

        [TestMethod]
        public void PlayersPlayInSameOrderEveryRound()
        {
            Player[] PlayerVerificationArray = new Player[monopolyGame.NumberOfActivePlayers];
            monopolyGame.Players.CopyTo(PlayerVerificationArray, 0);
            int verificationIndex = 0;

            while (!monopolyGame.GameOver)
            {
                if (PlayerVerificationArray.Length != monopolyGame.NumberOfActivePlayers)
                {
                    PlayerVerificationArray = CheckForRemovedPlayers(PlayerVerificationArray, monopolyGame.Players);
                    if (verificationIndex == 0)
                        verificationIndex++;
                }
                else
                {
                    verificationIndex++;
                }

                if (monopolyGame.CurrentPlayerNode.Next != null)
                    Assert.IsTrue(PlayerVerificationArray[verificationIndex].Equals(monopolyGame.CurrentPlayerNode.Next.Value));
                else
                    verificationIndex = 0;

                monopolyGame.TakeTurn();
            }
        }

        private Player[] CheckForRemovedPlayers(Player[] PlayerVerificationArray, LinkedList<Player> ActivePlayers)
        {
            Player[] temp = new Player[ActivePlayers.Count];

            int tempIndex = 0;
            for (int i = 0; i < PlayerVerificationArray.Length; i++)
            {
                if (PlayerVerificationArray[i].IsContainedIn(ActivePlayers))
                {
                    temp[tempIndex] = PlayerVerificationArray[i];
                    tempIndex++;
                }
            }

            return temp;
        }

        [TestMethod]
        public void PlayerCannotMoveOffOfBoard()
        {
            monopolyGame.MoveCurrentPlayerSetAmount(MonopolyBoard.BOARD_SIZE - 1);
            Assert.AreEqual(MonopolyBoard.BOARD_SIZE - 1, monopolyGame.CurrentPlayer.Position);
            monopolyGame.MoveCurrentPlayerSetAmount(6);
            Assert.AreEqual(5, monopolyGame.CurrentPlayer.Position);
        }

        [TestMethod]
        public void PlayersReceiveTwoHundredForPassingGo()
        {
            Assert.AreEqual(0, monopolyGame.CurrentPlayer.Money);
            monopolyGame.MoveCurrentPlayerSetAmount(MonopolyBoard.BOARD_SIZE + 1);
            Assert.AreEqual(200, monopolyGame.CurrentPlayer.Money);
        }

        [TestMethod]
        public void PlayersDoNotReceiveMoneyFromLeavingGoOnFirstTurn()
        {
            monopolyGame.TakeRound();

            foreach (Player player in monopolyGame.Players)
                Assert.AreEqual(0, player.Money);
        }

        [TestMethod]
        public void PlayersDoNotReceiveMoneyOnTurnAfterLandingOnGo()
        {
            monopolyGame.MoveCurrentPlayerSetAmount(MonopolyBoard.BOARD_SIZE - 1);
            Assert.AreEqual(0, monopolyGame.CurrentPlayer.Money);
            
            monopolyGame.MoveCurrentPlayerSetAmount(1);
            Assert.AreEqual(200, monopolyGame.CurrentPlayer.Money);

            monopolyGame.MoveCurrentPlayerSetAmount(1);
            Assert.AreEqual(200, monopolyGame.CurrentPlayer.Money);
        }

        [TestMethod]
        public void PassGoMultipleTimes_PlayerGetsMultiplePaymentsOfTwoHundred()
        {
            monopolyGame.MoveCurrentPlayerSetAmount(MonopolyBoard.BOARD_SIZE * 2);
            Assert.AreEqual(400, monopolyGame.CurrentPlayer.Money);
        }

        [TestMethod]
        public void PlayerDoesNotLandOnGoToJail_DoesNotGoToJail()
        {
            monopolyGame.MoveCurrentPlayerSetAmount(MonopolyBoard.GO_TO_JAIL - 1);
            Assert.AreEqual(MonopolyBoard.GO_TO_JAIL - 1, monopolyGame.CurrentPlayer.Position);

            monopolyGame.TakeTurn();
            monopolyGame.MoveCurrentPlayerSetAmount(MonopolyBoard.GO_TO_JAIL + 1);
            Assert.AreEqual(MonopolyBoard.GO_TO_JAIL + 1, monopolyGame.CurrentPlayer.Position);
        }

        [TestMethod]
        public void PlayerDoesNotLandOnGoToJail_MoneyUnaffected()
        {
            monopolyGame.CurrentPlayer.ReceiveMoney(500);
            monopolyGame.MoveCurrentPlayerSetAmount(MonopolyBoard.GO_TO_JAIL - 1);
            Assert.AreEqual(500, monopolyGame.CurrentPlayer.Money);

            monopolyGame.TakeTurn();

            monopolyGame.CurrentPlayer.ReceiveMoney(500);
            monopolyGame.MoveCurrentPlayerSetAmount(MonopolyBoard.GO_TO_JAIL + 1);
            Assert.AreEqual(500, monopolyGame.CurrentPlayer.Money);
        }

        [TestMethod]
        public void PlayerLandsOnGoToJail_PlayerGoesToJustVisiting()
        {
            monopolyGame.MoveCurrentPlayerSetAmount(MonopolyBoard.GO_TO_JAIL);
            Assert.AreEqual(MonopolyBoard.JAIL_OR_JUST_VISITING, monopolyGame.CurrentPlayer.Position);
        }

        [TestMethod]
        public void PlayerGoesToJail_MoneyUnaffected()
        {
            monopolyGame.CurrentPlayer.ReceiveMoney(500);
            monopolyGame.MoveCurrentPlayerSetAmount(MonopolyBoard.GO_TO_JAIL);
            Assert.AreEqual(500, monopolyGame.CurrentPlayer.Money);
        }

        [TestMethod]
        public void PlayerGoesToJail_PlayerDoesNotReceivePassGoMoney()
        {
            monopolyGame.MoveCurrentPlayerSetAmount(MonopolyBoard.GO_TO_JAIL);
            Assert.AreEqual(0, monopolyGame.CurrentPlayer.Money);
        }

        [TestMethod]
        public void PlayerLandsOnIncomeTaxWith1800_PlayerPays180()
        {
            monopolyGame.CurrentPlayer.ReceiveMoney(1800);
            monopolyGame.MoveCurrentPlayerSetAmount(MonopolyBoard.INCOME_TAX);
            Assert.AreEqual(1620, monopolyGame.CurrentPlayer.Money);
        }

        [TestMethod]
        public void PlayerLandsOnIncomeTaxWith2200_PlayerPays200()
        {
            monopolyGame.CurrentPlayer.ReceiveMoney(2200);
            monopolyGame.MoveCurrentPlayerSetAmount(MonopolyBoard.INCOME_TAX);
            Assert.AreEqual(2000, monopolyGame.CurrentPlayer.Money);
        }

        [TestMethod]
        public void PlayerLandsOnIncomeTaxWith0_PlayerPays0()
        {
            monopolyGame.MoveCurrentPlayerSetAmount(MonopolyBoard.INCOME_TAX);
            Assert.AreEqual(0, monopolyGame.CurrentPlayer.Money);
        }

        [TestMethod]
        public void PlayerLandsOnIncomeTaxWith2000_PlayerPays200()
        {
            monopolyGame.CurrentPlayer.ReceiveMoney(2000);
            monopolyGame.MoveCurrentPlayerSetAmount(MonopolyBoard.INCOME_TAX);
            Assert.AreEqual(1800, monopolyGame.CurrentPlayer.Money);
        }

        [TestMethod]
        public void PlayerPassesOverIncomeTax_MoneyUnaffected()
        {
            monopolyGame.CurrentPlayer.ReceiveMoney(500);
            monopolyGame.MoveCurrentPlayerSetAmount(MonopolyBoard.INCOME_TAX + 1);
            Assert.AreEqual(500, monopolyGame.CurrentPlayer.Money);
        }

        [TestMethod]
        public void PlayerLandsOnLuxuryTax_PlayerPays75()
        {
            monopolyGame.MoveCurrentPlayerSetAmount(MonopolyBoard.BOARD_SIZE + MonopolyBoard.LUXURY_TAX);
            Assert.AreEqual(125, monopolyGame.CurrentPlayer.Money);
        }

        [TestMethod]
        public void PlayerPassesOverLuxuryTax_MoneyUnchanged()
        {
            monopolyGame.MoveCurrentPlayerSetAmount(MonopolyBoard.BOARD_SIZE + MonopolyBoard.LUXURY_TAX + 1);
            Assert.AreEqual(200, monopolyGame.CurrentPlayer.Money);
        }

        [TestMethod]
        public void OnePlayerLosesInTwoPlayerGame_OtherPlayerWins()
        {
            CreateGameWithSpecifiedNumberOfPlayers(2);
            Player[] players = new Player[2];
            monopolyGame.Players.CopyTo(players, 0);
            monopolyGame.CurrentPlayer.Pay(monopolyGame.CurrentPlayer.Money + 1);
            monopolyGame.TakeTurn();
            Assert.AreEqual(players[1], monopolyGame.Winner);
        }

        [TestMethod]
        public void OnePlayerLosesInThreePlayerGame_OtherTwoKeepPlaying()
        {
            CreateGameWithSpecifiedNumberOfPlayers(3);
            ForceCurrentPlayerToGoBroke();
            monopolyGame.TakeTurn();
            Assert.AreEqual(2, monopolyGame.NumberOfActivePlayers);
            monopolyGame.TakeTurn();
            Assert.AreEqual(2, monopolyGame.NumberOfActivePlayers);
        }

        private void ForceCurrentPlayerToGoBroke()
        {
            monopolyGame.CurrentPlayer.Pay(monopolyGame.CurrentPlayer.Money + 1);
        }

        [TestMethod]
        public void OnePlayerLosesEachTurn_RemainingPlayerIsWinner()
        {
            Player TheChosenOne = monopolyGame.CurrentPlayer;

            while (monopolyGame.NumberOfActivePlayers > 1)
            {
                if (monopolyGame.CurrentPlayer != TheChosenOne)
                    ForceCurrentPlayerToGoBroke();
                monopolyGame.TakeTurn();
            }

            Assert.AreEqual(TheChosenOne, monopolyGame.Winner);
        }
    }
}