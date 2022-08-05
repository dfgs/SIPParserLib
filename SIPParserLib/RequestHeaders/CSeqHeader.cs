using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class CSeqHeader:RequestHeader<string>
	{
		public override string Name => "CSeq";
		public CSeqHeader(string Value):base(Value)
		{
		}

	}
}
