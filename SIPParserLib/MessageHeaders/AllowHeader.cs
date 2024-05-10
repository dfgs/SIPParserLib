using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class AllowHeader:MessageHeader<string>
	{
		public override string Name => "Allow";
		public AllowHeader(string? Value):base(Value)
		{
		}

	}
}
