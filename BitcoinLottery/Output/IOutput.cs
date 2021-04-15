using System;
using System.Collections.Generic;
using System.Text;
using BitcoinLottery.Model;

namespace BitcoinLottery.Output
{
    internal interface IOutput
	{
		void Submit(FoundAddress foundAddress);
	}
}
