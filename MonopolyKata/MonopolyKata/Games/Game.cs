using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Handlers;
using Monopoly.Players;

namespace Monopoly.Games
{
    public class Game
    {
        private LinkedListNode<Player> currentPlayerPointer;
        private LinkedList<Player> players;
        private TurnHandler turnHandler;
        private Banker banker;

        public Int16 Round { get; private set; }
        public Boolean Finished { get { return (Round > GameConstants.ROUND_LIMIT || NumberOfActivePlayers == 1); } }
        public Int32 NumberOfActivePlayers { get { return banker.GetNumberOfActivePlayers(); } }

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
                    return banker.GetWinner();
                return null;
            }
        }

        public Game(IEnumerable<Player> newPlayers, TurnHandler turnHandler, Banker banker)
        {
            CheckNumberOfPlayers(newPlayers);

            var randomizer = new PlayerOrderRandomizer();
            var randomizedPlayers = randomizer.Execute(newPlayers);
            players = new LinkedList<Player>(randomizedPlayers);

            this.turnHandler = turnHandler;
            this.banker = banker;
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
            ShiftToNextPlayer();
        }

        private void ShiftToNextPlayer()
        {
            var newPointer = currentPlayerPointer.Next ?? players.First;
            var losers = players.Where(p => banker.IsBankrupt(p)).ToList();

            foreach (var player in losers)
            {
                if (newPointer.Value == player)
                {
                    newPointer = newPointer.Next ?? players.First;

                    if (newPointer == players.First)
                        Round++;
                }

                players.Remove(player);
            }

            currentPlayerPointer = newPointer;
            if (currentPlayerPointer == players.First)
                Round++;
        }
    }
}