using NUnit.Framework;
using BitcoinLottery;

namespace BitcoinLotteryTest
{
    class LotteryTicketGeneratorTest
    {

        [Test]
        public void TestLotteryTicketGenerator()
        {
            var lotteryTicket = LotteryTicketGenerator.Generate();
            Assert.AreEqual(51, lotteryTicket.PrivateKey.Length);
            Assert.AreEqual(34, lotteryTicket.Compressed.Length);
            Assert.AreEqual(34, lotteryTicket.Uncompressed.Length);
        }

    }
}
