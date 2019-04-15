# GoogleTranslate
This is GoogleTranslate by translate.google.cn project.

这个项目是通过translate.google.cn站点来进行在线翻译的功能实现，无须翻墙即可使用，不同于translate.google.com

由于google 翻译接口已经开始收费，但是google还提供在线翻译功能，既然提供在线翻译，那我们就可以利用这个在线翻译来进行接入项目使用，希望可以给一些人予以帮助。

实现本项目的要点：

**1**. 通过识别translate.google.cn中的tkk内容，匹配后通过和文本的转换，最后得到可以访问连接的tk值，传入对应参数即可。
**2**. 本项目已经实现了翻译接口api/translate,post方式提交，参数为fr(需要翻译内容),lang(需要翻译内容的语言种类，如zh-CN),tolang(想要得到的文本内容语言种类，如zh-CN)

**注释**：本项目将会持续更新，最终会实现成一个可以在线翻译的功能网站，内部代码分析后可以自己嵌入到项目中，大致思路是因为公司的多语言项目很多内容不能实现翻译而开发的。

**ps**:*自己开发工具来简化自己写多语言的工作量，何乐而不为*。

> 代码内容为通过网络和自己的实践修改来完善的，其中gettk.js知识点比较多，本想着转到C#来写，发现写完后很多算法并不是很容易切换，如果有更好的把这个转换为C#代码的idea，请提交您的代码公开给大家，我也可以从中学习


![项目启动图][1]


  [1]: https://ws1.sinaimg.cn/large/63103a9cly1g235g81x53j20y40ixaaq.jpg
