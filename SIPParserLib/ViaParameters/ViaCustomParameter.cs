using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class ViaCustomParameter : ViaParameter<string?>
	{
		private string name;
		public override string Name => name;
		public ViaCustomParameter(string Name,string? Value) : base(Value)
		{
			this.name = Name;
		}

	}
}
