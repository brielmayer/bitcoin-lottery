using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using BitcoinLottery.Model;
using BitcoinLottery.Properties;
using CommandLine;
using NBitcoin;

namespace BitcoinLottery
{
    internal class Options
    {
        [Option('d', "dump", Required = true, HelpText = "CSV dump file containing bitcoin addresses with balance")]
        public string Dump { get; set; }

        [Option('f', "file", Required = false, HelpText = "Save found addresses to a file")]
        public string File { get; set; }

        [Option('e', "endpoint", Required = false, HelpText = "Send found addresses to a web endpoint")]
        public string Endpoint { get; set; }

        [Option('t', "threads", Required = false, Default = 1, HelpText = "Number of threads")]
        public int Threads { get; set; }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(Resources.Banner);

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(Run);

            Console.Read();
        }

        private static void Run(Options options)
        {
            Console.WriteLine("Initializing Bitcoin Lottery with {0} thread(s)...", options.Threads);
            var bitcoinAddressWithBalance = GetBitcoinAddressWithBalance(options.Dump);

            var threadSafeCounter = new ThreadSafeCounter();

            var foundAddresses = new HashSet<FoundAddress>();

            // run lottery
            for (var i = 0; i < options.Threads; i++)
            {
                var lottery = new Lottery(options, threadSafeCounter, foundAddresses, bitcoinAddressWithBalance);
                new Thread(lottery.Run).Start();
            }

            // run watch dog
            var watchDog = new WatchDog(options, threadSafeCounter, foundAddresses);
            new Thread(watchDog.Run).Start();
        }

        private static HashSet<KeyId> GetBitcoinAddressWithBalance(string path)
        {
            var bitcoinAddressWithBalance = new HashSet<KeyId>();
            using (var sr = File.OpenText(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var columns = line.Split(',');
                    bitcoinAddressWithBalance.Add(new KeyId(columns[3]));
                }
            }

            return bitcoinAddressWithBalance;
        }
    }
}