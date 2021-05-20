using System;
using NBitcoin;

namespace BitcoinLottery.Model
{
    public class LotteryTicket
    {
        public readonly string PrivateKey;
        public readonly string Compressed;
        public readonly string Uncompressed;

        public LotteryTicket(uint256 key) : this(key.ToBytes(false)) 
        { 
        }

        public LotteryTicket(byte[] data)
        {
            var compressedKey = new Key(data, -1, true);
            var uncompressedKey = new Key(data, -1, false);

            PrivateKey = uncompressedKey.GetWif(Network.Main).ToString();
            Uncompressed = uncompressedKey.PubKey.GetAddress(ScriptPubKeyType.Legacy, Network.Main).ToString();
            Compressed = compressedKey.PubKey.GetAddress(ScriptPubKeyType.Legacy, Network.Main).ToString();
        }

        public LotteryTicket(string privateKey, string compressed, string uncompressed)
        {
            PrivateKey = privateKey;
            Compressed = compressed;
            Uncompressed = uncompressed;
        }

        private bool Equals(LotteryTicket other)
        {
            return PrivateKey == other.PrivateKey && Compressed == other.Compressed && Uncompressed == other.Uncompressed;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((LotteryTicket) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PrivateKey, Compressed, Uncompressed);
        }
    }
}
