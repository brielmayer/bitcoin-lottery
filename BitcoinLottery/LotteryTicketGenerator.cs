using BitcoinLottery.Model;
using NBitcoin;
using System;

namespace BitcoinLottery
{
    public static class LotteryTicketGenerator
    {
        private static readonly uint256 N = uint256.Parse("fffffffffffffffffffffffffffffffebaaedce6af48a03bbfd25e8cd0364141");

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
            if (offset == 0 && array.Length == count)
                return array;

            var data = new byte[count];
            Buffer.BlockCopy(array, offset, data, 0, count);
            return data;
        }
    }
}