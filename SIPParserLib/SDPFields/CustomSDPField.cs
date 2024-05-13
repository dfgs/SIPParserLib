using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class CustomSDPField : SDPField
	{
		private string name;
		public override string Name
		{
			get => name;
		}

		public string Value
		{
			get;
			private set;
		}
		public override string DisplayValue =>Value?.ToString()??"";

		public CustomSDPField(string Name,string Value)
		{
			this.name = Name;this.Value = Value;
		}


	}
}
