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
	public class GenericClassParser<T> : ClassStringParser<T>
		where T:class
	{
		private static Regex regex = new Regex(@".*");
		private Func<string,T> converter;

		public GenericClassParser(ILogger Logger,Func<string, T> Converter) : base(Logger)
		{
			AssertParameterNotNull(Converter, nameof(Converter), out converter);
		}
		protected override IEnumerable<Regex> OnGetRegexes() => new Regex[] { regex };


		protected override bool OnParse(Regex Regex, Match Match, out T? Value)
		{
			string value;

			LogEnter();

			value = Match.Value;
			try
			{
				Value = converter(value);
			}
			catch(Exception ex)
			{
				Log(ex);
				Value = null;
				return false;
			}
			
			return true;
		}

		


	}
}
