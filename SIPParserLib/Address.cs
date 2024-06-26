﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public struct Address
	{
		public string? DisplayName
		{
			get;
			private set;
		}
		public URI URI
		{
			get;
			private set;
		}
		public AddressParameter[]? Parameters
		{
			get;
			set;
		}
		/*public string? Tag
		{
			get;
			private set;
		}*/

		public Address(string? DisplayName,URI URI, AddressParameter[]? Parameters)
		{
			if (URI == null) throw new ArgumentNullException(nameof(URI));
			this.DisplayName = DisplayName;this.URI = URI;this.Parameters = Parameters;
		}

		public override string? ToString()
		{
			if ((DisplayName == null) || (DisplayName == ""))
			{
				if ((Parameters == null) || (Parameters.Length == 0)) return $"{URI}";
				else return $"<{URI}>;{string.Join(";", Parameters)}";
			}
			else
			{
				if ((Parameters == null) || (Parameters.Length == 0)) return $"\"{DisplayName}\" <{URI}>";
				else return $"\"{DisplayName}\" <{URI}>;{string.Join(";", Parameters)}";
				
			}
		}

		public string? ToShortString()
		{
			if ((DisplayName == null) || (DisplayName == "")) return URI.ToShortString();
			else return $"\"{DisplayName}\" <{URI.ToShortString()}>";
		}

		public  string? ToHumanString()
		{
			return string.IsNullOrEmpty(DisplayName) ? URI.ToHumanString():DisplayName;
		}

	}
}
