using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLottery.Exception
{
    class FileException : System.Exception
    {
        public FileException(string message) : base(message)
        {
        }
    }
}
