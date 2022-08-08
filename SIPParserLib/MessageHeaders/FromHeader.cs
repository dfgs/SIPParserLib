using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class FromHeader:MessageHeader<string>
	{
		public override string Name => "From";
		public FromHeader(string Value):base(Value)
		{
		}

	}
}
