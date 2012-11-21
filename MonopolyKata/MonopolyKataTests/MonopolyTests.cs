using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;

namespace MonopolyKataTests
{
    [TestClass]
    public class MonopolyTests
    {
        Monopoly monopolyGame;
        String[] players = new String[6] { "Karl", "Allen", "Elliot", "Tim", "Keith", "Ramey" };
        LinkedList<Player> NonRandomizedPlayers;
        
        [TestInitialize]
        public void Setup()
        {
            monopolyGame = new Monopoly(players);
            NonRandomizedPlayers = new LinkedList<Player>();

            foreach (String player in players)
                NonRandomizedPlayers.AddLast(new Player(player));
        }
        
        [TestMethod]
        public void CanInitializeMonopoly()
        {
            Assert.IsNotNull(monopolyGame);
        }

        [TestMethod]
        public void MonopolyGameHasABoard()
        {
            Assert.IsNotNull(monopolyGame.GetBoard());
        }

        [TestMethod]
        public void MonopolyGameHasAPlayer()
        {
            Assert.IsNotNull(monopolyGame.GetPlayers());
        }

        [TestMethod]
        public void MonopolyGameHasMoreThanOnePlayer()
        {
            Assert.IsTrue(monopolyGame.GetPlayers().Count > 1);
        }

        [TestMethod]
        public void PassInSixPlayers_MonopolyGameHasSixPlayers()
        {
            Assert.AreEqual(6, monopolyGame.GetPlayers().Count);
        }

        [TestMethod]
        public void PlayerOrderIsRandomizedAtStart()
        {
            Assert.IsTrue(AssertRandomization(NonRandomizedPlayers, monopolyGame.GetPlayers()));
        }

        [TestMethod]
        public void RandomizingPlayerOrderMaintainsPlayerIntegrity()
        {
            AssertContent(NonRandomizedPlayers, monopolyGame.GetPlayers());
        }

        private static void AssertContent(LinkedList<Player> expected, LinkedList<Player> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);

            LinkedList<String> actualNames = GetListOfNamesFromPlayerList(actual);

            foreach (Player expectedPlayer in expected)
                Assert.IsTrue(actualNames.Contains(expectedPlayer.Name));
        }

        private static LinkedList<String> GetListOfNamesFromPlayerList(LinkedList<Player> playerList)
        {
            LinkedList<String> Names = new LinkedList<String>();

            foreach (Player player in playerList)
                Names.AddLast(player.Name);
            
            return Names;
        }

        private static bool AssertRandomization(LinkedList<Player> expected, LinkedList<Player> actual)
        {
            return RecursiveRandomizationAssert(expected.First, actual.First);
        }

        private static bool RecursiveRandomizationAssert(LinkedListNode<Player> expectedNode, LinkedListNode<Player> actualNode)
        {
            if (ExpectedOrActualAtEndOfList(expectedNode, actualNode))
                return false;
            else if (ExpectedAndActualNamesAreDifferent(expectedNode, actualNode))
                return true;
            else
                return RecursiveRandomizationAssert(expectedNode.Next, actualNode.Next);
        }

        private static bool ExpectedAndActualNamesAreDifferent(LinkedListNode<Player> expectedNode, LinkedListNode<Player> actualNode)
        {
            return expectedNode.Value.Name != actualNode.Value.Name;
        }

        private static bool ExpectedOrActualAtEndOfList(LinkedListNode<Player> expectedNode, LinkedListNode<Player> actualNode)
        {
            return expectedNode == null || actualNode == null;
        }

        [TestMethod]
        public void AtBeginningOfGame_CurrentPlayerIsFirstPlayer()
        {
            Assert.AreEqual(monopolyGame.GetPlayers().First, monopolyGame.GetCurrentPlayerNode());
        }

        [TestMethod]
        public void OnTurn_PlayerMovesForward()
        {
            monopolyGame.TakeTurn();
            Assert.AreNotEqual(0, monopolyGame.GetPlayers().First.Value.Position);
        }

        [TestMethod]
        public void AfterFirstTurn_NextPlayerIsTheCurrentPlayer()
        {
            monopolyGame.TakeTurns(2);
            Assert.AreNotEqual(monopolyGame.GetPlayers().First, monopolyGame.GetCurrentPlayerNode());
        }

        [TestMethod]
        public void AfterEveryoneTakesATurn_BeginNewRound()
        {
            monopolyGame.TakeTurns(monopolyGame.GetPlayers().Count);
            Assert.AreNotEqual(0, monopolyGame.GetRound());
        }

        [TestMethod]
        public void AtNewRound_FirstPlayerHasNewTurn()
        {
            monopolyGame.TakeRound();
            Assert.AreEqual(monopolyGame.GetPlayers().First, monopolyGame.GetCurrentPlayerNode());
        }

        [TestMethod]
        public void AfterTwentyRounds_GameEnds()
        {
            monopolyGame.TakeRounds(20);
            Assert.IsTrue(monopolyGame.IsGameOver());
        }
    }
}