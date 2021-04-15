using System.Threading;

namespace BitcoinLottery
{
    internal sealed class ThreadSafeCounter
    {
        private int _current;

        public int Increment()
        {
            return Interlocked.Increment(ref _current);
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
