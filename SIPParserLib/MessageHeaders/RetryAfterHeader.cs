using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class RetryAfterHeader : MessageHeader<string>
	{
		public override string Name => "Retry-After";
		public RetryAfterHeader(string? Value):base(Value)
		{
		}

	}
}
