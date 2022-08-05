using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class MaxForwardsHeader : RequestHeader<string>
	{
		public override string Name => "Max-Forwards";
		public MaxForwardsHeader(string Value):base(Value)
		{
		}

	}
}
