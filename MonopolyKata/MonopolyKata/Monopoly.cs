using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyKata
{
    public class Monopoly
    {
        private const Int32 BOARD_SIZE = 40;
        private const Int32 ROUND_LIMIT = 20;

        private Int32[] Board = new int[BOARD_SIZE];
        private LinkedList<Player> Players;
        private LinkedListNode<Player> CurrentPlayer;
        private Dice dice;
        private Int32 CurrentRound;
        private Boolean GameOver;

        public Monopoly(params String[] playerNames)
        {
            InitializeClassVariables();
            SetPlayers(playerNames);
            CurrentPlayer = Players.First;
        }

        private void InitializeClassVariables()
        {
            Players = new LinkedList<Player>();
            dice = new Dice();
            CurrentRound = 1;
            GameOver = false;
        }

        private void SetPlayers(IEnumerable<String> playerNames)
        {
            foreach (String playerName in RandomizePlayerOrder(playerNames))
                Players.AddLast(new Player(playerName));
        }

        private IEnumerable<String> RandomizePlayerOrder(IEnumerable<String> playerNames)
        {
            return playerNames.OrderBy(x => dice.RollSingleDie());
        }

        public void TakeRounds(Int32 rounds)
        {
            for (Int32 i = 0; i < rounds; i++)
                TakeRound();
        }

        public void TakeRound()
        {
            TakeTurns(Players.Count);
        }

        public void TakeTurns(Int32 turns)
        {
            for (Int32 i = 0; i < turns; i++)
                TakeTurn();
        }

        public void TakeTurn()
        {
            RollNewPositionForCurrentPlayer();
            NextPlayerTurn();
        }

        private void NextPlayerTurn()
        {
            if (CurrentPlayerIsNotLastPlayer())
                CurrentPlayer = CurrentPlayer.Next;
            else
            {
                CurrentPlayer = Players.First;
                AdjustRound();
            }
        }

        private Boolean CurrentPlayerIsNotLastPlayer()
        {
            return CurrentPlayer != Players.Last;
        }

        private void AdjustRound()
        {
            CurrentRound++;

            if (CurrentRound > ROUND_LIMIT)
                GameOver = true;
        }

        private void RollNewPositionForCurrentPlayer()
        {
            CurrentPlayer.Value.Position = (CurrentPlayer.Value.Position + dice.RollTwoDice()) % BOARD_SIZE;
        }

        public Int32 GetRound()
        {
            return CurrentRound;
        }

        public Boolean IsGameOver()
        {
            return GameOver;
        }

        public Int32[] GetBoard()
        {
            return Board;
        }

        public LinkedList<Player> GetPlayers()
        {
            return Players;
        }

        public Player GetCurrentPlayer()
        {
            return CurrentPlayer.Value;
        }

        public LinkedListNode<Player> GetCurrentPlayerNode()
        {
            return CurrentPlayer;
        }
    }
}
