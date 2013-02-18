using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Games;
using Monopoly.Handlers;
using Monopoly.Players;
using Monopoly.Tests.Board;
using Monopoly.Tests.Dice;
using Monopoly.Tests.Players.Strategies;

namespace Monopoly.Tests.Games
{
    [TestClass]
    public class GameTests
    {
        private Game game;
        private ControlledDice dice;
        private BoardHandler boardHandler;

        [TestInitialize]
        public void Setup()
        {
            SetupGameWithPlayers(8);
        }

        private void SetupGameWithPlayers(Int32 numberOfPlayers)
        {
            var players = GeneratePlayerIEnumerable(numberOfPlayers);
            var board = FakeBoardFactory.CreateBoardOfNormalSpaces();
            boardHandler = new BoardHandler(players, board);
            
            dice = new ControlledDice();
            var jailHandler = new JailHandler(dice, boardHandler);
            var turnHandler = new TurnHandler(dice, boardHandler, jailHandler);

            game = new Game(players, turnHandler);
        }

        private IEnumerable<Player> GeneratePlayerIEnumerable(Int32 NumberOfPlayers)
        {
            var playerList = new List<Player>();

            var strategies = new StrategyCollection();
            strategies.CreateNeverStrategyCollection();

            while (NumberOfPlayers-- > 0)
                playerList.Add(new Player("Player " + NumberOfPlayers, strategies));

            return playerList;
        }

        [TestMethod]
        public void GameHasEightPlayers()
        {
            Assert.AreEqual(8, game.NumberOfActivePlayers);
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void FewerThanTwoPlayers()
        {
            SetupGameWithPlayers(1);
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MoreThanEightPlayers()
        {
            SetupGameWithPlayers(9);
        }

        [TestMethod]
        public void NoWinnerAtStart()
        {
            Assert.IsNull(game.Winner);
        }

        [TestMethod]
        public void AfterFirstTurn_NextPlayerIsTheCurrentPlayer()
        {
            var firstPlayer = game.CurrentPlayer;
            game.TakeTurn();
            Assert.AreNotEqual(firstPlayer, game.CurrentPlayer);
        }

        [TestMethod]
        public void AllPlayersTakeATurnInARound()
        {
            var players = new List<Player>();
            while (game.Round == 1)
            {
                players.Add(game.CurrentPlayer);
                game.TakeTurn();
            }

            foreach (var player in players)
                Assert.AreNotEqual(0, player);
        }

        [TestMethod]
        public void AfterEveryoneTakesATurn_BeginNewRound()
        {
            var round = game.Round;
            game.TakeRound();
            Assert.AreEqual(round + 1, game.Round);
        }

        [TestMethod]
        public void AtNewRound_FirstPlayerHasNewTurn()
        {
            var firstPlayer = game.CurrentPlayer;
            game.TakeRound();
            Assert.IsTrue(firstPlayer.Equals(game.CurrentPlayer));
        }

        [TestMethod]
        public void AfterTwentyRounds_GameEnds()
        {
            game.Play();
            Assert.IsTrue(game.Finished);
        }

        [TestMethod]
        public void PlayersPlayInSameOrderEveryRound()
        {
            dice.SetPredeterminedDieValues(1, 1, 2, 3, 4, 5, 6);
            var playerVerificationList = new List<Player>();

            do
            {
                playerVerificationList.Add(game.CurrentPlayer);
                game.TakeTurn();
            } while (game.Round == 1);

            var verificationIndex = 0;
            while (!game.Finished)
            {
                verificationIndex %= playerVerificationList.Count;
                Assert.AreEqual(playerVerificationList[verificationIndex++], game.CurrentPlayer);
                game.TakeTurn();
                Assert.AreEqual(8, game.NumberOfActivePlayers);
            }
        }

        [TestMethod]
        public void OnePlayerLosesInTwoPlayerGame_OtherPlayerWins()
        {
            SetupGameWithPlayers(2);
            var winner = game.CurrentPlayer;

            game.TakeTurn();
            var loser = game.CurrentPlayer;
            loser.Pay(loser.Money + 1);
            game.TakeTurn();

            Assert.AreEqual(winner, game.Winner);
        }

        [TestMethod]
        public void OnePlayerLosesInThreePlayerGame_OtherTwoKeepPlaying()
        {
            SetupGameWithPlayers(3);
            var loser = game.CurrentPlayer;
            loser.Pay(loser.Money + 1);
            game.TakeTurn();

            Assert.IsTrue(loser.LostTheGame);
            Assert.IsFalse(game.Finished);
            Assert.AreEqual(2, game.NumberOfActivePlayers);
        }

        [TestMethod]
        public void OnePlayerLosesEachTurn_RemainingPlayerIsWinner()
        {
            var theChosenOne = game.CurrentPlayer;

            while (!game.Finished)
            {
                if (!theChosenOne.Equals(game.CurrentPlayer))
                {
                    var notTheChosenOne = game.CurrentPlayer;
                    notTheChosenOne.Pay(notTheChosenOne.Money + 1);
                }

                game.TakeTurn();
            }

            Assert.AreEqual(theChosenOne, game.Winner);
        }

        [TestMethod]
        public void AtEndOfGame_PlayerWithMostMoneyWins()
        {
            var theChosenOne = game.CurrentPlayer;

            theChosenOne.Collect(9266);
            game.Play();
            Assert.AreEqual(theChosenOne, game.Winner);
        }

        [TestMethod]
        public void ExhaustiveRun()
        {
            for (var i = 0; i < 1000; i++)
            {
                game = GameFactory.CreateGame(GeneratePlayerIEnumerable(8));
                game.Play();
            }
        }
    }
}