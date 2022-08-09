using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class ToHeader:MessageHeader<Address>
	{
		public override string Name => "To";
		public ToHeader(Address Value):base(Value)
		{
		}

	}
}
