using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class ContentTypeHeader : MessageHeader<string>
	{
		public override string Name => "Content-Type";
		public ContentTypeHeader(string? Value):base(Value)
		{
		}

	}
}
