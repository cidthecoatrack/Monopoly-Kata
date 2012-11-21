using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography;

namespace MonopolyKata
{
    public class Dice
    {
        private Random random;

        public Dice()
        {
            InitializeRandomInstanceWithCryptographicallyStrongRandomSeed();
        }

        private void InitializeRandomInstanceWithCryptographicallyStrongRandomSeed()
        {
            RNGCryptoServiceProvider Crypto = new RNGCryptoServiceProvider();
            Byte[] CryptoByteBuffer = new Byte[4];

            Crypto.GetBytes(CryptoByteBuffer);
            random = new Random(BitConverter.ToInt32(CryptoByteBuffer, 0));
        }
        
        public Int32 RollSingleDie()
        {
            return random.Next(1, 7);
        }

        public Int32 RollTwoDice()
        {
            return RollSingleDie() + RollSingleDie();
        }

        public Int32 RollUnboundedRandomNumber()
        {
            return random.Next();
        }
    }
}