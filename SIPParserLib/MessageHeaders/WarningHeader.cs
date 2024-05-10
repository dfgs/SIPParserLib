using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class WarningHeader : MessageHeader<string>
	{
		public override string Name => "Warning";
		public WarningHeader(string? Value):base(Value)
		{
		}

	}
}
