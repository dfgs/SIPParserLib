using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class ProxyAuthorizationHeader : MessageHeader<string>
	{
		public override string Name => "Proxy-Authorization";
		public ProxyAuthorizationHeader(string Value):base(Value)
		{
		}

	}
}
