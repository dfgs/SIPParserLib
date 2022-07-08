using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public struct HostPort
	{
		public string Host
		{
			get;
			private set;
		}
		public ushort Port
		{
			get;
			private set;
		}

		public HostPort(string Host,ushort Port)
		{
			this.Host = Host;this.Port = Port;
		}

	}
}
