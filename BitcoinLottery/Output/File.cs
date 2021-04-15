using System.Net.Http;
using System.Text;
using BitcoinLottery.Model;

namespace BitcoinLottery.Output
{
    internal class File : IOutput
    {
        private readonly string _filePath;

        public File(string filePath)
        {
            _filePath = filePath;
        }

        public void Submit(FoundAddress foundAddress)
		{
			StringBuilder sb = new StringBuilder();
            sb.AppendLine("-------------------------------------------------");
            sb.AppendLine("WIF Private Key: " + foundAddress.Wif);
            sb.AppendLine("H160: " + foundAddress.H160);
            sb.AppendLine("BTC Address: " + foundAddress.Address);
            sb.AppendLine("-------------------------------------------------");

            System.IO.File.AppendAllText(_filePath, sb.ToString());
		}
	}
}
