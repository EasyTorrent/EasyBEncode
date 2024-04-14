using System;
using System.Text;

namespace EasyBEncode
{
    /// <summary>
    /// describe：
    /// author：cxj
    /// date：2024/4/9 14:54:44
    /// </summary>
    public class BENumber : BEValue, IComparable<BENumber>
    {
        public long Value {  get; set; }
        public BENumber()
        {
            
        }
        public BENumber(int value)
        {
            Value = value;
        }        

        public override void Decode(ref Span<byte> buffer)
        {
            if (buffer.IsEmpty)
                throw new EasyBEncodeException("decode BENumber error,encodeValue null or whitespace");
            if (buffer[0] != 0x69)
                throw new EasyBEncodeException($"decode BENumber error,{buffer.ToString()} invalid format");

            var endIndex = buffer.IndexOf(new byte[] { 0x65 });
            var number = Encoding.ASCII.GetString(buffer[1..endIndex]);
            buffer = buffer[(endIndex + 1)..];

            if (!long.TryParse(number, out var value))
                throw new EasyBEncodeException($"decode BENumber error {number} not a number");
            
            Value = value;
        }

        public override bool Equals(object obj)
        {
            return obj is BENumber number && number.Value == Value;
        }
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override int LengthInBytes()
        {
            return 2 + Value.ToString().Length;
        }

        public override void Encode(ref Span<byte> buffer, ref int index)
        {
            buffer[index] = (byte)'i';
            index += 1;
            var numberStr = Value.ToString();
            foreach(var c in numberStr)
            {
                buffer[index] = (byte)c;
                index += 1;
            }
            buffer[index] = (byte)'e';
            index += 1;
        }

        public int CompareTo(BENumber other)
        {
            return other == null ? 1 : Value.CompareTo(other.Value);
        }
    }
}
