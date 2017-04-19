using System;
using System.Runtime.Serialization;

namespace QuickGraph
{
    public class NoPathFoundException : QuickGraphException
    {

		public NoPathFoundException() {

		}

		public NoPathFoundException(string str) : base(str) {

		}

		public NoPathFoundException(string str, Exception e) : base (str, e) {

		}

		protected NoPathFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) {

		}
    }
}
