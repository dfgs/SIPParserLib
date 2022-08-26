using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class SDP
	{
		private SDPField[] fields;
		public IEnumerable<SDPField> Fields
		{
			get => fields;
		}
		public int Count
		{
			get { return fields.Length; }
		}

		public SDP(SDPField[] Fields)
		{
			this.fields = Fields;
		}

		public T? GetField<T>()
			where T:SDPField
		{
			return fields.OfType<T>().FirstOrDefault();
		}

		public IEnumerable<T> GetFields<T>()
			where T : SDPField
		{
			return fields.OfType<T>();
		}


	}
}
