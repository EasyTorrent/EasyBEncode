using EasyBEncode;

namespace Test
{
    /// <summary>
    /// describe：
    /// author：cxj
    /// date：2024/4/9 15:23:35
    /// </summary>
    public class BETest
    {
        [InlineData("d4:nick11:Create Chen3:agei23e4:blog33:http://www.cnblogs.com/technology7:hobbiesl6:Coding10:Basketballee")]
        [Theory]
        public void BEDicDecode_Test1(string content)
        {
            var beDictionary = new BEDictionary();
            var buffer = new Span<char>([.. content]);
            beDictionary.Decode(ref buffer);
            Assert.Equal(4, beDictionary.DictionaryValue.Count);
        }

        [InlineData("d4:spami42e4:eggs11:hello world4:mored4:yeari2024e7:product7:Copilotee")]
        [Theory]
        public void BEDicDecode_Test2(string content)
        {
            var beDictionary = new BEDictionary();
            var buffer = new Span<char>([.. content]);
            beDictionary.Decode(ref buffer);
            Assert.Equal(3, beDictionary.DictionaryValue.Count);
        }

        [InlineData("li42e11:hello worldl4:20177:Copilotee")]
        [Theory]
        public void BEList_Test1(string content)
        {
            var beList = new BEList();
            var buffer = new Span<char>([.. content]);
            beList.Decode(ref buffer);
            Assert.Equal(3, beList.ListValue.Count);            
        }
        [InlineData("li42el4:20177:Copilote11:hello worlde")]
        [Theory]
        public void BEList_Test2(string content)
        {
            var beList = new BEList();
            var buffer = new Span<char>([.. content]);
            beList.Decode(ref buffer);
            Assert.Equal(3, beList.ListValue.Count);
        }
    }
}
