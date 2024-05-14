using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib.Parsers
{
	public class LineReader 
	{
		private const int bufferMaxSize = 16384;
		private Stream stream;
		private char[] buffer;
		private int bufferContentSize;
		private int bufferPosition;

		public bool EndOfStream
		{
			get => (bufferPosition == bufferContentSize) && (stream.Position == stream.Length);
		}
		public LineReader(Stream Stream)
		{
			if (Stream == null) throw new ArgumentNullException(nameof(Stream));
			this.stream = Stream;
			bufferContentSize = 0;
			bufferPosition = 0;
			buffer = new char[0];
		}

		private void FillBuffer()
		{
			byte[] tmpbuffer;

			tmpbuffer = new byte[bufferMaxSize];
			
			bufferPosition = 0;
			bufferContentSize=stream.Read(tmpbuffer, 0, bufferMaxSize);
			buffer=Encoding.UTF8.GetChars(tmpbuffer,0,bufferContentSize);
			bufferContentSize = buffer.Length;
		}
		public string? ReadLine()
		{
			StringBuilder? result;
			char c;

			result = null;
			do
			{
				if (bufferPosition == bufferContentSize)
				{
					if (stream.Position == stream.Length) return result?.ToString();
					FillBuffer();
				}

				c = buffer[bufferPosition++];
				if (result == null) result = new StringBuilder();
				if (c == '\n') return result?.ToString();
				if (c != '\r') result.Append(c);
			} while (true);

		}

	}
}
