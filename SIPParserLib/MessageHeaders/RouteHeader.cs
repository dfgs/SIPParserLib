using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class RouteHeader:MessageHeader<string>
	{
		public override string Name => "Route";
		public RouteHeader(string Value):base(Value)
		{
		}

	}
}
