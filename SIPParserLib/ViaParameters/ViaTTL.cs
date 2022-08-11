using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class ViaTTL : ViaParameter<byte>
	{
		public override string Name => "ttl";
		public ViaTTL(byte Value) : base(Value)
		{
		}

	}
}
