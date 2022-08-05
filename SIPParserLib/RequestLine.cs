using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public struct RequestLine
	{
		public string Method
		{
			get;
			private set;
		}
		public URI RequestURI
		{
			get;
			private set;
		}
		public string SIPVersion
		{
			get;
			private set;
		}

		public RequestLine(string Method,URI RequestURI, string SIPVersion)
		{
			this.Method = Method;this.RequestURI = RequestURI; this.SIPVersion= SIPVersion;
		}

	}
}
