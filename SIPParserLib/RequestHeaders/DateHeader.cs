using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class DateHeader:RequestHeader<string>
	{
		public override string Name => "Date";
		public DateHeader(string Value):base(Value)
		{
		}

	}
}
