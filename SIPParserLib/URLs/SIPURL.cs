using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public  class SIPURL:URI
	{
		public override string Scheme
		{
			get => "sip";
		}

		public UserInfo UserInfo
		{
			get;
			private set;
		}
		public HostPort HostPort
		{
			get;
			private set;
		}
		public URLParameter[] Parameters
		{
			get;
			private set;
		}

		public Header[] Headers
		{
			get;
			private set;
		}
		public SIPURL(UserInfo UserInfo, HostPort HostPort, URLParameter[] Parameters, Header[] Headers)
		{
			if (Parameters == null) throw new ArgumentNullException(nameof(Parameters));
			if (Headers == null) throw new ArgumentNullException(nameof(Headers));

			this.UserInfo = UserInfo;this.HostPort = HostPort;this.Parameters = Parameters;this.Headers = Headers;
		}

		public override string ToString()
		{
			string headers, parameters;

			if (Parameters.Length == 0) parameters = "";
			else parameters = $";{string.Join(';', Parameters)}";
			if (Headers.Length == 0) headers = "";
			else headers = $"?{string.Join('&', Headers)}";

			return $"sip:{UserInfo}@{HostPort}{parameters}{headers}";
		}

	}
}
