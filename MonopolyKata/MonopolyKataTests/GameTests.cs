using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;
using MonopolyKata.MonopolyBoard;
using MonopolyKata.MonopolyDice;
using MonopolyKata.MonopolyPlayer;
using MonopolyKataTests.MortgageStrategies;

namespace MonopolyKataTests
{
    [TestClass]
    public class GameTests
    {
        Game monopolyGame;
        String[] players = new String[8] { "Horse", "Car", "Karl", "Allen", "Elliot", "Tim", "Keith", "Ramey" };
        List<Player> playerList;

        private void CreateDefaultGame()
        {
            CreateGameWithSpecifiedNumberOfPlayersAndDice(players.Length, new Dice());
        }

        private void CreateGameWithSpecificDice(IDice dice)
        {
            CreateGameWithSpecifiedNumberOfPlayersAndDice(players.Length, dice);
        }

        private void CreateGameWithSpecifiedNumberOfPlayers(Int32 numberOfPlayers)
        {
            CreateGameWithSpecifiedNumberOfPlayersAndDice(numberOfPlayers, new Dice());
        }
        
        private void CreateGameWithSpecifiedNumberOfPlayersAndDice(Int32 numberOfPlayers, IDice dice)
        {
            monopolyGame = new Game(GeneratePlayerIEnumerable(numberOfPlayers), dice);
        }

        private ControlledDice CreateGameWithControlledDice(Int32 setRollValue)
        {
            var controlledDice = new ControlledDice();
            controlledDice.SetPredeterminedRollValue(setRollValue);
            CreateGameWithSpecificDice(controlledDice);
            return controlledDice;
        }

        private IEnumerable<Player> GeneratePlayerIEnumerable(Int32 NumberOfPlayers)
        {
            playerList = new List<Player>();
            var CountLimit = GenerateCountLimit(NumberOfPlayers);

            for (var i = 0; i < CountLimit; i++)
                playerList.Add(new Player(players[i], new NeverMortgage()));

            for (NumberOfPlayers -= CountLimit; NumberOfPlayers > 0; NumberOfPlayers--)
                playerList.Add(new Player(NumberOfPlayers.ToString(), new NeverMortgage()));

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
            Assert.AreEqual(2, monopolyGame.NumberOfActivePlayers);
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
        public void OnTurn_PlayerMoves()
        {
            CreateDefaultGame();
            var firstPlayer = monopolyGame.CurrentPlayer;
            monopolyGame.TakeTurn();
            Assert.AreNotEqual(Board.GO, firstPlayer.Position);
        }

        [TestMethod]
        public void AfterFirstTurn_NextPlayerIsTheCurrentPlayer()
        {
            CreateDefaultGame();
            var firstPlayer = monopolyGame.CurrentPlayer;
            monopolyGame.TakeTurn();
            Assert.AreNotEqual(firstPlayer, monopolyGame.CurrentPlayer);
        }

        [TestMethod]
        public void AllPlayersTakeATurnInARound()
        {
            CreateDefaultGame();
            monopolyGame.TakeRound();

            while (monopolyGame.CurrentRound == 2)
            {
                Assert.AreNotEqual(Board.GO, monopolyGame.CurrentPlayer.Position);
                monopolyGame.TakeTurn();
            }
        }

        [TestMethod]
        public void AfterEveryoneTakesATurn_BeginNewRound()
        {
            CreateDefaultGame();
            Assert.AreEqual(1, monopolyGame.CurrentRound);
            monopolyGame.TakeRound();
            Assert.AreEqual(2, monopolyGame.CurrentRound);
        }

        [TestMethod]
        public void AtNewRound_FirstPlayerHasNewTurn()
        {
            CreateDefaultGame();
            var firstPlayer = monopolyGame.CurrentPlayer;
            monopolyGame.TakeRound();
            Assert.IsTrue(firstPlayer.Equals(monopolyGame.CurrentPlayer));
        }

        [TestMethod]
        public void AfterTwentyRounds_GameEnds()
        {
            CreateDefaultGame();
            monopolyGame.PlayFullGame();
            Assert.IsTrue(monopolyGame.GameOver);
        }

        [TestMethod]
        public void PlayersPlayInSameOrderEveryRound()
        {
            CreateDefaultGame();
            var playerVerificationArray = new Player[monopolyGame.NumberOfActivePlayers];
            var verificationIndex = 0;
            var firstPlayer = monopolyGame.CurrentPlayer;
            
            do
            {
                playerVerificationArray[verificationIndex++] = monopolyGame.CurrentPlayer;
                monopolyGame.TakeTurn();
            } while (!firstPlayer.Equals(monopolyGame.CurrentPlayer));

            while (!monopolyGame.GameOver)
            {
                if (playerVerificationArray.Length != monopolyGame.NumberOfActivePlayers)
                {
                    playerVerificationArray = ResetVerificationArray(playerVerificationArray);
                    if (verificationIndex > 0)
                        verificationIndex--;
                }

                if (verificationIndex >= playerVerificationArray.Length)
                    verificationIndex = 0;

                Assert.IsTrue(playerVerificationArray[verificationIndex++].Equals(monopolyGame.CurrentPlayer));
                monopolyGame.TakeTurn();
            }
        }

        private Player[] ResetVerificationArray(Player[] verificationArray)
        {
            var temp = new Player[monopolyGame.NumberOfActivePlayers];
            var tempIndex = 0;

            for(var i = 0; i < verificationArray.Length; i++)
                if (monopolyGame.IsAnActivePlayer(verificationArray[i]))
                    temp[tempIndex++] = verificationArray[i];
                    
            return temp;
        }

        [TestMethod]
        public void PlayerCannotMoveOffOfBoard()
        {
            var controlledDice = CreateGameWithControlledDice(Board.BOARD_SIZE - 1);
            var testPlayer = monopolyGame.CurrentPlayer;

            monopolyGame.TakeRound();
            Assert.AreEqual(Board.BOARD_SIZE - 1, testPlayer.Position);
            
            controlledDice.SetPredeterminedRollValue(6);
            monopolyGame.TakeTurn();
            Assert.AreEqual(5, testPlayer.Position);
        }

        [TestMethod]
        public void PlayersReceive200ForLandingOnGo()
        {
            CreateGameWithControlledDice(Board.BOARD_SIZE);
            Player testPlayer = monopolyGame.CurrentPlayer;

            Assert.AreEqual(0, testPlayer.Money);
            monopolyGame.TakeTurn();
            Assert.AreEqual(200, testPlayer.Money);
        }

        [TestMethod]
        public void PlayersReceive200ForPassingGo()
        {
            CreateGameWithControlledDice(Board.BOARD_SIZE + 1);
            var testPlayer = monopolyGame.CurrentPlayer;

            Assert.AreEqual(0, testPlayer.Money);
            var priceOfProperty = 60;
            monopolyGame.TakeTurn();
            Assert.AreEqual(200 - priceOfProperty, testPlayer.Money);
        }

        [TestMethod]
        public void PlayersDoNotReceiveMoneyFromLeavingGo()
        {
            CreateDefaultGame();
            Player testPlayer;

            while (monopolyGame.CurrentRound == 1)
            {
                testPlayer = monopolyGame.CurrentPlayer;
                monopolyGame.TakeTurn();
                Assert.AreNotEqual(200, testPlayer.Money);
            }
        }

        [TestMethod]
        public void PassGoMultipleTimes_PlayerGetsMultiplePaymentsOfTwoHundred()
        {
            CreateGameWithControlledDice(Board.BOARD_SIZE * 2);
            var testPlayer = monopolyGame.CurrentPlayer;
            monopolyGame.TakeTurn();
            Assert.AreEqual(400, testPlayer.Money);
        }

        [TestMethod]
        public void OnePlayerLosesInTwoPlayerGame_OtherPlayerWins()
        {
            CreateGameWithSpecifiedNumberOfPlayers(2);
            var winner = monopolyGame.CurrentPlayer;

            monopolyGame.TakeTurn();
            ForceCurrentPlayerToGoBroke();

            Assert.AreEqual(winner, monopolyGame.Winner);
        }

        [TestMethod]
        public void OnePlayerLosesInThreePlayerGame_OtherTwoKeepPlaying()
        {
            CreateGameWithSpecifiedNumberOfPlayers(3);
            var loser = monopolyGame.CurrentPlayer;
            ForceCurrentPlayerToGoBroke();

            Assert.IsFalse(monopolyGame.IsAnActivePlayer(loser));
            Assert.AreEqual(2, monopolyGame.NumberOfActivePlayers);

            monopolyGame.TakeTurn();

            Assert.IsFalse(monopolyGame.IsAnActivePlayer(loser));
            Assert.AreEqual(2, monopolyGame.NumberOfActivePlayers);
        }

        private void ForceCurrentPlayerToGoBroke()
        {
            monopolyGame.CurrentPlayer.Pay(monopolyGame.CurrentPlayer.Money + 1);
            monopolyGame.TakeTurn();
        }

        [TestMethod]
        public void OnePlayerLosesEachTurn_RemainingPlayerIsWinner()
        {
            CreateDefaultGame();
            var TheChosenOne = monopolyGame.CurrentPlayer;

            while (!monopolyGame.GameOver)
            {
                if (TheChosenOne.Equals(monopolyGame.CurrentPlayer))
                {
                    TheChosenOne.ReceiveMoney(9266);
                    monopolyGame.TakeTurn();
                }
                else
                {
                    ForceCurrentPlayerToGoBroke();
                }
            }

            Assert.AreEqual(TheChosenOne, monopolyGame.Winner);
        }

        private void GivePlayerProperty(Player owner, Property property)
        {
            owner.ReceiveMoney(property.Price);
            property.LandOn(owner);
        }

        private void ZeroOutPlayersMoney(Player p)
        {
            p.ReceiveMoney(9266);
            monopolyGame.TakeTurn();
            p.Pay(p.Money);
            monopolyGame.TakeTurn();
        }

        [TestMethod]
        public void PlayerRollsDoubles_GoesAgain()
        {
            var controlledDice = new ControlledDice();
            controlledDice.SetPredeterminedDieValue(3, 3, 3, 1);
            CreateGameWithSpecificDice(controlledDice);

            var testPlayer = monopolyGame.CurrentPlayer;
            testPlayer.ReceiveMoney(9266);

            monopolyGame.TakeTurn();
            Assert.AreEqual(10, testPlayer.Position);
            var property = monopolyGame.GetSpaceByIndex(6) as Property;
            Assert.IsTrue(testPlayer.Owns(property));
        }

        [TestMethod]
        public void PlayerDoesNotRollDoubles_OnlyMovesOneRoll()
        {
            var controlledDice = new ControlledDice();
            controlledDice.SetPredeterminedDieValue(4, 2, 1, 1);
            CreateGameWithSpecificDice(controlledDice);

            var testPlayer = monopolyGame.CurrentPlayer;
            testPlayer.ReceiveMoney(9266);

            monopolyGame.TakeTurn();
            Assert.AreEqual(6, testPlayer.Position);
        }

        [TestMethod]
        public void PlayerRolls2Doubles_Moves3Times()
        {
            var controlledDice = new ControlledDice();
            controlledDice.SetPredeterminedDieValue(3, 3, 4, 4, 4, 2);
            CreateGameWithSpecificDice(controlledDice);

            var testPlayer = monopolyGame.CurrentPlayer;
            testPlayer.ReceiveMoney(9266);

            monopolyGame.TakeTurn();
            Assert.AreEqual(20, testPlayer.Position);
            var property = monopolyGame.GetSpaceByIndex(6) as Property;
            var otherProperty = monopolyGame.GetSpaceByIndex(14) as Property;
            Assert.IsTrue(testPlayer.Owns(property));
            Assert.IsTrue(testPlayer.Owns(otherProperty));
        }

        [TestMethod]
        public void PlayerRolls3Doubles_GoesToJustVisiting()
        {
            var controlledDice = new ControlledDice();
            controlledDice.SetPredeterminedDieValue(3, 3, 4, 4, 1, 1);
            CreateGameWithSpecificDice(controlledDice);

            var testPlayer = monopolyGame.CurrentPlayer;
            testPlayer.ReceiveMoney(9266);

            monopolyGame.TakeTurn();
            Assert.AreEqual(Board.JAIL_OR_JUST_VISITING, testPlayer.Position);
            var property = monopolyGame.GetSpaceByIndex(6) as Property;
            var otherProperty = monopolyGame.GetSpaceByIndex(14) as Property;
            var unownedProperty = monopolyGame.GetSpaceByIndex(16) as Property;
            Assert.IsTrue(testPlayer.Owns(property));
            Assert.IsTrue(testPlayer.Owns(otherProperty));
            Assert.IsFalse(testPlayer.Owns(unownedProperty));
        }
    }
}