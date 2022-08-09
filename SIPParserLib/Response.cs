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

		

		

		public Response(StatusLine StatusLine, MessageHeader[] Headers, string Body) : base(Headers,Body)
		{
			this.StatusLine = StatusLine;
		}

	}


}
