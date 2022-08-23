using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class CustomSDPField : SDPField
	{
		private char name;
		public override char Name
		{
			get => name;
		}

		public string Value
		{
			get;
			private set;
		}

		public CustomSDPField(char Name,string Value)
		{
			this.name = Name;this.Value = Value;
		}


	}
}
