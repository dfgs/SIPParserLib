using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class ToHeader:MessageHeader<string>
	{
		public override string Name => "To";
		public ToHeader(string Value):base(Value)
		{
		}

	}
}
