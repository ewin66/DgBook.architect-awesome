#	**Docker使用MariaDB**



作者：speculatecat

链接：https://www.jianshu.com/p/32542630c2bd



这篇文章主要分为两个部分。
 第一部分通过描述使用场景介绍为什么要使用 Docker 以及在 Docker 中使用 MariaDB 有什么优势。
 接下来在第二部分中详细介绍如何安装 Docker ，使用 MariaDB 镜像以及如何挂载数据卷以及迁移数据的技巧。

##  为什么要使用 Docker

程序的开发过程中，尤其是涉及服务器的开发，往往部署的服务器和开发的电脑不是一样的环境，而在现实中，不同的开发者的开发环境不同，甚至同一个人，办公室的开发机，自己的 Laptop ，家里的电脑所使用的系统、环境也不一样。这就可能导致写好的程序在服务器上运行出现问题，或者在办公室未完成的程序，回到家想继续写代码，却又发现因为系统环境不一样而又出现意料之外的 Crash 。
 因此，我们就有理由寻找一种能让我们专注解决问题，而从不断地配置、调试系统环境这些非问题根源的杂务中解放出来。Docker 正是能解决这一需求的一个利器。

##  在 Docker 中使用 MariaDB 的优势

在我的工作中，开发服务器在公司，生产服务器位于云端，他们都是基于 Ubuntu 的 Linux 系统，而我在公司的开发机是一台 WIndow10 的 WorkStation，另外还有一台 Mac 系统的 Laptop 用来移动办公以及回家使用。
 在开发的过程中，服务器和开发机分别是三个不同的系统，然而又因为开发服务器属于公司的内网，因此如果在家还想继续工作的话，连接上开发服务器又不太方便。而为三个系统都安装上 MariaDB 以及其他相应的服务，配置和维护，共享数据库中的数据，都需要花费大量的时间和精力。
 如果使用 Docker ， 那么情况将简单很多。Docker 提供了 Window、 Linux、 MacOS 三个系统的支持，那么只需要在三个系统中都安装好 Docker 服务，然后使用 MariaDB 的 Images，通过 Docker 提供的挂载 Volume 在共享数据库，即可大量减少花费在维护、配置不同系统、服务版本不同的时间。

##  Docker 安装

Docker 安装可以参考 Docker 官网。Window 和 MacOS 系统均有一件安装包，Ubuntu 系统可以他通过下载安装包安装，详细可参照官网介绍。
 安装完成后，由于国内网络原因，连接 Docker Hub 速度有点慢，因此可以替换国内的镜像源。
 这里我使用阿里云提供的 Docker Hub 镜像加速服务，使用这个服务需要先注册一个阿里云开发者账号。详细参照以下操作文档： [阿里云Docker镜像站点](https://link.jianshu.com?t=https://cr.console.aliyun.com/#/accelerator) 。
 Docker 安装完成后，还可以根据需要更改 Docker Images 存放的位置，因为 Docker Images 所占的容量都比较大，因此可以自己开发机或者服务器的具体情况更改。

##  MariaDB 镜像使用【获取-启动-进入】

Docker 中提供了很多 MariaDB 的镜像，可以通过以下**命令查询**

```bash
docker search mariadb
```

一般而言，我们使用官方提供的镜像，以下为获取下载镜像，默认获取最新的版本

```bash
docker pull mariadb	
```

接下来，我们将启动一个 MariaDB 容器

```bash
docker run --name mariadb  -d -p 3306:3306  --privileged=true -e MYSQL_ROOT_PASSWORD=root mariadb
```

docker run --name mariadb_test -e MYSQL_ROOT_PASSWORD=my-secret=pw -d mariadb

> > --name: 以什么名字启动容器
> >
> > -p 3306:3306 :将容器端口映射到服务器端口【主机：容器】
> >
> > -e MYSQL_ROOT_PASSWORD=123456 :设置服务器密码为123456
>
> > --privileged=true：容器内的root拥有真正root权限，否则容器内root只是外部普通用户权限
> >
> > -v /docker/mysql/conf/my.cnf:/etc/my.cnf：**映射配置文件**【[挂载](https://blog.csdn.net/woniu211111/article/details/80968154)】
> >
> > -v /docker/mysql/data:/var/lib/mysql：**映射数据目录** 
>
> docker run --name mysql -p 3306:3306   -e MYSQL_ROOT_PASSWORD=wsx1001  -d mysql
>
> *787b06122b723e547b1216d499b69d99725a703217cd09f49e9ce548f9ba27dc*
>
> docker run -d -p 3306:3306 --privileged=true -v /docker/mysql/conf/my.cnf:/etc/mysql/my.cnf -v /docker/mysql/data:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=123456 --name mysqltest2 mysql:5.7





```
docker run --name mariadb_test -e MYSQL_ROOT_PASSWORD=my-secret=pw -d mariadb
> f5605d02f9f50d1bc813423454fb421ba17513c440487f26b41e26844d652136
docker ps -a
> CONTAINER ID        IMAGE               COMMAND                  CREATED             STATUS                     PORTS               NAMES
  f5605d02f9f5        mariadb             "docker-entrypoint..."   20 seconds ago      Up 18 seconds              3306/tcp            mariadb_test
```

 我们可以看到，一个 ID 为 f5605d02f9f5 的 MariaDB 容器已经在运行中。
 接下来，我们将进入容器，并查看数据库

```bash
docker exec -it mysql mysql -uroot -proot			##	Name
docker exec -it mariadb mariadb -uroot -proot	
docker exec -it f5605d02f9f5 bash					##	ID
```

```sql
docker exec -it f5605d02f9f5 bash
> root@f5605d02f9f5:/#
##  已经进入容器 f5605d02f9f5 中
root@f5605d02f9f5:/##  mysql -u root -p
Enter password: my-secret-pw
> Welcome to the MariaDB monitor.  Commands end with ; or \g.
Your MariaDB connection id is 9
Server version: 10.2.8-MariaDB-10.2.8+maria~jessie mariadb.org binary distribution

Copyright (c) 2000, 2017, Oracle, MariaDB Corporation Ab and others.

Type 'help;' or '\h' for help. Type '\c' to clear the current input statement.

MariaDB [(none)]>
##  以 root 身份进入 mysql client
MariaDB [(none)]> show databases;
+--------------------+
| Database           |
+--------------------+
| information_schema |
| mysql              |
| performance_schema |
+--------------------+
3 rows in set (0.00 sec)
##  可以看到数据库内数据库信息
```

这时的 MariaDB 已经可以正常使用，但是无法远程连接，因此我们需要映射端口来让我们的数据库能被远程访问。

```
docker run -d -P --name mariadb_connect -e MYSQL_ROOT_PASSWORD=my-secret-pw mariadb
> bf2280bb46bc3d624e6c0596c1545c39d4da249590a739401ec64635129d297b
docker ps -a
> CONTAINER ID        IMAGE               COMMAND                  CREATED             STATUS              PORTS                     NAMES
  bf2280bb46bc        mariadb             "docker-entrypoint..."   7 seconds ago       Up 5 seconds        0.0.0.0:32769->3306/tcp   mariadb_connect
```

我们可以看到，通过 `-P` 参数，Docker 会为我们自动分配一个未被使用的端口，这里是 32769，接下来，我们可以通过 Navicat 工具来测试一下是否能连接。





![img](https:////upload-images.jianshu.io/upload_images/1638540-8ae17f2491ab4d1b.jpg?imageMogr2/auto-orient/strip%7CimageView2/2/w/1000)

Navcat 连接成功截图

 我们可以看到，通过在 Navicat 中正确填入主机地址、端口号、用户名及密码，然后点击 Test Conection 即可看到连接测试成功。

 上面我们绑定的参数 

```
-P
```

 是让 Docker 随机映射一个可用的端口，如果我们要自定义映射的端口，可以用 

```
-p hostPort:containerPort
```

 。



##  挂载数据卷用于保存数据库数据

为了方便迁移数据库中的数据，我们可以通过挂载数据卷来实现。

```
##  以挂载本地 /data/Db/mariadb 为例
docker run -d --name mariadb_volume -P -v /data/Db/mariadb:/var/lib/mysql mariadb
```

这样，数据库中的数据将保存在我们挂载的本地文件 `/data/Db/mariadb` 上。我们可以迁移或者备份这个文件夹，来实现数据库迁移。
 一般一个自动生成的空数据库文件，大概有 100多兆 ，而且这个文件夹中包含很多子文件，因此如果通过 `SSH` 或者 `FTP` 传输都需要比较长的时间，这里我们可以通过压缩打包来减少文件夹的容量。

```
##  Ubuntu 压缩
cd /data/Db
tar zcvf mariadb.tar.gz mariadb
##  Mac Window 直接在图形界面压缩
```

打包压缩后，容量会大幅度减少，在 Ubuntu 上，压缩后的文件大小大概在 2兆 左右。然后就可以将这个压缩文件传输要目标计算机，解压到恰当的路径即可使用。

```
##  Ubuntu 解压
tar zxvf mariadb.tar.gz
```

有一点值得注意，如果我们在数据库中创建了新的用户，我们将这个存储数据库数据的数据卷迁移到其他地方，再重新挂载启动数据库后，这些数据库用户也是可用的。因此如果挂载有数据的数据卷时，可以不用 `MYSQL_ROOT_PASSWORD` 这个参数。

##  参考资料

[Docker —— 从入门到实践](https://link.jianshu.com?t=https://www.gitbook.com/book/yeasy/docker_practice/details)  
 [library/mariadb - Docker Hub](https://link.jianshu.com?t=https://hub.docker.com/_/mariadb/)

作者：speculatecat

链接：https://www.jianshu.com/p/32542630c2bd

来源：简书

简书著作权归作者所有，任何形式的转载都请联系作者获得授权并注明出处。



----



#	补录

###	进入后

docker exec -it mysql mysql -uroot -proot

```mysql
select host,user,plugin,authentication_string from mysql.user;
```



```mysql
    USE mysql; 
     
    ALTER USER 'root'@'%' IDENTIFIED WITH mysql_native_password BY 'wsx1001';
     
    FLUSH PRIVILEGES;
```







###  修改ROOT密码**



​        CMD，切换到MariaDB解压后路径/bin,执行“mysql -uroot  -p"切换至MariaDB模式，再执行“SET PASSWORD FOR 'root'@'localhost' = PASSWORD('新密码');”，即可。





# End

