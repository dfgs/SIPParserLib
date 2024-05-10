using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class WWWAuthenticateHeader : MessageHeader<string>
	{
		public override string Name => "WWW-Authenticate";
		public WWWAuthenticateHeader(string? Value):base(Value)
		{
		}

	}
}
