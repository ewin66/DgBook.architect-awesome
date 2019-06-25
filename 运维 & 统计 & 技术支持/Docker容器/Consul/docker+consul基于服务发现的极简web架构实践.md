# docker+consul基于服务发现的极简web架构实践



https://www.jianshu.com/p/d16a1bea5cbb



自从docker的出现，web架构方式也出现了新的变化。这几年一直在关注docker，但是从未实践过。最近有点时间，决定实践下，用以评估今后架构转型的可行性。

写本文的目的是，分享自己的实践，希望能让初学者能少走点冤枉路。

这里就不再千篇一律的讲什么docker的原理的，既然是初学者，那些说实话没什么用。初学者更需要的是通过实践，窥到docker的外形。

为保持精炼，文章会隐藏一些细节，如果不明白，请留言。

实践过程中，发现docker这个大玩具，知识点还是非常多的。

所以在实践前做了一些知识储备：

1. Docker — 从入门到实践：[https://yeasy.gitbooks.io/docker_practice/content/introduction/what.html](https://link.jianshu.com?t=https://yeasy.gitbooks.io/docker_practice/content/introduction/what.html)
    这是一本好书，让我缩短了知识储备的时间，非常建议初学者还是先仔细研读。
    不过细节部分讲的不是特别细，很多知识还是得各种查资料。
2. Docker文档：[https://docs.docker.com/](https://link.jianshu.com?t=https://docs.docker.com/)
    查资料用的
3. consul文档：[https://www.consul.io/docs/guides/index.html](https://link.jianshu.com?t=https://www.consul.io/docs/guides/index.html)
    如果你选择consul，这个是必读的，这家伙比传说中的复杂多了。
4. consul的docker镜像:[https://hub.docker.com/r/progrium/consul/](https://link.jianshu.com?t=https://hub.docker.com/r/progrium/consul/)
    包装consul的镜像，简化了consul的部署，这也是docker的魅力。
5. 《Nodejs微服务架构》
    比较务实的一本书，从书里找到了一些灵感。比如Seneca可能是下一步会做的技术选型，去集中化的方式避免了服务发现的繁琐。

## 架构

下面废话不多说，架构大概是这样的。很简单，没有那种高来高去的东西，新手需要的是情境无关。



![img](https:////upload-images.jianshu.io/upload_images/698777-c510cf3779979cdc.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1000)

image.png

### 对于架构的考虑：

1. 首先架构应该是，简单的，可迭代演进的。这样可操作性和可维护性会更强。
2. 谈架构，无外乎就是高可用和可扩展，脱离这两个都是耍流氓。还有就是省钱，动不动20台服务器，创业公司伤不起。所以，解决好了就是好架构。
3. 监控方案是后续迭代演进的事，你必须要保证你的系统正常运转，才能缩短开发周期。留出更多的时间，你可以做这些重要的事。
4. 关于负载均衡器，有很多备选方案，现在云服务这么发达，可选的方案也很多，甚至有跨机房的负载均衡。比自己搭nginx+keepalived要方便的多。
5. 选择consul，用于服务发现，解决的是服务互访的问题。
6. 没有集群方案，这里没有考虑使用集群方案，比如swarm之类的，在实践过程中，发现配置过于繁琐，更不用说zookeeper了。
7. 为什么没有使用consul-template，虽然很巧妙，但是这两个服务集成的耦合度过高。nginx模板配置繁琐，这会极大增加运维成本。不太像是一个适用与生产环境的成熟方案。

### 架构原理

第一步，所有应用启动之后会向consu集群注册自己，注册的信息包括

- 所属数据中心 DC1
- 所属数据中心的宿主机节点
- 所属节点的服务，服务访问方式ip，端口

如何注册？很多方式，比较简单的方式是，应用在启动的时候往consul 注册Api发送注册服务信息。
 可以使用shell或者应用程序来发送。比如nodejs 可以引入node-consul库来发送注册信息。

后期consul会负责服务节点的健康检查。

第二步，当应用间存在访问时，如Api网关访问微服务，web应用访问微服务，微服务之间互访。这里可以使用consul Api定期请求服务状态的方式，来获取可用的节点，后面会详细介绍。请求到节点后还可以在应用程序级别做一些负载均衡策略。没有使用dns的原因的，dns使用起来也不是很方便，配置起来也很繁琐。

## 基础工作

### 安装docker

首先是安装docker，mac下安装很简单，其他环境除了安装过程不一样，后续基本一样。
 [https://docs.docker.com/docker-for-mac/](https://link.jianshu.com?t=https://docs.docker.com/docker-for-mac/)
 安装之后有GUI界面可以用，可以让新手快速使用起来。



![img](https:////upload-images.jianshu.io/upload_images/698777-030734ea147bfac3.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/682)

image.png

### 安装后的配置

如果你还没有注册 docker hub.，按提示注册即可。
 登陆完后，进入Preferences...
 添加阿里云docker hub镜像：[https://45599kaw.mirror.aliyuncs.com](https://link.jianshu.com?t=https://45599kaw.mirror.aliyuncs.com)
 也可以自己注册一个阿里云，开通容器云服务，镜像是免费送的。



![img](https:////upload-images.jianshu.io/upload_images/698777-168a9834760378fd.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/760)

image.png

打开终端，实验下是否成功

```
$ docker search nginx
```

出现登陆提示：
 登陆请注意，登录名使用注册时的用户名，千万别用邮箱。

### 安装虚拟机

安装VirtualBox
 作为实验性项目，使用VirutalBox可以快速构建你想要的物理环境，而且docker和virtualbox搭配的很好，使用docker-machine可以非常简单的管理所有虚拟机。

## 开始

好了，万事具备。现在我们开始创建虚拟机。

使用docker工具包自带的 docker-machine工具，可以帮你快速创建一个docker宿主机。

在这个架构中，我们一共只需要创建3台宿主机

docker-machine命令后面会用的比较频繁，所以我们改个短点的名字。

这里我用zsh，bash类似。

```
$ vi ~/.zshrc
#增加
alias dm="docker-machine”
```

依次创建3台虚拟机
 `$ dm create -d "virtualbox” node1`
 `$ dm create -d "virtualbox” node2`
 `$ dm create -d "virtualbox" node3`

ip是自动分配的，不出意外的话，会得到下面对应的ip（如果真出意外了，就改改ip吧）。

宿主机 node1: 192.169.99.100
 宿主机 node2: 192.169.99.101
 宿主机 node3: 192.169.99.102

```
$ dm ls
NAME    ACTIVE   DRIVER       STATE     URL                         SWARM   DOCKER        ERRORS
node1   -        virtualbox   Running   tcp://192.168.99.100:2376           v17.06.0-ce
node2   -        virtualbox   Running   tcp://192.168.99.101:2376           v17.06.0-ce
node3   -        virtualbox   Running   tcp://192.168.99.102:2376           v17.06.0-ce
```

### 第一台宿主机配置

宿主机node1
 我们新开一个终端
 `$ dm ssh node1`
 这个命令可以快速登入node1宿主机

```
$ sudo vi /etc/docker/daemon.json
{
  "experimental" : true,
  "registry-mirrors" : [
    "https://45599kaw.mirror.aliyuncs.com"
  ]
}
```

虽然可以架设regsiter私服，但是使用起来的麻烦程度，远远超过重复下载带来的代价。所以不用纠结了，就这么整，非常简单。

改完之后，重启docker

```
$ sudo /etc/init.d/docker restart
```

执行命令后有提示错误，不用理会。

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

progrium/consul 镜像名称，本地没有就自动从公共docker库下载
 后面的都是consul的参数：
 -server \  以服务节点启动
 -bootstrap-expect 3 \ 预期的启动节点数3，最少是3，要不达不到cluster的效果
 -advertise 192.168.99.100  告诉集群，我的ip是什么，就是注册集群用的

执行完毕后 ，使用docker ps看下，是否运行正常。docker logs就不用看了，里面各种警告和错误，其实那都是假象。

但是consul cluster你必须明白，只有3个consul-server节点都启动正常了，整个集群才能正常启动。
 打开  http://192.168.99.100:8500/
 但是你看不到下面的consul标签，因为集群还没有都起来。





![img](https:////upload-images.jianshu.io/upload_images/698777-a3c74de8be597b98.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1000)



### 配置下一台consul-server

开启一个新的终端

```
$ dm ssh node2  #进入node2宿主机
```

增加daemon.json，重启docker，不再赘述。

```
$ docker run -h node2 --name consul -d -v /data:/data   --restart=always\
    -p   8300:8300 \
    -p   8301:8301/udp \
    -p   8302:8302 \
    -p   8302:8302/udp \
    -p   8400:8400 \
    -p   8500:8500 \
progrium/consul -server \
-advertise 192.168.99.101 \
-join  192.168.99.100
```

这里多了一个参数
 -join  192.168.99.100 代表的是加入node1建立好的consul-server

好，已经加入了，但是集群还是没有完备。

用同样的方法配置 最后一台
 新开终端进入node3

```
$ docker run -h node3 --name consul -d -v /data:/data   --restart=always\
    -p   8300:8300 \
    -p   8301:8301/udp \
    -p   8302:8302 \
    -p   8302:8302/udp \
    -p   8400:8400 \
    -p   8500:8500 \
progrium/consul -server \
-advertise 192.168.99.102 \
-join  192.168.99.100
```

### consul配置完毕

检查是否成功
 没出意外的话，就看到下面的界面
 http://192.168.99.100:8500/





![img](https:////upload-images.jianshu.io/upload_images/698777-1d1000a1f205c5e4.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1000)



但是我有预感，意外的可能性比较大。如果不成功，可以留言，这里面细节比较多。。。

## 开始启动应用

这里拿最简单的nginx服务作为演示，情境无关。

### 启动nginx

进入3个节点
 执行
 $ docker run -d -p 80:80 --name nginx nginx

在node1，node2,node3中， 分别执行以下命令

```
$ curl -X PUT -d '{"id": "nginx","name": "nginx","address": "192.168.99.100","port": 80,"checks": [{"http": "http://192.168.99.100/","interval": "5s"}]}' http://127.0.0.1:8500/v1/agent/service/register
$ curl -X PUT -d '{"id": "nginx","name": "nginx","address": "192.168.99.101","port": 80,"checks": [{"http": "http://192.168.99.101/","interval": "5s"}]}' http://127.0.0.1:8500/v1/agent/service/register
$ curl -X PUT -d '{"id": "nginx","name": "nginx","address": "192.168.99.102","port": 80,"checks": [{"http": "http://192.168.99.102/","interval": "5s"}]}' http://127.0.0.1:8500/v1/agent/service/register
```

好了，我们启动了3个nginx，并将它们都注册到了consul.



![img](https:////upload-images.jianshu.io/upload_images/698777-9784e2bbd45de52e.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1000)



## 健康检查

### 获取状态

http 命令工具 httpie，可以自行安装，替代curl，可以高亮格式化返回的json

$ http [http://192.168.99.100:8500/v1/health/checks/nginx](https://link.jianshu.com?t=http://192.168.99.100:8500/v1/health/checks/nginx)

HTTP/1.1 200 OK
 Content-Type: application/json
 Date: Sun, 30 Jul 2017 11:41:59 GMT
 Transfer-Encoding: chunked
 X-Consul-Index: 20
 X-Consul-Knownleader: true
 X-Consul-Lastcontact: 0

```
[    
    {
        "CheckID": "service:nginx",
        "Name": "Service 'nginx' check",
        "Node": "node1",
        "Notes": "",
        "Output": "HTTP GET http://192.168.99.100/: 200 OK Output: <!DOCTYPE html>\n<html>\n<head>\n<title>Welcome to nginx!</title>\n<style>\n    body {\n        width: 35em;\n        margin: 0 auto;\n        font-family: Tahoma, Verdana, Arial, sans-serif;\n    }\n</style>\n</head>\n<body>\n<h1>Welcome to nginx!</h1>\n<p>If you see this page, the nginx web server is successfully installed and\nworking. Further configuration is required.</p>\n\n<p>For online documentation and support please refer to\n<a href=\"http://nginx.org/\">nginx.org</a>.<br/>\nCommercial support is available at\n<a href=\"http://nginx.com/\">nginx.com</a>.</p>\n\n<p><em>Thank you for using nginx.</em></p>\n</body>\n</html>\n",
        "ServiceID": "nginx",
        "ServiceName": "nginx",
        "Status": "passing"
    },
    {
        "CheckID": "service:nginx",
        "Name": "Service 'nginx' check",
        "Node": "node2",
        "Notes": "",
        "Output": "HTTP GET http://192.168.99.101/: 200 OK Output: <!DOCTYPE html>\n<html>\n<head>\n<title>Welcome to nginx!</title>\n<style>\n    body {\n        width: 35em;\n        margin: 0 auto;\n        font-family: Tahoma, Verdana, Arial, sans-serif;\n    }\n</style>\n</head>\n<body>\n<h1>Welcome to nginx!</h1>\n<p>If you see this page, the nginx web server is successfully installed and\nworking. Further configuration is required.</p>\n\n<p>For online documentation and support please refer to\n<a href=\"http://nginx.org/\">nginx.org</a>.<br/>\nCommercial support is available at\n<a href=\"http://nginx.com/\">nginx.com</a>.</p>\n\n<p><em>Thank you for using nginx.</em></p>\n</body>\n</html>\n",
        "ServiceID": "nginx",
        "ServiceName": "nginx",
        "Status": "passing"
    },
    {
        "CheckID": "service:nginx",
        "Name": "Service 'nginx' check",
        "Node": "node3",
        "Notes": "",
        "Output": "HTTP GET http://192.168.99.102/: 200 OK Output: <!DOCTYPE html>\n<html>\n<head>\n<title>Welcome to nginx!</title>\n<style>\n    body {\n        width: 35em;\n        margin: 0 auto;\n        font-family: Tahoma, Verdana, Arial, sans-serif;\n    }\n</style>\n</head>\n<body>\n<h1>Welcome to nginx!</h1>\n<p>If you see this page, the nginx web server is successfully installed and\nworking. Further configuration is required.</p>\n\n<p>For online documentation and support please refer to\n<a href=\"http://nginx.org/\">nginx.org</a>.<br/>\nCommercial support is available at\n<a href=\"http://nginx.com/\">nginx.com</a>.</p>\n\n<p><em>Thank you for using nginx.</em></p>\n</body>\n</html>\n",
        "ServiceID": "nginx",
        "ServiceName": "nginx",
        "Status": "passing"
    }
]
```

可以看到3个nginx的服务节点都是passing状态，这时候你可以选择一个使用了。

### 制造一些事故

进入node3
 `$ docker kill nginx`




![img](https:////upload-images.jianshu.io/upload_images/698777-4f03bcb021ccb3da.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1000)





进入node2

```
$ docker kill consul
```



![img](https:////upload-images.jianshu.io/upload_images/698777-8d55f3d470ebd073.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1000)



再次检查

```
$ http http://192.168.99.100:8500/v1/health/checks/nginx
[
    {
        "CheckID": "service:nginx",
        "Name": "Service 'nginx' check",
        "Node": "node1",
        "Notes": "",
        "Output": "HTTP GET http://192.168.99.100/: 200 OK Output: <!DOCTYPE html>\n<html>\n<head>\n<title>Welcome to nginx!</title>\n<style>\n    body {\n        width: 35em;\n        margin: 0 auto;\n        font-family: Tahoma, Verdana, Arial, sans-serif;\n    }\n</style>\n</head>\n<body>\n<h1>Welcome to nginx!</h1>\n<p>If you see this page, the nginx web server is successfully installed and\nworking. Further configuration is required.</p>\n\n<p>For online documentation and support please refer to\n<a href=\"http://nginx.org/\">nginx.org</a>.<br/>\nCommercial support is available at\n<a href=\"http://nginx.com/\">nginx.com</a>.</p>\n\n<p><em>Thank you for using nginx.</em></p>\n</body>\n</html>\n",
        "ServiceID": "nginx",
        "ServiceName": "nginx",
        "Status": "passing"
    },
    {
        "CheckID": "service:nginx",
        "Name": "Service 'nginx' check",
        "Node": "node2",
        "Notes": "",
        "Output": "HTTP GET http://192.168.99.101/: 200 OK Output: <!DOCTYPE html>\n<html>\n<head>\n<title>Welcome to nginx!</title>\n<style>\n    body {\n        width: 35em;\n        margin: 0 auto;\n        font-family: Tahoma, Verdana, Arial, sans-serif;\n    }\n</style>\n</head>\n<body>\n<h1>Welcome to nginx!</h1>\n<p>If you see this page, the nginx web server is successfully installed and\nworking. Further configuration is required.</p>\n\n<p>For online documentation and support please refer to\n<a href=\"http://nginx.org/\">nginx.org</a>.<br/>\nCommercial support is available at\n<a href=\"http://nginx.com/\">nginx.com</a>.</p>\n\n<p><em>Thank you for using nginx.</em></p>\n</body>\n</html>\n",
        "ServiceID": "nginx",
        "ServiceName": "nginx",
        "Status": "passing"
    },
    {
        "CheckID": "service:nginx",
        "Name": "Service 'nginx' check",
        "Node": "node3",
        "Notes": "",
        "Output": "Get http://192.168.99.102/: dial tcp 192.168.99.102:80: connection refused",
        "ServiceID": "nginx",
        "ServiceName": "nginx",
        "Status": "critical"
    }
]
```

可以看到，当consul服务挂掉一个的时候，并不影响nginx服务的健康状况。其中有一个nginx已经处于critical状态。这样我们就有足够的信息不选择不健康的节点。

## 总结

好了，终于写完了。总的来说，这个已经算是极简的架构了。当然，docker的生命周期远不止这些，比如ci，发布上线，灰度发布等。docker远没有传说中那么简单，美妙。docker有很多的好处，但是需要DevOPS做很多的工作。

在实践过程中，我发现有一个或许是更优的架构。那就是seneca的方案，使用事件相应作为微服务的提供方式，这样就避免了服务发现这件事，完全不需要注册服务，选择服务这么麻烦。也不用为服务发现服务搭建一个集群确保其高可用。

小礼物走一走，来简书关注我











---









# consul+docker实现服务注册







近期新闻

css宣布支持三角函数
ES10即将来临
基本架构

在这里插入图片描述
注册中心： 每个服务提供者向注册中心登记自己的服务，将服务名与主机Ip，端口等一些附加信息告诉注册中心，注册中心按服务名分类组织服务清单。如A服务运行在192.168.1.82:3000，192.168.1.83：3000实例上。那么维护的内容如下：
在这里插入图片描述
简单来说，服务中心会维护一份，在册的服务名与服务ip的映射关系。同时注册中心也会检查注册的服务是否可用，不可用则剔除。
服务消费者：即在注册中心注册的服务，他们之间不再通过具体地址访问，而是通过服务名访问其他在册服务的资源。
技术说明

Consul:采用Go开发的高可用的服务注册与配置服务，本文的注册中心采用Consul实现。
Docker:基于Go语言的开源应用容器引擎。
Registor:Go语言编写，针对docker使用的，通过检查本机容器进程在线或者停止运行状态，去注册服务的工具。这里主要用作对服务消费者的监控。
示例环境： 系统：macos； docker: 17.09.1-ce； docker-compose:1.17.1。
docker命令创建注册中心

docker run -h node1  --name consul -d --restart=always\
    -p  8400 \
    -p   8500:8500 \
progrium/consul -server \
-bootstrap-expect 1 -advertise 127.0.0.1

    1
    2
    3
    4
    5

下面来解释下各个参数
-h 节点名字
–name 容器（container）名称，后期用来方便启动关闭，看日志等，这个一定要写
-d 后台运行
-v /data:/data 使用宿主机的/data目录映射到容器内部的/data,用于保存consul的注册信息，要不docker 一重启，数据是不保留的。
–restart=always 这个可以活得长一点
下面几个参数都是consul集群用的，非集群模式可以不使用。
-p 8500:8500
progrium/consul 镜像名称，本地没有就自动从公共docker库下载
后面的都是consul的参数：
-server \ 以服务节点启动
-bootstrap-expect 3 \ 预期的启动节点数3，最少是3，要不达不到cluster的效果
-advertise 127.0.0.1 告诉集群，我的ip是什么，就是注册集群用的
使用docker-compose创建注册中心

使用docker命令创建注册中心比较麻烦，并且不好维护，这里使用docker-compose来实现。
docker-compose:
compose是 Docker 容器进行编排的工具，定义和运行多容器的应用，可以一条命令启动多个容器，使用Docker Compose不再需要使用shell脚本来启动容器。

Compose 通过一个配置文件来管理多个Docker容器，在配置文件中，所有的容器通过services来定义，然后使用docker-compose脚本来启动，停止和重启应用，和应用中的服务以及所有依赖服务的容器，非常适合组合使用多个容器进行开发的场景。
首先看下docker-compose配置文件：

version: "3.0"

services:
    # consul server，对外暴露的ui接口为8500，只有在2台consul服务器的情况下集群才起作用
    consulserver:
        image: progrium/consul:latest
        hostname: consulserver
        ports:
            - "8300"
            - "8400"
            - "8500:8500"
            - "53"
        command: -server -ui-dir /ui -data-dir /tmp/consul --bootstrap-expect=3

    # consul server1在consul server服务起来后，加入集群中
    consulserver1:
        image: progrium/consul:latest
        hostname: consulserver1
        depends_on:
            - "consulserver"
        ports:
            - "8300"
            - "8400"
            - "8500"
            - "53"
        command: -server -data-dir /tmp/consul -join consulserver
    
    # consul server2在consul server服务起来后，加入集群中
    consulserver2:
        image: progrium/consul:latest
        hostname: consulserver2
        depends_on:
            - "consulserver"
        ports:
            - "8300"
            - "8400"
            - "8500"
            - "53"
        command: -server -data-dir /tmp/consul -join consulserver
    
    1
    2
    3
    4
    5
    6
    7
    8
    9
    10
    11
    12
    13
    14
    15
    16
    17
    18
    19
    20
    21
    22
    23
    24
    25
    26
    27
    28
    29
    30
    31
    32
    33
    34
    35
    36
    37
    38
    39

这里consulserver,consulserver1等是服务名，image定义镜像名，depends_on定义依赖容器,command可以覆盖容器启动后默认命令。详细的配置项介绍及compose命令，可查看这里。
下面进入模版目录，运行

docker-compose up -d

    1

浏览器输入http://127.0.0.1:8500，可以看到consul server服务已启动。效果如下：
在这里插入图片描述

接下来，添加registrator。方法是在配置文件里加上registrator信息，registrator保证了，如果服务已停止，则从注册中心中移除。

    registrator:
        image: gliderlabs/registrator:master
        hostname: registrator
        depends_on:
            - "consulserver"
        volumes:
            - "/var/run/docker.sock:/tmp/docker.sock"
        command: -internal consul://consulserver:8500
    
    1
    2
    3
    4
    5
    6
    7
    8

注册中心已经建好了，接下来我们创建需要注册在consul server上的服务。
搭建service web服务

首先我们创建一个服务的本地镜像，用来创建服务容器。
创建server.js,这里用express框架创建node服务，端口3000。

var express = require("express");
var PORT = 3000;
var app = express();
const ip = require("ip");
app.get("/getRemoteIp", (req, res) => {
    res.send({ ip: ip.address() });
});
app.get("/", function(req, res) {
    res.send("Helloworld\n");
});
app.listen(PORT);
console.log("Running on http://localhost:" + PORT);

    1
    2
    3
    4
    5
    6
    7
    8
    9
    10
    11
    12

再创建package.json定义依赖。
创建镜像最重要的是Dockerfile。

FROM node:alpine
RUN mkdir -p /home/Service
WORKDIR /home/Service 
COPY . /home/Service
RUN npm install
EXPOSE 3000
CMD npm start

    1
    2
    3
    4
    5
    6
    7

FROM:构建镜像的基础源镜像
WORKDIR：容器的工作目录
COPY:将本地的东西拷贝到容器的指定目录下
EXPOSE:将容器内的某个端口导出给主机，用于我们访问
CMD:每次容器启动的时候执行的命令
详细配置项信息可以看这里。

接着就可以创建镜像了：

docker build -t client-server .

    1

建成后使用docker images查看镜像，也可以运行镜像，验证是否创建成功。

docker run -d -p 3000:3000 client-server

    1

下面可以创建consul client了，配置文件如下。

version: "3.0"
##	 启动node-service-web节点服务

services:
    web:
        image: client-server:latest
        environment:
            SERVICE_3000_NAME: service-web
        ports:
            - "3000"

    1
    2
    3
    4
    5
    6
    7
    8
    9

这里client-server就是刚创建的镜像。
运行docker-compose -f docker-compose.web.yml up -d --scale web=3启动服务，–scale web=3表示启动三台服务器。效果如下：
在这里插入图片描述
验证Registrator功能

我们停止一个service-web 服务，看下Registrator是否会将无效的服务移除。
首先看下当前服务 docker ps
在这里插入图片描述
再停止一个服务

docker stop de8848a31fe7

    1

接着登录http://localhost:8500/ui/#/dc1/services/consul，看到service-web变为两个，说明Registrator会移除下线服务。

参考文档：
https://zhuanlan.zhihu.com/p/36114437
https://www.jianshu.com/p/d16a1bea5cbb
https://www.jianshu.com/p/ab76ba86eafc

https://www.cnblogs.com/bossma/p/9756809.html
--------------------- 
作者：巨大星星星 
来源：CSDN 
原文：https://blog.csdn.net/qq_36228442/article/details/89085373 
版权声明：本文为博主原创文章，转载请附上博文链接！



