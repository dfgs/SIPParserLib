using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class ContentEncodingHeader: RequestHeader<string>
	{
		public override string Name => "Content-Encoding";
		public ContentEncodingHeader(string Value):base(Value)
		{
		}

	}
}
