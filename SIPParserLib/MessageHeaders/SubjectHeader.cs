using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class SubjetHeader:MessageHeader<string>
	{
		public override string Name => "Subject";
		public SubjetHeader(string Value):base(Value)
		{
		}

	}
}
