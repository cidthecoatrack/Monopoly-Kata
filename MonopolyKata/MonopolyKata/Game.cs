using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Handlers;
using Monopoly.Board.Spaces;
using Monopoly.Dice;
using Monopoly;

namespace Monopoly
{
    public class Game
    {
        private LinkedListNode<Player> currentPlayerPointer;
        private LinkedList<Player> players;
        private TurnHandler turnHandler;

        public Int16 Round { get; private set; }
        public Boolean Finished { get { return (Round > GameConstants.ROUND_LIMIT || NumberOfActivePlayers == 1); } }
        public Int32 NumberOfActivePlayers { get { return players.Count; } }

        public Player CurrentPlayer
        {
            get { return currentPlayerPointer.Value; }
            private set { currentPlayerPointer.Value = value; }
        }

        public Player Winner
        {
            get
            {
                if (Finished)
                    return players.OrderByDescending(x => x.Money).First();
                return null;
            }
        }

        public Game(IEnumerable<Player> newPlayers, IDice dice, List<ISpace> board, JailHandler jailHandler)
        {
            CheckNumberOfPlayers(newPlayers);

            var randomizer = new PlayerOrderRandomizer();
            var randomizedPlayers = randomizer.Execute(newPlayers, dice);
            players = new LinkedList<Player>(randomizedPlayers);

            turnHandler = new TurnHandler(dice, board, jailHandler);
            currentPlayerPointer = players.First;
            Round = 1;
        }

        private void CheckNumberOfPlayers(IEnumerable<Player> newPlayers)
        {
            if (newPlayers.Count() < GameConstants.MINIMUM_NUMBER_OF_PLAYERS || newPlayers.Count() > GameConstants.MAXIMUM_NUMBER_OF_PLAYERS)
                throw new ArgumentOutOfRangeException();
        }

        public void Play()
        {
            while (!Finished)
                TakeRound();
        }

        public void TakeRound()
        {
            var turnCount = 0;
            while (!Finished && turnCount++ < players.Count)
                TakeTurn();
        }

        public void TakeTurn()
        {
            turnHandler.TakeTurn(CurrentPlayer);
            EndTurn();
        }

        private void EndTurn()
        {
            if (CurrentPlayer.LostTheGame)
                RemovePlayer();
            else
                ShiftToNextPlayer();
        }

        private void ShiftToNextPlayer()
        {
            currentPlayerPointer = currentPlayerPointer.Next ?? players.First;
            if (currentPlayerPointer == players.First)
                Round++;
        }

        private void RemovePlayer()
        {
            var newPointer = currentPlayerPointer.Next ?? players.First;
            players.Remove(CurrentPlayer);
            currentPlayerPointer = newPointer;
        }
    }
}