using System;
using System.Security.Cryptography;

namespace MonopolyKata.MonopolyDice
{
    public class Dice : IDice
    {
        protected Random random;

        public Boolean Doubles { get; private set; }
        public Int32 DoublesCount { get; private set; }

        public Dice()
        {
            InitializeRandomInstanceWithCryptographicallyStrongRandomSeed();
        }

        private void InitializeRandomInstanceWithCryptographicallyStrongRandomSeed()
        {
            var Crypto = new RNGCryptoServiceProvider();
            var CryptoByteBuffer = new Byte[4];
            Crypto.GetBytes(CryptoByteBuffer);
            random = new Random(BitConverter.ToInt32(CryptoByteBuffer, 0));
            DoublesCount = 0;
        }
        
        public Int32 RollSingleDie()
        {
            return random.Next(1, 7);
        }

        public Int32 RollTwoDice()
        {
            var Die1 = RollSingleDie();
            var Die2 = RollSingleDie();
            
            if (Die1 == Die2)
            {
                Doubles = true;
                DoublesCount++;
            }
            else
            {
                Doubles = false;
                DoublesCount = 0;
            }

            return Die1 + Die2;
        }
    }
}