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
		protected override IEnumerable<Regex> OnGetRegexes() => new Regex[] { regex };

		protected override bool OnParse(Regex Regex, Match Match, out UserInfo? Result)
		{
			string? user;
			string? password;

			LogEnter();

			Result = null;

			user = SIPString.Unescape(Match.Groups["User"].Value);
			if (user == null) return false;

			password = Match.Groups["Password"].MatchedValue();
			Result=new UserInfo(user, password);

			return true;
		}


	}
}
