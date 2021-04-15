using NBitcoin;
using System.Collections.Generic;
using BitcoinLottery.Model;
using BitcoinLottery.Output;

namespace BitcoinLottery
{
    internal class Lottery
    {
        private readonly Options _options;

        private readonly ThreadSafeCounter _threadSafeCounter;
            
        private readonly HashSet<FoundAddress> _foundAddresses;

        private readonly HashSet<KeyId> _bitcoinAddressToBalance;

        public Lottery(Options options, ThreadSafeCounter threadSafeCounter, HashSet<FoundAddress> foundAddresses, HashSet<KeyId> bitcoinAddressToBalance)
        {
            _options = options;
            _threadSafeCounter = threadSafeCounter;
            _foundAddresses = foundAddresses;
            _bitcoinAddressToBalance = bitcoinAddressToBalance;
        }

        public void Run()
        {
            while (true)
            {
                var key = new Key();
                var publicKeyHash = key.PubKey.Hash;
                if (_bitcoinAddressToBalance.Contains(publicKeyHash))
                {
                    var wif = key.GetWif(Network.Main).ToString();
                    var h160 = publicKeyHash.ToString();
                    var address = publicKeyHash.GetAddress(Network.Main).ToString();

                    var foundAddress = new FoundAddress(wif, h160, address);

                    if (_options.Endpoint != null)
                    {
                        var endPoint = new Endpoint(_options.Endpoint);
                        endPoint.Submit(foundAddress);
                    }

                    if (_options.File != null)
                    {
                        var file = new File(_options.File);
                        file.Submit(foundAddress);
                    }

                    _foundAddresses.Add(foundAddress);
                }

                _threadSafeCounter.Increment();
            }
        }
    }
}
