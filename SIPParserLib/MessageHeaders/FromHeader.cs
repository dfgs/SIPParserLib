using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class FromHeader:MessageHeader<Address>
	{
		public override string Name => "From";
		public FromHeader(Address Value):base(Value)
		{
		}

	}
}
