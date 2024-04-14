using System;
using System.Collections;
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
    public class BEDictionary : BEValue, IDictionary<BEString, BEValue>
    {

        public BEDictionary() 
        {
        }

        public BEDictionary(SortedDictionary<BEString, BEValue> dictionaryValue)
        {
            DictionaryValue = dictionaryValue;
        }

        public BEValue this[BEString key] 
        { 
            get => DictionaryValue[key]; 
            set => DictionaryValue[key] = value;
        }

        public SortedDictionary<BEString, BEValue> DictionaryValue { get; set; } = new SortedDictionary<BEString, BEValue>();

        public ICollection<BEString> Keys => DictionaryValue.Keys;

        public ICollection<BEValue> Values => DictionaryValue.Values;

        public int Count => DictionaryValue.Count;

        public bool IsReadOnly => false;

        public void Add(BEString key, BEValue value)
        {
            DictionaryValue.Add(key, value);
        }

        public void Add(KeyValuePair<BEString, BEValue> item)
        {
            DictionaryValue.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            DictionaryValue.Clear();
        }

        public bool Contains(KeyValuePair<BEString, BEValue> item)
        {
            if (!DictionaryValue.ContainsKey(item.Key))
                return false;
            return DictionaryValue[item.Key].Equals(item.Value);
        }

        public bool ContainsKey(BEString key)
        {
            return DictionaryValue.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<BEString, BEValue>[] array, int arrayIndex)
        {
            foreach (var item in DictionaryValue)
                array[arrayIndex++] = new KeyValuePair<BEString, BEValue>(item.Key, item.Value);
        }

        public override void Decode(ref Span<byte> buffer)
        {
            if (buffer.IsEmpty) 
                throw new EasyBEncodeException("decode BEDictionary error,encodeValue null or whitespace");
            if (buffer[0] != 0x64)
                throw new EasyBEncodeException($"decode BEDictionary error,invalid format");

            buffer[0] = 0x6c;
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

        
        public override void Encode(ref Span<byte> buffer, ref int index)
        {
            buffer[index] = (byte)'d';
            index += 1;
            foreach (var kv in DictionaryValue)
            {
                kv.Key.Encode(ref buffer, ref index);
                kv.Value.Encode(ref buffer, ref index);
            }
            buffer[index] = (byte)'e';
            index += 1;
            
        }

        public IEnumerator<KeyValuePair<BEString, BEValue>> GetEnumerator()
        {
            return DictionaryValue.GetEnumerator();
        }

        public override int LengthInBytes()
        {
            int length = 2; 

            foreach (var kv in DictionaryValue)
            {
                length += kv.Key.LengthInBytes();
                length += kv.Value.LengthInBytes();
            }
            return length;
        }

        public bool Remove(BEString key)
        {
           return DictionaryValue.Remove(key);
        }

        public bool Remove(KeyValuePair<BEString, BEValue> item)
        {
            return DictionaryValue.Remove(item.Key);
        }

        public bool TryGetValue(BEString key, out BEValue value)
        {
            return DictionaryValue.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return DictionaryValue.GetEnumerator();
        }
    }
}
