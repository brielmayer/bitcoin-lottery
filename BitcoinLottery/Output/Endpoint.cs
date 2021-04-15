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

        public async void Submit(FoundAddress foundAddress)
		{
			var values = new Dictionary<string, string>
			{
				{ "wif", foundAddress.Wif },
                { "h160", foundAddress.H160 },
				{ "address", foundAddress.Address },
            };

            var content = new FormUrlEncodedContent(values);
            await Client.PostAsync(_endPoint, content);
        }
	}
}
