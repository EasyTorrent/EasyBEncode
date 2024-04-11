using System;

namespace EasyBEncode
{
    /// <summary>
    /// describe：
    /// author：cxj
    /// date：2024/4/9 14:53:12
    /// </summary>
    public class BEString : BEValue
    {
        //[length]:[string]
        public string StringValue { get; set; } = string.Empty;

        public BEString()
        {
        }
        public BEString(string value)
        {
            StringValue = value;
        }

        public override void Decode(ref Span<char> buffer)
        {
            if (buffer.IsEmpty)
                throw new EasyBEncodeException("decode BEString error,encodeValue null or whitespace");
            var index = buffer.IndexOf(":");
            var len = int.Parse(buffer[..index]);
            var value = new string(buffer.Slice(index + 1, len));
            buffer = buffer[(index + 1 + len)..];
            if (value.Length != len)
                throw new EasyBEncodeException("decode BEString error,len not match");
            StringValue = value;           
        }

        public override string Encode()
        {
            return $"{StringValue.Length}:{StringValue}";
        }
    }
}
