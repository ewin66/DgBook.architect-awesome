# docker上搭建consul集群全流程



 		[docker上搭建consul集群全流程](https://www.cnblogs.com/miaoying/p/10303067.html) 	

consul简介：

consul是提供服务发现、简单配置管理、分区部署的服务注册发现解决方案。
主要特性：服务发现\健康检查\基于Key-Value的配置\支持TLS安全通讯\支持多数据中心部署

consul的实例叫agent
agent有两种运行模式：server和client
每个数据中心至少要有一个server，一般推荐3-5个server（避免单点故障）
client模式agent是一个轻量级进程，执行健康检查，转发查询请求到server。
服务service是注册到consul的外部应用，比如spring web server

consul架构：

![img](assets/813379-20190122130704613-1359978704.png)

 

\1. 在docker上安装consul（默认安装最新版本）

```
docker pull consul
```

\2. 启动第一个consul服务：consul1

docker run --name consul1 -d -p 8500:8500 -p 8300:8300 -p 8301:8301 -p 8302:8302 -p 8600:8600 consul:1.2.2 agent -server -bootstrap-expect 2 -ui -bind=0.0.0.0 -client=0.0.0.0



```
docker run --name consul1 -d -p 8500:8500 -p 8300:8300 -p 8301:8301 -p 8302:8302 -p 8600:8600 consul:1.2.2 agent -server -bootstrap-expect 2 -ui -bind=0.0.0.0 -client=0.0.0.0
```



docker run -d --name consul server1 --net=host -e'CONSUL_LOCAL_CONFIG={"skip_leave_on_interrupt": true}' consul agent-server -bind=10.10.10.79 -bootstrap -expect=1  -client0.0.0.0 –ui



8500 http 端口，用于 http 接口和 web ui
8300 server rpc 端口，同一数据中心 consul server 之间通过该端口通信
8301 serf lan 端口，同一数据中心 consul client 通过该端口通信
8302 serf wan 端口，不同数据中心 consul server 通过该端口通信
8600 dns 端口，用于服务发现
-bbostrap-expect 2: 集群至少两台服务器，才能选举集群leader
-ui：运行 web 控制台
-bind： 监听网口，0.0.0.0 表示所有网口，如果不指定默认未127.0.0.1，则无法和容器通信
-client ： 限制某些网口可以访问

\3. 获取 consul server1 的 ip 地址

```
docker inspect --format '{{ .NetworkSettings.IPAddress }}' consul1
```

输出是：172.17.0.2

\4. 启动第二个consul服务：consul2， 并加入consul1（使用join命令）

```
docker run --name consul3 -d -p 8501:8500 consul agent -server -ui -bind=0.0.0.0 -client=0.0.0.0 -join 172.17.0.2
```

\5. 启动第三个consul服务：consul3，并加入consul1

```
docker run --name consul3 -d -p 8502:8500 consul agent -server -ui -bind=0.0.0.0 -client=0.0.0.0 -join 172.17.0.2
```

\6. 目前我启动了5个consul服务，然后stop掉了两个，详情如下图所示：

![img](assets/813379-20190122112241297-178907747.png)

 

\7. 宿主机浏览器访问：http://localhost:8500 或者 http://localhost:8501 或者 http://localhost:8502

（由于我一开始启动了5个consul服务，然后stop掉了两个，所以我的控制台如下所示）

![img](assets/813379-20190122112118079-929545958.png)

\8. 任意stop掉其中一个consul，只要剩余consul数目大于等于两个，宿主机就能正常访问对应的链接；

\9. 创建test.json文件，以脚本形式注册服务到consul：

test.json文件内容如下：

[![复制代码](assets/copycode.gif)](javascript:void(0);)

```
{
    "ID": "test-service1",
    "Name": "test-service1",
    "Tags": [
        "test",
        "v1"
    ],
    "Address": "127.0.0.1",
    "Port": 8000,
    "Meta": {
        "X-TAG": "testtag"
    },
    "EnableTagOverride": false,
    "Check": {
        "DeregisterCriticalServiceAfter": "90m",
        "HTTP": "http://zhihu.com",
        "Interval": "10s"
    }
}
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

通过 http 接口注册服务（端口可以是8500. 8501， 8502等能够正常访问consul的就行）：

```
curl -X PUT --data @test.json http://localhost:8500/v1/agent/service/register
```

控制台如下所示：

![img](assets/813379-20190122114229522-1854264926.png)

 

 \10. 宿主机浏览器访问以下链接可以看到所有通过健康检查的可用test-server1服务列表

（任意正常启动consul的端口皆可）：

```
http://localhost:8501/v1/health/service/test-server1?passing
```

 输出json格式的内容，如下所示：

![img](assets/813379-20190122115433596-1166178746.png)

其它应用程序可以通过这种方式轮询获取服务列表，这就是微服务能够动态知道其依赖微服务可用列表的原理。

\11. 解绑定：

```
curl -i -X PUT http://127.0.0.1:8501/v1/agent/service/deregister/test-server1
```

\12. 集群方式需要至少启动两个consul server，本机调试web应用时，为了方便可以用 -dev 参数方式仅启动一个consul server

```
docker run --name consul0 -d -p 8500:8500 -p 8300:8300 -p 8301:8301 -p 8302:8302 -p 8600:8600 consul:1.2.2 agent -dev -bind=0.0.0.0 -client=0.0.0.0
```

 







标签: [docker](https://www.cnblogs.com/miaoying/tag/docker/), [consul](https://www.cnblogs.com/miaoying/tag/consul/)



---



#	 			[Docker 容器部署 Consul 集群](https://www.cnblogs.com/lxsky/p/5276067.html) 		


Docker 容器部署 Consul 集群

**一、docker安装与启动**
**1.1安装docker**
[root@localhost /]# yum -y install docker-io

**1.2更改配置文件**
[root@localhost /]# vi /etc/sysconfig/docker
other-args列更改为：other_args="--exec-driver=lxc --selinux-enabled"

**1.3启动docker服务**
[root@localhost /]# service docker start
Starting cgconfig service:                                 [  OK  ]
Starting docker:                                           [  OK  ]

**1.4将docker加入开机启动**
[root@localhost /]# chkconfig docker on

**1.5基本信息查看**
docker version：查看docker的版本号，包括客户端、服务端、依赖的Go等

[root@localhost /]# docker version
Client version: 1.0.0
Client API version: 1.12
Go version (client): go1.2.2
Git commit (client): 63fe64c/1.0.0
Server version: 1.0.0
Server API version: 1.12
Go version (server): go1.2.2
Git commit (server): 63fe64c/1.0.0

docker info ：查看系统(docker)层面信息，包括管理的images, containers数等
[root@localhost /]# docker info
Containers: 16
Images: 40
Storage Driver: devicemapper
 Pool Name: docker-253:0-1183580-pool
 Data file: /var/lib/docker/devicemapper/devicemapper/data
 Metadata file: /var/lib/docker/devicemapper/devicemapper/metadata
 Data Space Used: 2180.4 Mb
 Data Space Total: 102400.0 Mb
 Metadata Space Used: 3.4 Mb
 Metadata Space Total: 2048.0 Mb
Execution Driver: lxc-0.9.0
Kernel Version: 2.6.32-431.el6.x86_64


**二、progrium/consul镜像安装**
**2.1搜索镜像**
[root@localhost /]# docker search consul

docker.io   docker.io/progrium/consul                                                              231                  [OK]
docker.io   docker.io/gliderlabs/consul                                                            43                   [OK]
……

**2.2下载镜像progrium/consul** 
[root@localhost /]# docker pull docker.io/progrium/consul

**2.3查看镜像**
[root@localhost /]# docker images -a   ////列出所有的images（包含历史）

**三、在Docker 容器中启动Consul Agent,这里的操作参考progrium/consul的官方说明https://hub.docker.com/r/progrium/consul/**
**3.1以Server 模式在容器中启动一个agent**
[root@localhost /]# docker run -p 8400:8400 -p 8500:8500 -p 8600:53/udp -h **node1** progrium/consul -server -bootstrap

我们测试一下，可以通过curl访问http端口:
[root@localhost /]# curl localhost:8500/v1/catalog/nodes

再测试一下，也可以通过dig访问一下 DNS 端口:
[root@localhost /]# dig @0.0.0.0 -p 8600 node1.node.consul

**3.2用Docker 容器启动Consul集群**
分别启动三个server节点，并且绑定到同一个ip
-bootstrap-expect：在一个datacenter中期望提供的server节点数目，当该值提供的时候，consul一直等到达到指定sever数目的时候才会引导整个集群

[root@localhost /]# docker run -d --name **node1** -h node1 progrium/consul -server -bootstrap-expect 3
[root@localhost /]# JOIN_IP="$(docker inspect -f '{{.NetworkSettings.IPAddress}}' **node1**)"   ////获取node1的ip地址
[root@localhost /]# docker run -d --name **node2** -h **node2** progrium/consul -server -join $JOIN_IP
[root@localhost /]# docker run -d --name **node3** -h **node3** progrium/consul -server -join $JOIN_IP
启动client节点
[root@localhost /]# docker run -d -p 8400:8400 -p 8500:8500 -p 8600:53/udp --name **node4** -h **node4** progrium/consul -join $JOIN_IP
这时使用nsenter工具连接到node1上运行consul info，可以到到node1为state = Leader

查看正在运行的容器
[root@localhost /]# docker ps
查看所有的容器（运行中和关闭的）
[root@localhost /]# docker ps -a
删除所有容器
[root@localhost /]# docker rm $(docker ps -a -q)

**3.3进入docker容器操作查看**
docker使用 -d 参数时，容器启动后会进入后台。
某些时候需要进入容器进行操作，特别是测试时。有很多种方法，包括使用ssh，docker attach命令或 nsenter工具等。

**3.3.1使用docker attach，多个窗口不方便**
[root@localhost /]# docker attach node3
2016/03/14 03:35:40 [INFO] agent: Synced service 'consul'

**3.3.2使用nsenter**
先安装

[root@localhost /]# cd /tmp
[root@localhost /]# curl https://www.kernel.org/pub/linux/utils/util-linux/v2.24/util-linux-2.24.tar.gz
[root@localhost /]# tar zxf util-linux-2.24.tar.gz
[root@localhost /]# cd util-linux-2.24
[root@localhost /]# ./configure --without-ncurses
[root@localhost /]# make nsenter
[root@localhost /]# cp nsenter /usr/local/bin                   ////不用make install，直接把make生成的nsenter 复制到/usr/local/bin目录下即可


[root@localhost /]# PID=`docker inspect --format "{{ .State.Pid }}" node1 `
[root@localhost /]# nsenter --target $PID --mount --uts --ipc --net --pid
上面2条命令可以合一：nsenter --target `docker inspect --format "{{ .State.Pid }}" node1` --mount --uts --ipc --net --pid
node1:/#             ////ok了可以随便输入linux命令检查一下看看，比如 
开始consul命令
node1:/# consul members
node1:/# consul info

 





标签: [Docker](https://www.cnblogs.com/lxsky/tag/Docker/), [Consul](https://www.cnblogs.com/lxsky/tag/Consul/), [nsenter](https://www.cnblogs.com/lxsky/tag/nsenter/)



---









#  			[consul部署多台Docker集群](https://www.cnblogs.com/gudanshiyigerendekuanghuan/p/10603516.html) 		



# Consul

1. 最近在学习Ocelot，发现里面集成Consul，所有部署一下多机版集群，后来发现网上都是在一台虚拟机中的Docker部署，而且大同小异，没有真正解释清楚。

   # 前提准备

2. 4台Centos虚拟机，本人安装VM虚拟机，用复制镜像快速搭建环境。（需要脚本的话联系我）

3. 第一台安装好后，把Docker安装好，设置docker开机启动，关掉防火墙，设置静态IP等。

4. 然后用copy虚拟机，修改ip地址后，全部启动

   > 这些操作可自行百度

   # Consul

   > 目前都是单数据中心，多数据中心后面更新，此篇仅供入门参考,如果有不对的地方欢迎指正

5. Consul分client和server模式
    

- client 负责注册服务，转发请求，没有持久化的功能 配置文件启动 ，会默认遍历所有/config/file 下的*.json文件

- server 也可以注册服务，但是推荐client，能持久化数据，存放在 /config/data下

  # 在客户端模式下运行Consul Agent

  1. `--net=host`:docker内部对于虚拟机的来说也是localhost

     > 如果主机上的其他容器也使用--net=host，这将是一个很好的配置，它还会将代理暴露给直接在容器外部的主机上运行的进程

1. `-bind`:这是给其他consul server来加入集群的ip

2. `-join`:加入集群

3. `-client`:使用此配置，Consul的客户端接口将绑定到网桥IP，并可供该网络上的其他容器使用，但不能在主机网络上使用。

   > Consul还将接受-client=0.0.0.0绑定到所有接口的选项。

4. `-bootstrap-expect`设置服务数量，当达到设定数量启动集群。-bind的这台机器成为leader

5. `-ui`管理界面

6. `-h`:设置node的名称，集群的服务器不能取同名的node名称

7. `CONSUL_BIND_INTERFACE`:ifconfig查看，好像虚拟机的这个名称不一样，我的是ens33，还有叫eth0的。

   # 需要用到的命令

8. Docker中`Consul`部署

- `docker inspect -f '{{.NetworkSettings.IPAddress}}' consul1`查看容器内Consul1的ip

- `docker exec -t consul名称 consul members` 查看集群成员

- `ifconfig` 查看ip配置

- `/sbin/ifconfig ens33 | sed -n 's/.*inet \(addr:\)\?\([0-9.]\{7,15\}\) .*/\2/p'`

- `docker run -d  --name consul1 --net=host -e  CONSUL_BIND_INTERFACE=ens33 consul agent -server=true -client=0.0.0.0  -bind=192.168.110.100 -ui -bootstrap-expect=3`  leader 服务

- `docker run -d --name consul2 -h=node1 --net=host -e  CONSUL_BIND_INTERFACE=ens33 consul agent -server=true -client=0.0.0.0  -join=192.168.110.100 -ui`  follower

- `docker run -d --name consul4 -h=node4--net=host -e  CONSUL_BIND_INTERFACE=ens33 consul agent -server=false -client=0.0.0.0  -join=192.168.110.100 -ui` client

  # 启动

  ## 现有四台虚拟机，ip地址分别是：

1. 192.168.110.100

2. 192.168.110.101

3. 192.168.110.102

4. 192.168.110.103
    在第一台服务器中运行server作为leader
    `docker run -d --name consul1 -h=node1 --net=host -e  CONSUL_BIND_INTERFACE=ens33 consul agent -server=true -client=0.0.0.0  -bind=192.168.110.100 -ui -bootstrap-expect=3`
    接下来三台台加入集群
    `docker run -d  --name consul2 -h=node2 --net=host -e  CONSUL_BIND_INTERFACE=ens33 consul agent -server=true -client=0.0.0.0  -join=192.168.110.100 -ui`
    `docker run -d  --name consul3 -h=node3 --net=host -e  CONSUL_BIND_INTERFACE=ens33 consul agent -server=true -client=0.0.0.0  -join=192.168.110.100 -ui`
    `docker run -d -v /consulconfig:/config/file --name  consul4  -h=node4 --net=host -e CONSUL_BIND_INTERFACE=ens33 consul agent  -config-dir=/config/file    -server=false -client=0.0.0.0  -join=192.168.110.100 -ui`

   > 如果需要挂载数据文件，请指定`-data-dir`

# 查看状态

- `docker exec -t consul1 consul operator raft list-peers` 查看投票状态
- `docker exec -t consul名称 consul members` 查看集群成员



