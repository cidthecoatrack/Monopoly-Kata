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
            var newPointer = currentPlayerPointer;

            do
            {
                newPointer = newPointer.Next ?? players.First;

                if (newPointer == players.First)
                    Round++;
            } while (banker.IsBankrupt(newPointer.Value));

            currentPlayerPointer = newPointer;
            RemoveBankruptPlayers();
        }

        private void RemoveBankruptPlayers()
        {
            var losers = banker.GetBankrupcies(players);

            foreach (var player in losers)
                players.Remove(player);
        }
    }
}