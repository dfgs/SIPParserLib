using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public struct SDPAttribute
	{
		public string Name
		{
			get;
			private set;
		}

		public string? Value
		{
			get;
			private set;
		}

		public SDPAttribute(string Name,string? Value)
		{
			this.Name = Name;this.Value = Value; 
		}

	}
}
