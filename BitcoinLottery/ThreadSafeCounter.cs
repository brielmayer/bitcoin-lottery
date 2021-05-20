using System.Threading;

namespace BitcoinLottery
{
    internal sealed class ThreadSafeCounter
    {
        private int _current;

        public int Increment(int val = 1)
        {
            return Interlocked.Add(ref _current, val);
        }

        public void Reset()
        {
            Interlocked.Exchange(ref _current, 0);
        }

        public int Value()
        {
            return _current;
        }
    }
}