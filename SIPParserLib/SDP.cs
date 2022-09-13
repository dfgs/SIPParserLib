using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class SDP
	{
		private Regex mediaListRegex = new Regex(@"(?<Index>\d+) *");
		private Regex codecRegex = new Regex(@"(?<Index>\d+) (?<Name>.*)");

		private SDPField[] fields;
		public IEnumerable<SDPField> Fields
		{
			get => fields;
		}
		public int Count
		{
			get { return fields.Length; }
		}

		public SDP(SDPField[] Fields)
		{
			this.fields = Fields;
		}

		public T? GetField<T>()
			where T:SDPField
		{
			return fields.OfType<T>().FirstOrDefault();
		}

		public IEnumerable<T> GetFields<T>()
			where T : SDPField
		{
			return fields.OfType<T>();
		}
		public IEnumerable<string> GetRTPCodecs()
		{
			MatchCollection matches;
			MediaField? mediaField;
			Match codecMatch;

			string index;

			mediaField = GetField<MediaField>();
			if (mediaField == null) yield break;
			matches= mediaListRegex.Matches(mediaField.Format);
			
			foreach(Match indexMatch in matches)
			{
				index= indexMatch.Groups["Index"].Value;

				foreach (AttributeField attributeField in GetFields<AttributeField>().Where(item => item.Value.Name == "rtpmap"))
				{
					codecMatch = codecRegex.Match(attributeField?.Value.Value ?? "");
					if (!codecMatch.Success) continue;
					if (codecMatch.Groups["Index"].Value == index) yield return codecMatch.Groups["Name"].Value;
				}
			}
		}
		public string? GetCodec()
		{
			MediaField? mediaField;


			mediaField = GetField<MediaField>();
			if (mediaField == null) return null;

			switch (mediaField.Protocol)
			{
				case "udptl":return mediaField.Format;
				case "RTP/AVP": return GetRTPCodecs().FirstOrDefault();
				default:return mediaField.Format;
			}
			
		}



	}
}
