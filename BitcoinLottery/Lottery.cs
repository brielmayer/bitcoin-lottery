using System.Collections.Concurrent;
using System.Collections.Generic;
using BitcoinLottery.Model;
using BitcoinLottery.Output;

namespace BitcoinLottery
{
    internal class Lottery
    {
        private readonly Options _options;

        private readonly ThreadSafeCounter _threadSafeCounter;

        private readonly ConcurrentBag<LotteryTicket> _winningLotteryTickets;

        private readonly HashSet<string> _bitcoinAddressToBalance;

        public Lottery(Options options, ThreadSafeCounter threadSafeCounter, ConcurrentBag<LotteryTicket> winningLotteryTickets, HashSet<string> bitcoinAddressToBalance)
        {
            _options = options;
            _threadSafeCounter = threadSafeCounter;
            _winningLotteryTickets = winningLotteryTickets;
            _bitcoinAddressToBalance = bitcoinAddressToBalance;
        }

        public void Run()
        {
            while (true)
            {
                LotteryTicket lotteryTicket = LotteryTicketGenerator.Generate();
                if (_bitcoinAddressToBalance.Contains(lotteryTicket.Uncompressed) || _bitcoinAddressToBalance.Contains(lotteryTicket.Compressed))
                {
                    if (_options.Endpoint != null)
                    {
                        var endPoint = new Endpoint(_options.Endpoint);
                        endPoint.Submit(lotteryTicket);
                    }

                    if (_options.File != null)
                    {
                        var file = new File(_options.File);
                        file.Submit(lotteryTicket);
                    }

                    _winningLotteryTickets.Add(lotteryTicket);
                }

                _threadSafeCounter.Increment();
            }
        }
    }
}