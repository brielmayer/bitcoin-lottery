using BitcoinLottery.Model;
using NBitcoin;
using System;

namespace BitcoinLottery
{
    class LotteryTicketGenerator
    {
        private readonly static uint256 N = uint256.Parse("fffffffffffffffffffffffffffffffebaaedce6af48a03bbfd25e8cd0364141");

        public static LotteryTicket Generate()
        {
            var data = new byte[32];

            do
            {
                RandomUtils.GetBytes(data);
            } while (!Check(data));

            return new LotteryTicket(data);
        }

        private static bool Check(byte[] vch)
        {
            var candidateKey = new uint256(SafeSubarray(vch, 0, 32), false);
            return candidateKey > 0 && candidateKey < N;
        }

        private static byte[] SafeSubarray(byte[] array, int offset, int count)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (offset < 0 || offset > array.Length)
                throw new ArgumentOutOfRangeException("offset");
            if (count < 0 || offset + count > array.Length)
                throw new ArgumentOutOfRangeException("count");
            if (offset == 0 && array.Length == count)
                return array;

            var data = new byte[count];
            Buffer.BlockCopy(array, offset, data, 0, count);
            return data;
        }
    }
}
