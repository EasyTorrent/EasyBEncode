using EasyBEncode;
using System.Text;

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
            var buffer = new Span<byte>(Encoding.ASCII.GetBytes(content));
            beDictionary.Decode(ref buffer);
            Assert.Equal(4, beDictionary.DictionaryValue.Count);
        }

        [InlineData("d1:rd2:id20:abcdefghij01234567895:token8:aoeusnth6:valuesl6:axje.u6:idhtnmee1:t2:aa1:y1:re")]
        [Theory]
        public void BEDicDecode_Test2(string content)
        {
            var beDictionary = new BEDictionary();
            var buffer = new Span<byte>(Encoding.ASCII.GetBytes(content));
            beDictionary.Decode(ref buffer);
            Assert.Equal(3, beDictionary.DictionaryValue.Count);
        }
        [InlineData("d1:ad2:id20:abcdefghij0123456789e1:q4:ping1:t2:aa1:y1:qe")]
        [Theory]
        public void BEDicDecode_Test3(string content)
        {
            var beDictionary = new BEDictionary();
            var buffer = new Span<byte>(Encoding.ASCII.GetBytes(content));
            beDictionary.Decode(ref buffer);
            Assert.Equal(4, beDictionary.DictionaryValue.Count);
        }
        [InlineData("li42e11:hello worldl4:20177:Copilotee")]
        [Theory]
        public void BEList_Test1(string content)
        {
            var beList = new BEList();
            var buffer = new Span<byte>(Encoding.ASCII.GetBytes(content));
            beList.Decode(ref buffer);
            Assert.Equal(3, beList.ListValue.Count);            
        }
        [InlineData("li42el4:20177:Copilote11:hello worlde")]
        [Theory]
        public void BEList_Test2(string content)
        {
            var beList = new BEList();
            var buffer = new Span<byte>(Encoding.ASCII.GetBytes(content));
            beList.Decode(ref buffer);
            Assert.Equal(3, beList.ListValue.Count);

        }

        [InlineData("64313a6164323a696432303ab30a9dcf6452f673ad1ed92f8d84dad5dfff55e1363a74617267657432303a4cf562309bad098c52e126d0727b252a2000aa1d65313a71393a66696e645f6e6f6465313a74383a78e005f1d8ed073c313a79313a7165")]
        [Theory]
        public void FindNode_Test2(string content)
        {
            var beList = new BEDictionary();
            var buffer = new Span<byte>(Convert.FromHexString(content));
            beList.Decode(ref buffer);
        }
        [InlineData("64313a6164323a696432303ab30a9dcf6452f673ad1ed92f8d84dad5dfff55e1363a74617267657432303a4cf562309bad098c52e126d0727b252a2000aa1d65313a71393a66696e645f6e6f6465313a74383a78e005f1d8ed073c313a79313a7165"
            , "64323a6970363a2421059f7389313a7264323a696432303a4c0eeb6005d6eabcbfffb489c65623f179fa3f56353a6e6f6465733230383a4cf51c5cc7cd58f6d7ef8d0c964b6df7b5edfa0373dd5b3dafda4ccfd45d11a412941f08f75e39e294776494cf878cf9fe131ae44ca2ffeafa686104d145260152917591d82316096a0882921ae34ca47e9d9979e147de2421da2db9ff6178613c09da131f0fc41c4ca4f9eeb3585ab4e8bce2505a018389661f12758eaa2a2e1ae14c953e0bdf2d2de2db609387c2209ecda064473a7c8376dd1ae14cfee72bc634b68075db28effb49b4f2946a12072ee8d267fa074ca5b2b4cb94ab5b6e58da2beaf7f5a56cb696128cf93e441ae2313a706932393537376565313a74383a78e005f1d8ed073c313a76343a4c540207313a79313a7265")]
        [Theory]
        public void FindNode_Test3(string queryContent,string responseContent)
        {
            var query = new BEDictionary();
            var queryBuffer = new Span<byte>(Convert.FromHexString(queryContent));
            query.Decode(ref queryBuffer);


           
            int index = 0;
            var queryEncodeBuffer = new Span<byte>(new byte[query.LengthInBytes()]);
            query.Encode(ref queryEncodeBuffer, ref index);
            var queryEncodeHex = BitConverter.ToString(queryEncodeBuffer.ToArray()).Replace("-", "").ToLower();
            Assert.Equal(queryEncodeHex, queryContent);

            var response = new BEDictionary();
            var responseBuffer = new Span<byte>(Convert.FromHexString(responseContent));
            response.Decode(ref responseBuffer);

            index = 0;
            var responseEncodeBuffer = new Span<byte>(new byte[response.LengthInBytes()]);
            response.Encode(ref responseEncodeBuffer, ref index);
            var responseEncodeHex = BitConverter.ToString(responseEncodeBuffer.ToArray()).Replace("-", "").ToLower();
            Assert.Equal(responseEncodeHex, responseContent);
        }
    }
}
