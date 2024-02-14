using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public abstract class URI
	{
		public abstract string Scheme
		{
			get;
		}

		
		public URI()
		{
		}

		public abstract string? ToShortString();
		public abstract string? ToHumanString();


	}
}
