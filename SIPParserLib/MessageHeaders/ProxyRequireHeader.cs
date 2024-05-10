using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class ProxyRequireHeader : MessageHeader<string>
	{
		public override string Name => "Proxy-Require";
		public ProxyRequireHeader(string? Value):base(Value)
		{
		}

	}
}
