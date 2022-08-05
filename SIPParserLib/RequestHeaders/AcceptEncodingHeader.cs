using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class AcceptEncodingHeader:RequestHeader<string>
	{
		public override string Name => "Accept-Encoding";
		public AcceptEncodingHeader(string Value):base(Value)
		{
		}

	}
}
