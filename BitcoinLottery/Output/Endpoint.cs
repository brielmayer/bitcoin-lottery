using System;
using System.Collections.Generic;
using System.Net.Http;
using BitcoinLottery.Exception;
using BitcoinLottery.Model;

namespace BitcoinLottery.Output
{
    public class Endpoint : IOutput
    {
        private static readonly HttpClient Client = new HttpClient();

        private readonly string _endPoint;

        public Endpoint(string endPoint)
        {
            _endPoint = endPoint;
        }
        
        public void SanityCheck()
        {
            string body = Client.GetAsync(_endPoint).Result.Content.ReadAsStringAsync().Result;
            if(body != "ok")
            {
                throw new EndpointException("eN_Pilz");
            }
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