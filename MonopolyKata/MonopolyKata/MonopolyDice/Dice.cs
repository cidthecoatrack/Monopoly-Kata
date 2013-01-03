using System;
using System.Security.Cryptography;

namespace MonopolyKata.MonopolyDice
{
    public class Dice : IDice
    {
        protected Random random;

        public Boolean Doubles { get; private set; }
        public Int32 DoublesCount { get; private set; }
        public Int32 Value { get; private set; }

        public Dice()
        {
            InitializeRandomInstanceWithCryptographicallyStrongRandomSeed();
            DoublesCount = 0;
        }

        private void InitializeRandomInstanceWithCryptographicallyStrongRandomSeed()
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
            
            if (Die1 == Die2)
            {
                Doubles = true;
                DoublesCount++;
            }
            else
            {
                Reset();
            }

            Value = Die1 + Die2;
        }

        public void Reset()
        {
            DoublesCount = 0;
            Doubles = false;
        }
    }
}