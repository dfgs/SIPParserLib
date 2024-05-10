using LogLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIPParserLib.Parsers
{
	public class UserInfoParser : ClassStringParser<UserInfo>
	{
		private static Regex regex = new Regex(@"^(?<User>[^:]+)(:(?<Password>.*))?$");
		
		public UserInfoParser(ILogger Logger) : base(Logger)
		{
		}
		protected override Regex OnGetRegex() => regex;

		protected override bool OnParse(Match Match, out UserInfo? Result)
		{
			string user;
			string? password;

			LogEnter();

			user = Match.Groups["User"].Value;
			password = Match.Groups["Password"].MatchedValue();

			Result=new UserInfo(user, password);

			return true;
		}


	}
}
