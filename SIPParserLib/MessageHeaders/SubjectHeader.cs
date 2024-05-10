using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class SubjectHeader:MessageHeader<string>
	{
		public override string Name => "Subject";
		public SubjectHeader(string? Value):base(Value)
		{
		}

	}
}
