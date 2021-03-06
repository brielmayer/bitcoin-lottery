using System.Text;
using BitcoinLottery.Exception;
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

        public void SanityCheck()
        {
            using var fileStream = System.IO.File.OpenWrite(_filePath);
            if (!fileStream.CanWrite)
            {
                throw new FileException("eN_Pilz File");
            }
        }

        public void Submit(LotteryTicket lotteryTicket)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-------------------------------------------------");
            sb.AppendLine("WIF Private Key: " + lotteryTicket.PrivateKey);
            sb.AppendLine("Uncompressed Address: " + lotteryTicket.Uncompressed);
            sb.AppendLine("Compressed Address: " + lotteryTicket.Compressed);
            sb.AppendLine("-------------------------------------------------");

            System.IO.File.AppendAllText(_filePath, sb.ToString());
        }
    }
}