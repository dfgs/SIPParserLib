using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class AcceptHeader:RequestHeader<string>
	{
		public override string Name => "Accept";
		public AcceptHeader(string Value):base(Value)
		{
		}

	}
}
