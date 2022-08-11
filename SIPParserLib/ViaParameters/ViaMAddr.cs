using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class ViaMAddr : ViaParameter<string>
	{
		public override string Name => "maddr";
		public ViaMAddr(string Value) : base(Value)
		{
		}

	}
}
