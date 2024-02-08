using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class StatusLine
	{
		public ushort StatusCode
		{
			get;
			private set;
		}
		public string Reason
		{
			get;
			private set;
		}

		public StatusLine(ushort StatusCode, string Reason)
		{
			this.StatusCode = StatusCode; this.Reason = Reason;
		}

		public override string ToString()
		{
			return $"{StatusCode} {Reason}";
		}


	}


}
