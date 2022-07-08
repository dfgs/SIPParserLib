using System;
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

		public URI(UserInfo UserInfo)
		{
			this.UserInfo = UserInfo;
		}

	}
}
