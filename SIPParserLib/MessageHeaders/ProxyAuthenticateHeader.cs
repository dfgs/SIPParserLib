using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class ProxyAuthenticateHeader : MessageHeader<string>
	{
		public override string Name => "Proxy-Authenticate";
		public ProxyAuthenticateHeader(string? Value):base(Value)
		{
		}

	}
}
