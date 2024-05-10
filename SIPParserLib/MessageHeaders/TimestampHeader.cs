using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class TimestampHeader : MessageHeader<string>
	{
		public override string Name => "Timestamp";
		public TimestampHeader(string? Value):base(Value)
		{
		}

	}
}
