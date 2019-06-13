

Hangfire



Hangfire包含三大核心组件：客户端、持久化存储、服务端，官方的流程介绍图如下：

![hangfire-workflow](assets/41545-20161220061245103-587175152.png)





从图中可以看出，这三个核心组件是可以分离出来单独部署的，例如可以部署多台Hangfire服务，提高处理后台任务的吞吐量。关于任务持久化存储，支持Sqlserver，MongoDb，Mysql或是Redis等等。











![img](assets/41545-20161220061513182-418802853.png)







![img](assets/41545-20161220061420728-850974488.png)















