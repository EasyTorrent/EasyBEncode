# EasyBEncode

<a href="https://www.nuget.org/packages/EasyBEncode">
  <img alt="Stars" src="https://buildstats.info/nuget/EasyBEncode">
</a>
<a href="https://github.com/fallingrust/EasyBEncode/graphs/contributors">
<img alt="Contributors" src="https://img.shields.io/github/contributors/EasyTorrent/EasyBEncode.svg?style=flat-square">
</a>
<a href="https://github.com/fallingrust/EasyBEncode/network/members">
<img alt="Forks" src="https://img.shields.io/github/forks/EasyTorrent/EasyBEncode.svg?style=flat-square">
</a>
<a href="https://img.shields.io/github/issues/fallingrust/EasyBEncode.svg">
<img alt="Issues" src="https://img.shields.io/github/issues/EasyTorrent/EasyBEncode.svg?style=flat-square">
</a>
<a href="https://github.com/fallingrust/EasyBEncode/blob/master/LICENSE.txt">
<img alt="MIT License" src="https://img.shields.io/github/license/EasyTorrent/EasyBEncode">
</a>

### 关于

基于C#的BEncode编码实现

### Decode使用

```cs
 var beDictionary = new BEDictionary();
 var buffer = new Span<char>([.. content]);
 beDictionary.Decode(ref buffer);
```

### Encode使用

```cs
 var beNumber = new BENumber(20);
 var encodeData = beNumber.Encode();
```
