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
        private const Int16 ROUND_LIMIT = 20;
        
        private LinkedList<Player> players;
        private LinkedListNode<Player> currentPlayerPointer;
        private Dice dice;
        private Int16 currentRound;

        private Player currentPlayer
        {
            get { return currentPlayerPointer.Value; }
            set { currentPlayerPointer.Value = value; }
        }

        public Boolean GameOver
        {
            get { return (currentRound > ROUND_LIMIT || Winner != null); }
        }

        public Int16 CurrentRound
        {
            get { return currentRound; }
        }

        public LinkedList<Player> Players
        {
            get { return players; }
        }

        public Player CurrentPlayer
        {
            get { return currentPlayerPointer.Value; }
        }

        public LinkedListNode<Player> CurrentPlayerNode
        {
            get { return currentPlayerPointer; }
        }

        public Int32 NumberOfActivePlayers
        {
            get { return players.Count; }
        }

        public Player Winner
        {
            get
            {
                if (players.Count == 1)
                    return players.First.Value;
                return null;
            }
        }

        public Monopoly(IEnumerable<Player> NewPlayers)
        {
            if (InvalidNumberOfPlayers(NewPlayers))
                throw new ArgumentOutOfRangeException();
            
            InitializeClassVariables();
            SetPlayers(NewPlayers);
            currentPlayerPointer = players.First;
        }

        private static bool InvalidNumberOfPlayers(IEnumerable<Player> NewPlayers)
        {
            return NewPlayers.Count<Player>() < 2 || NewPlayers.Count<Player>() > 8;
        }

        private void InitializeClassVariables()
        {
            players = new LinkedList<Player>();
            dice = new Dice();
            currentRound = 1;
        }

        private void SetPlayers(IEnumerable<Player> NewPlayers)
        {
            foreach (Player player in RandomizePlayerOrder(NewPlayers))
                players.AddLast(player);
        }

        private IEnumerable<Player> RandomizePlayerOrder(IEnumerable<Player> NewPlayers)
        {
            return NewPlayers.OrderBy(player => dice.RollSingleDie());
        }

        public void PlayFullGame()
        {
            TakeRounds(ROUND_LIMIT);
        }

        public void TakeRounds(Int32 rounds)
        {
            for (Int16 i = 0; i < rounds; i++)
                TakeRound();
        }

        public void TakeRound()
        {
            TakeTurns(players.Count);
        }

        public void TakeTurns(Int32 turns)
        {
            for (Int16 i = 0; i < turns; i++)
                TakeTurn();
        }

        public void TakeTurn()
        {
            RollForCurrentPlayer();
            NextPlayerTurn();
            CheckForLosers();
        }

        private void NextPlayerTurn()
        {
            if (CurrentPlayerIsNotLastPlayer())
            {
                currentPlayerPointer = currentPlayerPointer.Next;
            }
            else
            {
                currentPlayerPointer = players.First;
                currentRound++;
            }
        }

        private Boolean CurrentPlayerIsNotLastPlayer()
        {
            return currentPlayerPointer != players.Last;
        }

        private void RollForCurrentPlayer()
        {
            MoveCurrentPlayerSetAmount(dice.RollTwoDice());
        }

        public void MoveCurrentPlayerSetAmount(Int32 AmountToMove)
        {
            if (!GameOver)
            {
                currentPlayer.Move(AmountToMove);
                CheckIfCurrentPlayerPassedGo();
                CheckForSpecialSpaces();
            }
        }

        private void CheckForSpecialSpaces()
        {
            switch (currentPlayer.Position)
            {
                case MonopolyBoard.GO_TO_JAIL: currentPlayer.SetPosition(MonopolyBoard.JAIL_OR_JUST_VISITING); break;
                case MonopolyBoard.INCOME_TAX: DeductIncomeTaxFromCurrentPlayer(); break;
                case MonopolyBoard.LUXURY_TAX: currentPlayer.Pay(75); break;
                default: break;
            }
        }

        private void DeductIncomeTaxFromCurrentPlayer()
        {
            if (currentPlayer.Money / 10 < 200)
                currentPlayer.Pay(currentPlayer.Money / 10);
            else
                currentPlayer.Pay(200);
        }

        private void CheckIfCurrentPlayerPassedGo()
        {
            while(CurrentPlayerHasPassedGo())
            {
                currentPlayer.SetPosition(currentPlayer.Position - MonopolyBoard.BOARD_SIZE);
                currentPlayer.ReceiveMoney(200);
            }
        }

        private Boolean CurrentPlayerHasPassedGo()
        {
            return (currentPlayer.Position >= MonopolyBoard.BOARD_SIZE);
        }

        private void CheckForLosers()
        {
            CheckForLosers(players.First);
        }

        private void CheckForLosers(LinkedListNode<Player> playerNode)
        {
            if (playerNode.Value.LostTheGame)
            {
                RemovePlayer(playerNode);
                CheckForLosers();
            }
            else if (playerNode.Next != null)
            {
                CheckForLosers(playerNode.Next);
            }
        }

        private void RemovePlayer(LinkedListNode<Player> playerNode)
        {
            Player tempCurrentPlayer = currentPlayerPointer.Value;
            players.Remove(playerNode.Value);
            currentPlayerPointer = players.Find(tempCurrentPlayer);
        }
    }
}