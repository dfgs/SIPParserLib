using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class RequireHeader : MessageHeader<string>
	{
		public override string Name => "Require";
		public RequireHeader(string? Value):base(Value)
		{
		}

	}
}
