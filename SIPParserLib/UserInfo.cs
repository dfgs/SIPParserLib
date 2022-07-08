using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public struct UserInfo
	{
		public string User
		{
			get;
			private set;
		}
		public string? Password
		{
			get;
			private set;
		}

		public UserInfo(string User,string? Password)
		{
			this.User = User;this.Password = Password;
		}

	}
}
