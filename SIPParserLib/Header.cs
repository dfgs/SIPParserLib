using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public struct Header
	{
		public string Name
		{
			get;
			private set;
		}
		public string Value
		{
			get;
			private set;
		}

		public Header(string Name, string Value)
		{
			this.Name = Name; this.Value = Value;
		}

	}


}
