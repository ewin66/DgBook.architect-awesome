​     [全部文章](https://blog.csdn.net/u011781521)>              Docker                

- 排序：

  [默认](javascript:void(0);)

  [按更新时间](https://blog.csdn.net/u011781521/article/category/6480920?orderby=UpdateTime)

  [按访问量](https://blog.csdn.net/u011781521/article/category/6480920?orderby=ViewCount)

  [ 		 			 		RSS订阅](https://blog.csdn.net/u011781521/rss/list)     

####          [         原        Centos6.5通过Dockerfile构建Nginx并安装lua-nginx-module模块      ](https://blog.csdn.net/u011781521/article/details/82150833)     

​       [            一、前言  公司的服务器版本用的是：   centos6.5 2.6.32-431.el6.x86_64  通过命令安装docker   yum -y install docker-io  安装成功之后，启动Docker，提示：   docker: unrecognized service ...      ](https://blog.csdn.net/u011781521/article/details/82150833)     

​         2018-08-28 17:27:02       



​         阅读数 656        



​         评论数 0        

####          [         原        Docker(七)----搭建Portainer可视化界面      ](https://blog.csdn.net/u011781521/article/details/80469804)     

​       [          一、什么是Portainer？Portainer是Docker的图形化管理工具，提供状态显示面板、应用模板快速部署、容器镜像网络数据卷的基本操作（包括上传下载镜像，创建容器等操作）、事件日志显示、容器控制台操作、Swarm集群和服务等集中管理和操作、登录用户管理和控制等功能。功能十分全面，基本能满...       ](https://blog.csdn.net/u011781521/article/details/80469804)     

​         2018-05-27 16:07:59       



​         阅读数 22701        



​         评论数 3        

####          [         原        Docker(六)----Swarm搭建Docker集群      ](https://blog.csdn.net/u011781521/article/details/80468985)     

​       [         一、什么是SwarmSwarm这个项目名称特别贴切。在Wiki的解释中，Swarm  behavior是指动物的群集行为。比如我们常见的蜂群，鱼群，秋天往南飞的雁群都可以称作Swarm  behavior。Swarm项目正是这样，通过把多个Docker Engine聚集在一起，形成一个大的docker...      ](https://blog.csdn.net/u011781521/article/details/80468985)     

​         2018-05-27 15:37:42       



​         阅读数 7682        



​         评论数 0        

####          [         原        Docker(五)----Docker-Compose部署nginx代理Tomcat集群      ](https://blog.csdn.net/u011781521/article/details/80466451)     

​       [         1.前言使用Docker镜像部署Nginx代理的多个Tomcat集群目录结构如下:docker-compose.yml etc └── localtime      #文件 mysql ├── conf          #目录 │   └── my.cnf    #文件 └── mysqldb ...      ](https://blog.csdn.net/u011781521/article/details/80466451)     

​         2018-05-27 12:23:14       



​         阅读数 2844        



​         评论数 0        

####          [         原        Docker(四)----Docker-Compose 详解      ](https://blog.csdn.net/u011781521/article/details/80464826)     

​       [         1.  什么是Docker-ComposeCompose项目来源于之前的fig项目，使用python语言编写,与docker/swarm配合度很高。Compose  是 Docker 容器进行编排的工具，定义和运行多容器的应用，可以一条命令启动多个容器，使用Docker Compose不再需要使用sh...       ](https://blog.csdn.net/u011781521/article/details/80464826)     

​         2018-05-27 00:49:25       



​         阅读数 36978        



​         评论数 2        

####          [         原        Docker(三)----Dockerfile搭建Nginx环境与文件挂载      ](https://blog.csdn.net/u011781521/article/details/80464220)     

​       [         1.Dockerfile文件# This my first nginx Dockerfile # Version 1.0  # Base images 基础镜像 FROM centos:centos7  #MAINTAINER 维护者信息 MAINTAINER fendo 2312892206@q...      ](https://blog.csdn.net/u011781521/article/details/80464220)     

​         2018-05-26 20:04:27       



​         阅读数 5954        



​         评论数 0        

####          [         原        Docker(二)----Dockerfile文件详解      ](https://blog.csdn.net/u011781521/article/details/80464065)     

​       [         一、什么是Dockerfile?Dockerfile是一个包含用于组合映像的命令的文本文档。可以使用在命令行中调用任何命令。  Docker通过读取Dockerfile中的指令自动生成映像。docker build命令用于从Dockerfile构建映像。可以在docker  build命令中使用-f标...      ](https://blog.csdn.net/u011781521/article/details/80464065)     

​         2018-05-26 19:01:53       



​         阅读数 3366        



​         评论数 0        

####          [         原        CentOS7搭建Docker私有仓库      ](https://blog.csdn.net/u011781521/article/details/80206649)     

​       [         (一)前言1.什么是Docker私有仓库Registry官方的Docker  hub是一个用于管理公共镜像的好地方，我们可以在上面找到我们想要的镜像，也可以把我们自己的镜像推送上去。但是，有时候我们的服务器无法访问互联网，或者你不希望将自己的镜像放到公网当中，那么你就需要Docker  Registr...      ](https://blog.csdn.net/u011781521/article/details/80206649)     

​         2018-05-05 16:25:27       



​         阅读数 719        



​         评论数 0        

####          [         原        Ubuntu15安装Docker      ](https://blog.csdn.net/u011781521/article/details/55222935)     

​       [         1.更新系统   sudo apt-get update       二、添加Docker的APT仓库   先通过命令   lsb_release    查看Ubuntu发行版本        报: No lsb modules are available 错误、解决方法为安装LSB模块  sud...      ](https://blog.csdn.net/u011781521/article/details/55222935)     

​         2017-02-15 21:47:48       



​         阅读数 787        



​         评论数 0        

####          [         原        Windwos 10 安装Docker      ](https://blog.csdn.net/u011781521/article/details/53673737)     

​       [         一、下载安装包   下载地址   官网地址: https://github.com/boot2docker/windows-installer/releases       国内地址: https://get.daocloud.io/toolbox/ 或者这个 http://get.daoclou...      ](https://blog.csdn.net/u011781521/article/details/53673737)     

​         2016-12-15 18:50:27       



​         阅读数 729        



​         评论数 0        

####          [         原        Docker(一)----CentOS7安装Docker与测试      ](https://blog.csdn.net/u011781521/article/details/53047674)     

​       [         一、安装docker     yum -y install docker-io        安装成功！    二、启动Docker        service docker start      ps -ef | grep docker    ...      ](https://blog.csdn.net/u011781521/article/details/53047674)     

​         2016-11-05 20:36:53       



​         阅读数 2971        



​         评论数 2        