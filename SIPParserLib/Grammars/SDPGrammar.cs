using ParserLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public static class SDPGrammar
	{
		public static ISingleParser<string> EOL = Parse.String("\r\n");

		public static ISingleParser<SDPField> VersionField =
			from name in Parse.Char('v')
			from _ in Parse.Char('=')
			from value in Parse.Byte()
			from __ in EOL
			select new VersionField(value);
		
		public static ISingleParser<SDPField> OriginField =
			from name in Parse.Char('o')
			from _ in Parse.Char('=')
			from userName in Parse.Except(' ').OneOrMoreTimes().ToStringParser()
			from __ in Parse.Char(' ')
			from sessionID in Parse.Except(' ').OneOrMoreTimes().ToStringParser()
			from ___ in Parse.Char(' ')
			from sessionVersion in Parse.Except(' ').OneOrMoreTimes().ToStringParser()
			from ____ in Parse.Char(' ')
			from networkType in Parse.Except(' ').OneOrMoreTimes().ToStringParser()
			from _____ in Parse.Char(' ')
			from addressType in Parse.Except(' ').OneOrMoreTimes().ToStringParser()
			from ______ in Parse.Char(' ')
			from address in Parse.Except('\r').OneOrMoreTimes().ToStringParser()
			from _______ in EOL
			select new OriginField(userName,sessionID,sessionVersion,networkType,addressType,address);

		public static ISingleParser<SessionNameField> SessionNameField =
			from name in Parse.Char('s')
			from _ in Parse.Char('=')
			from value in Parse.Except('\r').OneOrMoreTimes().ToStringParser()
			from __ in EOL
			select new SessionNameField(value);

		public static ISingleParser<SessionInformationField> SessionInformationField =
			from name in Parse.Char('i')
			from _ in Parse.Char('=')
			from value in Parse.Except('\r').OneOrMoreTimes().ToStringParser()
			from __ in EOL
			select new SessionInformationField(value);

		public static ISingleParser<SDPField> ConnectionField =
			from name in Parse.Char('c')
			from _ in Parse.Char('=')
			from networkType in Parse.Except(' ').OneOrMoreTimes().ToStringParser()
			from _____ in Parse.Char(' ')
			from addressType in Parse.Except(' ').OneOrMoreTimes().ToStringParser()
			from ______ in Parse.Char(' ')
			from address in Parse.Except('\r').OneOrMoreTimes().ToStringParser()
			from _______ in EOL
			select new ConnectionField(networkType, addressType, address);
		
		public static ISingleParser<TimingField> TimingField =
			from name in Parse.Char('t')
			from _ in Parse.Char('=')
			from startTime in Parse.Digit().OneOrMoreTimes().ToStringParser()
			from _____ in Parse.Char(' ')
			from stopTime in Parse.Digit().OneOrMoreTimes().ToStringParser()
			from _______ in EOL
			select new TimingField(UInt32.Parse(startTime), UInt32.Parse(stopTime));


		public static ISingleParser<MediaField> MediaField =
			from name in Parse.Char('m')
			from _ in Parse.Char('=')
			from media in Parse.Except(' ').OneOrMoreTimes().ToStringParser()
			from __ in Parse.Char(' ')
			from port in Parse.Digit().OneOrMoreTimes().ToStringParser()
			from ___ in Parse.Char(' ')
			from protocol in Parse.Except(' ').OneOrMoreTimes().ToStringParser()
			from ____ in Parse.Char(' ')
			from format in Parse.Except('\r').OneOrMoreTimes().ToStringParser()
			from _______ in EOL
			select new MediaField(media,UInt16.Parse(port),protocol,format);

		public static ISingleParser<SDPAttribute> Attribute =
			from name in Parse.Except(':', '\r').OneOrMoreTimes().ToStringParser()
			from value in Parse.ZeroOrOneTime(from _ in Parse.Char(':') from value in Parse.Except('\r').OneOrMoreTimes().ToStringParser() select value)
			select new SDPAttribute(name, value.FirstOrDefault());
		
		public static ISingleParser<AttributeField> AttributeField =
			from name in Parse.Char('a')
			from _ in Parse.Char('=')
			from value in Attribute
			from __ in EOL
			select new AttributeField(value);

		public static ISingleParser<SDPField> CustomSDPField =
			from name in Parse.Any()
			from _ in Parse.Char('=')
			from value in Parse.Except('\r').ReaderIncludes(' ').OneOrMoreTimes().ToStringParser()
			from __ in EOL
			select new CustomSDPField(name.ToString(), value);




		public static ISingleParser<SDPField> SDPField = VersionField.Or(OriginField).Or(SessionNameField).Or(SessionInformationField).Or(ConnectionField).Or(TimingField).Or(MediaField).Or(AttributeField) .Or(CustomSDPField);


		public static ISingleParser<SDP> SDP= from fields in SDPField.OneOrMoreTimes() select new SDP(fields.ToArray());


	}
}
