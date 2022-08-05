using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class ContactHeader:RequestHeader<string>
	{
		public override string Name => "Contact";
		public ContactHeader(string Value):base(Value)
		{
		}

	}
}
