using System;
using System.Collections.Generic;
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

        private readonly HashSet<FoundAddress> _foundAddresses;

        public WatchDog(Options options, ThreadSafeCounter threadSafeCounter, HashSet<FoundAddress> foundAddresses)
        {
            _options = options;
            _threadSafeCounter = threadSafeCounter;
            _foundAddresses = foundAddresses;
        }

        public void Run()
        {
            var stopWatch = Stopwatch.StartNew();
            while (true)
            {
                Console.Clear();
                Console.WriteLine(Resources.Banner);

                var ts = stopWatch.Elapsed;

                Console.WriteLine("Threads.........: {0}", _options.Threads);
                Console.WriteLine("Keys./.Second...: {0}", _threadSafeCounter.Value());
                Console.WriteLine("Running.........: {0}", $"{ts.Days}d {ts.Hours:00}h {ts.Minutes:00}m {ts.Seconds:00}s");
                Console.WriteLine("Found.Addresses.: {0}", _foundAddresses.Count);
                foreach (var foundAddress in _foundAddresses)
                {
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("WIF Private Key.: {0}", foundAddress.Wif);
                    Console.WriteLine("H160............: {0}", foundAddress.H160);
                    Console.WriteLine("BTC Address.....: {0}", foundAddress.Address);
                    Console.WriteLine("-------------------------------------------------");
                }

                _threadSafeCounter.Reset();
                Thread.Sleep(1000);
            }
        }

    }
}
