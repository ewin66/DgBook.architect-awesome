

docker run --name consul1 -d -p 8500:8500 -p 8300:8300 -p 8301:8301 -p 8302:8302 -p 8600:8600 consul:1.2.2 agent -server -bootstrap-expect 2 -ui -bind=0.0.0.0 -client=0.0.0.0







```
docker run -d --name consul server1 --net=host -e'CONSUL_LOCAL_CONFIG={"skip_leave_on_interrupt": true}' consul agent-server -bind=10.10.10.79 -bootstrap -expect=1  -client0.0.0.0 –ui

--------------------- 
作者：yinwaner 
来源：CSDN 
原文：https://blog.csdn.net/yinwaner/article/details/80762757 
版权声明：本文为博主原创文章，转载请附上博文链接！
```







docker	run -d --name consul server1 --net=host -e'CONSUL_LOCAL_CONFIG={"skip_leave_on_interrupt": true}' consul agent-server -bind=10.10.10.79 -bootstrap-expect=1  -client0.0.0.0 –ui





 docker run -p 8400:8400 -p 8500:8500 -p 8600:53/udp -h **node1** progrium/consul -server -bootstrap -ui



#### 配置consul-server

启动第一台 consul-server，非常简单，一条命令搞定，这就是docker的魅力。

```
$ docker run -h node1  --name consul -d -v /data:/data --restart=always\
    -p   8300:8300 \
    -p   8301:8301 \
    -p   8301:8301/udp \
    -p   8302:8302 \
    -p   8302:8302/udp \
    -p   8400:8400 \
    -p   8500:8500 \
progrium/consul -server \
-bootstrap-expect 3 \
-advertise 192.168.99.100
```

下面来解释下各个参数
 -h 节点名字
 --name 容器（container）名称，后期用来方便启动关闭，看日志等，这个一定要写
 -d 后台运行
 -v /data:/data 使用宿主机的/data目录映射到容器内部的/data,用于保存consul的注册信息，要不docker 一重启，数据是不保留的。
 --restart=always  这个可以活得长一点
 下面几个参数都是consul集群用的，非集群模式可以不使用。
 -p   8300:8300 
 -p   8301:8301 
 -p   8301:8301/udp 
 -p   8302:8302 
 -p   8302:8302/udp \



```
docker run --name consul1 -d -p 8500:8500 -p 8300:8300 -p 8301:8301 -p 8302:8302 -p 8600:8600 consul: agent -server -bootstrap-expect 2 -ui -bind=0.0.0.0 -client=0.0.0.0
```



```
docker run --name consul1 -d -p 8500:8500 -p 8300:8300 -p 8301:8301 -p 8302:8302 -p 8600:8600 consul agent -server -bootstrap-expect 2 -ui -bind=0.0.0.0 -client=0.0.0.0
```

docker run -h node1  --name consul -d -v /data:/data --restart=always\  -p   8300:8300     -p   8301:8301  -p   8301:8301/udp      -p   8302:8302    -p   8400:8400    -p   8500:8500   progrium/consul -server \   -bootstrap-expect 3 \   -advertise 192.168.99.100





docker run --name consul1 -d -p 8500:8500 -p 8300:8300 -p 8301:8301 -p 8302:8302 -p 8600:8600 consul agent -server -bootstrap-expect 2 -ui -bind=0.0.0.0 -client=0.0.0.0



 		[docker上搭建consul集群全流程](https://www.cnblogs.com/miaoying/p/10303067.html) 	

consul简介：

consul是提供服务发现、简单配置管理、分区部署的服务注册发现解决方案。
主要特性：服务发现\健康检查\基于Key-Value的配置\支持TLS安全通讯\支持多数据中心部署

consul的实例叫agent
agent有两种运行模式：server和client
每个数据中心至少要有一个server，一般推荐3-5个server（避免单点故障）
client模式agent是一个轻量级进程，执行健康检查，转发查询请求到server。
服务service是注册到consul的外部应用，比如spring web server





https://www.cnblogs.com/miaoying/p/10303067.html

```
docker pull consul
docker run --name consul1 -d -p 8500:8500 -p 8300:8300 -p 8301:8301 -p 8302:8302 -p 8600:8600 consul agent -server -bootstrap-expect 2 -ui -bind=0.0.0.0 -client=0.0.0.0
docker inspect --format '{{ .NetworkSettings.IPAddress }}' consul1
docker run --name consul2 -d -p 8501:8500 consul agent -server -ui -bind=0.0.0.0 -client=0.0.0.0 -join 172.17.0.2
docker run --name consul3 -d -p 8502:8500 consul agent -server -ui -bind=0.0.0.0 -client=0.0.0.0 -join 172.17.0.2


```