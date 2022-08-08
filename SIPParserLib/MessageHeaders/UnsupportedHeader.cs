using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class UnsupportedHeader : MessageHeader<string>
	{
		public override string Name => "Unsupported";
		public UnsupportedHeader(string Value):base(Value)
		{
		}

	}
}
