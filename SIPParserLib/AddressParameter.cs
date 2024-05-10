using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public struct AddressParameter
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

		public AddressParameter(string Name,string? Value)
		{
			this.Name = Name;this.Value = Value;
		}
		public override string ToString()
		{
			if (Value == null) return Name;
			if (Value.ToString() == "") return Name;
			return $"{Name}={Value}";
		}

	}
}
