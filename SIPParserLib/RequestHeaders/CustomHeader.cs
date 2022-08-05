using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class CustomHeader:RequestHeader<string>
	{
		private string name;
		public override string Name => name;
		public CustomHeader(string Name,string Value):base(Value)
		{
			this.name = Name;
		}

	}
}
