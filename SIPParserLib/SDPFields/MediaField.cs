using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class MediaField : SDPField
	{
		public override string Name => "m";

		public string Media
		{
			get;
			private set;
		}
		public ushort Port
		{
			get;
			private set;
		}
		public string Protocol
		{
			get;
			private set;
		}
		public string Format
		{
			get;
			private set;
		}

		public override string DisplayValue => $"{Media} {Port} {Protocol} {Format}";


		public MediaField(string Media,ushort Port,string Protocol,string Format)
		{
			this.Media = Media;this.Port = Port;this.Protocol = Protocol;this.Format = Format;
		}


	}
}
