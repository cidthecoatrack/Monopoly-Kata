using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;
using MonopolyKata.MonopolyBoard;
using MonopolyKata.MonopolyBoard.Spaces;
using MonopolyKata.MonopolyPlayer;
using MonopolyKataTests.Strategies.JailStrategies;
using MonopolyKataTests.Strategies.MortgageStrategies;

namespace MonopolyKataTests
{
    [TestClass]
    public class GameTests
    {
        Game game;
        BoardFactory boardFactory;
        List<ISpace> board;
        ControlledDice dice;

        private IEnumerable<Player> GeneratePlayerIEnumerable(Int32 NumberOfPlayers)
        {
            var playerList = new List<Player>();

            while (NumberOfPlayers-- > 0)
                playerList.Add(new Player("Player " + NumberOfPlayers, new NeverMortgage(), new NeverPay()));

            return playerList;
        }

        [TestInitialize]
        public void Setup()
        {
            dice = new ControlledDice();
            boardFactory = new BoardFactory();
            board = boardFactory.CreateMonopolyBoard(dice);
            game = new Game(GeneratePlayerIEnumerable(8), dice, board);
        }

        [TestMethod]
        public void CreateTwoPlayerGame_GameHasTwoPlayers()
        {
            game = new Game(GeneratePlayerIEnumerable(2), dice, board);
            Assert.AreEqual(2, game.NumberOfActivePlayers);
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void FewerThanTwoPlayers_MonopolyWillNotPlay()
        {
            game = new Game(GeneratePlayerIEnumerable(1), dice, board);
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MoreThanEightPlayers_MonopolyWillNotPlay()
        {
            game = new Game(GeneratePlayerIEnumerable(9), dice, board);
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
            dice.SetPredeterminedRollValue(3);

            while (game.Round == 1)
            {
                var player = game.CurrentPlayer;
                game.TakeTurn();
                Assert.AreEqual(3, player.Position);
            }
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
            game = new Game(GeneratePlayerIEnumerable(8), dice, boardFactory.CreateBoardOfNormalSpaces());
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
            game = new Game(GeneratePlayerIEnumerable(2), dice, board);
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
            game = new Game(GeneratePlayerIEnumerable(3), dice, board);
            var loser = game.CurrentPlayer;
            loser.Pay(loser.Money + 1);
            game.TakeTurn();

            Assert.IsTrue(loser.LostTheGame);
            Assert.IsFalse(game.Finished);
            Assert.AreEqual(2, game.NumberOfActivePlayers);

            game.TakeTurn();
            Assert.IsTrue(loser.LostTheGame);
            Assert.IsFalse(game.Finished);
            Assert.AreEqual(2, game.NumberOfActivePlayers);
        }

        [TestMethod]
        public void OnePlayerLosesEachTurn_RemainingPlayerIsWinner()
        {
            game = new Game(GeneratePlayerIEnumerable(8), dice, boardFactory.CreateBoardOfNormalSpaces());
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
            game = new Game(GeneratePlayerIEnumerable(8), dice, boardFactory.CreateBoardOfNormalSpaces());
            var theChosenOne = game.CurrentPlayer;

            theChosenOne.ReceiveMoney(9266);
            game.Play();
            Assert.AreEqual(theChosenOne, game.Winner);
        }
    }
}