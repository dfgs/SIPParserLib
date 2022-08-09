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


		public Request(RequestLine RequestLine, MessageHeader[] Headers,string Body):base(Headers,Body)
		{
			this.RequestLine = RequestLine;
		}

	}
}
