using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class ServerHeader:MessageHeader<string>
	{
		public override string Name => "Server";
		public ServerHeader(string? Value):base(Value)
		{
		}

	}
}
