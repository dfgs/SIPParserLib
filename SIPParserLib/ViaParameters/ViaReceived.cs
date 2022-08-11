using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class ViaReceived : ViaParameter<HostPort>
	{
		public override string Name => "received";
		public ViaReceived(HostPort Value) : base(Value)
		{
		}

	}
}
