using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLottery.Exception
{
    class EndpointException : System.Exception
    {
        public EndpointException(string message) : base(message)
        {
        }
    }
}
