using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class ViaBranch : ViaParameter<string?>
	{
		public override string Name => "branch";
		public ViaBranch(string? Value) : base(Value)
		{
		}

	}
}
