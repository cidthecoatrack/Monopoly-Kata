using MonopolyKata.MonopolyDice;
using MonopolyKata.MonopolyPlayer;

namespace MonopolyKata.Handlers
{
    public class JailHandler
    {
        private IDice dice;
        private Player player;

        public JailHandler(IDice dice, Player player)
        {
            this.dice = dice;
            this.player = player;
        }

        public void HandleJail()
        {
            if (player.IsInJail)
            {
                SeeIfPlayerCanGetOutOfJailOnDoubles();
            }
            else if (dice.DoublesCount >= GameConstants.DOUBLES_LIMIT)
            {
                player.GoToJail();
                dice.Reset();
            }
        }

        private void SeeIfPlayerCanGetOutOfJailOnDoubles()
        {
            if (dice.Doubles)
            {
                player.IsInJail = false;
                dice.Reset();
            }
            else if (player.TurnsInJail >= GameConstants.TURNS_IN_JAIL_LIMIT)
            {
                player.PayToGetOutOfJail();
            }
        }
    }
}