using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class ExpiresHeader:MessageHeader<string>
	{
		public override string Name => "Expires";
		public ExpiresHeader(string? Value):base(Value)
		{
		}

	}
}
