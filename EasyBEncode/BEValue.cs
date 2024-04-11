using System;

namespace EasyBEncode
{
    /// <summary>
    /// describe：
    /// author：cxj
    /// date：2024/4/9 14:51:15
    /// </summary>
    public abstract class BEValue
    {
        public BEValue()
        {
            
        }      

        public abstract string Encode();

        public abstract void Decode(ref Span<char> buffer);
    }
}
