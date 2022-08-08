using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class Response:SIPMessage
	{

		public StatusLine StatusLine
		{
			get;
			private set;
		}

		public MessageHeader[] Headers
		{
			get;
			private set;
		}

		public string Body
		{
			get;
			private set;
		}

		public Response(StatusLine StatusLine, MessageHeader[] Headers, string Body)
		{
			this.StatusLine = StatusLine;
			this.Headers = Headers;
			this.Body = Body;
		}

	}


}
