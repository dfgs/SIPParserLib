using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public struct URIHeader
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

		public URIHeader(string Name, string Value)
		{
			this.Name = Name; this.Value = Value;
		}

		public override string ToString()
		{
			return $"{Name}={Value}";
		}


	}


}
