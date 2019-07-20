

## [基于.net core微服务（Consul、Ocelot、Docker、App.Metrics+InfluxDB+Grafana、Exceptionless、数据一致性、Jenkins）](https://www.cnblogs.com/wyt007/p/10631109.html)





## 1、微服务简介



一种架构模式，提倡将单一应用程序划分成一组小的服务，服务之间互相协调、互相配合，为用户提供最终价值。每个服务运行在其独立的进程中，服务与服务间采用轻量级的通信机制互相沟通（RESTful   API）。每个服务都围绕着具体的业务进行构建，并且能够被独立地部署到生产环境、类生产环境等。应尽量避免统一的、集中式的服管理机制，对具体的一个服务而言，应根据业务上下文，选择合适的语言、工具对其进行构建。 　　　　　　　　　　　　　　　　　　　　——马丁•福勒

### 1.1、.net core下的微服务构件

- 服务治理：Consul
- API网关：Ocelot
- 作业调度：Quartz.NET,Hangfire
- 分布式日志：Exceptionless
- ESB：Masstransit(RabbitMQ)
- APM：Metrac.App,Buttfly

### 1.2、微架构

![img](https://img2018.cnblogs.com/blog/991704/201903/991704-20190331113046831-1623413271.png)

## 2、Consul

http api官方文档地址：https://www.consul.io/api/index.html

Api本地url: http://localhost:8500/v1/agent/services

### 2.1、Consul是什么

是一个服务管理软件。支持多数据中心下，分布式高可用的，服务发现和配置共享。consul支持**健康检查**，**允许存储键值对**。一致性协议采用 Raft 算法,用来保证服务的高可用。成员管理和消息广播 采用GOSSIP协议，支持ACL访问控制。

**服务注册**：一个服务将其位置信息在“中心注册节点”注册的过程。该服务一般会将它的主机IP地址以及端口号进行注册，有时也会有服务访问的认证信息，使用协议，版本号，以及关于环境的一些细节信息。

**服务发现**：服务发现可以让一个应用或者组件发现其运行环境以及其它应用或组件的信息。用户配置一个服务发现工具就可以将实际容器跟运行配置分离开。常见配置信息包括：ip、端口号、名称等。

### 2.2、述语

- Agent

  Agent是长期运行在每个consul集群成员节点上守护进程。通过命令consul agent启动。Agent有client和server两种模式。由于每个节点都必须运行agent，所有节点要么是client要么是server。所有的Agent都可以调用DNS或HTTP API，并负责检查和维护服务同步。

- client
  　　运行client模式的Agent，将所有的RPCs转发到Server。Client是相对无状态的。Client唯一所做的是在后台参与LAN gossip pool。只消耗少量的资源，少量的网络带宽。

- Server
  　　运行Server模式的Agent，参与Raft quorum，维护集群的状态，响应RPC查询，与其他数据中心交互WAN gossip，转发查询到Leader或远程数据中心。

- Datacenter
  　　数据中心的定义似乎是显而易见的，有一些细节是必须考虑的。例如，在EC2，多个可用性区域是否被认为组成了单一的数据中心？我们定义数据中心是在同一个网络环境中——私有的，低延迟，高带宽。这不包括基于公共互联网环境，但是对于我们而言，在同一个EC2的多个可用性区域会被认为是一个的数据中心。

- Consensus
  　　当本系列文档中，consensus，意味着leader election协议，以及事务的顺序。由于这些事务是基于一个有限状态机，consensus的定义意味着复制状态机的一致性。

- Gossip 
  　　consul是建立在Serf之上，提供了完成的Gossip协议，用于成员维护故障检测、事件广播。详细细节参见gossip documentation。这足以知道gossip是基于UDP协议实现随机的节点到节点的通信，主要是在UDP。

- LAN Gossip
  　　指的是LAN gossip pool，包含位于同一个局域网或者数据中心的节点。

- WAN Gossip
  　　指的是WAN gossip pool，只包含server节点，这些server主要分布在不同的数据中心或者通信是基于互联网或广域网的。

- RPC
  　　远程过程调用。是允许client请求服务器的请求/响应机制。

### 2.3、部署结构图

![img](https://img2018.cnblogs.com/blog/991704/201903/991704-20190331120434947-1335806623.png)

### 2.4、命令

-  -advertise
  通知展现地址用来改变我们给集群中的其他节点展现的地址，一般情况下-bind地址就是展现地址
- **-bootstrap**
  用来控制一个server是否在bootstrap模式，在一个datacenter中只能有一个server处于bootstrap模式，当一个server处于bootstrap模式时，可以自己选举为raft leader。  
- -bootstrap-expect
  在一个datacenter中期望提供的server节点数目，当该值提供的时候，consul一直等到达到指定sever数目的时候才会引导整个集群，该标记不能和bootstrap公用
- **-bind**
  该地址用来在集群内部的通讯，集群内的所有节点到地址都必须是可达的，默认是0.0.0.0
- -client
  consul绑定在哪个client地址上，这个地址提供HTTP、DNS、RPC等服务，默认是127.0.0.1
- **-config-file**
  明确的指定要加载哪个配置文件
- **-config-dir**
  配置文件目录，里面所有以.json结尾的文件都会被加载
- **-data-dir**
  提供一个目录用来存放agent的状态，所有的agent允许都需要该目录，该目录必须是稳定的，系统重启后都继续存在
- -datacenter
  该标记控制agent允许的datacenter的名称，默认是dc1
- **-encrypt**
  指定secret key，使consul在通讯时进行加密，key可以通过consul keygen生成，同一个集群中的节点必须使用相同的key
- **-join**
  加入一个已经启动的agent的ip地址，可以多次指定多个agent的地址。如果consul不能加入任何指定的地址中，则agent会启动失败，默认agent启动时不会加入任何节点。
- -retry-join
  和join类似，但是允许你在第一次失败后进行尝试。
- -retry-interval
  两次join之间的时间间隔，默认是30s
- -retry-max
  尝试重复join的次数，默认是0，也就是无限次尝试
- -log-level
  consul agent启动后显示的日志信息级别。默认是info，可选：trace、debug、info、warn、err。
- -node
  节点在集群中的名称，在一个集群中必须是唯一的，默认是该节点的主机名
- -protocol
  consul使用的协议版本
- -rejoin
  使consul忽略先前的离开，在再次启动后仍旧尝试加入集群中。
- **-server**
  定义agent运行在server模式，每个集群至少有一个server，建议每个集群的server不要超过5个
- -syslog
  开启系统日志功能，只在linux/osx上生效
- -ui-dir
  提供存放web ui资源的路径，该目录必须是可读的
- -pid-file
  提供一个路径来存放pid文件，可以使用该文件进行SIGINT/SIGHUP(关闭/更新)agent

### 2.5、常用API

consul的主要接口是RESTful HTTP API，该API可以用来增删查改nodes、services、checks、configguration。所有的endpoints主要分为以下类别：

- **kv** - Key/Value存储
- **agent** - Agent控制
- **catalog** - 管理nodes和services
- **health** - 管理健康监测
- **session** - Session操作
- **acl** - ACL创建和管理
- **event** - 用户Events
- **status** - Consul系统状态

 

- agent endpoints

  ：agent endpoints用来和本地agent进行交互，一般用来服务注册和检查注册，支持以下接口

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  /v1/agent/checks : 返回本地agent注册的所有检查(包括配置文件和HTTP接口)
  /v1/agent/services : 返回本地agent注册的所有 服务
  /v1/agent/members : 返回agent在集群的gossip pool中看到的成员
  /v1/agent/self : 返回本地agent的配置和成员信息
  /v1/agent/join/<address> : 触发本地agent加入node
  /v1/agent/force-leave/<node>>: 强制删除node
  /v1/agent/check/register : 在本地agent增加一个检查项，使用PUT方法传输一个json格式的数据
  /v1/agent/check/deregister/<checkID> : 注销一个本地agent的检查项
  /v1/agent/check/pass/<checkID> : 设置一个本地检查项的状态为passing
  /v1/agent/check/warn/<checkID> : 设置一个本地检查项的状态为warning
  /v1/agent/check/fail/<checkID> : 设置一个本地检查项的状态为critical
  /v1/agent/service/register : 在本地agent增加一个新的服务项，使用PUT方法传输一个json格式的数据
  /v1/agent/service/deregister/<serviceID> : 注销一个本地agent的服务项
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

- catalog endpoints

  ：catalog endpoints用来注册/注销nodes、services、checks

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  /v1/catalog/register : Registers a new node, service, or check
  /v1/catalog/deregister : Deregisters a node, service, or check
  /v1/catalog/datacenters : Lists known datacenters
  /v1/catalog/nodes : Lists nodes in a given DC
  /v1/catalog/services : Lists services in a given DC
  /v1/catalog/service/<service> : Lists the nodes in a given service
  /v1/catalog/node/<node> : Lists the services provided by a node
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

- health endpoints

  ：health endpoints用来查询健康状况相关信息，该功能从catalog中单独分离出来

  ```
  /v1/healt/node/<node>: 返回node所定义的检查，可用参数?dc=
  /v1/health/checks/<service>: 返回和服务相关联的检查，可用参数?dc=
  /v1/health/service/<service>: 返回给定datacenter中给定node中service
  /v1/health/state/<state>: 返回给定datacenter中指定状态的服务，state可以是"any", "unknown", "passing", "warning", or "critical"，可用参数?dc=
  ```

- session endpoints

  ：session endpoints用来create、update、destory、query sessions

  ```
  /v1/session/create: Creates a new session
  /v1/session/destroy/<session>: Destroys a given session
  /v1/session/info/<session>: Queries a given session
  /v1/session/node/<node>: Lists sessions belonging to a node
  /v1/session/list: Lists all the active sessions
  ```

- acl endpoints

  ：acl endpoints用来create、update、destory、query acl

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  /v1/acl/create: Creates a new token with policy
  /v1/acl/update: Update the policy of a token
  /v1/acl/destroy/<id>: Destroys a given token
  /v1/acl/info/<id>: Queries the policy of a given token
  /v1/acl/clone/<id>: Creates a new token by cloning an existing token
  /v1/acl/list: Lists all the active tokens
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

- event endpoints

  ：event endpoints用来fire新的events、查询已有的events

  ```
  /v1/event/fire/<name>: 触发一个新的event，用户event需要name和其他可选的参数，使用PUT方法
  /v1/event/list: 返回agent知道的events
  ```

- status endpoints

  ：status endpoints用来或者consul 集群的信息

  ```
  /v1/status/leader : 返回当前集群的Raft leader
  /v1/status/peers : 返回当前集群中同事
  ```

### 2.6、使用consul

- 启动

  语法：

  ```
  consul agent -server -datacenter=数据中心名称 -bootstrap -data-dir 数据存放路径 -config-file 配置文件路径 -ui-dir UI存放路径 -node=n1 -bind 本机IP
  ```

  注册成Windows服务

  ```
  sc.exe create "Consul" binPath= "E:\Consul\consule.exe agent -server -datacenter=数据中心名称 -bootstrap -data-dir 数据存放路径 -config-file 配置文件路径 -ui-dir UI存放路径 -node=n1 -bind 本机IP"
  ```

  示例：

  ```
  consul agent -server -datacenter=dc1 -bootstrap -data-dir /tmp/consul -config-file ./conf -ui-dir ./dist -node=n1 -bind 127.0.0.1 
  ```

- 查看集群成员

  ```
  consul members
  ```

- 把192.168.1.126加入集群

  ```
  consul join 192.168.1.126
  ```

- 查看节点raft信息

  ```
  consul operator raft list-peers
  ```

### 2.7、项目实例

- 项目准备
  项目地址：https://github.com/786744873/HisMicroserviceSample
  项目部署说明：分别部署 192.168.103.203 、 192.168.103.207 两台服务器
  ![img](https://img2018.cnblogs.com/blog/991704/201903/991704-20190331124446523-1276330235.png)

- 配置consul配置文件

  文件结构：

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  │  consul.exe
  │  
  ├─conf
  │      service.json
  │      watchs.json
  │      xacl.json
  │      
  ├─data
  ├─dist
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  service.json（服务发现配置）：
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  {
      "encrypt": "7TnJPB4lKtjEcCWWjN6jSA==",    //加密秘钥
      "services": [{
              "id": "BasicsService",    //服务id
              "name": "BasicsService",    //服务名称
              "tags": ["BasicsService"],    //服务标签
              "address": "192.168.103.203",    //服务地址
              "port": 6801,    //端口
              "checks": [{
                  "id": "BasicsServiceCheck",    //检查id
                  "name": "BasicsServiceCheck",    //检查名称
                  "http": "http://192.168.103.203:6801/health",    //检车接口地址
                  "interval": "10s",    //检查周期
                  "tls_skip_verify": false,    //跳过验证
                  "method": "GET",    //检查请求方法
                  "timeout": "1s"    //请求超时时间
              }]
          },
          {
              "id": "InvoicingService",    //服务id
              "name": "InvoicingService",    //服务名称
              "tags": ["InvoicingService"],    //服务标签
              "address": "192.168.103.203",    //服务地址
              "port": 6802,    //端口
              "checks": [{
                  "id": "InvoicingServiceCheck",    //检查id
                  "name": "InvoicingServiceCheck",    //检查名称
                  "http": "http://192.168.103.203:6802/health",    //检车接口地址
                  "interval": "10s",    //检查周期
                  "tls_skip_verify": false,    //跳过验证
                  "method": "GET",    //检查请求方法
                  "timeout": "1s"    //请求超时时间
              }]
          }
      ]
  }
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  watchs.json（服务监控配置）：
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  {
      "watches": [{
          "type": "checks",    //监控触发类型
          "handler_type": "http",    //异常通知类型
          "state": "critical",    //监控触发状态
          "http_handler_config": {
              "path": "http://localhost:6801/notice",    //通知地址
              "method": "POST",    //通知请求方式
              "timeout": "10s",    //通知超时时间
              "header": {
                  "Authorization": ["Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiZ3N3IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL2V4cGlyYXRpb24iOiIyMDIyLzEyLzMxIDEyOjM2OjEyIiwibmJmIjoxNTE0Njk0OTcyLCJleHAiOjE1MTQ3MzA5NzIsImlzcyI6ImdzdyIsImF1ZCI6ImdzdyJ9.jPu1yZ8jORN5QgCuPV50sYOKvX88GLSDiRX_0fpEzU4"]
              }    //请求头
          }
      }]
  }
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

- 分别启动 

  192.168.103.203

   、 

  192.168.103.207

   上的应用基础和进销存服务，然后再启动Consul，我们让 

  192.168.103.203

   作为主Consul

  第一台service（192.168.103.203）：

  ```
  consul agent -server -datacenter=dc1 -bootstrap -data-dir ./data -config-file ./conf -ui-dir ./dist -node=n1 -bind 192.168.103.203
  ```

  第二台service（192.168.103.207）：

  ```
  consul agent -server -datacenter=dc1 -data-dir ./data -config-file ./conf -ui-dir ./dist -node=n2 -bind 192.168.103.207
  ```

  然后可以通过访问192.168.103.203:8500进入UI页面查看信息

- client

  ```
  consul agent -datacenter=dc1 -data-dir /tmp/consul -node cn1
  ```

  Mac OX系统，进入consul所在目录执行：

  ```
  Sudo scp consul /usr/local/bin/
  ```

### 2.8、Consul DNS

DnsAgent.exe作为DNS工具

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
[
  {
  "Pattern": "^.*\\.consul$",
  "NameServer": "127.0.0.1:8600",
  "QueryTimeout": 1000,
  "CompressionMutation": false
  }
]
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

访问地址：http://服务名称.service.consul

## 3、Ocelot

github地址：https://github.com/TomPallister/Ocelot

Ocelot的目标是使用.NET运行微服务/面向服务架构，我们需要一个统一的入口进入我们的服务，提供监控、鉴权、负载均衡等机制，也可以通过编写中间件的形式，来扩展Ocelot的功能。  Ocelot是一堆特定顺序的中间件。

![img](https://img2018.cnblogs.com/blog/991704/201903/991704-20190331134909278-11044504.png)

### 3.1、Ocelot使用

- 安装Ocelot

  ```
  Install-Package Ocelot
  ```

- 引入在Program.cs中加载配置文件

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  public static IWebHost BuildWebHost(string[] args)
  {
  
      IWebHostBuilder builder = new WebHostBuilder();
      //注入WebHostBuilder
      return builder.ConfigureServices(service =>
      {
          service.AddSingleton(builder);
      })
          //加载configuration配置文人年
          .ConfigureAppConfiguration(conbuilder =>
          {
              conbuilder.AddJsonFile("appsettings.json");
              conbuilder.AddJsonFile("configuration.json");
          })
          .UseContentRoot(Directory.GetCurrentDirectory())
          .UseKestrel()
          .UseUrls("http://*:6800")
          .UseStartup<Startup>()
          .Build();
  }
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

- 修改Startup.cs

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  public void ConfigureServices(IServiceCollection services)
  {       
      //注入配置文件
      services.AddOcelot(Configuration);
  }
  public  void Configure(IApplicationBuilder app, IHostingEnvironment env)
  {
      //添加中间件
      app.UseOcelot().Wait();
  }
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

- 创建配置文件（configuration.json）

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  {
    "ReRoutes": [    //路由配置
      {
        "DownstreamPathTemplate": "/{url}",    //下游请求路由
        "DownstreamScheme": "http",    //下游请求方式，有http或https
        "DownstreamHostAndPorts": [    //下游请求的host和端口，为了配合负载均衡，可以配置多项
          {
            "Host": "localhost",
            "Port": 6801
          }
        ],
        "UpstreamPathTemplate": "/basics/{url}",    //上游请求路由
        "UpstreamHttpMethod": [ "Get", "Post", "Delete", "Put" ],    //上游请求谓词
        //"ServiceName": "BasicsService",    //Consul中注册服务的名称
        //"LoadBalancer": "RoundRobin",    //负载均衡（可选）LeastConnection –请求空闲的Url  RoundRobin – 轮询请求  NoLoadBalance – 无负载均衡
        //"UseServiceDiscovery": true,    //是否启用负载均衡
        "ReRouteIsCaseSensitive": false,    //
        "QoSOptions": {    //熔断设置（可选）
          "ExceptionsAllowedBeforeBreaking": 3,    //允许异常请求数
          "DurationOfBreak": 10,    //熔断时间，以秒为单位
          "TimeoutValue": 5000    //请求超时数，以毫秒为单位
        },
        "HttpHandlerOptions": {    //
          "AllowAutoRedirect": false,    //
          "UseCookieContainer": false,    //
          "UseTracing": false    //
        },
        "AuthenticationOptions": {    //
          "AuthenticationProviderKey": "",    //
          "AllowedScopes": []    //
        },
        "RateLimitOptions": {    //限流设置（可选）
          "ClientWhitelist": [ "admin" ],    //白名单，不受限流控制的
          "EnableRateLimiting": true,    //是否启用限流
          "Period": "1m",    //统计时间段：1s, 2m, 3h, 4d
          "PeriodTimespan": 15,    //间隔多少秒后可以重试
          "Limit": 100    //设定时间段内允许的最大请求数
        }
      },
      {
        "DownstreamPathTemplate": "/{url}",
        "DownstreamScheme": "http",
        "DownstreamHostAndPorts": [
          {
            "Host": "localhost",
            "Port": 6802
          }
        ],
        "UpstreamPathTemplate": "/invoicing/{url}",
        "UpstreamHttpMethod": [ "Get", "Post", "Delete", "Put" ],
        //"ServiceName": "InvoicingService",
        //"LoadBalancer": "RoundRobin",
        //"UseServiceDiscovery": true,
        "ReRouteIsCaseSensitive": false,
        "QoSOptions": {
          "ExceptionsAllowedBeforeBreaking": 3,
          "DurationOfBreak": 10,
          "TimeoutValue": 5000
        },
        "HttpHandlerOptions": {
          "AllowAutoRedirect": false,
          "UseCookieContainer": false,
          "UseTracing": false
        },
        "AuthenticationOptions": {
          "AuthenticationProviderKey": "",
          "AllowedScopes": []
        },
        "RateLimitOptions": {
          "ClientWhitelist": [ "admin" ],
          "EnableRateLimiting": true,
          "Period": "1m",
          "PeriodTimespan": 15,
          "Limit": 100
        }
      },
      {
        "DownstreamPathTemplate": "/{url}",
        "DownstreamScheme": "http",
        "DownstreamHostAndPorts": [
          {
            "Host": "localhost",
            "Port": 6806
          }
        ],
        "UpstreamPathTemplate": "/authentication/{url}",
        "UpstreamHttpMethod": [ "Get", "Post", "Delete", "Put" ],
        //"ServiceName": "AuthenticationService",
        //"LoadBalancer": "RoundRobin",
        //"UseServiceDiscovery": true,
        "ReRouteIsCaseSensitive": false,
        "QoSOptions": {
          "ExceptionsAllowedBeforeBreaking": 3,
          "DurationOfBreak": 10,
          "TimeoutValue": 5000
        },
        "HttpHandlerOptions": {
          "AllowAutoRedirect": false,
          "UseCookieContainer": false,
          "UseTracing": false
        },
        "AuthenticationOptions": {
          "AuthenticationProviderKey": "",
          "AllowedScopes": []
        },
        "RateLimitOptions": {
          "ClientWhitelist": [ "admin" ],
          "EnableRateLimiting": true,
          "Period": "1m",
          "PeriodTimespan": 15,
          "Limit": 100
        }
      }
  
    ],
    "GlobalConfiguration": {    //全局设置
      "ServiceDiscoveryProvider": {//Consul服务地址，用于上方的服务发现
        "Host": "localhost",
        "Port": 8500
      },
      "RateLimitOptions": {    //全局限流设置（可选）
        "ClientIdHeader": "clientid",    //识别请求头，默认是 ClientId
        "QuotaExceededMessage": "access is denied",    //被限流后，当请求过载时返回的提示消息
        "HttpStatusCode":     //600,当请求过载时返回的http状态码
        "DisableRateLimitHeaders": false    //此值指定是否禁用X-Rate-Limit和Rety-After标头
      }
    }
  } 
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

## 4、Docker

容器是一个打包了应用服务的环境。它是一个轻量级的虚拟机，每一个容器由一组特定的应用和必要的依赖库组成。

![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190407123203718-156144231.png)

### 4.1、Docker-镜像常用命令 

```
docker images:查看本地镜像，docker images ubu*，通配符查看
docker inspect ubuntu:查看镜像详细信息
docker search aspnetcore:搜索docker hub上符合要求的镜像
docker pull microsoft/aspnetcore:拉取镜像，在run时不用从docker hub拉取
docker rmi 镜像ID1 镜像ID2：删除镜像ID1和ID2，如果强制删除加-f
```

### 4.2、Docker-容器常用命令

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
docker create ubuntu:14.04：创建容器，处于停止状态
docker ps：查看运行的容器，加-a查看所有容器。加-l查询出最后创建的容器，加-n=3查看最后创建的3个容器
docker start 容器名：运行已存在的容器
docker stop 容器名：停止容器
docker rm 容器名：删除容器，docker rm $(docker ps -a -q)删除所有容器
docker run -i -t --name ubuntu14 ubuntu:14.04 /bin/bash：运行一个ubuntu14.04的，带终端的容器，名字叫ubuntu14 ，-i用于打开容器的标准输入，-t让容器建立一个命令行终端
docker run --name back_ubuntu14 -d ubuntu:14.04 /bin/sh -c "while true;do echo hello world;sleep 1;done"：-d是后台开容器
docker attach 容器名：依附容器
docker logs -f --tail=5  back_ubuntu14：查看最近的5条日志
docker top 容器名：查看容器进程 
docker inspect 容器名：查看容器信息，查看具体子项docker inspect --format='{{.NetworkSettings.IPAddress}}'  back_ubuntu14
docker export 容器名 >容器存储名称.tar：导出容器
win powershell下  docker export 容器ID -o 名字.tar
docker import 容器存储名称.tar：导入镜像
docker commit -m="abc" --author="gsw" 容器ID  镜像名称：提交容器到本地镜像
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

### 4.4、Docker-Dockerfile

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
FROM：指定待扩展的父级镜像。除了注释外，在文件开头必须是一个FROM指令，接下来的指令便在这个父级镜像的环境中运行，直到遇到下一个FROM指令。通过添加多个FROM指令，可以在同一个Dockerfile文件中创建多个镜像。
MAINTAINER：用来声明创建的镜像的作都信息。非必需
RUN：用来修改镜像命令，常用来安装库、程序 以及配置程序。一条RUN指令执行完毕后，会在当前镜像上创建一个新的镜像层，接下来的指令会在新的镜像上继续执行。
EXPOSE：用来指明容器内进程对外开放的端口，多个端口之间使用空格隔开。运行容器时，通过参数-P(大写)即可将EXPOSE里所指定的端口映射到主机上国外的坠机端口，其队容器或主机就可以通过映射后的端口与此容器通信。同时，我们也可以通过-p(小写)参数将Dockerfile中EXPOSE中没有的端口设置成公开的。
ADD：向新镜像中添加文件，这个文件可以是一个主机文件，也可以是一个网络文件，也可以是一个文件夹。
VOLUME：在镜像里创建一个指定路径的挂载点，这个路径可以来自主机或都其他容器。多个容器可以通过同一个挂载点共享数据，即便其中一个容器已经停止，挂载点也仍然可以访问，只有当挂载点的容器引用全部消失时，挂载点才会自动删除。
WORKDIR：为接下来执行的指令指定一个新的工作目录，这个目录可以是绝对目录，也可以是相对目录。
ENV：设置容器运行的环境变量。在运行容器的时候，通过-e参数可以修改这个环境变量值 ，也可以添加新的环境变量
CMD：用来设置启动容器时默认运行的命令。
ENTRYPOINT：与CMD类似，它也是用来指定容器启动时默认运行的命令。
USER：为容器的运行及接下来RUN、CMD、ENTRYPOINT等指令的运行指定用户或UID
ONBUILD：触发指令。构建镜像的时候，Docker的镜像构建器会将所有的ONBUILD指令指定的命令保存到镜像的元数据中，这些命令在当前镜像的构建的构建过程中并不会执行。只有新的镜像用用FRMO指令指定父镜像为这个镜像时，便会触发。
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

### 4.5、Docker生成asp.net core镜像和运行

发布asp.net core项目，并在发布文件夹下创建Dockerfile文件，复制下面内容

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
#父镜像
FROM microsoft/aspnetcore

#设置工作目录
WORKDIR /app

#复制发布文件到/app下
COPY . /app

#设置端口
EXPOSE 80

#使用dotnet XXXXXXXXX.dll来运行asp.net core项目，注意大小写
ENTRYPOINT ["dotnet", “XXXXXXXXX.dll"]
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

#### 4.6、Docker生成asp.net core镜像和运行

```
docker build -t xxxxxxxxxxx:latest .
docker run -it -p 6801:6801  xxxxxxxxxxx:latest
```

**注意：**docker内部web的端口， 上述命令中，第二个端口为docker内web的端口。

## 5、App.Metrics+InfluxDB+Grafana

**建议：建议在网关上进行监控，因为网关上监控可以监控所有**

App.Metrics：https://www.app-metrics.io

InfluxDB1.5.1-1：https://portal.influxdata.com

Grafana-5.0.4：https://grafana.com/get

### 5.1、安装使用

- 下载 influxdb
  https://portal.influxdata.com
- 下载  Grafana
  https://grafana.com/get
- 运行influxdb-版本号下的influxd.exe
- 运行grafana-版本号下，bin目录下grafana-server.exe
- 运行influxdb-版本号下的influx.exe，输入 create database influxdbtest 创建数据库，同时 create user "user1" with password '123456'  创建用户
- 配置Grafana，然后启动网关程序，登录localhost:3000查看监控信息，用户名密码是：admin

### 5.2、配置Grafana

#### 5.2.1、配置数据源

![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190407153452671-33241260.png)

#### 5.2.2、配置Dashboard

我们采用模板导入模式，将项目引用 App.Metrics 并访问 App.Metrics 源地址：https://www.app-metrics.io/

![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190407154203826-1001444478.png)

获取到InfluxDB对应的仪表盘编号2125，然后输入使用该模板

![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190407154329933-1762067431.png)

![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190407154429651-705168606.png)

导入成功后

![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190407154621267-575876501.png)

### 5.3、App.Metrics监控数据采集

- 项目nuget引用

  ```
  Install-Package App.Metrics
  Install-Package App.Metrics.AspNetCore.Endpoints
  Install-Package App.Metrics.AspNetCore.Reporting
  Install-Package App.Metrics.AspNetCore.Tracking
  Install-Package App.Metrics.Reporting.InfluxDB
  ```

- 修改配置文件 

  appsettings.json

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  "InfluxDB": {
    "IsOpen": true,//是否开启监控
    "DataBaseName": "influxdbtest",//数据库名称
    "ConnectionString": "http://localhost:8086",//数据库地址
    "username": "user1",//用户名
    "password": "123456",//密码
    "app": "HIS",//自定义名字
    "env": "Ocelot"//自定义环境
  }
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

- 修改 

  Startup.cs

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  public void ConfigureServices(IServiceCollection services)
  {
      ...
      #region Metrics监控配置
      string IsOpen = Configuration.GetSection("InfluxDB:IsOpen").Value.ToLower();//是否打开
      if (IsOpen == "true")
      {
          string database = Configuration.GetSection("InfluxDB")["DataBaseName"];//数据库名称
          string InfluxDBConStr = Configuration.GetSection("InfluxDB")["ConnectionString"];//数据库地址
          string app = Configuration.GetSection("InfluxDB")["app"];//自定义名字
          string env = Configuration.GetSection("InfluxDB")["env"];//自定义环境
          string username = Configuration.GetSection("InfluxDB")["username"];//用户名
          string password = Configuration.GetSection("InfluxDB")["password"];//密码
          var uri = new Uri(InfluxDBConStr);
  
          //配置metrics
          var metrics = AppMetrics.CreateDefaultBuilder()
          .Configuration.Configure(
          options =>
          {
              options.AddAppTag(app);
              options.AddEnvTag(env);
          })
          .Report.ToInfluxDb(
          options =>
          {
              options.InfluxDb.BaseUri = uri;
              options.InfluxDb.Database = database;
              options.InfluxDb.UserName = username;
              options.InfluxDb.Password = password;
              options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
              options.HttpPolicy.FailuresBeforeBackoff = 5;
              options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
              options.FlushInterval = TimeSpan.FromSeconds(5);
          })
          .Build();
  
          services.AddMetrics(metrics);//注入metrics
          services.AddMetricsReportScheduler();//报表
          services.AddMetricsTrackingMiddleware();//追踪
          services.AddMetricsEndpoints();      //终点    
      }
  
      #endregion
      ...
  }
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
  {
      #region 使用中间件Metrics
      string IsOpen = Configuration.GetSection("InfluxDB")["IsOpen"].ToLower();
      if (IsOpen == "true")
      {
          app.UseMetricsAllMiddleware();
          app.UseMetricsAllEndpoints();               
      }
      #endregion
      //使用中间件
      //await app.UseOcelot();
  }
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  接下来就可以进行追踪了

![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190407162117108-599766977.png)

### 5.4、APM-Grafana告警

- 打开grafana-版本号下，conf目录下的 

  defaults.ini

   ，填写[smtp]节点信息

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  [smtp]
  enabled = true
  host = smtp.163.com:25
  user =ego_it
  # If the password contains # or ; you have to wrap it with triple quotes. Ex """#password;"""
  password =******
  cert_file =
  key_file =
  skip_verify = false
  from_address = ego_it@163.com
  from_name = Grafana
  ehlo_identity =
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

- 配置
  ![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190407165152088-1039821560.png)

- 添加报警触发条件

  

  添加新的查询条件
  ![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190407170024405-2096792235.png)

  创建alert
  ![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190407170126634-2039804567.png)

  ![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190407171112878-137803132.png) 

## 6、Exceptionless

- 在线方式
  https://exceptionless.com/注册用户，新建Organizations和Project，并选项目类型。

- 离线方式

  下载地址：https://github.com/exceptionless/Exceptionless/releases
        解压压缩包，运行Start.bat
        系统会自动下载elasticsearch和kibana

  

  ElasticSearch是一个基于Lucene的搜索服务器。它提供了一个分布式多用户能力的全文搜索引擎，基于RESTful  web接口。Elasticsearch是用Java开发的，并作为Apache许可条款下的开放源码发布，是当前流行的企业级搜索引擎。设计用于云计算中，能够达到实时搜索，稳定，可靠，快速，安装使用方便。

  Kibana是一个开源的分析与可视化平台，设计出来用于和Elasticsearch一起使用的。你可以用kibana搜索、查看、交互存放在Elasticsearch索引里的数据，使用各种不同的图表、表格、地图等kibana能够很轻易地展示高级数据分析与可视化。

### 6.1、创建组织

![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190407220454363-1596043274.png)

### 6.2、创建项目

![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190407220533665-1150476865.png)

### 6.3、集成Exceptionless 客户端

```
Install-Package Exceptionless.AspNetCore
```

通过 API 密钥执行  app.UseExceptionless("Qa3OzvEJC9FXo9SdwwFBv6bAkVbjWQKbV6hhtYEM")  方法

### 6.4、示例代码

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
#region Exceptionless测试
try
{
    ExceptionlessClient.Default.SubmitLog("调试Exceptionless.Logging.LogLevel.Debu", Exceptionless.Logging.LogLevel.Debug);
    ExceptionlessClient.Default.SubmitLog("错误Exceptionless.Logging.LogLevel.Error", Exceptionless.Logging.LogLevel.Error);
    ExceptionlessClient.Default.SubmitLog("大错Exceptionless.Logging.LogLevel.fatal", Exceptionless.Logging.LogLevel.Fatal);
    ExceptionlessClient.Default.SubmitLog(" Exceptionless.Logging.LogLevel.Info", Exceptionless.Logging.LogLevel.Info);
    ExceptionlessClient.Default.SubmitLog(" Exceptionless.Logging.LogLevel.Off", Exceptionless.Logging.LogLevel.Off);
    ExceptionlessClient.Default.SubmitLog(" Exceptionless.Logging.LogLevel.Other", Exceptionless.Logging.LogLevel.Other);
    ExceptionlessClient.Default.SubmitLog(" Exceptionless.Logging.LogLevel.Trace", Exceptionless.Logging.LogLevel.Trace);
    ExceptionlessClient.Default.SubmitLog("Exceptionless.Logging.LogLevel.Warn", Exceptionless.Logging.LogLevel.Warn);


    var data = new Exceptionless.Models.DataDictionary();
    data.Add("data1key", "data1value");
    ExceptionlessClient.Default.SubmitEvent(new Exceptionless.Models.Event {
        Count = 1,
        Date = DateTime.Now,
        Data = data, Geo = "geo",
        Message = "message",
        ReferenceId = "referencelId",
        Source = "source",
        Tags = new Exceptionless.Models.TagSet() { "tags" },
        Type = "type",
        Value = 1.2m });
    ExceptionlessClient.Default.SubmitFeatureUsage("feature");
    ExceptionlessClient.Default.SubmitNotFound("404 not found");
    ExceptionlessClient.Default.SubmitException(new Exception("自定义异常"));

    throw new DivideByZeroException("throw DivideByZeroException的异常：" + DateTime.Now);
}
catch (Exception exc)
{
    exc.ToExceptionless().Submit();
}
#endregion
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

### 6.5、本地部署

[本地部署官方wiki](https://github.com/exceptionless/Exceptionless/wiki/Self-Hosting)

下载[Windows版本安装包](https://github.com/exceptionless/Exceptionless/releases/download/v4.1.0/Exceptionless.4.1.2861.zip)，并进行解压，然后双击运行Start.bat即可

需要环境：

- .NET 4.6
- Java 1.8+ (The JAVA_HOME environment variable must also be configured when using Windows.)
- IIS Express 8+
- PowerShell 3+

![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190408005959475-301678242.png)

### 6.6、项目集成

注意：本地化不能再使用 app.UseExceptionless(apiKey: "tJxBWkCbgDLCMoKKqWII3Eyw4aJOsyOCgX26Yurm"); 形式来上传日志数据，应采用另外的方式：配置文件方式

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
"Exceptionless": {
  "ApiKey": "tJxBWkCbgDLCMoKKqWII3Eyw4aJOsyOCgX26Yurm",
  "ServerUrl": "http://localhost:50000",
  "DefaultData": {
  },
  "DefaultTags": [ "xplat" ],
  "Settings": {
    "FeatureXYZEnabled": false
  }
}
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

然后修改 Startup.cs 

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    ...
    //app.UseExceptionless(apiKey: "tJxBWkCbgDLCMoKKqWII3Eyw4aJOsyOCgX26Yurm");
    //上方的方法本地化不适用
    app.UseExceptionless(Configuration);
    ...
}
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

搞定

![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190408011048372-1289756004.png)

### 6.7、查询语法

![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190408011210325-1643925285.png)

示例

![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190408011304850-2139966505.png)

### 6.8、常见问题

![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190408004614966-1563274955.png)

 

Invoke-WebRequest : 请求被中止: 未能创建 SSL/TLS 安全通道。

elasticsearch-XXX”，因为该路径不存在。

解决方案：编辑Start-ElasticSearch.ps1，将所需的文件全部下载下来，然后解压进行拷贝，如下图，然后在双击运行Start.bat即可

![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190408004748705-118175774.png)

帮助类：

![img](assets/ContractedBlock.gif)  View Code

## 7、数据一致性

![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190408011528008-666282001.png)

- Ｃ：数据一致性(consistency)：如果系统对一个写操作返回成功，那么之后的读请求都必须读到这个新数据；如果返回失败，那么所有读操作都不能读到这个数据，对调用者而言数据具有强一致性(strong  consistency) (又叫原子性 atomic、线性一致性 linearizable consistency)
- A：服务可用性(availability)：所有读写请求在一定时间内得到响应，可终止、不会一直等待
- P：分区容错性(partition-tolerance)：在网络分区的情况下，被分隔的节点仍能正常对外服务

### 7.1、最终一致性

- 可用性，可靠性，
- 最终一致性：在微服务之间使用事件驱动通信和发布订阅系统实现最终一致性

![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190408012540894-5823289.png)

- **强一致性**：当更新操作完成之后，任何多个后续进程或者线程的访问都会返回最新的更新过的值。这种是对用户最友好的，就是用户上一次写什么，下一次就保证能读到什么。根据  CAP 理论，这种实现需要牺牲可用性。=> 在传统单体式应用中，大部分都是强一致性的应用，想想我们写过多少工作单元模式的Code？
- **弱一致性**：系统并不保证续进程或者线程的访问都会返回最新的更新过的值。系统在数据写入成功之后，不承诺立即可以读到最新写入的值，也不会具体的承诺多久之后可以读到。
- **最终一致性**：弱一致性的特定形式。系统保证在没有后续更新的前提下，系统**最终**返回上一次更新操作的值。在没有故障发生的前提下，不一致窗口的时间主要受通信延迟，系统负载和复制副本的个数影响。
- 为保证可用性，互联网分布式架构中经常将**强一致性需求**转换成**最终一致性**的需求，并通过系统执行**幂等性**的保证，保证数据的**最终一致性**。

　　在微服务架构中，各个微服务之间通常会使用事件驱动通信和发布订阅系统实现最终一致性。

### 7.2、最终一致性-补偿机制

- Polly：实现重试，熔断机制
- 或提供后台任务调度实现补偿

![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190408012651593-790150137.png)

### 7.3、幂等和防重

- 其任意多次执行对资源本身所产生的影响均与一次执行的影响相同。
- 对重复删除或返回成功结果；防重可以在数据库级别处理也以以在MQ级别

### 7.4、MassTransit

MassTransit 是一个自由、开源、轻量级的消息总线, 用于使用.  NET 框架创建分布式应用程序。MassTransit 在现有消息传输上提供了一组广泛的功能, 从而使开发人员能够友好地使用基于消息的会话模式异步连接服务。基于消息的通信是实现面向服务的体系结构的可靠和可扩展的方式。

　　官网地址：http://masstransit-project.com/，GitHub地址：https://github.com/MassTransit/MassTransit （目前：1590Star，719Fork）

　　类似的国外开源组件还有[NServiceBus](http://particular.net/)，没有用过，据说MassTransit比NServiceBus更加轻量级，并且在开发之初就选用了RabbitMQ作为消息传输组件，当然MassTransit还支持Azure Service Bus。类似的国内开源组件则有园友savorboard（杨晓东）的[CAP](https://www.cnblogs.com/savorboard/p/cap.html)

### 7.5、最简单的发送/接收实例

这里以MassTransit + RabbitMQ为例子，首先请确保安装了RabbitMQ，如果没有安装，可以阅读我的[RabbitMQ在Windows环境下的安装与使用](https://www.cnblogs.com/wyt007/p/9054608.html)去把RabbitMQ先安装到你的电脑上。另外，RabbitMQ的背景知识也有一堆，有机会也还是要了解下Exchange，Channel、Queue等内容。

![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190409000439976-316452160.png)

- 准备两个控制台程序，一个为Sender（发送者），一个为Receiver（接收者），并分别通过NuGet安装MassTransit以及MassTransit.RabbitMQ

  ```
  Install-Package MassTransit
  Install-Package MassTransit.RabbitMQ
  ```

- 编写Sender

  ![img](https://images.cnblogs.com/OutliningIndicators/ContractedBlock.gif)  View Code

   这里首先连接到我的RabbitMQ服务，然后向指定的Queue发送消息（这里是一个ABC类型的实例对象）。

- 编写Receiver

  ![img](https://images.cnblogs.com/OutliningIndicators/ContractedBlock.gif)  View Code

  对于Receiver，要做的事就只有两件：一是连接到RabbitMQ，二是告诉RabbitMQ我要接收哪个消息队列的什么类型的消息。

- 测试一下：
  ![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190409004130486-692718907.png)

### 7.6、一对一的发布/订阅实例（类似于RabbitMQ的工作模式）

除了简单的发送/接收模式外，我们用的更多的是发布/订阅这种模式。

**注意：发布方如果发布时没有订阅方，发布的数据将会丢失**

- 准备下图所示的类库和控制台项目，并对除Messages类库之外的其他项目安装MassTransit以及MassTransit.RabbitMQ。
  ![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190409010300518-1134965662.png)

- MEDemo_Entity

   类库：准备需要传输的实体类信息

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  public interface IEntity
  {
     int ID { get; set; }
  }
  
  public class Entity:IEntity
  {
      public int ID { get; set; }
      public string Name { get; set; }
      public DateTime Time { get; set; }
  }
  
  public class MyEntity:Entity
  {
      public int Age { get; set; }
  }
  
  public class YouEntity
  {
      public int ID { get; set; }
      public string Name { get; set; }
      public DateTime Time { get; set; }
      public int Age { get; set; }
  }
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

- MEDemo_Producer

   ：接收我的消息吧骚年们

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  static void Main(string[] args)
  {
      Console.Title = "发布方";
  
      var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            var host = cfg.Host(new Uri("rabbitmq://localhost"), hst =>
            {
                hst.Username("guest");
                hst.Password("guest");
            });
  
        });
      bus.Start();
      do
      {
          Console.WriteLine("请出请按q,否则请按其他键！");
  
          string value = Console.ReadLine();
  
          if (value.ToLower() == "q")
          {
              break;
          }
  
          bus.Publish(new Entity() { ID = 1, Name = "张三", Time = DateTime.Now });
      }
      while (true);
  
      bus.Stop();
  }
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  这里向RabbitMQ发布了两个不同类型的消息（Entity和IEntity）

- MEDemo_ConsumerA

   ：我只接收Entity和IEntity类型的消息，其他的我不要

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  class Program
  {
      static void Main(string[] args)
      {
          Console.Title="订阅方";
          var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
          {
              var host = cfg.Host(new Uri("rabbitmq://localhost/"), hst =>
              {
                  hst.Username("guest");
                  hst.Password("guest");
              });
              cfg.ReceiveEndpoint(host, "mewyt", e =>
              {                   
                  e.Consumer<IEntityConsumer>();
                  e.Consumer<EntityConsumer>();
                  e.Consumer<MyEntityConsumer>();
              });
              cfg.ReceiveEndpoint(host, "mewyt1", e =>
              {
                  e.Consumer<YouEntityConsumer>();                
              });
          });
  
          bus.Start();        
          Console.ReadLine();
          bus.Stop();
      }
  }
  
  public class IEntityConsumer : IConsumer<IEntity>
  {
      public async Task Consume(ConsumeContext<IEntity> context)
      {           
          await Console.Out.WriteLineAsync($"IEntityConsumer 类型 {context.Message.GetType()} {context.Message.ID}");
  
      }
  }
  public class EntityConsumer : IConsumer<Entity>
  {
      public async Task Consume(ConsumeContext<Entity> context)
      {
          await Console.Out.WriteLineAsync($"EntityConsumer  类型 {context.Message.GetType()}  {context.Message.ID} {context.Message.Name} {context.Message.Time}");
      }
  }
  public class MyEntityConsumer : IConsumer<MyEntity>
  {
      public async Task Consume(ConsumeContext<MyEntity> context)
      {
          await Console.Out.WriteLineAsync($"MyEntityConsumer 类型 {context.Message.GetType()}  {context.Message.ID} {context.Message.Name} {context.Message.Time} {context.Message.Age}");
      }
  }
  
  public class YouEntityConsumer : IConsumer<YouEntity>
  {
      public async Task Consume(ConsumeContext<YouEntity> context)
      {
          await Console.Out.WriteLineAsync($"YouEntityConsumer 类型 {context.Message.GetType()}  {context.Message.ID} {context.Message.Name} {context.Message.Time} {context.Message.Age}");
      }
  }
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

- 测试一下：启动两个MEDemo_ConsumerA，一个MEDemo_Producer
  ![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190409011407842-1171273102.png)

### 7.6、一对多的发布/订阅实例（队列名不同即可）

![img](https://img2018.cnblogs.com/blog/991704/201904/991704-20190409013202053-1315548363.png)

- PSDemo_Entity

   类库：准备需要传输的实体类信息

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  public class EntityA
  {
      public string Name { get; set; }
  
      public DateTime Time { get; set; }
  }
  public class EntityB
  {
      public string Name { get; set; }
      public DateTime Time { get; set; }
      public int Age { get; set; }
  }
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

- PSDemo_Publisher

   ：发布消息

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  class Program
  {
      static void Main(string[] args)
      {
         var bus= Bus.Factory.CreateUsingRabbitMq(cfg =>
          {
              var host = cfg.Host(new Uri("rabbitmq://localhost/"), hst =>
              {
                  hst.Username("guest");
                  hst.Password("guest");
              });               
          });
          do
          {
              Console.WriteLine("请出请按q,否则请按其他键！");
              string value = Console.ReadLine();
              if (value.ToLower() == "q")
              {
                  break;
              }
  
              bus.Publish(new PSDemo_Entity.EntityA() { Name="张三", Time = DateTime.Now });
              bus.Publish(new PSDemo_Entity.EntityB() { Name = "李四", Time = DateTime.Now,Age=22 });
          }
          while (true);        
  
          bus.Stop();
      }
  }
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

- PSDemo_SubscriberA

   ：订阅EntityA和EntityB

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  class Program
  {
      static void Main(string[] args)
      {
          Console.Title="订阅者A";
  
          var bus= Bus.Factory.CreateUsingRabbitMq(cfg =>
          {
              var host = cfg.Host(new Uri("rabbitmq://localhost/"), hst =>
              {
                  hst.Username("guest");
                  hst.Password("guest");
              });
  
              cfg.ReceiveEndpoint(host, "wytPSA", e =>
              {
                  e.Consumer<ConsumerA>();
                  e.Consumer<ConsumerB>();
              });
          });        
  
          bus.Start();        
          Console.ReadLine();
          bus.Stop();
      }
  }
  public class ConsumerA : IConsumer<PSDemo_Entity.EntityA>
  {
      public async Task Consume(ConsumeContext<PSDemo_Entity.EntityA> context)
      {
          await Console.Out.WriteLineAsync($"订阅者A  ConsumerA收到信息: {context.Message.Name}  {context.Message.Time} 类型：{context.Message.GetType()}");
      }
  }
  public class ConsumerB : IConsumer<PSDemo_Entity.EntityB>
  {
      public async Task Consume(ConsumeContext<PSDemo_Entity.EntityB> context)
      {
          await Console.Out.WriteLineAsync($"订阅者A  ConsumerB收到信息: {context.Message.Name}  {context.Message.Time} 类型：{context.Message.GetType()}");
      }
  }
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

- PSDemo_SubscriberB

   ：订阅EntityA

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  class Program
  {
      static void Main(string[] args)
      {
          Console.Title="订阅者B";
  
          var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
          {
              var host = cfg.Host(new Uri("rabbitmq://localhost/"), hst =>
              {
                  hst.Username("guest");
                  hst.Password("guest");
              });
  
              cfg.ReceiveEndpoint(host, "wytPSB", e =>
              {
                  e.Consumer<ConsumerA>();
              });
          });
  
          bus.Start();     
          Console.ReadLine();
          bus.Stop();
      }
  }
  public class ConsumerA : IConsumer<PSDemo_Entity.EntityA>
  {
      public async Task Consume(ConsumeContext<PSDemo_Entity.EntityA> context)
      {
          await Console.Out.WriteLineAsync($"订阅者B  ConsumerA收到信息:  {context.Message.Name}  {context.Message.Time}  类型：{context.Message.GetType()}");
      }
  }
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

- 测试一下：启动PSDemo_SubscriberA和PSDemo_SubscriberB，一个PSDemo_Publisher

  

   

### 7.7、带返回状态消息的示例

之前的例子都是发布之后，不管订阅者有没有收到以及收到后有没有处理成功（即有没有返回消息，类似于HTTP请求和响应），在MassTransit中提供了这样的一种模式，并且还可以结合GreenPipes的一些扩展方法实现重试、限流以及熔断机制。这一部分详见官方文档：http://masstransit-project.com/MassTransit/usage/request-response.html

1. 准备下图所示的三个项目：通过NuGet安装MassTransit以及MassTransit.RabbitMQ
   ![img](https://img2018.cnblogs.com/blog/991704/201905/991704-20190501131654874-1966926968.png)

2. RRDemo_Entity.Entity

    ：准备请求和响应的消息传输类型

   [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

   ```
   public interface IRequestEntity
   {
       int ID { get; set; }
       string Name { get; set; }
   }
   public class RequestEntity : IRequestEntity
   {
       public int ID { get; set; }
       public string Name { get; set; }
   }
   
   public interface IResponseEntity
   {
       int ID { get; set; }
       string Name { get; set; }
   
       int RequestID { get; set; }
   }
   public class ResponseEntity : IResponseEntity
   {
       public int ID { get; set; }
       public string Name { get; set; }
   
       public int RequestID { get; set; }
   }
   ```

   [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

3. RRDemo_Server.Program

    请求接收端

   [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

   ```
   class Program
   {
       static void Main(string[] args)
       {
           Console.Title = "应答方";
           var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
           {
               var host = cfg.Host(new Uri("rabbitmq://localhost/"), hst =>
               {
                   hst.Username("guest");
                   hst.Password("guest");
               });
               cfg.ReceiveEndpoint(host, "request_response_wyt", e =>
               {
                   e.Consumer<RequestConsumer>();
               });
           });
           bus.Start();     
           Console.ReadLine();
           bus.Stop();
       }
   }
   
   public class RequestConsumer : IConsumer<IRequestEntity>
   {
       public async Task Consume(ConsumeContext<IRequestEntity> context)
       {
           Console.ForegroundColor = ConsoleColor.Red;
           await Console.Out.WriteLineAsync($"收到请求id={context.Message.ID} name={context.Message.Name}");
           Console.ResetColor();
           var response = new ResponseEntity
           {
               ID = 22,
               Name = $"李四",
               RequestID = context.Message.ID
           };
           Console.ForegroundColor = ConsoleColor.Green;
           Console.WriteLine($"应答ID={response.ID},Name={response.Name},RequestID={response.RequestID}");
           Console.ResetColor();
           context.Respond(response);
       }
   }
   ```

   [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

4. RRDemo_Client.Program

    请求发送端

   [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

   ```
   static void Main(string[] args)
   {
       Console.Title = "请求方";
   
       var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
       {
           var host = cfg.Host(new Uri("rabbitmq://localhost/"), hst =>
           {
               hst.Username("guest");
               hst.Password("guest");
           });
           //重试
           cfg.UseRetry(ret =>
           {
               ret.Interval(3, 10);// 消费失败后重试3次，每次间隔10s
           });
           //限流
           cfg.UseRateLimit(1000, TimeSpan.FromSeconds(100));// 1分钟以内最多1000次调用访问
           //熔断
           cfg.UseCircuitBreaker(cb =>
           {
               cb.TrackingPeriod = TimeSpan.FromSeconds(60);//1分钟
               cb.TripThreshold = 15;// 当失败的比例至少达到15%才会启动熔断
               cb.ActiveThreshold = 10;// 当失败次数至少达到10次才会启动熔断
               cb.ResetInterval = TimeSpan.FromMinutes(5);// 当在1分钟内消费失败率达到15%或调用了10次还是失败时，暂停5分钟的服务访问
   
           });
       });
       bus.Start();
   
       var serviceAddress = new Uri($"rabbitmq://localhost/request_response_wyt");
       var client = bus.CreateRequestClient<IRequestEntity, IResponseEntity>(serviceAddress, TimeSpan.FromHours(10));
       // 创建请求客户端，10H之内木有回馈则认为是超时(Timeout)
   
       while (true)
       {
           Console.WriteLine("请出请按q,否则请按其他键！");
           string value = Console.ReadLine();
           if (value.ToLower() == "q")
           {
               break;
           }
   
           Task.Run(async () =>
           {
               var request = new RequestEntity() { ID = 1, Name = "张三" };
               var response = await client.Request(request);
   
               Console.WriteLine($"请求ID={request.ID},Name={request.Name}");
               Console.WriteLine($"应签ID={response.ID},Name={response.Name},RequestID={response.RequestID}");
           }).Wait();
       }
   
   }
   ```

   [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

5. 效果展示

   

   **注意**：这里的请求方关闭后应答方则无法将应答再回复给请求方，会丢失

### 7.8、带Observer模式的发布/订阅示例

在某些场景中，我们需要针对一个消息进行类似于AoP（面向切面编程）或者监控的操作，比如在发送消息之前和结束后记日志等操作，我们可以借助MassTransit中的Observer模式来实现。（在MassTransit的消息接收中，可以通过两种模式来实现：一种是基于实现IConsumer接口，另一种就是基于实现IObserver接口）关于这一部分，详见官方文档：http://masstransit-project.com/MassTransit/usage/observers.html

- 准备以下图所示的项目：
  ![img](https://img2018.cnblogs.com/blog/991704/201905/991704-20190502121321435-190168354.png)

- ObserverSubscriber

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  class Program
  {
      static void Main(string[] args)
      {
          Console.Title = "订阅方";
          var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
          {
              var host = cfg.Host(new Uri("rabbitmq://localhost/"), hst =>
              {
                  hst.Username("guest");
                  hst.Password("guest");
              });
              cfg.ReceiveEndpoint(host, "ObserverTest", e =>
              {
                  e.Consumer<EntityConsumer>();
              });
          });
          var observer = new ReceiveObserver();
          var handle = bus.ConnectReceiveObserver(observer);
          bus.Start();
          Console.ReadLine();
          bus.Stop();
      }
  }
  public class ReceiveObserver : IReceiveObserver
  {
      public Task PreReceive(ReceiveContext context)
      {
  
          Console.WriteLine("------------------PreReceive--------------------");
          Console.WriteLine(Encoding.Default.GetString(context.GetBody()));
          Console.WriteLine("--------------------------------------");
          return Task.CompletedTask;
      }
  
      public Task PostReceive(ReceiveContext context)
      {
      
          Console.WriteLine("-----------------PostReceive---------------------");
          Console.WriteLine(Encoding.Default.GetString(context.GetBody()));
          Console.WriteLine("--------------------------------------");
          return Task.CompletedTask;
      }
  
      public Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType)
          where T : class
      {
     
          Console.WriteLine("------------------PostConsume--------------------");
          Console.WriteLine($"ID={ (context.Message as Entity).ID},Name={(context.Message as Entity).Name},Time={(context.Message as Entity).Time}");
          Console.WriteLine("--------------------------------------");
          return Task.CompletedTask;
      }
  
      public Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan elapsed, string consumerType, Exception exception) where T : class
      {
       
          Console.WriteLine("-----------------ConsumeFault---------------------");
          Console.WriteLine($"ID={ (context.Message as Entity).ID},Name={(context.Message as Entity).Name},Time={(context.Message as Entity).Time}");
          Console.WriteLine("--------------------------------------");
          return Task.CompletedTask;
      }
  
      public Task ReceiveFault(ReceiveContext context, Exception exception)
      {            
          Console.WriteLine("----------------ReceiveFault----------------------");
          Console.WriteLine(Encoding.Default.GetString(context.GetBody()));
          Console.WriteLine("--------------------------------------");
          return Task.CompletedTask;
      }
  }
  
  
  public class EntityConsumer : IConsumer<Entity>
  {
      public async Task Consume(ConsumeContext<Entity> context)
      {
          await Console.Out.WriteLineAsync($"IEntityConsumer 类型 {context.Message.GetType()} {context.Message.ID} {context.Message.Age} {context.Message.Name} {context.Message.Time}");
  
      }
  }
  
  public class Entity
  {
      public int ID { get; set; }
  
      public int Age { get; set; }
      public string Name { get; set; }
      public DateTime Time { get; set; }
  
  }
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

- ObserverPublish

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  ```
  class Program
  {
      static void Main(string[] args)
      {
          Console.Title = "发布方";
  
          var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
          {
              var host = cfg.Host(new Uri("rabbitmq://localhost"), hst =>
              {
                  hst.Username("guest");
                  hst.Password("guest");
              });
  
          });
          var observer = new SendObserver();
          var handle = bus.ConnectSendObserver(observer);
  
          var observer1 = new PublishObserver();
          var handle1 = bus.ConnectPublishObserver(observer1);
          bus.Start();
          do
          {
              Console.WriteLine("请出请按q,否则请按其他键！");
  
              string value = Console.ReadLine();
  
              if (value.ToLower() == "q")
              {
                  break;
              }
  
              bus.Publish(new Entity() { ID = 1, Age = 10, Name = "张三", Time = DateTime.Now });
          }
          while (true);
  
          bus.Stop();
      }
  }
  
  public class PublishObserver : IPublishObserver
  {
      public Task PrePublish<T>(PublishContext<T> context)
          where T : class
      {
          Console.WriteLine("------------------PrePublish--------------------");
          Console.WriteLine($"ID={ (context.Message as Entity).ID},Name={(context.Message as Entity).Name},Time={(context.Message as Entity).Time}");
          Console.WriteLine("--------------------------------------");
          return Task.CompletedTask;
      }
  
      public Task PostPublish<T>(PublishContext<T> context)
          where T : class
      {
          Console.WriteLine("------------------PostPublish--------------------");
          Console.WriteLine($"ID={ (context.Message as Entity).ID},Name={(context.Message as Entity).Name},Time={(context.Message as Entity).Time}");
          Console.WriteLine("--------------------------------------");
          return Task.CompletedTask;
      }
  
      public Task PublishFault<T>(PublishContext<T> context, Exception exception)
          where T : class
      {
          Console.WriteLine("------------------PublishFault--------------------");
          Console.WriteLine($"ID={ (context.Message as Entity).ID},Name={(context.Message as Entity).Name},Time={(context.Message as Entity).Time}");
          Console.WriteLine("--------------------------------------");
          return Task.CompletedTask;
      }
  }
  
  public class SendObserver : ISendObserver
  {
      public Task PreSend<T>(SendContext<T> context)
          where T : class
      {
          Console.WriteLine("------------------PreSend--------------------");
          Console.WriteLine($"ID={ (context.Message as Entity).ID},Name={(context.Message as Entity).Name},Time={(context.Message as Entity).Time}");
          Console.WriteLine("--------------------------------------");
          return Task.CompletedTask;
      }
  
      public Task PostSend<T>(SendContext<T> context)
          where T : class
      {
          Console.WriteLine("------------------PostSend--------------------");
          Console.WriteLine($"ID={ (context.Message as Entity).ID},Name={(context.Message as Entity).Name},Time={(context.Message as Entity).Time}");
          Console.WriteLine("--------------------------------------");
          return Task.CompletedTask;
      }
  
      public Task SendFault<T>(SendContext<T> context, Exception exception)
          where T : class
      {
          Console.WriteLine("------------------SendFault--------------------");
          Console.WriteLine($"ID={ (context.Message as Entity).ID},Name={(context.Message as Entity).Name},Time={(context.Message as Entity).Time}");
          Console.WriteLine("--------------------------------------");
          return Task.CompletedTask;
      }
  }
  ```

  [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

- 效果展示

  Publish:

  ![img](https://images.cnblogs.com/OutliningIndicators/ContractedBlock.gif)  View Code

   Subscribe:

  ![img](https://images.cnblogs.com/OutliningIndicators/ContractedBlock.gif)  View Code

### 7.9、数据一致性示例

详见：https://github.com/786744873/DataConsistentSample

![img](https://img2018.cnblogs.com/blog/991704/201905/991704-20190502150820093-1882337153.png)

 

## 8、Jenkins

官方地址：https://jenkins.io/

Jenkins 是一款流行的开源持续集成（CI）与持续部署（CD）工具，广泛用于项目开发，具有自动化构建、测试和部署等功能。

　　使用Jenkins的目的在于：

　　（1）持续、自动地构建/测试软件项目。 
　　（2）监控软件开放流程，快速问题定位及处理，提升开发效率。

### 8.1、Jenkins下载与安装

这里我们下载Windows版本的

![img](https://img2018.cnblogs.com/blog/991704/201905/991704-20190502144039416-1787127423.png)

安装完成后会提示我们解锁 Jenkins

![img](https://img2018.cnblogs.com/blog/991704/201905/991704-20190502144129350-812381050.png)

这里选择**安装推荐的插件**

**![img](https://img2018.cnblogs.com/blog/991704/201905/991704-20190502144630958-1135438208.png)**

创建管理账户 => 也可以直接使用admin账户继续

![img](https://img2018.cnblogs.com/blog/991704/201905/991704-20190502144924052-807312107.png)

配置Jenkins端口，默认8080，最好不要使用8080端口

![img](https://img2018.cnblogs.com/blog/991704/201905/991704-20190502145056594-1826837638.png)

修改Jenkins服务端口，改为8080-->8081

修改安装目录下 jenkins.xml 文件

![img](https://img2018.cnblogs.com/blog/991704/201905/991704-20190502150113989-1735383372.png)

然后重启Jenkins服务

### 8.2、持续集成Asp.Net Core项目

1. 我们以Github上面的项目为例，github项目地址：[https://github.com/786744873/first.git
   ](https://github.com/786744873/first.git)![img](https://img2018.cnblogs.com/blog/991704/201905/991704-20190502150348868-767818407.png)

2. 配置源代码
   ![img](https://img2018.cnblogs.com/blog/991704/201905/991704-20190502151219544-1190187689.png)

3. 构建触发器（这里每半小时轮询一次）
   ![img](https://img2018.cnblogs.com/blog/991704/201905/991704-20190502151319994-2032373751.png)

4. 构建

   

   

   [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

   ```
   cd CITest
   cd CITest
   dotnet restore
   dotnet build
   dotnet publish -o "bin\Debug\netcoreapp2.0\publish"
   cd bin\Debug\netcoreapp2.0\publish
   docker rm clitest -f
   docker rmi clitest -f
   docker build -t clitest:latest .
   docker run -p 4555:4555 -d --name clitest clitest:latest
   ```

   [![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

5. 保存，然后去配置构建邮件发送

   Jenkins->系统管理->系统设置

   设置系统管理员收件地址(实际上这边配置的是发件人的邮箱地址)：

   

   ![img](https://img2018.cnblogs.com/blog/991704/201905/991704-20190502201613057-1867497362.png)

    

6. 继续进行项目配置
   ![img](https://img2018.cnblogs.com/blog/991704/201905/991704-20190502202158887-1381426956.png)

   ![img](https://img2018.cnblogs.com/blog/991704/201905/991704-20190502202234050-1793733017.png)

7. 构建项目

   

   ![img](https://img2018.cnblogs.com/blog/991704/201905/991704-20190502204354741-604278101.png)

    

 

作者：[一个大西瓜](https://www.cnblogs.com/wyt007/)

出处：[https://www.cnblogs.com/wyt007/ ](https://www.cnblogs.com/wyt007/)