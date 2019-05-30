



阮一峰的网络日志  » [首页](http://www.ruanyifeng.com/blog/) » [档案](http://www.ruanyifeng.com/blog/archives.html) 

​             

 [ ![img](data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAABGdBTUEAAK/INwWK6QAAABl0RVh0U29mdHdhcmUAQWRvYmUgSW1hZ2VSZWFkeXHJZTwAAAUzSURBVHjavFdbbFRVFF3nPjoz7dTWTittaW0jUDRAUqaNojyqREnEQKgfUj9MqqAmhqRt/OCD4CuY+Kckoh+aiGKC+gMJbdHoRysJ8dkhhmJLNdDKtJU+6GMK87j3Hs85d2Z6HzNtMYWb3Dn3NWftvfba+5xNYDl+e6Fkj6yqb/oDRbWq14vlPBLRKCITkxf0ROLt+hNjp1PPSRK4kA3vF1dXNRcWlyA2OQU9eos9opAkAiKxD+XkKO6t15aRWO7J/MgmAZU8MEgexgZHMX518Dh72sYMmVKShnxWuWHdHtxKIDIYTgMuDzgfmSOIQkYMpdUF8OY92Hytt4/jvkg47czzU16iQovM3QFwmNck+Yyduu7D6NA0Z6JR4THntFs9V4tWQg6Ui3s6MwKDncsFTnXKLJhDSeUK3AgPtyhccDzmVs999buRt/1Vm4i0od+hX7+MRG87jPGB/w1u8FPj9xEw7McVrnYuOCvtpjTth3J/nTg99c8LRhKhr6D3dTB5R24bXFwbMXBsyZzeoXaycEpJ95TB09AGX/NpqLVNtw8urnVzLvHjFNxiFqRy2OOHuqUVnue+ACkoWzo4O6lGzTmuHq6nPvY2m9rVqjrIK2rMEKxqyG5NPAKt+wjo0LklgfNxJkZMA3KJvqRUk3z5UFY3QH14P0h+WUY79HPvgv7VuSg4ZRGY1YgZgqXmORccF17sy2ehnf9AeO085K2HQFbtXBScj0LcpgF2cN+WV+DZ/LJQu6gD4R7oV7pBJwbSgtMvfiPoVp56DySwxm7EtkMs1WdAB7qzggsDJKQYsHucSkOudrkiCPWR/fA2nYCn8SNIK4NptSMyAu3sAdDRkIsJdfth0LzSrODUoPNZ4KI9SxJI5UHk7D4GdQfz2us31c7CoHMjRkKuDPHseCMrONVhNcDJwMJpKFVvg9L4OaTiNWm1x789KCqkrXhVBiEz0WYCT2nAzQAD1/vaETv1GrRfP4Vx5cfMNcDPwvP0h0DhanPym7OIf/+O67vcJ1/PCJ4KgdzaUP6Wz+dU+5yIL6fV+PsHGAOdwlPpvvUOyeeAVGyCdqkDNB6DPjsBSrnndfOGevOh3RhGItxvA+fX1CtbGFhgYUFkFMZPR6F1HnClHq8HyubWtJexX06CRmdt33hrd7nA7SFY4qoGpnYuOKcRykPPgDCBcsHx9Iv+fNL2PueBehCWUfYQIIMGLOCcOmXDXsh1+yCt35tUPfvzGFuSvzvoinXOxqa02qOhM6733nVP2MAdaej2XN11DPKjLZCD+yBvahGCo7JfTKAN9UD7s8Oe9zUNIhz8fWI8DG2k38WCFdxugANcXrvTVd1IEbuv3Jour7Hzn7jLMBNfKs7R3i67gRVrbeCOEDhinmWhAatsqdquM2XzHZINhK2cqTjHr/XZdVJUbgN3MWAVXKbSyg9jesRW2xP9di+lwrL5ojM3m2H/kG9hwcIA37c71W6wJdW2J2S5nrjYbq/t1AHAhJsKQeyfPvf6IMJgghPJhFZ4x0KlfLFvt22du45Au/A1SOlGc0P672XXwhLtOcM0kTTEMMd0qkVmMNXxMd/tsedUjInr4SQDgOfeXMSiN0FCL5WHah4L1qqYXPJOJlttd+a5M+YpcG5poLYKQ5f+6JJ4r8bbJYP47hq4r7QAs9PjYNhHJd4o8l5taiwuOpa7AS4XKqI/5NjJbTnaWK92nLdLuhQAJayRNMiygXPBeQN+Qbvu0zDc3y+aUzhbkGR73sI7ljvUnndx2q3t+X8CDAD66FtrIL864AAAAABJRU5ErkJggg==) ](http://www.ruanyifeng.com/feed.html)

- 上一篇：[每周分享第 13 期 ](http://www.ruanyifeng.com/blog/2018/07/weekly-issue-13.html)
- 下一篇：[每周分享第 14 期 ](http://www.ruanyifeng.com/blog/2018/07/weekly-issue-14.html)

分类：

- [理解计算机](http://www.ruanyifeng.com/blog/computer/)

# CAP 定理的含义





作者： [阮一峰](http://www.ruanyifeng.com)

日期： [2018年7月16日](http://www.ruanyifeng.com/blog/2018/07/)

感谢 腾讯课堂NEXT学院 赞助本站，腾讯官方的前端培训 正在招生中。

   ![腾讯课堂 NEXT 学院](assets/bg2019301803.jpg)  

分布式系统（distributed system）正变得越来越重要，大型网站几乎都是分布式的。

分布式系统的最大难点，就是各个节点的状态如何同步。CAP 定理是这方面的基本定理，也是理解分布式系统的起点。

本文介绍该定理。它其实很好懂，而且是显而易见的。下面的内容主要参考了 Michael Whittaker 的[文章](https://mwhittaker.github.io/blog/an_illustrated_proof_of_the_cap_theorem/)。

## 一、分布式系统的三个指标

![img](assets/bg2018071607.jpg)

1998年，加州大学的计算机科学家 Eric Brewer 提出，分布式系统有三个指标。

> - Consistency
> - Availability
> - Partition tolerance

它们的第一个字母分别是 C、A、P。

Eric Brewer 说，这三个指标不可能同时做到。这个结论就叫做 CAP 定理。

## 二、Partition tolerance

先看 Partition tolerance，中文叫做"分区容错"。

大多数分布式系统都分布在多个子网络。每个子网络就叫做一个区（partition）。分区容错的意思是，区间通信可能失败。比如，一台服务器放在中国，另一台服务器放在美国，这就是两个区，它们之间可能无法通信。

![img](assets/bg2018071601.png)

上图中，G1 和 G2 是两台跨区的服务器。G1 向 G2 发送一条消息，G2 可能无法收到。系统设计的时候，必须考虑到这种情况。

一般来说，分区容错无法避免，因此可以认为 CAP 的 P 总是成立。CAP 定理告诉我们，剩下的 C 和 A 无法同时做到。

## 三、Consistency

Consistency 中文叫做"一致性"。意思是，写操作之后的读操作，必须返回该值。举例来说，某条记录是 v0，用户向 G1 发起一个写操作，将其改为 v1。

![img](assets/bg2018071602.png)

接下来，用户的读操作就会得到 v1。这就叫一致性。

![img](assets/bg2018071603.png)

问题是，用户有可能向 G2 发起读操作，由于 G2 的值没有发生变化，因此返回的是 v0。G1 和 G2 读操作的结果不一致，这就不满足一致性了。

![img](assets/bg2018071604.png)

为了让 G2 也能变为 v1，就要在 G1 写操作的时候，让 G1 向 G2 发送一条消息，要求 G2 也改成 v1。

![img](assets/bg2018071605.png)

这样的话，用户向 G2 发起读操作，也能得到 v1。

![img](assets/bg2018071606.png)

## 四、Availability

Availability 中文叫做"可用性"，意思是只要收到用户的请求，服务器就必须给出回应。

用户可以选择向 G1 或 G2 发起读操作。不管是哪台服务器，只要收到请求，就必须告诉用户，到底是 v0 还是 v1，否则就不满足可用性。

## 五、Consistency 和 Availability 的矛盾

一致性和可用性，为什么不可能同时成立？答案很简单，因为可能通信失败（即出现分区容错）。

如果保证 G2 的一致性，那么 G1 必须在写操作时，锁定 G2 的读操作和写操作。只有数据同步后，才能重新开放读写。锁定期间，G2 不能读写，没有可用性不。

如果保证 G2 的可用性，那么势必不能锁定 G2，所以一致性不成立。

综上所述，G2 无法同时做到一致性和可用性。系统设计时只能选择一个目标。如果追求一致性，那么无法保证所有节点的可用性；如果追求所有节点的可用性，那就没法做到一致性。

[更新 2018.7.17]

读者问，在什么场合，可用性高于一致性？

举例来说，发布一张网页到 CDN，多个服务器有这张网页的副本。后来发现一个错误，需要更新网页，这时只能每个服务器都更新一遍。

一般来说，网页的更新不是特别强调一致性。短时期内，一些用户拿到老版本，另一些用户拿到新版本，问题不会特别大。当然，所有人最终都会看到新版本。所以，这个场合就是可用性高于一致性。

（完）

### 文档信息

- 版权声明：自由转载-非商用-非衍生-保持署名（[创意共享3.0许可证](http://creativecommons.org/licenses/by-nc-nd/3.0/deed.zh)）
- 发表日期： 2018年7月16日

​     [Teambition：研发管理工具](https://www.teambition.com/agile?utm_source=ruanyifeng&utm_content=agile)     
​     [![Teambition](assets/bg2019031201.jpg)](https://www.teambition.com/agile?utm_source=ruanyifeng&utm_content=agile)   

​     [饥人谷：专业前端培训机构](http://qr.jirengu.com/api/taskUrl?tid=58)     
​     [![饥人谷](assets/bg2019042105.png)](http://qr.jirengu.com/api/taskUrl?tid=50)   

## 相关文章

- 2018.10.16: [exFAT 文件系统指南](http://www.ruanyifeng.com/blog/2018/10/exfat.html)

  ​                               国庆假期，我拍了一些手机视频，打算存到新买的移动硬盘。                             

- 2018.05.09: [根域名的知识](http://www.ruanyifeng.com/blog/2018/05/root-domain.html)

  ​                               域名是互联网的基础设施，只要上网就会用到。                             

- 2018.01.21: [汇编语言入门教程](http://www.ruanyifeng.com/blog/2018/01/assembly-language-primer.html)

  ​                               学习编程其实就是学高级语言，即那些为人类设计的计算机语言。                             

- 2018.01.11: [加密货币的本质](http://www.ruanyifeng.com/blog/2018/01/cryptocurrency-tutorial.html)

  ​                               现在，各种加密货币（cryptocurrency）不计其数。                             

## 广告[（购买广告位）](http://www.ruanyifeng.com/support.html)

[API 调试和文档生成利器](https://www.apipost.cn/article/1003?fr=ruanyifeng)

![="ApiPost"](assets/bg2019032602.jpg)

[硅谷的机器学习课程](http://t.cn/ESy76dU)

![="优达学城"](assets/bg2019042801.jpg)

## 留言（47条）

​                                                            Mike   说：                  

三个圆为什么不能有重合？CAP也应该有前提，如果技术提升了，比如量子通信，CAP还能成立吗？

​                    2018年7月16日 09:16  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-390975)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            [owenliang](http://yuerblog.cc)   说：                  

可以顺便讲讲BASE

​                    2018年7月16日 09:30  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-390977)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            [史蒂夫](http://www.cndota.top)   说：                  

> ```
> 引用Mike的发言：
> ```
>
> 三个圆为什么不能有重合？CAP也应该有前提，如果技术提升了，比如量子通信，CAP还能成立吗？

网络通信不管怎么样都会有失败的可能，只能尽量提高可用性

​                    2018年7月16日 10:13  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-390983)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            [yz](http://sstruct.github.io)   说：                  

看起来提高可用性是比较合理的

​                    2018年7月16日 11:07  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-390989)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            freeman   说：                  

没有一致性只有可用性的使用场景是什么，可以给举俩例子不

​                    2018年7月16日 11:32  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-390991)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            Mike   说：                  

> ```
> 引用史蒂夫的发言：
> ```
>
> 
>
> 网络通信不管怎么样都会有失败的可能，只能尽量提高可用性

我想表达的是，CAP不应该是拿来就用，而是需要带有怀疑的精神看问题。

​                    2018年7月16日 11:42  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-390992)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            [命中水、](http://www.cxiansheng.cn)   说：                  

学到了。每种架构都有其不可控的因素。没有完美的设计。

​                    2018年7月16日 14:35  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-390995)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            [周公瑾](http://575994088@qq.com)   说：                  

这个的前提是网络通信可能失败。假设解决了这个问题，那么就有更远的通信，以后若是星球级的网络通信呢？

​                    2018年7月16日 14:38  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-390996)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            迷茫的小白一个   说：                  

如果这3个指标不能同时做到，那分布式系统不是不靠谱么，现如今互联网上这么多的分布式系统都是怎么解决这些问题的呢？

​                    2018年7月16日 16:08  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-391002)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            大名   说：                  

AP 不是没有C 只是在不能在某一刻实现数据一致性，只是有延时而已。

​                    2018年7月16日 16:54  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-391004)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            lance6716   说：                  

"一般来说，分区容错无法避免，因此可以认为 CAP 的 P 总是成立。"这个按照文中逻辑应该是P 总是不成立吧

​                    2018年7月17日 10:20  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-391016)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            shadow   说：                  

> ```
> 引用史蒂夫的发言：
> ```
>
> 
>
> 网络通信不管怎么样都会有失败的可能，只能尽量提高可用性

阮老师开头已经说了，是 “介绍该定理”， 一个劲在这杠，有意思么

​                    2018年7月17日 11:23  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-391021)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            [只为了推广自己博客](http://ilove33.top/)   说：                  

要强一致性的地方:  唯一ID生成, 这种性能很差. 并发不高.

要高可用: 最终一致, 即有可能读到脏数据. 但是一段时间之后总是能够读到新数据.

​                    2018年7月17日 13:36  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-391023)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            你 胡   说：                  

《前方的路》和《未来世界的幸存者》买到了

​                    2018年7月18日 01:31  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-391055)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            九饼   说：                  

老师您好，叨扰了，请问您是否有意向做公开课程和知识分享呢，当然是付费的。我们是一个还没上线的区块链APP

​                    2018年7月18日 09:52  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-391056)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            Ivan   说：                  

> ```
> 引用迷茫的小白一个的发言：
> ```
>
> 如果这3个指标不能同时做到，那分布式系统不是不靠谱么，现如今互联网上这么多的分布式系统都是怎么解决这些问题的呢？

很多折中的办法，比如分布式存储就是可用性+最终一致性

​                    2018年7月18日 14:16  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-391088)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            [hohode](http://www.hohode.com)   说：                  

就像最后说的，一般情况还是先保证可用性，然后才是一致性，当然两个都很总要，最好都能满足。

​                    2018年7月18日 15:47  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-391090)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            [Fionn](http://www.fxtaoo.com)   说：                  

例子让我想到前端讲的“渐进增强”。不要求所有访问者体验一致，关键是先能够看到。

​                    2018年7月18日 15:53  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-391091)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            [tkokof](https://blog.csdn.net/tkokof1)   说：                  

小小笔误:
 "锁定期间，G2 不能读写，没有可用性不"
 最后应该是多写了一个“不”~

​                    2018年7月18日 21:47  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-391102)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            zlx   说：                  

阮老师，您首页随机图片的那段代码能不能拿来用啊????

​                    2018年7月19日 10:06  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-391109)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            阮老师的小粉丝   说：                  

> ```
> 引用Mike的发言：
> ```
>
> 三个圆为什么不能有重合？CAP也应该有前提，如果技术提升了，比如量子通信，CAP还能成立吗？

我感觉不成立了，因为运算速度超块，

​                    2018年7月19日 17:18  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-391126)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            YU   说：                  

> ```
> 引用阮老师的小粉丝的发言：
> ```
>
> 我感觉不成立了，因为运算速度超块，

再快也不能等于零
 

​                    2018年7月19日 22:26  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-391133)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            王洁玉   说：                  

多个了‘不’

​                    2018年7月20日 15:17  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-391182)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            lanruo   说：                  

阮老师，你推荐看的书我都买来看了哦，特别是《黑客与画家》，《穷爸爸和富爸爸》。

​                    2018年7月23日 09:34  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-391284)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            A-ZZ   说：                  

> ```
> 引用lance6716的发言：
> ```
>
> "一般来说，分区容错无法避免，因此可以认为 CAP 的 P 总是成立。"这个按照文中逻辑应该是P 总是不成立吧

分区相当于对通信的时限要求。系统如果不能在时限内达成数据一致性，就意味着发生了分区的情况，必须就当前操作在C和A之间做出选择
 

​                    2018年7月23日 14:12  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-391293)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            lomoye   说：                  

简单明了 赞

​                    2018年7月24日 22:15  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-391364)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            wyf   说：                  

> ```
> 引用Mike的发言：
> ```
>
> 三个圆为什么不能有重合？CAP也应该有前提，如果技术提升了，比如量子通信，CAP还能成立吗？

\1. 量子通信不是这样玩的……目前量子通讯还得借助传统信道传点东西，所以量子通讯只能用于机密传输（或者仅仅传一串真随机数）

\2. 只有所有服务器同时（或者高频率同步）接收数据修改，才能同时满足C和A。然而这个前提违反了P。

\3. 就是因为所有服务器并不总是能完成同步，才说“可以认为P总是成立”，于是C和A不能同时满足。

 （ps: 2的前提不就是我们努力追求的吗……）

​                    2018年7月24日 23:12  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-391365)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            吃人   说：                  

谢谢阮老师

​                    2018年7月26日 19:27  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-391441)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            吃人   说：                  

> ```
> 引用lance6716的发言：
> ```
>
> "一般来说，分区容错无法避免，因此可以认为 CAP 的 P 总是成立。"这个按照文中逻辑应该是P 总是不成立吧

我也这么觉得，是不是我们理解有问题啊

​                    2018年7月26日 19:28  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-391442)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            V   说：                  

> ```
> 引用lance6716的发言：
> ```
>
> "一般来说，分区容错无法避免，因此可以认为 CAP 的 P 总是成立。"这个按照文中逻辑应该是P 总是不成立吧

"分区容错的意思是，区间通信可能失败。"划重点，注意看这一句，即“区间通信可能失败”这件事总是成立

​                    2018年8月 2日 14:36  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-391727)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            suntimer   说：                  

> ```
> 引用lance6716的发言：
> ```
>
> "一般来说，分区容错无法避免，因此可以认为 CAP 的 P 总是成立。"这个按照文中逻辑应该是P 总是不成立吧

是的，分区容错性是指具有容错性，无法避免，那就是不总是满足的，所以应该是描述不够准确吧

​                    2018年8月22日 20:46  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-392443)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            cybort   说：                  

这个定理说的是，“一个分区的系统，不可能同时做到数据严格一致和即时响应”，要是总能保持实时一致的话那就不叫分区了。

​                    2018年8月29日 16:00  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-392674)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            Cat   说：                  

之前有人说阮一峰老师分享的很多知识都不准确，都是相当入门的概念，详细理解之后都是错的，我都没有太care，因为很多分享的东西我也不需要深入学习，只是了解一下就好了。
 但是看到这盘文章之后，发现准确性实在是太有问题了。底下的评论也都是在装毕讨论一些不知所谓的东西。

​                    2018年9月26日 11:36  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-393438)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            Hatchin   说：                  

> ```
> 引用Cat的发言：
> ```
>
> 之前有人说阮一峰老师分享的很多知识都不准确，都是相当入门的概念，详细理解之后都是错的，我都没有太care，因为很多分享的东西我也不需要深入学习，只是了解一下就好了。
>  但是看到这盘文章之后，发现准确性实在是太有问题了。底下的评论也都是在装毕讨论一些不知所谓的东西。

所以为什么你不说一下哪里有问题呢？是不是这里空白的地方太小，你写不下？

​                    2018年11月27日 17:44  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-395979)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            [Jenkenyang](http://无)   说：                  

> ```
> 引用Mike的发言：
> ```
>
> 三个圆为什么不能有重合？CAP也应该有前提，如果技术提升了，比如量子通信，CAP还能成立吗？

是这样的：她说的不能实现的意思，是不能完全满足，因为总会有失败的情况出现，技术提高了，是提高了它的可用范围，但是最终还是会遇到瓶颈。

​                    2018年11月28日 16:10  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-396095)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            青檬的菜鸟   说：                  

<https://mwhittaker.github.io/blog/an_illustrated_proof_of_the_cap_theorem/>

​                    2018年12月18日 17:36  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-397659)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            三角形小于零   说：                  

我觉得提到 CAP 首先要先指明这是针对分布式 “存储系统”的一个理论。 

​                    2019年1月 1日 16:00  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-399776)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            leozmm   说：                  

> ```
> 引用lance6716的发言：
> ```
>
> "一般来说，分区容错无法避免，因此可以认为 CAP 的 P 总是成立。"这个按照文中逻辑应该是P 总是不成立吧

 文中的讨论逻辑是： 1. CAP定理表明，C、A、P必有1个不成立； 2. 一般情况下，P一定成立 所以，由1、2推导出C、A中必有1个不成立。后续的写作也都是建立在这个推论基础上。         

​                    2019年1月14日 17:12  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-403451)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            大白   说：                  

一致性和可用性例子,哪里有分区容错?

​                    2019年1月19日 14:13  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-405379)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            大白   说：                  

如果p成立还有其他什么事

​                    2019年1月19日 14:14  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-405380)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            peter pan   说：                  

谢谢老师的讲解  看其他博客差点被一些瞎扯的人给误导了

​                    2019年1月31日 10:09  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-407319)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            e_sun   说：                  

关于CAP理论，这一篇算是写的比较准确且浅显易懂的了。
 还是建议加上关于拜占庭将军问题的前篇，和最终一致性等应用的续篇，这样前因后果才完整。

不过理论终究是理论，如果联系实际Availability就很难确定。什么叫不可用呢？
 多久不响应叫不可用，是超过1秒，1毫秒还是永远，永远有多远？
 还有一个因素就是分区的时间，理论中G1和G2的连接是永久断开了，但是联系实际难道中国和美国服务器连接断了没人去查，没人去修吗？难道不会建两条物理链路随时切换？

所以把可用性的时间约束和分区的时间约束联合起来考虑，现实中的分布式系统是有可能同时满足CAP的。

​                    2019年2月 3日 11:01  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-407601)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            jason.zhang   说：                  

> ```
> 引用lance6716的发言：
> ```
>
> "一般来说，分区容错无法避免，因此可以认为 CAP 的 P 总是成立。"这个按照文中逻辑应该是P 总是不成立吧

分区容错发生在分布式系统内部互访通信，是指分布式网络中部分网络不可用，但系统依然正常对外提供服务。

比如：北京的订单系统，访问上海的库存系统，可能导致失败。如果发生失败，就要在A和C之间做出选择。
 要么停止系统进行错误恢复，要么继续服务但是降低一致性，所以说只能保证AP或CP。

​                    2019年2月13日 16:31  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-408206)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            tong   说：                  

> ```
> 引用Mike的发言：
> ```
>
> 
>
> 我想表达的是，CAP不应该是拿来就用，而是需要带有怀疑的精神看问题。

你要怀疑什么？！这是已经严格证明过的定理。不是猜想。

​                    2019年3月 1日 19:10  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-409660)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            CD 游客   说：                  

> ```
> 引用jason.zhang的发言：
> ```
>
> 
>
> 分区容错发生在分布式系统内部互访通信，是指分布式网络中部分网络不可用，但系统依然正常对外提供服务。
>
> 比如：北京的订单系统，访问上海的库存系统，可能导致失败。如果发生失败，就要在A和C之间做出选择。
>  要么停止系统进行错误恢复，要么继续服务但是降低一致性，所以说只能保证AP或CP。

都CP了还能叫正常对外提供服务吗?

​                    2019年3月11日 16:10  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-409859)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            易直   说：                  

> ```
> 引用yz的发言：
> ```
>
> 看起来提高可用性是比较合理的

不一定，对一致性要求比较高的系统，例如银行

​                    2019年4月 4日 18:05  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-410369)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

​                                                            chongzi   说：                  

分布式系统：在互相隔离的空间中，提供数据服务的系统。
 CAP抽象：不同空间的数据，在同一时间，状态一致。

C：代表状态一致
 A：代表同一时间
 P：代表不同空间
 CP:不同空间中的数据，如果要求他们所有状态一致，则必然不在同一时间。
 AP:不同空间中，如果要求同一时间都可以从任意的空间拿到数据，则必然数据的状态不一致。
 CA:不同空间的数据，如果要求任意时间都可以从任意空间拿到状态一致的数据，则空间数必然为1.

​                    2019年4月 6日 01:01  | [#](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-410391)  | [引用](http://www.ruanyifeng.com/blog/2018/07/cap.html#comment-text) 

## 我要发表看法



您的留言                     （HTML标签部分可用）



您的大名：

   «-必填

电子邮件：

   «-必填，不公开

个人网址：

   «-我信任你，不会填写广告链接

​                         记住个人信息？





   «- 点击按钮

2019 © [联系方式](http://www.ruanyifeng.com/contact.html) | [邮件订阅](https://app.feedblitz.com/f/f.fbz?Sub=348868)