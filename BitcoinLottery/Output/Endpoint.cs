using System.Collections.Generic;
using System.Net.Http;
using BitcoinLottery.Model;

namespace BitcoinLottery.Output
{
    internal class Endpoint : IOutput
    {
        private static readonly HttpClient Client = new HttpClient();

        private readonly string _endPoint;

        public Endpoint(string endPoint)
        {
            _endPoint = endPoint;
        }

        public async void Submit(LotteryTicket lotteryTicket)
        {
            var values = new Dictionary<string, string>
            {
                {"wif", lotteryTicket.PrivateKey},
                {"uncompressed", lotteryTicket.Uncompressed},
                {"compressed", lotteryTicket.Compressed},
            };

            var content = new FormUrlEncodedContent(values);
            await Client.PostAsync(_endPoint, content);
        }
    }
}