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
		public IEnumerable<int> GetMediaIndices()
		{
			MatchCollection matches;
			MediaField? mediaField;

			mediaField = GetField<MediaField>();
			if (mediaField == null) yield break;
			matches= mediaListRegex.Matches(mediaField.Format);
			
			foreach(Match match in matches)
			{
				yield return int.Parse(match.Groups["Index"].Value);
			}
		}
		public string? GetCodec(int MediaIndex)
		{
			Match match;
			string index;

			index = MediaIndex.ToString();
			foreach(AttributeField attributeField in GetFields<AttributeField>().Where(item=>item.Value.Name=="rtpmap"))
			{
				match = codecRegex.Match(attributeField?.Value.Value??"");
				if (!match.Success) continue;
				if (match.Groups["Index"].Value == index) return match.Groups["Name"].Value;
			}
			return null;
		}



	}
}
