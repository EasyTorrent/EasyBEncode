using System;
using System.Runtime.InteropServices;
using System.Text;

namespace EasyBEncode
{
    /// <summary>
    /// describe：
    /// author：cxj
    /// date：2024/4/9 14:53:12
    /// </summary>
    public class BEString : BEValue, IEquatable<BEString>, IComparable<BEString>
    {
        public string StringValue { get => Encoding.UTF8.GetString(TextBytes.Span); }

        public string HexStringValue { get => BitConverter.ToString(TextBytes.Span.ToArray()).Replace("-", "").ToLower(); }

        public ReadOnlyMemory<byte> TextBytes { get; private set; }
        public BEString()
        {
        }
        public BEString(string value)
        {
            TextBytes = new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(value));
        }
        public BEString(byte[] value)
        {
            TextBytes = new ReadOnlyMemory<byte>((byte[])value.Clone());
        }
        public override void Decode(ref Span<byte> buffer)
        {
            if (buffer.IsEmpty)
                throw new EasyBEncodeException("decode BEString error,encodeValue null or whitespace");
            var index = buffer.IndexOf(new byte[] { 0x3a });
            var len = int.Parse(Encoding.ASCII.GetString((buffer[..index])));
            var strBuffer = buffer.Slice(index + 1, len);
            TextBytes = new ReadOnlyMemory<byte>(strBuffer.ToArray());
            buffer = buffer[(index + 1 + len)..];
        }

        public bool Equals(BEString? other)
        {
            return other != null &&  TextBytes.Span.SequenceEqual(other.TextBytes.Span);
        }
        public override int GetHashCode()
        {
            if (TextBytes.Span.Length >= 4)
                return MemoryMarshal.Read<int>(TextBytes.Span);
            if (TextBytes.Span.Length > 0)
                return TextBytes.Span[0];
            return 0;
        }

        public override int LengthInBytes()
        {
            return TextBytes.Length.ToString().Length + 1 + TextBytes.Length;
        }

        public override void Encode(ref Span<byte> buffer, ref int index)
        {
            var lenStr = TextBytes.Length.ToString();

            foreach(var c in lenStr)
            {
                buffer[index] = (byte)c;
                index += 1;
            }
            buffer[index] = (byte)':';
            index += 1;
            foreach (var c in TextBytes.Span)
            {
                buffer[index] = c;
                index += 1;
            }
        }

        public int CompareTo(BEString other)
        {
            return other is null ? 1 : TextBytes.Span.SequenceCompareTo(other.TextBytes.Span);
        }
    }
}
