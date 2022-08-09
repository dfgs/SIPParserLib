using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public abstract class SIPMessage
	{
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

		public SIPMessage(MessageHeader[] Headers,string Body)
		{
			this.Headers = Headers;
			this.Body = Body;
		}

		public T? GetHeader<T>()
			where T: MessageHeader
		{
			return Headers.OfType<T>().FirstOrDefault();
		}

	}



}
