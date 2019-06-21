# 	[docker 常用命令](https://www.cnblogs.com/talkjd-04/p/10488533.html)



因为已经安装docker，所以我们可以使用docker --help 命令查看docker 的基本命令都有哪些？

　　docker pull：是拉去镜像。

　　docker pash :推送镜像。

　　docker images：查看已存在镜像。

　　docker rmi +imagesID ：删除镜像。

　　docker  build + 容器Id +newName：构建新的镜像 通过Dockerfile 方式。

　　docker commit ：通过容器构建镜像（-a :提交的镜像作者；-c :使用Dockerfile指令来创建镜像；-m :提交时的说明文字；-p :在commit时，将容器暂停）。

　　docker history :历史镜像。

　　docker run **：**创建一个新的容器并运行一个命令。

　　 docker start :启动一个或多少已经被停止的容器。

　　 docker stop :停止一个运行中的容器（向容器发送停止的消息，返回后停止）。

　　 docker restart :重启容器。

　　 docker kill :杀掉一个运行中的容器（直接杀死）。

　　 docker pause:暂停容器中所有的进程。

 

   docker unpause:恢复容器中所有的进程。

   docker create **：**创建一个新的容器但不启动它。

　　 docker exec **：**在运行的容器中执行命令。

　　 docker ps **:** 列出容器。

　　 docker inspect **:** 获取容器/镜像的元数据。

   docker attach **:**连接到正在运行中的容器。

   docker top **:**查看容器中运行的进程信息。

　　 docker cp **:**用于容器与主机之间的数据拷贝。

   docker **search：**从Docker Hub查找镜像.

　　docker tag **:**标记本地镜像，将其归入某一仓库.

　　docker info : 显示 Docker 系统信息，包括镜像和容器数.

　　docker version :显示 Docker 版本信息。

　　docker network ls：查看网络设备。

　　docker network create +网络设备：创建网络设备

　　docker network inspect +网络设备：查看网络设备的详细信息。 

　　docker port +容器名称:查询改容器对应的主机端口的信息

　　--icc 是否允许容器连接（默认是为true:允许容器网络连接;false：则反之）。