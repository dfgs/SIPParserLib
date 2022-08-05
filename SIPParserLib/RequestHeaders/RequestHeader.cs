using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public abstract class RequestHeader
	{
		public abstract string Name
		{
			get;
		}
	}
	public abstract class RequestHeader<T>:RequestHeader
	{
		
		public T Value
		{
			get;
		}

		public RequestHeader(T Value)
		{
			this.Value = Value;
		}

		public override string ToString()
		{
			return $"{Name}: {Value}";
		}


	}


}
