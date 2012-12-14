using System;
using System.Collections.Generic;
using System.Linq;
using MonopolyKata.MonopolyBoard;
using MonopolyKata.MonopolyDice;
using MonopolyKata.MonopolyPlayer;

namespace MonopolyKata
{
    public class Game
    {
        private const Int16 ROUND_LIMIT = 20;
        
        private LinkedListNode<Player> currentPointer;
        private LinkedList<Player> players;
        private IDice dice;
        private ISpace[] board;

        public Int16 CurrentRound { get; private set; }
        public Boolean GameOver { get { return (CurrentRound > ROUND_LIMIT || Winner != null); } }
        public Int32 NumberOfActivePlayers { get { return players.Count; } }

        private LinkedListNode<Player> currentPlayerPointer
        {
            get { return currentPointer; }
            set
            {
                if (value == players.First)
                    CurrentRound++;
                currentPointer = value;
            }
        }
        public Player CurrentPlayer
        {
            get { return currentPlayerPointer.Value; }
            private set { currentPlayerPointer.Value = value; }
        }
        public Player Winner
        {
            get
            {
                if (players.Count == 1)
                    return CurrentPlayer;
                else if (CurrentRound > ROUND_LIMIT)
                    return playerWithMostMoney();
                return null;
            }
        }

        private Player playerWithMostMoney()
        {
            var playerWithMostMoney = CurrentPlayer;
            var maxCash = playerWithMostMoney.Money;

            foreach (var player in players)
            {
                if (player.Money > maxCash)
                {
                    playerWithMostMoney = player;
                    maxCash = playerWithMostMoney.Money;
                }
            }

            return playerWithMostMoney;
        }

        public Game(IEnumerable<Player> newPlayers, IDice dice)
        {
            if (InvalidNumberOfPlayers(newPlayers))
                throw new ArgumentOutOfRangeException();

            this.dice = dice;
            InitializeClassVariables();
            SetPlayers(newPlayers);
            currentPlayerPointer = players.First;
        }

        private static Boolean InvalidNumberOfPlayers(IEnumerable<Player> newPlayers)
        {
            return newPlayers.Count() < 2 || newPlayers.Count() > 8;
        }

        private void InitializeClassVariables()
        {
            players = new LinkedList<Player>();
            CurrentRound = 0;
            board = Board.GetMonopolyBoard();
        }

        private void SetPlayers(IEnumerable<Player> newPlayers)
        {
            foreach (var player in PlayerOrderRandomizer.Execute(newPlayers))
                players.AddLast(player);
        }

        public void PlayFullGame()
        {
            while (!GameOver)
                TakeRound();
        }

        public void TakeRound()
        {
            var turnCount = 0;
            while (!GameOver && turnCount++ < players.Count)
                TakeTurn();
        }

        public void TakeTurn()
        {
            CurrentPlayer.HandleMortgages();
            TakeCurrentPlayerTurn();
            CurrentPlayer.HandleMortgages();

            if (CurrentPlayer.LostTheGame)
                RemoveCurrentPlayer();
            else
                NextPlayerTurn();
        }

        private void TakeCurrentPlayerTurn()
        {
            do RollForCurrentPlayer();
            while (CurrentPlayerCanGoAgain());
        }

        private Boolean CurrentPlayerCanGoAgain()
        {
            return dice.Doubles && dice.DoublesCount < 3 && !CurrentPlayer.LostTheGame;
        }

        private void NextPlayerTurn()
        {
            currentPlayerPointer = currentPlayerPointer.Next ?? players.First;
        }

        private void RollForCurrentPlayer()
        {
            var roll = dice.RollTwoDice();

            if (dice.DoublesCount == 3)
                CurrentPlayer.SetPosition(10);
            else
                MoveCurrentPlayerForward(roll);
        }

        private void MoveCurrentPlayerForward(Int32 amountToMove)
        {
            CurrentPlayer.Move(amountToMove);
            CheckIfCurrentPlayerPassedGo();
            board[CurrentPlayer.Position].LandOn(CurrentPlayer);
        }

        private void CheckIfCurrentPlayerPassedGo()
        {
            while(CurrentPlayerHasPassedGo())
            {
                CurrentPlayer.SetPosition(CurrentPlayer.Position - Board.BOARD_SIZE);
                CurrentPlayer.ReceiveMoney(200);
            }
        }

        private Boolean CurrentPlayerHasPassedGo()
        {
            return CurrentPlayer.Position >= Board.BOARD_SIZE;
        }

        private void RemoveCurrentPlayer()
        {
            var newPointer = currentPlayerPointer.Next ?? players.First;
            players.Remove(CurrentPlayer);
            currentPlayerPointer = newPointer;
        }

        #region Just For Testing
        public Boolean IsAnActivePlayer(Player player)
        {
            return players.Contains(player);
        }

        public ISpace GetSpaceByIndex(Int32 index)
        {
            return board[index];
        }
        #endregion
    }
}