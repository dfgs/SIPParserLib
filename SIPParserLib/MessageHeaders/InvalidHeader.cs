using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class InvalidHeader:MessageHeader<string>
	{
		public override string Name => "Invalid header";
		public InvalidHeader(string? Value):base(Value)
		{
			
		}

	}
}
