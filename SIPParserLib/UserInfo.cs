using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class UserInfo
	{
		public string User
		{
			get;
			set;
		}
		public string? Password
		{
			get;
			set;
		}

		
		public UserInfo(string User,string? Password)
		{
			this.User = User;this.Password = Password;
		}

		public override string ToString()
		{
			if (Password==null) return User;
			else return $"{User}:{Password}";
		}

	}
}
