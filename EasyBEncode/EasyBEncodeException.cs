using System;
using System.Runtime.Serialization;

namespace EasyBEncode
{
    /// <summary>
    /// describe：
    /// author：cxj
    /// date：2024/4/9 15:40:51
    /// </summary>
    public class EasyBEncodeException : Exception
    {
        public EasyBEncodeException()
        {
        }

        public EasyBEncodeException(string message) : base(message)
        {
        }

        public EasyBEncodeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EasyBEncodeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
