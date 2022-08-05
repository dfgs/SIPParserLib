using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class ResponseKeyHeader : RequestHeader<string>
	{
		public override string Name => "Response-Key";
		public ResponseKeyHeader(string Value):base(Value)
		{
		}

	}
}
