## 消息队列

- [《消息队列-推/拉模式学习 & ActiveMQ及JMS学习》](https://www.cnblogs.com/charlesblc/p/6045238.html)
  - RabbitMQ 消费者默认是推模式（也支持拉模式）。
  - Kafka 默认是拉模式。
  - Push方式：优点是可以尽可能快地将消息发送给消费者，缺点是如果消费者处理能力跟不上，消费者的缓冲区可能会溢出。
  - Pull方式：优点是消费端可以按处理能力进行拉去，缺点是会增加消息延迟。
- [《Kafka、RabbitMQ、RocketMQ等消息中间件的对比 —— 消息发送性能和区别》](https://blog.csdn.net/yunfeng482/article/details/72856762)

### 消息总线

消息总线相当于在消息队列之上做了一层封装，统一入口，统一管控、简化接入成本。

- [《消息总线VS消息队列》](https://blog.csdn.net/yanghua_kobe/article/details/43877281)

### 消息的顺序

- [《如何保证消费者接收消息的顺序》](https://www.cnblogs.com/cjsblog/p/8267892.html)

### RabbitMQ

支持事务，推拉模式都是支持、适合需要可靠性消息传输的场景。

- [《RabbitMQ的应用场景以及基本原理介绍》](https://blog.csdn.net/whoamiyang/article/details/54954780)
- [《消息队列之 RabbitMQ》](https://www.jianshu.com/p/79ca08116d57) 
- [《RabbitMQ之消息确认机制（事务+Confirm）》](https://blog.csdn.net/u013256816/article/details/55515234)

### RocketMQ

Java实现，推拉模式都是支持，吞吐量逊于Kafka。可以保证消息顺序。

- [《RocketMQ 实战之快速入门》](https://www.jianshu.com/p/824066d70da8)
- [《RocketMQ 源码解析》](http://www.iocoder.cn/categories/RocketMQ/?vip&architect-awesome)

### ActiveMQ

纯Java实现，兼容JMS，可以内嵌于Java应用中。

- [《ActiveMQ消息队列介绍》](https://www.cnblogs.com/wintersun/p/3962302.html)
- ActiveMQ[[教程]](https://www.e-learn.cn/content/java/487993)

### Kafka

高吞吐量、采用拉模式。适合高IO场景，比如日志同步。

- [官方网站](http://kafka.apache.org/)
- [《各消息队列对比，Kafka深度解析，众人推荐，精彩好文！》](https://blog.csdn.net/allthesametome/article/details/47362451)
- [《Kafka分区机制介绍与示例》](http://lxw1234.com/archives/2015/10/538.htm)

### Redis 消息推送

生产者、消费者模式完全是客户端行为，list 和 拉模式实现，阻塞等待采用 blpop 指令。

- [《Redis学习笔记之十：Redis用作消息队列》](https://blog.csdn.net/qq_34212276/article/details/78455004)

### ZeroMQ

 TODO