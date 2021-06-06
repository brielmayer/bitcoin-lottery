using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using BitcoinLottery.Exception;
using BitcoinLottery.Model;
using BitcoinLottery.Output;
using BitcoinLottery.Properties;
using CommandLine;

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
            // sanity check
            try
            {
                SanityCheck(options);
            } 
            catch(EndpointException e)
            {
                Console.WriteLine("Sanity check failed for endpoint {0}: {1}", options.Endpoint, e.Message);
                return;
            }
            catch (FileException e)
            {
                Console.WriteLine("Sanity check failed for file {0}: {1}", options.File, e.Message);
                return;
            }

            // init btc addresses
            Console.WriteLine("Initializing Bitcoin Lottery with {0} thread(s)...", options.Threads);
            var bitcoinAddressWithBalance = GetBitcoinAddressWithBalance(options.Dump);

            var threadSafeCounter = new ThreadSafeCounter();
            var winningLotteryTickets = new ConcurrentBag<LotteryTicket>();

            // run lottery
            for (var i = 0; i < options.Threads; i++)
            {
                var lottery = new Lottery(options, threadSafeCounter, winningLotteryTickets, bitcoinAddressWithBalance);
                new Thread(lottery.Run).Start();
            }

            // run watch dog
            var watchDog = new WatchDog(options, threadSafeCounter, winningLotteryTickets);
            new Thread(watchDog.Run).Start();
        }

        private static void SanityCheck(Options options)
        {
            // endpoint
            if (options.Endpoint != null)
            {
                var endPoint = new Endpoint(options.Endpoint);
                endPoint.SanityCheck();
            }

            // file
            if (options.File != null)
            {
                var file = new Output.File(options.File);
                file.SanityCheck();
            }
        }

        private static HashSet<string> GetBitcoinAddressWithBalance(string path)
        {
            var bitcoinAddressWithBalance = new HashSet<string>();
            using (var sr = System.IO.File.OpenText(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var columns = line.Split(',');
                    bitcoinAddressWithBalance.Add(columns[0]);
                }
            }

            return bitcoinAddressWithBalance;
        }
    }
}