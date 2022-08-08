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
	}
	public abstract class MessageHeader<T>:MessageHeader
	{
		
		public T Value
		{
			get;
		}

		public MessageHeader(T Value)
		{
			this.Value = Value;
		}

		public override string ToString()
		{
			return $"{Name}: {Value}";
		}


	}


}
