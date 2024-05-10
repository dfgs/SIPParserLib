using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class HideHeader:MessageHeader<string>
	{
		public override string Name => "Hide";
		public HideHeader(string? Value):base(Value)
		{
		}

	}
}
