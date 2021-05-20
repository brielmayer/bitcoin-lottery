using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using BitcoinLottery.Model;
using BitcoinLottery.Properties;

namespace BitcoinLottery
{
    internal sealed class WatchDog
    {
        private readonly Options _options;

        private readonly ThreadSafeCounter _threadSafeCounter;

        private readonly ConcurrentBag<LotteryTicket> _winningLotteryTickets;

        public WatchDog(Options options, ThreadSafeCounter threadSafeCounter, ConcurrentBag<LotteryTicket> winningLotteryTickets)
        {
            _options = options;
            _threadSafeCounter = threadSafeCounter;
            _winningLotteryTickets = winningLotteryTickets;
        }

        public void Run()
        {
            var stopWatch = Stopwatch.StartNew();
            while (true)
            {
                Console.Clear();
                Console.WriteLine(Resources.Banner);

                var ts = stopWatch.Elapsed;

                Console.WriteLine("Threads..........: {0}", _options.Threads);
                Console.WriteLine("Tickets./.Second.: {0}", _threadSafeCounter.Value());
                Console.WriteLine("Running..........: {0}", $"{ts.Days}d {ts.Hours:00}h {ts.Minutes:00}m {ts.Seconds:00}s");
                Console.WriteLine("Winning.Tickets..: {0}", _winningLotteryTickets.Count);
                foreach (var lotteryTicket in _winningLotteryTickets)
                {
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("WIF Private Key......: {0}", lotteryTicket.PrivateKey);
                    Console.WriteLine("Uncompressed Address.: {0}", lotteryTicket.Uncompressed);
                    Console.WriteLine("Compressed Address...: {0}", lotteryTicket.Compressed);
                    Console.WriteLine("-------------------------------------------------");
                }

                _threadSafeCounter.Reset();
                Thread.Sleep(1000);
            }
        }
    }
}