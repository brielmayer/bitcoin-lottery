using BitcoinLottery.Model;

namespace BitcoinLottery.Output
{
    internal interface IOutput
    {
        void SanityCheck();
        void Submit(LotteryTicket lotteryTicket);
    }
}