using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class StatusLine
	{
		public string StatusCode
		{
			get;
			private set;
		}
		public string Reason
		{
			get;
			private set;
		}

		public StatusLine(string StatusCode, string Reason)
		{
			this.StatusCode = StatusCode; this.Reason = Reason;
		}

		public override string ToString()
		{
			return $"{StatusCode} {Reason}";
		}


	}


}
