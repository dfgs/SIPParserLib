using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class ReferToHeader:MessageHeader<Address>
	{
		public override string Name => "Refer-To";
		public ReferToHeader(Address Value):base(Value)
		{
		}

	}
}
