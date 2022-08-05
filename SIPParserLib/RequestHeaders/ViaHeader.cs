using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class ViaHeader:RequestHeader<string>
	{
		public override string Name => "Via";
		public ViaHeader(string Value):base(Value)
		{
		}

	}
}
