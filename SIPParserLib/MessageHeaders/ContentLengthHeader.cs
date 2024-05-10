using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class ContentLengthHeader : MessageHeader<string>
	{
		public override string Name => "Content-Length";
		public ContentLengthHeader(string? Value):base(Value)
		{
		}

	}
}
