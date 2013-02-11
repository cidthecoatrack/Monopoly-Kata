using System;
using System.Security.Cryptography;

namespace MonopolyKata.MonopolyDice
{
    public class Dice : IDice
    {
        protected Random random;

        public Boolean Doubles { get; private set; }
        public Int32 Value { get; private set; }

        public Dice()
        {
            var Crypto = new RNGCryptoServiceProvider();
            var CryptoByteBuffer = new Byte[4];
            Crypto.GetBytes(CryptoByteBuffer);
            random = new Random(BitConverter.ToInt32(CryptoByteBuffer, 0));
        }
        
        public Int32 RollSingleDie()
        {
            return random.Next(1, 7);
        }

        public void RollTwoDice()
        {
            var Die1 = RollSingleDie();
            var Die2 = RollSingleDie();

            Doubles = (Die1 == Die2);
            Value = Die1 + Die2;
        }
    }
}