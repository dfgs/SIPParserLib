using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class ReferredByHeader:MessageHeader<Address>
	{
		public override string Name => "Referred-By";
		public ReferredByHeader(Address Value):base(Value)
		{
		}

	}
}
