using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public  class TELURL:URI
	{
		public override string Scheme
		{
			get => "tel";
		}

		public string PhoneNumber
		{
			get;
			private set;
		}
		
		
		public TELURL(string PhoneNumber)
		{
			if (PhoneNumber == null) throw new ArgumentNullException(nameof(PhoneNumber));
			this.PhoneNumber = PhoneNumber;
		}

		public override string ToString()
		{
			return $"tel:{PhoneNumber}";
		}

		public override string? ToShortString()
		{
			return $"tel:{PhoneNumber}";
		}
		public override string? ToHumanString()
		{
			return PhoneNumber;
		}
	}
}
