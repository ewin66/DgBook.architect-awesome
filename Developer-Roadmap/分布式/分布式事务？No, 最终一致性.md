# 分布式事务？No, 最终一致性

[![itegel](assets/v2-d03803f5bc3a9d1bdf028c874328f0a2_xs.jpg)](https://www.zhihu.com/people/itegel)

[itegel](https://www.zhihu.com/people/itegel)

小小程序猿

[apodemakeles](https://www.zhihu.com/people/apodemakeles)

、

[陶文](https://www.zhihu.com/people/tao-wen-54)

、

[ULiiAn](https://www.zhihu.com/people/uliian)

 等 

## 分布式一致性

## 一、写在前面

现今互联网界，分布式系统和微服务架构盛行。

一个简单操作，在服务端非常可能是由多个服务和数据库实例协同完成的。

在互联网金融等一致性要求较高的场景下，多个独立操作之间的一致性问题显得格外棘手。

基于水平扩容能力和成本考虑，传统的强一致的解决方案（e.g.单机事务）纷纷被抛弃。其理论依据就是响当当的[CAP原理](https://link.zhihu.com/?target=https%3A//en.wikipedia.org/wiki/CAP_theorem)。

我们往往为了可用性和分区容错性，忍痛放弃强一致支持，转而追求最终一致性。大部分业务场景下，我们是可以接受短暂的不一致的。

本文主要讨论一些最终一致性相关的实现思路。

![img](https://pic3.zhimg.com/80/v2-135c492dbc2225ca86af34b837e446fa_hd.jpg)





## 二、最终一致性解决方案

这个时候一般都会去举一个例子：A给B转100元。

当然，A跟B很不幸的被分在了不同的数据库实例上。甚者这两个人可能是在不同机构开的户。

下面讨论基本都是围绕这个场景的。更复杂的场景需要各位客官发挥下超人的想象力和扩展能力了。

谈到最终一致性，人们首先想到的应该是2PC解决方案。

## 1. 两阶段提交

两阶段提交需要有一个协调者，来协调两个操作之间的操作流程。当参与方为更多时，其逻辑其实就比较复杂了。

而参与者需要实现两阶段提交协议。Pre commit阶段需要锁住相关资源，commit或rollback时分别进行实际提交或释放资源。

看似还不错。但是考虑到各种异常情况那就比较痛苦了。

举个例子：如下图，执行到提交阶段，调用A的commit接口超时了，协调者该如何做？

我们一般会假设预提交成功后，提交或回滚肯定是成功的（由参与者保障）。

上述情况协调者只能选择继续重试。这也就要求下游接口必须实现幂等（关于幂等的实现下面我们单独再讨论下）。

一般，下游出现故障，不是短时重试能解决的。所以，我们一般也需要有定时去处理中间状态的逻辑。

这个地方，其实如果有个支持重试的MQ，可以扔到MQ。在实践中，我们曾经也尝试自己实现了一个基于MySQL的重试队列。下面还会聊到这一点。

另外，我们也利用了一些外部重试机制。比如支付场景，微信和支付宝都有非常可靠的通知机制。

我们在通知处理接口中做一些重试策略。如果重试失败，就返回微信或支付宝失败。

这样第三方还会接着回调我们(怀疑他们可能发现了我厂回调成功率比其他商户要低^_^)，不过作为小厂，利用一些大厂成熟的机制还是可取的。





## 2. 异步确保（没有事务消息）

“异步确保”这个词不一定是准确的，还没找到更合适的词，抱歉。

异步化不只是为了一致性，有时候更多的考虑响应时间，下游稳定性等因素。本节只讨论通过异步方案，如何实现最终一致性。

该方案关键是要有个消息表。另外，一般会有个队列，而且我们一般都会假设这个MQ不丢消息。不过很不幸此MQ还不支持事务消息。

基本思路就是：

1. 消息生产方，需要额外建一个消息表，并记录消息发送状态。消息表和业务数据要在一个事务里提交。实现时为了简单，可以只是增加一个字段。新增字段会跟业务强耦合，新增表处理起来不同交易数据可以通用处理。不过因为消息表跟业务需要在一个事务里，所以存储耦合在所难免。
2. 消息消费方，需要处理这个消息，并完成自己的业务逻辑。此时如果本地事务处理成功，那发送给生产方一个confirm消息，表明已经处理成功了。如果处理失败，该消息还是需要放回MQ的。如果MQ支持重试，那就省事儿了。如果不支持，可以考虑把该消息放回队尾或另建一个队列特殊处理。当然非要处理成功才能继续，那只能block在这条消息了（估计一般人不会这么做）。Kafka  lowlevel接口是支持自己设置offset的，所以可以实现block。
3. 生产方定时扫描本地消息表，把还没处理完成的消息由发送一遍。如果有靠谱的自动对账补账逻辑，其实这一步也可以省略。在实践中，丢消息或者下游处理失败这种场景还是非常少的。这里要看业务上能不能容忍不一致到一个对账补账周期。

当然如果进一步简化，那么MQ也可以不要的。直接用一个脚本处理，一些低频场景，也没啥大问题。当然离线扫表这个事情，总让人不爽。业务量不大且也出初期相信很多人干活儿这事儿。

另外，对一致性要求不高的或者有其他兜底方案的场景（比如较为频繁的对账补账机制），我们就不需要关心消息的confirm等情况，只要扔给消息，就认为万事大吉，一般也是可取的。



上面我们除了处理业务逻辑，还做了很多繁琐的事情。把这些杂活儿都扔给一个中间件多好！这就是阿里等大厂做的事务消息中间件了（比如Notify，RockitMQ的事务消息，请看下节）。

## 3. 异步确保（事务消息）

事务消息实际上是一个很理想的想法。

理想是：我们只要把消息扔到MQ，那么这个消息肯定会被消费成功。生产方不用担心消息发送失败，也不用担心消息会丢失。

回到现实，消费方，如果消息处理失败了，还有机会继续消费，直到成功为止（消费方逻辑bug导致消费失败情况不在本文讨论范围内）。

但遗憾的是市面上大部分MQ都不支持事务消息，其中包括看起来可以一统江湖的kafka。

RocketMQ号称支持，但是还没开源(事务消息相关部分没开源)。阿里云据说免费提供，没玩过（羡慕下阿里等大厂内部猿类们）。不过从网上公开的资料看，用起来还是有些不爽的地方。这是后话了，毕竟解决了很多问题。

事务消息，关键一点是把上小节中繁琐的消息状态和重发等用中间件形式封装了。

我厂目前还没提供成熟的支持事务消息的MQ。下面以网传RMQ为例，说明事务消息大概是怎么玩的：

RMQ的事务消息相对于普通MQ，相当于提供了2PC的提交接口。

生产方需要先发送一个prepared消息给RMQ。如果操作1失败，返回失败。

然后执行本地事务，如果成功了需要发送Confirm消息给RMQ。2失败，则调用RMQ cancel接口。 

那问题是3失败了（或者超时）该如何处理呢？

别急，RMQ考虑到这个问题了。 RMQ会要求你实现一个check的接口。生产方需要实现该接口，并告知RMQ自己本地事务是否执行成功（第4步）。RMQ会定时轮训所有处于pre状态的消息，并调用对应的check接口，以决定此消息是否可以提交。

当然第5步也可能会失败。这时候需要RMQ支持消息重试。处理失败的消息果断时间再进行重试，直到成功为止（超过重试次数后会进死信队列，可能得人肉处理了，因为没用过所以细节不是很了解）。

支持消息重试，这一点也很重要。消息重试机制也不仅仅在这里能用到，还有其他一些特殊的场景，我们会依赖他。下一小节，我们简单探讨一下这个问题。

RMQ还是很强大的。我们认为这个程度的一致性已经能够满足绝大部分互联网应用场景。代价是生产方做了不少额外的事情，但相比没有事务消息情况，确实解放了不少劳动力。 



P.S. 据说阿里内部因为历史原因，用notify比RMQ要多，他们俩基本原理类似。

## 4. 补偿交易（Compensating Transaction）

补偿交易，其核心思想是:针对每个操作，都要注册一个与其对应的补偿操作。一般来说操作本身和其补偿（撤销）操作会在一个事务里完成。

当其后续操作失败后，需要按相反顺序完成前面注册的所有撤销操作。

跟2PC比，他的核心价值应该是少了锁资源的代价。流程也相对简单一点。但实际操作中，补偿操作不太好定义，其中间状态处理也会比较棘手。

比如A:-100(补偿为A:+100),
B:+100。那么如果B:+100失败后就需要执行A:+100。

曾经有位大牛同事(也是我灰常崇拜的一位技术控)一直热衷于这个思路，相信有些场景用补偿交易模式也是个不错的选择。

他更多是不断思考如何让补偿看起来跟注册个单库事务一样简单。做到业务无感知。

因为本人没有相关实战经验，所以留个[链接在这里](https://link.zhihu.com/?target=https%3A//docs.microsoft.com/en-us/azure/architecture/patterns/compensating-transaction)，供大家扩展阅读。偷懒了，截个此文中的一张图。





## 5. 消息重试

上面多次提到消息重试。如果说事务消息重点解决了生产者和MQ之间的一致性问题，那么重试机制对于确保消费者和MQ之间的一致性是至关重要的。

重试可以是pull模式，也可以是push模式。我厂目前已经提供push模式的消息重试，这个还是要赞一下的！

消息重试，重试顾名思义是要解决消息一次性传递过程中的失败场景。举个例子，支付宝回调商户，然后商户系统挂了，怎么办？答案是重试！

一般来说，消息如果消费失败，就会被放到重试队列。如果是延迟时间固定（比如每次延迟2s），那么只需要按失败的顺序进队列就好了，然后对队首的消息，只有当延迟时间到达才能被消费。

这里会有个水位的概念。如果按时间作为水位，那么期望执行时间大于当前时间的消息才是高于水位以上的。其他消息对consumer不可见。

如果要实现每个消息延迟时间不一样，之前想过一种基于队列的方案是，按秒的维度建多个队列。按执行时间入到不同的队列，一天86400个队列（一般丑陋）。然后cosumer按时间消费不同队列。

当然如果不依赖队列可以有更灵活的方案。

之前做支付时候，做了个基于DB的延时队列。每次消息进去时候，都会把下次执行时间设置一下。再对这个时间做个索引....

略土，but it works。毕竟失败的消息不该很多，所以DB容量也不用太在意。很多时候，能跑起来的，简单的架构会得到更多人喜爱。

我厂提供了一种基于redis的延时队列，可以支持消息重试。用到的主要数据结构是redis的zset，按消息处理时间排序。

当然实现起来也没说的那么简单。MQ遇到的持久化问题，内存数据丢失问题，重试次数控制，消息追溯等等都需要有一些额外的开发量。

综上，如果MQ能够提供消息重试特性，那就不要自己折腾了。这里还是有不少坑的。

## 6. 幂等（接口支持重入）

即使没有MQ，重试也是无处不在的。所以幂等问题不是因为用到MQ后引入的，而是老问题。

幂等怎么做？

如果是单条insert操作，我们一般会依赖唯一键。如果一个事务里包含一个单条insert，那也可以依赖这条insert做幂等，当insert抛异常就回滚事务。

如果是update操作，那么状态机控制和版本控制异常重要。这里要多加小心。

再复杂点的，可以考虑引入一个log表。该log对操作id（消息id？）进行唯一键控制。然后整个操作用事务控制。当插入log失败时整个事务回滚就好了。

有人会说先查log表或者利用redis等缓存，加锁。我想说的是这个基本上都不work。除非在事务里进行查寻。所以建议，所幸让代码简单点，直接插入，依赖数据库唯一键冲突回滚掉就好了。

用唯一键挡重入是目前为止个人觉得最有安全感的方式。当然对数据库会有一些额外性能损耗。问题就变成了有多大的并发，其中又有多大是需要重试的？

我相信Fasion IO卡+分库分表之后，想达到数据库性能瓶颈还是有点难度的（主要是针对金融类场景）。

## 三、后记

本文略虚，当然目前最终一致性没有一个放之四海而皆准的成功实践。需要大家根据不同的业务特性和发展阶段，选则适当的方式来实现。

纠结最终一致性问题，其实万恶之源是因为RPC本身会失败，会有结果不确定的情况。

隐约感觉本人职业生涯大部分时间都会跟各种失败和timeout搏斗了。

本文重点讨论利用MQ实现最终一致性。主要原因有：

\1. 目前市面上的MQ都相对非常强大，几乎都号称可以做到不丢数据。相信未来对事务消息应该也会更加普及。

\2. 异步化几乎是不同处理能力（响应时间、吞吐量）和稳定性（99.99%的服务依赖99.9%的服务）的服务之间解耦的毕竟之路。

当然前面的讨论还很浅显。能力有限，希望能够不断完善此文，请各位看到的客观不吝赐教。

下一篇，希望能够跟大家share一下，最近在做的一个项目。其主要目的利用现有还未支持事务消息的MQ，在业务层实现类事务消息逻辑，并且尽量不让代码变成一坨。



本人在知乎处女文，会有人看到吗？

编辑于 2018-02-07

分布式一致性

分布式事务

交易系统











### 推荐阅读



# 深入理解分布式事务

1、什么是分布式事务分布式事务就是指事务的参与者、支持事务的服务器、资源服务器以及事务管理器分别位于不同的分布式系统的不同节点之上。以上是百度百科的解释，简单的说，就是一次大的…

bill



# IM系统的MQ消息中间件选型：Kafka还是RabbitMQ？

Jack ...发表于即时通讯技...



# Redis缓存和MySQL数据一致性方案详解

优知学院



# 分布式系统理论基础 - CAP

bangerlee

## 34 条评论



写下你的评论...







- [![uncle creepy](assets/da8e974dc_s.jpg)](https://www.zhihu.com/people/creepyuncle)

  [uncle creepy](https://www.zhihu.com/people/creepyuncle)

  2 年前

  那个大牛的补偿思路是多应用参与的广义分布式事务下效率最高的方法

  

[![itegel](assets/v2-d03803f5bc3a9d1bdf028c874328f0a2_s.jpg)](https://www.zhihu.com/people/itegel)

[itegel](https://www.zhihu.com/people/itegel)

 (作者) 

回复

[uncle creepy](https://www.zhihu.com/people/creepyuncle)

2 年前

嗯，看起来比较理想。但做起来没那么容易。需要结合业务场景，还需要考虑业务上是否能偶容忍补偿这回事儿。另外，需要解决补偿交易跟正常交易耦合的问题（至少需要存储耦合，因为注册补偿跟交易提交需要有事务保障），所以目前还没法以独立于业务的服务或中间件形式提供服务。那位大牛目前可以通过拦截器形式提供一些封装了，对业务的侵入其实还可以接受。金融领域对钱比较敏感，所以还没大范围推广。我也相信这是一条光明道路。



[![uncle creepy](https://pic4.zhimg.com/da8e974dc_s.jpg)](https://www.zhihu.com/people/creepyuncle)

[uncle creepy](https://www.zhihu.com/people/creepyuncle)

回复

[itegel](https://www.zhihu.com/people/itegel)

 (作者) 

2 年前

之前我看过ebay的那篇BASE，侵入性是很强，08年的时候能搞成广义的分布式事务，还是很强的，只是现在业务方，老是想侵入性低，又要效率高，这是做不到的



[![居尚](https://pic4.zhimg.com/da8e974dc_s.jpg)](https://www.zhihu.com/people/ju-shang-38)

[居尚](https://www.zhihu.com/people/ju-shang-38)

2 年前

看到了，谢谢。



[![itegel](https://pic4.zhimg.com/v2-d03803f5bc3a9d1bdf028c874328f0a2_s.jpg)](https://www.zhihu.com/people/itegel)

[itegel](https://www.zhihu.com/people/itegel)

 (作者) 

回复

[居尚](https://www.zhihu.com/people/ju-shang-38)

2 年前

thx:)



[![时阳](assets/384975faf_s.jpg)](https://www.zhihu.com/people/shi-yang-34-4)

[时阳](https://www.zhihu.com/people/shi-yang-34-4)

2 年前

异步确保的图最后两步怎么是  B-100，没理解。幂等那里update操作，我觉得一般数据操作都有乐观锁，已经保证不会被改两次了，update xx=xx  ,version=version+1 where version = #version  .但如果insert操作反而有问题，主键不再业务层生成的话，就会有重复。当然有log表做消息id约束是最好的。



[![itegel](https://pic4.zhimg.com/v2-d03803f5bc3a9d1bdf028c874328f0a2_s.jpg)](https://www.zhihu.com/people/itegel)

[itegel](https://www.zhihu.com/people/itegel)

 (作者) 

回复

[时阳](https://www.zhihu.com/people/shi-yang-34-4)

2 年前

图有问题，我改一下，多谢！当初不知道咋进水了……幂等你说的很对。基本都是这些思路。一般交易都有唯一ID，所以insert还好。



[![celery](https://pic4.zhimg.com/da8e974dc_s.jpg)](https://www.zhihu.com/people/celery-8-58)

[celery](https://www.zhihu.com/people/celery-8-58)

2 年前

多谢期待下一篇



[![王嘉胤](assets/8efa6ce416b23d36cb1637078af2895b_s.jpg)](https://www.zhihu.com/people/wang-jia-yin-26)

[王嘉胤](https://www.zhihu.com/people/wang-jia-yin-26)

2 年前

关于那个大牛提的事务补偿方案，如果补偿时，发现数据已经被其他操作修改了，怎么办？



[![itegel](https://pic4.zhimg.com/v2-d03803f5bc3a9d1bdf028c874328f0a2_s.jpg)](https://www.zhihu.com/people/itegel)

[itegel](https://www.zhihu.com/people/itegel)

 (作者) 

回复

[王嘉胤](https://www.zhihu.com/people/wang-jia-yin-26)

2 年前

补偿是不考虑并发问题的。所以有一些场景限制。比如机票，预定了，他的补偿就是释放该座位，不存在无法补偿的情况。比如转出资金，补偿就是转入。只要账户还存在就不会有补偿失败。但是提现到银行卡就很难补偿，银行转账成功，目前是无法取消的（似乎ATM24小时内可以）。当然那位大牛还想做的是把两阶段和补偿结合。那样，能用两阶段场景就可以补偿。



[![sungine漾离](assets/9b6e72500_s.jpg)](https://www.zhihu.com/people/sunginex)

[sungine漾离](https://www.zhihu.com/people/sunginex)

回复

[itegel](https://www.zhihu.com/people/itegel)

 (作者) 

1 年前

之前一个同事在招行的ATM机存款时，正赶上光纤中断，结果就是钱数没存到账户，钱也没从ATM机里拿出来，相当于丢了。打客服也没有用～～～～～





[![Fan Vic](assets/736454cd29fead58fca1e37d2084c613_s.jpg)](https://www.zhihu.com/people/Vic-Fan)

[Fan Vic](https://www.zhihu.com/people/Vic-Fan)

2 年前

谢谢分享



[![蓝波湾帅哥](https://pic4.zhimg.com/da8e974dc_s.jpg)](https://www.zhihu.com/people/shang-guan-shuai-ge)

[蓝波湾帅哥](https://www.zhihu.com/people/shang-guan-shuai-ge)

2 年前

2个月后看到了 谢谢



[![kdyzm](assets/49d6d4e79d3fa1e2225dd10a6770fe16_s.jpg)](https://www.zhihu.com/people/kdyzm)

[kdyzm](https://www.zhihu.com/people/kdyzm)

1 年前

虽然不怎么明白，但是感觉很厉害的样子



[![王亮](assets/v2-53d3dc47c73d7b2dc604969bdd661fd2_s.jpg)](https://www.zhihu.com/people/wang-liang-54-11-95)

[王亮](https://www.zhihu.com/people/wang-liang-54-11-95)

1 年前

[LCN](http://link.zhihu.com/?target=http%3A//www.txlcn.org) 开源的分布式事务框架 支持springcloud dubbo



[![cccc](assets/08a3094a65f16cca6271e5f3ffbe6aa2_s.jpg)](https://www.zhihu.com/people/jk-chen-99)

[cccc](https://www.zhihu.com/people/jk-chen-99)

1 年前

我是小白一个，作者我想请教一个问题，补偿和回滚有什么区别？



[![itegel](https://pic4.zhimg.com/v2-d03803f5bc3a9d1bdf028c874328f0a2_s.jpg)](https://www.zhihu.com/people/itegel)

[itegel](https://www.zhihu.com/people/itegel)

 (作者) 

回复

[cccc](https://www.zhihu.com/people/jk-chen-99)

1 年前

补偿是老板给你发了年终奖，后悔了，又从你下个月工资扣回去了。回滚是老板说给你涨薪10000，又反悔了，在发工资前又调回去了…



[![小愤青](assets/v2-d35fb5f4ba88b59b653119a9da9bde1c_s.jpg)](https://www.zhihu.com/people/MrChen1993)

[小愤青](https://www.zhihu.com/people/MrChen1993)

回复

[cccc](https://www.zhihu.com/people/jk-chen-99)

8 个月前

因为事务是原子性的,回滚是事务提交前,不提交.导致数据库并没有修改过.补偿的话是数据库已经修改过一次,然后再做一次相反的操作,从而达到前一次的修改看起来没有执行,实际上已经进行过两次修改了



[![lookas2001](assets/a15f8fd6dbcf75eed6673c75191ba887_s.jpg)](https://www.zhihu.com/people/lookas2001)

[lookas2001](https://www.zhihu.com/people/lookas2001)

1 年前

想了一个小小的问题

事物顺序问题

用户u1有着1000元

此时u1通过数据库服务器db1发出了给u2 1000元的要求，此时db1完成了这个操作，并且投递消息到消息队列中了。

但是u1是一个非常快的用户，紧接着他通知了他的朋友使用他的账户在远在美国（高延迟网络）也给u3发出了1000元的要求，而在美国的db2还没有收到消息队列告诉的u1需要减1000元并且给u2加1000元的消息。然后db2就傻乎乎的减了两次1000元，然后db2发出的事物同步消息导致。。。所有的db都傻乎乎的减了两次1000元。。。

于是u1就很赚了。



[![疯梨大叔](assets/v2-fe6ee8e831003e97f810fa9409dabd30_s.jpg)](https://www.zhihu.com/people/wanghongji)

[疯梨大叔](https://www.zhihu.com/people/wanghongji)

回复

[lookas2001](https://www.zhihu.com/people/lookas2001)

1 年前

现实中不会有这种场景的，因为看你描述的db1处理完异步通知db2，所以db1和db2在业务上是不同角色的，所以不可能两种角色提供了相同的扣款服务接口的。
我觉得你把分布式集群中各节点之间的同步，跟文章中说的分布式业务搞混了。
好那咱们说说同一个集群的提供相同业务的节点的情况：你如果要求多节点都可以写入，那它写入时的多节点备份就得是同步的；你如果要求备份写入异步，则就只能一个节点提供写入服务，其他节点只允许读了。



[![很激动](https://pic4.zhimg.com/da8e974dc_s.jpg)](https://www.zhihu.com/people/hen-ji-dong-57)

[很激动](https://www.zhihu.com/people/hen-ji-dong-57)

回复

[lookas2001](https://www.zhihu.com/people/lookas2001)

1 年前

这问题设想就不成立哈，因为就算在美国的U3也要先到db1先去查询用户是否有钱，有多少，更新操作都会先查询的



[![东方景腾](https://pic4.zhimg.com/da8e974dc_s.jpg)](https://www.zhihu.com/people/dong-fang-jing-teng)

[东方景腾](https://www.zhihu.com/people/dong-fang-jing-teng)

1 年前

笔者发文的时候，RocketMQ早就开源了的吧。现在版本都到4.x了



[![itegel](https://pic4.zhimg.com/v2-d03803f5bc3a9d1bdf028c874328f0a2_s.jpg)](https://www.zhihu.com/people/itegel)

[itegel](https://www.zhihu.com/people/itegel)

 (作者) 

回复

[东方景腾](https://www.zhihu.com/people/dong-fang-jing-teng)

1 年前

有一种开源叫阉割版……



[![陈连杰](assets/be8c5d9ee47fb7259a56a572873733a9_s.jpg)](https://www.zhihu.com/people/chen-lian-jie-6)

[陈连杰](https://www.zhihu.com/people/chen-lian-jie-6)

1 年前

幂等最终还是要引入分布式吧。



[![Alan](assets/v2-71a9ab8e50c7cdb3502c39ca46b21daa_s.jpg)](https://www.zhihu.com/people/alan-77-33)

[Alan](https://www.zhihu.com/people/alan-77-33)

1 年前

其实成功也好，失败也好，都容易处理，怕的就是超时，不知道成功还是失败；之前也是没想到完美的解决方案，之前采用的方式   1.查询验证，2.执行提交，超时发邮件给开发人员，3，定期对账，有问题人工介入；好在失败几率不是那么高，一天都碰不到一单；不过有次一个服务出了问题，出现n多超时.........人工刷数据差点累死；看过2段式啥的，感觉都不是那么靠谱，要么就是实用场景和实现复杂度太高，网络通信确实不可靠，还是三次握手问题，求解决方案..



[![apodemakeles](assets/ef81a7110_s.jpg)](https://www.zhihu.com/people/apodemakeles)

[apodemakeles](https://www.zhihu.com/people/apodemakeles)

1 年前

求问，异步确保那里，提供给B的确认接口，要怎么设计才能降低耦合度？
说通俗点就是，假如这个模式下不只有B，还有C，D...，现在可以说BCD都依赖于A吧？那A的这个确认功能要怎么设计，才可以不知道BCD的具体存在呢？



[![itegel](https://pic4.zhimg.com/v2-d03803f5bc3a9d1bdf028c874328f0a2_s.jpg)](https://www.zhihu.com/people/itegel)

[itegel](https://www.zhihu.com/people/itegel)

 (作者) 

回复

[apodemakeles](https://www.zhihu.com/people/apodemakeles)

1 年前

好问题！一般来说我们其实很多时候都不太需要确认的，每天对个账，大部分问题都解决了。比如写到MQ后，生产方就不管了，消费者自己订阅，自己消费，应该是最松散的耦合了。
如果一定要确认，一般是对数据达到一致状态的时效性要求比较高的场景。那多方确认跟1方确认，我觉得没有本质区别，只需要标记多个状态就好，这个可以通过配置等方式灵活处理。至于解耦这个事情，我觉得没必要太迷信，关键还是要看业务需要。具体实现时多考虑依赖反转，比消除依赖可能更可行。比如我们搞个依赖注册中心，通过下游主动注册依赖的方式，很多时候可能会更合理(像Spring  IOC？)。



[![apodemakeles](https://pic1.zhimg.com/ef81a7110_s.jpg)](https://www.zhihu.com/people/apodemakeles)

[apodemakeles](https://www.zhihu.com/people/apodemakeles)

1 年前

呃...首先感谢回复，不过可能说岔了。

用直白一点的描述更容易沟通

我理解的文章中的异步确保：

1. 有个用户转账，调用A，A写DB，写MQ
2. B消费MQ，写DB，调用A confirm接口，A的confirm接口其实是修改DB中针对这条转账的记录。一旦调用，转账记录里有个字段是complete, 就置为true
3. A定时扫描，发现一定时间之后complete字段不是true的，重新发MQ

细节可能不一样，原理是否理解对了？如果理解对了，继续下个问题：

假设还消费MQ的还有C,D, 那confirm接口以及DB中的complete字段要怎么实现呢？

难道要设计出complete B complete C complete D三个字段？这就相当于A知道BCD的存在了

还是您说的注册中心其实是A事务的注册中心，由A实现，对于转账功能，如果BCD服务要实现异步确保，则在上生产环境前要去A那里注册一下，注册信息为服务名+业务操作？

（这么说来确实是个好办法，而且很适合提成一个中间件或者基础服务M进行托管，但为了解决A写DB和写M的事务问题，就要M和A之间有一种确保...呃...这不就是文中介绍的RocketMQ么？）



[![itegel](https://pic4.zhimg.com/v2-d03803f5bc3a9d1bdf028c874328f0a2_s.jpg)](https://www.zhihu.com/people/itegel)

[itegel](https://www.zhihu.com/people/itegel)

 (作者) 

回复

[apodemakeles](https://www.zhihu.com/people/apodemakeles)

1 年前

我觉得我们俩基本思路是一致的。

1.表设计不一定要设计成多个字段。可以增加个表，专门用于记录下游信息和消息到达状态。只要跟消息本身在一个库里就有事务保证了。

\2.   MQ跟DB是没有事务保证的。只有A的业务记录跟消息记录(落库)之间是有事务保障的。所以MQ可以不要。完全通过扫表来解决你的需求。当然反过来，如果MQ的稳定性能满足要求，业务上又能容忍较长时间不一致，那么DB里的消息记录也可以不需要，消息只要发送到MQ就认为万事大吉就好了。所以也不需要下游给A发送确认消息了。只需要每天(一般是天级，当然你也可以设计成更短时间)上下游之间做对账就好。



[![sungine漾离](https://pic4.zhimg.com/9b6e72500_s.jpg)](https://www.zhihu.com/people/sunginex)

[sungine漾离](https://www.zhihu.com/people/sunginex)

1 年前

很完善的一篇入门文,不知道国际上有没有更好的方案



[![Chris Yuan](assets/0102eb1956d41065f01bdeb719c73935_s.jpg)](https://www.zhihu.com/people/Chris-yuan)

[Chris Yuan](https://www.zhihu.com/people/Chris-yuan)

回复

[sungine漾离](https://www.zhihu.com/people/sunginex)

12 天前

有了 



[![慢死](https://pic4.zhimg.com/da8e974dc_s.jpg)](https://www.zhihu.com/people/man-si-15-96)

[慢死](https://www.zhihu.com/people/man-si-15-96)

9 个月前

虽然不怎么明白，但是感觉很厉害的样子



[![蓝色天空](https://pic4.zhimg.com/da8e974dc_s.jpg)](https://www.zhihu.com/people/lan-se-tian-kong-32-46)

[蓝色天空](https://www.zhihu.com/people/lan-se-tian-kong-32-46)

4 个月前

感谢博主分享,另一篇也不错,讲的挺透彻:[分布式事务 CAP 理解论证 解决方案](http://link.zhihu.com/?target=https%3A//blog.csdn.net/weixin_40533111/article/details/85069536)