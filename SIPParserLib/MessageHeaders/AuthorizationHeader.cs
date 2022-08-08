using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class AuthorizationHeader : MessageHeader<string>
	{
		public override string Name => "Authorization";
		public AuthorizationHeader(string Value):base(Value)
		{
		}

	}
}
