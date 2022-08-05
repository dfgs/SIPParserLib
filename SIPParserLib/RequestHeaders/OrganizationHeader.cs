using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class OrganizationHeader:RequestHeader<string>
	{
		public override string Name => "Organization";
		public OrganizationHeader(string Value):base(Value)
		{
		}

	}
}
