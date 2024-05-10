using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public abstract class MessageHeader
	{
		public abstract string Name
		{
			get;
		}

		public abstract string GetStringValue();
	}
	public abstract class MessageHeader<T>:MessageHeader
	{
		
		public T? Value
		{
			get;
		}

		public MessageHeader(T? Value)
		{
			this.Value = Value;
		}

		public override string ToString()
		{
			if (Value == null) return Name;
			if (Value.ToString() == "") return Name;			
			return $"{Name}: {Value}";
		}

		public override string GetStringValue()
		{
			return Value?.ToString()??"";
		}

	}


}
