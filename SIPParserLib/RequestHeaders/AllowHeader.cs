using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class AllowHeader:RequestHeader<string>
	{
		public override string Name => "Allow";
		public AllowHeader(string Value):base(Value)
		{
		}

	}
}
