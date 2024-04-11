using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBEncode
{
    /// <summary>
    /// describe：
    /// author：cxj
    /// date：2024/4/9 14:55:58
    /// </summary>
    public class BEDictionary : BEValue
    {

        public BEDictionary() 
        {
            
        }

        public BEDictionary(Dictionary<BEString, BEValue> dictionaryValue)
        {
            
        }

        public Dictionary<BEString, BEValue> DictionaryValue { get; set; } = new Dictionary<BEString, BEValue>();

        public override void Decode(ref Span<char> buffer)
        {
            if (buffer.IsEmpty) 
                throw new EasyBEncodeException("decode BEDictionary error,encodeValue null or whitespace");
            if (!buffer.StartsWith("d"))
                throw new EasyBEncodeException($"decode BEDictionary error,invalid format");

            buffer[0] = 'l';
            var beList = new BEList();
            beList.Decode(ref buffer);
            if (beList.ListValue.Count % 2 != 0)
                throw new EasyBEncodeException($"decode BEDictionary error, invalid data");
            var keys = beList.ListValue.Where(p => beList.ListValue.IndexOf(p) % 2 == 0 && p is BEString).Select(p => p as BEString).ToArray();
            var values = beList.ListValue.Where(p => beList.ListValue.IndexOf(p) % 2 != 0).ToArray();
            if (keys.Count() != values.Count())
                throw new EasyBEncodeException($"decode BEDictionary error,invalid data");
            for (int i = 0; i < keys.Count(); i++)
            {
                DictionaryValue.Add(keys[i], values[i]);
            }
        }

        public override string Encode()
        {
            var sb = new StringBuilder();
            sb.Append("d");
            foreach (var kv in DictionaryValue)
            {
                sb.Append(kv.Key.Encode()).Append(kv.Value.Encode());
            }
            sb.Append("e");
            return sb.ToString();
        }
    }
}
