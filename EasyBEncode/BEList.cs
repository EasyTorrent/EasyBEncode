using System;
using System.Collections.Generic;
using System.Text;

namespace EasyBEncode
{
    /// <summary>
    /// describe：
    /// author：cxj
    /// date：2024/4/9 14:54:59
    /// </summary>
    public class BEList : BEValue
    {
        public BEList()
        {
                
        }
        public BEList(List<BEValue> listValue)
        {
            ListValue.Clear();
            ListValue.AddRange(listValue);           
        }

       

        public List<BEValue> ListValue { get; set; } = new List<BEValue>();
        public override void Decode(ref Span<char> buffer)
        {
            if (buffer.IsEmpty)
                throw new EasyBEncodeException("decode BEList error,encodeValue null or whitespace");
            if (!buffer.StartsWith("l") )
                throw new EasyBEncodeException($"decode BEList error, invalid format");
            buffer = buffer[1..];
            
            while (!buffer.IsEmpty && buffer[0] != 'e' )
            {
                if (char.IsDigit(buffer[0]))
                {
                    var beString = new BEString();
                    beString.Decode(ref buffer);
                    ListValue.Add(beString);
                }
                else if (buffer[0] == 'd')
                {
                    var beDictionary = new BEDictionary();
                    beDictionary.Decode(ref buffer);
                    ListValue.Add(beDictionary);
                }
                else if (buffer[0] == 'l')
                {                    
                    var beList = new BEList();
                    beList.Decode(ref buffer);
                    ListValue.Add(beList);
                }
                else if (buffer[0] == 'i')
                {
                    var beNumber = new BENumber();
                    beNumber.Decode(ref buffer);
                    ListValue.Add(beNumber);
                }
                else
                {
                    new EasyBEncodeException($"decode BEList error,invalid data");
                }                
            }
            if (!buffer.IsEmpty)
            {
                buffer = buffer[1..];
            }
        }

        public override string Encode()
        {
            var sb = new StringBuilder();
            sb.Append("l");           
            ListValue.ForEach(p =>
            {
                sb.Append(p.Encode());
            });
            sb.Append("e");
            return sb.ToString();
        }
    }
}
