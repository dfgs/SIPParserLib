using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class UserAgentHeader:RequestHeader<string>
	{
		public override string Name => "User-Agent";
		public UserAgentHeader(string Value):base(Value)
		{
		}

	}
}
