using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class PriorityHeader : MessageHeader<string>
	{
		public override string Name => "Priority";
		public PriorityHeader(string Value):base(Value)
		{
		}

	}
}
