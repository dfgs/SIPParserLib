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
	public class MessageHeaderParser : ClassStringParser<MessageHeader>
	{
		private static Regex regex = new Regex(@"^(?<Name>[^:]+): *(?<Value>.+)?$");
		private static Regex viaRegex = new Regex(@"^(?<Value>[^;]+);(?<Parameters>.+)$");

		private IStructStringParser<Address> addressParser;
		private IClassStringParser<ViaParameter> viaParameterParser;

		public MessageHeaderParser(ILogger Logger,IStructStringParser<Address> AddressParser,IClassStringParser<ViaParameter> ViaParameterParser) : base(Logger)
		{
			AssertParameterNotNull(AddressParser, nameof(AddressParser), out addressParser);
			AssertParameterNotNull(ViaParameterParser, nameof(ViaParameterParser), out viaParameterParser);
		}

		public MessageHeaderParser(ILogger Logger) : this(Logger, new AddressParser(Logger), new ViaParameterParser(Logger) )
		{
		}

		protected override Regex OnGetRegex() => regex;


		protected override bool OnParse(Match Match, out MessageHeader? Result)
		{
			string name;
			string? value;
			Address? address;
			Match match;
			ViaParameter[]? viaParameters;

			LogEnter();

			Result = null;

			name = Match.Groups["Name"].Value;
			value = Match.Groups["Value"].MatchedValue();

			switch(name)
			{
				case "Accept":
					Result = new AcceptHeader(value);
					return true;
				case "Accept-Encoding":
					Result = new AcceptEncodingHeader(value);
					return true;
				case "Accept-Language":
					Result = new AcceptLanguageHeader(value);
					return true;
				case "Allow":
					Result = new AllowHeader(value);
					return true;
				case "Authorization":
					Result = new AuthorizationHeader(value);
					return true;
				case "Call-ID":
					Result = new CallIDHeader(value);
					return true;
				case "Contact":
					Result = new ContactHeader(value);
					return true;
				case "Content-Encoding":
					Result = new ContentEncodingHeader(value);
					return true;
				case "Content-Length":
					Result = new ContentLengthHeader(value);
					return true;
				case "Content-Type":
					Result = new ContentTypeHeader(value);
					return true;
				case "CSeq":
					Result = new CSeqHeader(value);
					return true;
				case "Date":
					Result = new DateHeader(value);
					return true;
				case "Encryption":
					Result = new EncryptionHeader(value);
					return true;
				case "Expires":
					Result = new ExpiresHeader(value);
					return true;
				case "From":
					if (!addressParser.Parse(value, out address, true)) return false;
					if (address == null) return false;
					Result = new FromHeader(address.Value);
					return true;
				case "Hide":
					Result = new HideHeader(value);
					return true;
				case "Max-Forwards":
					Result = new MaxForwardsHeader(value);
					return true;
				case "Organization":
					Result = new OrganizationHeader(value);
					return true;
				case "Priority":
					Result = new PriorityHeader(value);
					return true;
				case "Proxy-Authenticate":
					Result = new ProxyAuthenticateHeader(value);
					return true;
				case "Proxy-Authorization":
					Result = new ProxyAuthorizationHeader(value);
					return true;
				case "Proxy-Require":
					Result = new ProxyRequireHeader(value);
					return true;
				case "Record-Route":
					Result = new RecordRouteHeader(value);
					return true;
				case "Refer-To":
					if (!addressParser.Parse(value, out address, true)) return false;
					if (address == null) return false;
					Result = new ReferToHeader(address.Value);
					return true;
				case "Referred-By":
					if (!addressParser.Parse(value, out address, true)) return false;
					if (address == null) return false;
					Result = new ReferredByHeader(address.Value);
					return true;
				case "Require":
					Result = new RequireHeader(value);
					return true;
				case "Response-Key":
					Result = new ResponseKeyHeader(value);
					return true;
				case "Retry-After":
					Result = new RetryAfterHeader(value);
					return true;
				case "Route":
					Result = new RouteHeader(value);
					return true;
				case "Server":
					Result = new ServerHeader(value);
					return true;
				case "Subject":
					Result = new SubjectHeader(value);
					return true;
				case "Timestamp":
					Result = new TimestampHeader(value);
					return true;
				case "To":
					if (!addressParser.Parse(value, out address, true)) return false;
					if (address == null) return false;
					Result = new ToHeader(address.Value);
					return true;
				case "Unsupported":
					Result = new UnsupportedHeader(value);
					return true;
				case "User-Agent":
					Result = new UserAgentHeader(value);
					return true;
				case "Via":
					if (value == null) return false;
					match = viaRegex.Match(value);
					if (!match.Success) return false;
					if (!viaParameterParser.ParseAll(match.Groups["Parameters"],';', out viaParameters, true)) return false;
					if (viaParameters == null) return false;
					Result = new ViaHeader(match.Groups["Value"].Value,viaParameters);
					return true;//*/
				case "Warning":
					Result = new WarningHeader(value);
					return true;
				case "WWW-Authenticate":
					Result = new WWWAuthenticateHeader(value);
					return true;



				default:
					Result=new CustomHeader(name, value);
					return true;
			}
			

		}


	}
}
