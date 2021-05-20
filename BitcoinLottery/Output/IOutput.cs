using BitcoinLottery.Model;

namespace BitcoinLottery.Output
{
    internal interface IOutput
    {
        void Submit(LotteryTicket lotteryTicket);
    }
}