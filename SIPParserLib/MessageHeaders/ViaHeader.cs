using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class ViaHeader:MessageHeader<string>
	{
		public override string Name => "Via";

		public ViaParameter[] Parameters
		{
			get;
			private set;
		}

		public ViaHeader(string Value, ViaParameter[] Parameters) :base(Value)
		{
			this.Parameters = Parameters;
		}

		public T? GetParameter<T>()
			where T:ViaParameter
		{
			return Parameters.OfType<T>().FirstOrDefault();
		}
		public override string GetStringValue()
		{
			return $"{Value};{string.Join(';',(object[])Parameters)}";
		}

	}
}
