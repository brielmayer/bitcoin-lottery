using System.Collections.Generic;
using BitcoinLottery.Model;
using NUnit.Framework;
using NBitcoin;

namespace BitcoinLotteryTest
{
    public class Tests 
    {
        private readonly List<LotteryTicket> _lotteryTickets = new List<LotteryTicket>
        {
            new LotteryTicket("5HpHagT65TZzG1PH3CSu63k8DbpvD8s5ip4nEB3kEsreAnchuDf", "1BgGZ9tcN4rm9KBzDn7KprQz87SZ26SAMH", "1EHNa6Q4Jz2uvNExL497mE43ikXhwF6kZm"),
            new LotteryTicket("5HpHagT65TZzG1PH3CSu63k8DbpvD8s5ip4nEB3kEsreAvUcVfH", "1cMh228HTCiwS8ZsaakH8A8wze1JR5ZsP", "1LagHJk2FyCV2VzrNHVqg3gYG4TSYwDV4m"),
            new LotteryTicket("5HpHagT65TZzG1PH3CSu63k8DbpvD8s5ip4nEB3kEsreB1FQ8BZ", "1CUNEBjYrCn2y1SdiUMohaKUi4wpP326Lb", "1NZUP3JAc9JkmbvmoTv7nVgZGtyJjirKV1"),
            new LotteryTicket("5HpHagT65TZzG1PH3CSu63k8DbpvD8s5ip4nEB3kEsreB4AD8Yi", "1JtK9CQw1syfWj1WtFMWomrYdV3W2tWBF9", "1MnyqgrXCmcWJHBYEsAWf7oMyqJAS81eC"),
            new LotteryTicket("5HpHagT65TZzG1PH3CSu63k8DbpvD8s5ip4nEB3kEsreBF8or94", "17Vu7st1U1KwymUKU4jJheHHGRVNqrcfLD", "1E1NUNmYw1G5c3FKNPd435QmDvuNG3auYk"),
            new LotteryTicket("5HpHagT65TZzG1PH3CSu63k8DbpvD8s5ip4nEB3kEsreBKdE2NK", "1Cf2hs39Woi61YNkYGUAcohL2K2q4pawBq", "1UCZSVufT1PNimutbPdJUiEyCYSiZAD6n"),
            new LotteryTicket("5HpHagT65TZzG1PH3CSu63k8DbpvD8s5ip4nEB3kEsreBR6zCMU", "19ZewH8Kk1PDbSNdJ97FP4EiCjTRaZMZQA", "1BYbgHpSKQCtMrQfwN6b6n5S718EJkEJ41"),
            new LotteryTicket("5HpHagT65TZzG1PH3CSu63k8DbpvD8s5ip4nEB3kEsreBbMaQX1", "1EhqbyUMvvs7BfL8goY6qcPbD6YKfPqb7e", "1JMcEcKXQ7xA7JLAMPsBmHz68bzugYtdrv"),
            new LotteryTicket("5HpHagT65TZzG1PH3CSu63k8DbpvD8s5ip4nEB3kEsreBd7uGcN", "1HSxWThjiwbC4dJbXHMpBfwRenB12UguG5", "1CijKR7rDvJJBJfSPyUYrWC8kAsQLy2B2e"),
            new LotteryTicket("5HpHagT65TZzG1PH3CSu63k8DbpvD8s5ip4nEB3kEsreBoNWTw6", "13DaZ9nfmJLfzU6oBnD2sdCiDmf3M5fmLx", "1GDWJm5dPj6JTxF68WEVhicAS4gS3pvjo7"),
        };

        [Test]
        public void TestLotteryTickets()
        {
            for (int i = 0; i < _lotteryTickets.Count; i++)
            {
                LotteryTicket actualLotteryTicket = new LotteryTicket(new uint256((uint)i + 1));
                Assert.AreEqual(_lotteryTickets[i], actualLotteryTicket);
            }
        }
    }
}