using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class Request:SIPMessage
	{
		public RequestLine RequestLine
		{
			get;
			private set;
		}

		public RequestHeader[] Headers
		{
			get;
			private set;
		}

		public Request(RequestLine RequestLine, RequestHeader[] Headers)
		{
			this.RequestLine = RequestLine;
			this.Headers = Headers;
		}

	}
}
