using System;

namespace EasyBEncode
{
    /// <summary>
    /// describe：
    /// author：cxj
    /// date：2024/4/9 14:54:44
    /// </summary>
    public class BENumber : BEValue
    {
        public int Value {  get; set; }
        public BENumber()
        {
            
        }
        public BENumber(int value)
        {
            Value = value;
            Encode();
        }        

        public override void Decode(ref Span<char> buffer)
        {
            if (buffer.IsEmpty)
                throw new EasyBEncodeException("decode BENumber error,encodeValue null or whitespace");
            if (!buffer.StartsWith("i"))
                throw new EasyBEncodeException($"decode BENumber error,{buffer.ToString()} invalid format");

            var endIndex = buffer.IndexOf('e');
            var number = new string(buffer[1..endIndex]);
            buffer = buffer[(endIndex + 1)..];

            if (!int.TryParse(number, out var value))
                throw new EasyBEncodeException($"decode BENumber error {number} not a number");
            
            Value = value;
        }

        public override string Encode()
        {
            return $"i{Value}e";
        }
    }
}
