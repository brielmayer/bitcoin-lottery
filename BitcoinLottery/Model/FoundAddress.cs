namespace BitcoinLottery.Model
{
    internal class FoundAddress
	{
        public FoundAddress(string wif, string h160, string address)
        {
            Wif = wif;
            H160 = h160;
            Address = address;
        }

        public string Wif { get; set; }

        public string H160 { get; set; }

        public string Address { get; set; }
    }
}
