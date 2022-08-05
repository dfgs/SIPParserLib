using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class CallIDHeader:RequestHeader<string>
	{
		public override string Name => "Call-ID";
		public CallIDHeader(string Value):base(Value)
		{
		}

	}
}
