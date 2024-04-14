using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EasyBEncode
{
    /// <summary>
    /// describe：
    /// author：cxj
    /// date：2024/4/9 14:54:59
    /// </summary>
    public class BEList : BEValue, IList<BEValue>
    {
        public BEList()
        {
                
        }
        public BEList(List<BEValue> listValue)
        {
            ListValue.Clear();
            ListValue.AddRange(listValue);           
        }

        public BEValue this[int index] { get => ListValue[index]; set => ListValue[index] = value; }

        public List<BEValue> ListValue { get; set; } = new List<BEValue>();

        public int Count => ListValue.Count;

        public bool IsReadOnly => false;

        public void Add(BEValue item)
        {
            ListValue.Add(item);
        }

        public void Clear()
        {
            ListValue.Clear();
        }

        public bool Contains(BEValue item)
        {
           return ListValue.Contains(item);
        }

        public void CopyTo(BEValue[] array, int arrayIndex)
        {
            ListValue.CopyTo(array, arrayIndex);
        }

        public override void Decode(ref Span<byte> buffer)
        {
            if (buffer.IsEmpty)
                throw new EasyBEncodeException("decode BEList error,encodeValue null or whitespace");
            if (buffer[0] != 0x6c)
                throw new EasyBEncodeException($"decode BEList error, invalid format");
            buffer = buffer[1..];
            
            while (!buffer.IsEmpty && buffer[0] != 0x65 )
            {
                if (buffer[0] >= 0x30 && buffer[0] <= 0x39)
                {
                    var beString = new BEString();
                    beString.Decode(ref buffer);
                    ListValue.Add(beString);
                }
                else if (buffer[0] == 0x64)
                {
                    var beDictionary = new BEDictionary();
                    beDictionary.Decode(ref buffer);
                    ListValue.Add(beDictionary);
                }
                else if (buffer[0] == 0x6c)
                {                    
                    var beList = new BEList();
                    beList.Decode(ref buffer);
                    ListValue.Add(beList);
                }
                else if (buffer[0] == 0x69)
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

       

        public override void Encode(ref Span<byte> buffer, ref int index)
        {
            buffer[index] = (byte)'l';
            index += 1;
            foreach (var item in ListValue)
            {
                item.Encode(ref buffer, ref index);
            }
            buffer[index] = (byte)'e';
            index += 1;
        }

        public IEnumerator<BEValue> GetEnumerator()
        {
            return ListValue.GetEnumerator();
        }

        public int IndexOf(BEValue item)
        {
            return ListValue.IndexOf(item);
        }

        public void Insert(int index, BEValue item)
        {
            ListValue.Insert(index, item);
        }

        public override int LengthInBytes()
        {
            var length = 2;
            foreach(var item in ListValue)
            {
                length += item.LengthInBytes();
            }
            return length;
        }

        public bool Remove(BEValue item)
        {
            return ListValue.Remove(item);
        }

        public void RemoveAt(int index)
        {
            ListValue.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ListValue.GetEnumerator();
        }
    }
}
