﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public struct URI
	{
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
		public URI(UserInfo UserInfo, HostPort HostPort, URLParameter[] Parameters, Header[] Headers)
		{
			if (Parameters == null) throw new ArgumentNullException(nameof(Parameters));
			if (Headers == null) throw new ArgumentNullException(nameof(Headers));

			this.UserInfo = UserInfo;this.HostPort = HostPort;this.Parameters = Parameters;this.Headers = Headers;
		}

	}
}