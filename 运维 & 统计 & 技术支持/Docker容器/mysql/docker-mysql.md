

# Docker for Mysql-[DgBook]

## 连招

docker search mysql 
docker pull mysql:8.0.11 
docker run --name mysql  -d -p 63306:63306 -e MYSQL_ROOT_PASSWORD=root mysql 
docker exec -it mysql mysql -uroot -proot 

> docker  						 exec -it mysql5.7 bash
> root@9e1dcf8298ce:/#  mysql -u root -p

mysql> select host,user,plugin,authentication_string from mysql.user; 
mysql> ALTER USER 'root'@'%' IDENTIFIED WITH mysql_native_password BY 'wsx1001';  
mysql>  FLUSH PRIVILEGES;

## 分解



###	docker search mysql

​			docker[安装mysql](https://blog.csdn.net/belvine/article/details/89553972)，设置mysql初始密码

###	docker pull mysql

​		docker pull mysql

or	docker pull mysql:8.0.11

```bash
 docker pull mysql
Using default tag: latest
latest: Pulling from library/mysql
Digest: sha256:415ac63da0ae6725d5aefc9669a1c02f39a00c574fdbc478dfd08db1e97c8f1b
Status: Image is up to date for mysql:latest
```

PS C:\WINDOWS\system32> docker pull mysql:8.0.11
8.0.11: Pulling from library/mysql
Digest: sha256:ffa442557c7a350939d9cd531f77d6cbb98e868aeb4a328289e0e5469101c20e
Status: Image is up to date for mysql:8.0.11

### docker run             mysql

--name mysql

```bash
docker run --name mysql	-d -p 63306:63306 -e MYSQL_ROOT_PASSWORD=root mysql
```



PS C:\WINDOWS\system32> docker run -d -p 63306:63306 -e MYSQL_ROOT_PASSWORD=root mysql



docker run -d -p 3366:3366-e MYSQL_ROOT_PASSWORD=root mysql

> 2e70f8849e9e9675a07ef01847d70eb18bba5f45bbb073e8882a7a9eeaae522b	【不用重命名】

PS C:\WINDOWS\system32> docker run --name mysql  -d -p 63306:63306 -e MYSQL_ROOT_PASSWORD=root mysql

> 5e60576203f417f71ae90b86240b8174f9b5674ea6ff9ac191e4cd72b01ac3ed	【重命名】

### docker exec  登录上

PS C:\WINDOWS\system32> docker exec -it mysql mysql -uroot -proot

```bash
[root@bogon ~]# docker exec -it mysql5.7 bash
root@9e1dcf8298ce:/#  mysql -u root -p

root	wsx1001
```



###	参数

> -e MYSQL_USER="woniu"  ：添加woniu用户
>
> -e MYSQL_PASSWORD="123456"：设置添加的用户密码
>
> -e MYSQL_ROOT_PASSWORD="123456"：设置root用户密码
>
> --character-set-server=utf8：设置字符集为utf8
>
> --collation-server=utf8_general_cli：设置字符比较规则为utf8_general_cli
>
> --privileged=true：容器内的**root拥有真正root权限**，否则容器内root只是外部普通用户权限
>
> -v /docker/mysql/conf/my.cnf:/etc/my.cnf：映射配置文件
>
> -v /docker/mysql/data:/var/lib/mysql：映射数据目录 
>
> 
>
> 作者：码农code之路 
> 来源：CSDN 
> 原文：https://blog.csdn.net/woniu211111/article/details/80968154 
> 版权声明：本文为博主原创文章，转载请附上博文链接！



PS C:\WINDOWS\system32> docker exec -it mysql mysql -uroot -proot
mysql: [Warning] Using a password on the command line interface can be insecure.
Welcome to the MySQL monitor.  Commands end with ; or \g.
Your MySQL connection id is 8
Server version: 8.0.16 MySQL Community Server - GPL

Copyright (c) 2000, 2019, Oracle and/or its affiliates. All rights reserved.

Oracle is a registered trademark of Oracle Corporation and/or its
affiliates. Other names may be trademarks of their respective
owners.

Type 'help;' or '\h' for help. Type '\c' to clear the current input statement.

mysql>

### 看机	用户等信息



mysql> select host,user,plugin,authentication_string from mysql.user;
+-----------+------------------+-----------------------+------------------------------------------------------------------------+

| host | user | plugin | authentication_string |
| ---- | ---- | ------ | --------------------- |
|      |      |        |                       |
+-----------+------------------+-----------------------+------------------------------------------------------------------------+
| %         | root             | caching_sha2_password | $A$005$|.R-PeX
UMM^h9`{gr0hP1L0XBf7/eNO0jVrYO686U919as4FXBixj.4l8MD |
| localhost | mysql.infoschema | caching_sha2_password | $A$005$THISISACOMBINATIONOFINVALIDSALTANDPASSWORDTHATMUSTNEVERBRBEUSED |
| --------- | ---------------- | --------------------- | ------------------------------------------------------------ |
|           |                  |                       |                                                              |
| localhost | mysql.session | caching_sha2_password | $A$005$THISISACOMBINATIONOFINVALIDSALTANDPASSWORDTHATMUSTNEVERBRBEUSED |
| --------- | ------------- | --------------------- | ------------------------------------------------------------ |
|           |               |                       |                                                              |
| localhost | mysql.sys | caching_sha2_password | $A$005$THISISACOMBINATIONOFINVALIDSALTANDPASSWORDTHATMUSTNEVERBRBEUSED |
| --------- | --------- | --------------------- | ------------------------------------------------------------ |
|           |           |                       |                                                              |
| localhost | root | caching_sha2_password | $A$005$+_t~4GkJRg<e:z+HfkokKsy7dMx.BhYbQCR9GIdfoBUMJ4FUBszWz5LGMT0t8 |
| --------- | ---- | --------------------- | ------------------------------------------------------------ |
|           |      |                       |                                                              |
+-----------+------------------+-----------------------+------------------------------------------------------------------------+
5 rows in set (0.00 sec)

###	mysql日志查看

```bash
docker logs mysql5.7
```



### 修改密码

mysql> ALTER USER 'root'@'%' IDENTIFIED WITH mysql_native_password BY 'wsx1001';
Query OK, 0 rows affected (0.02 sec)

mysql>  FLUSH PRIVILEGES;
Query OK, 0 rows affected (0.09 sec)











---

---





###	获取镜像

docker pull mysql

docker pull mysql:8.0.15

### 使用镜像

docker run -d -P mysql

docker ps -a 发现没有启动	查看CONTAINER 

docker logs 10

docker run -d -P -e MYSQL_ROOT_PASSWORD=root mysql



docker run --name mysql  -d -p 3366:3366 -e MYSQL_ROOT_PASSWORD=root mysql    

[

docker run --name mysql  -d -p 63306:63306 -e MYSQL_ROOT_PASSWORD=root mysql

sudo docker run -d -p 3306:3306 -v /home/walter/softwares/tutum-docker-mysql/data:/var/lib/mysql -e MYSQL_PASS="mypass" tutum/mysql

```
-v /docker/mysql/config/my.cnf:/etc/my.cnf -v /docker/mysql/data:/var/lib/mysql
```



docker run -d --name mysql --net=backend -p 63306:63306 -e MYSQL_ROOT_PASSWORD=root -v /docker/mysql/config/my.cnf:/etc/my.cnf -v /docker/mysql/data:/var/lib/mysql

docker run -d --name mysql --net=backend -p 63306:63306 -e MYSQL_ROOT_PASSWORD=root -v /f/docker_volumn/mysql8015/data:/var/lib/mysql -v /f/docker_volumn/mysql8015/conf:/etc/mysql/conf.d org.pzy/mysql8.0.15

]

**docker run -p 3306:3306 --name mysql -v  /usr/local/mysql/my.cnf:/etc/mysql/my.cnf -v /usr/local/mysql/logs:/logs  -v /usr/local/mysql/data:/mysql_data -e MYSQL_ROOT_PASSWORD=root -d  mysql:5.6**

- **-p 3306:3306：**将容器的3306端口映射到主机的3306端口
- **-v $PWD/conf/my.cnf:/etc/mysql/my.cnf：**将主机当前目录下的conf/my.cnf挂载到容器的/etc/mysql/my.cnf
- **-v $PWD/logs:/logs：**将主机当前目录下的logs目录挂载到容器的/logs
- **-v $PWD/data:/mysql_data：**将主机当前目录下的data目录挂载到容器的/mysql_data
- **-e MYSQL_ROOT_PASSWORD=123456：**初始化root用户的密码

sudo docker rename modest_goldberg mysql



sudo docker run --name mysql_client --link mysql:db -d mysql



##	docker run 命令参数

-d 后台运行

-p 暴露端口

-e 设置环境变量，与在dockerfile env设置相同效果

--name 设置namne

https://hub.docker.com/_/mysql/ 
http://blog.csdn.net/wongson/article/details/49077353  
作者：芋智波佐助 
来源：CSDN 
原文：https://blog.csdn.net/u011686226/article/details/52815788 
版权声明：本文为博主原创文章，转载请附上博文链接！

---

##	 windows docker进行[数据库卷挂载](https://www.jianshu.com/p/5d554c09f1dc)



https://www.jianshu.com/p/5d554c09f1dc

```bash
docker run -d --name mysql --net=backend -p 63306:63306 -e MYSQL_ROOT_PASSWORD=root -v /f/docker_volumn/mysql57/data:/var/lib/mysql -v /f/docker_volumn/mysql57/conf:/etc/mysql/conf.d org.pzy/mysql8.0.15
```

docker run -d --name mysql --net=backend -p 63306:63306 -e MYSQL_ROOT_PASSWORD=root -v /f/docker_volumn/mysql57/data:/var/lib/mysql -v /f/docker_volumn/mysql57/conf:/etc/mysql/conf.d org.pzy/mysql



##	挂载外部配置和数据安装

1.创建目录和配置文件my.cnf

    mkdir /docker
    mkdir /docker/mysql
    mkdir /docker/mysql/conf
    mkdir /docker/mysql/data
     
    创建my.cnf配置文件
    touch /docker/mysql/conf/my.cnf
     
    my.cnf添加如下内容：
    [mysqld]
    user=mysql
    character-set-server=utf8
    default_authentication_plugin=mysql_native_password
    [client]
    default-character-set=utf8
    [mysql]
    default-character-set=utf8

2.创建容器，并后台启动

docker run -d -p 3306:3306 --privileged=true -v /docker/mysql/conf/my.cnf:/etc/mysql/my.cnf -v /docker/mysql/data:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=123456 --name mysqltest2 mysql:5.7

参数说明：

--privileged=true：容器内的root拥有真正root权限，否则容器内root只是外部普通用户权限

-v /docker/mysql/conf/my.cnf:/etc/my.cnf：映射配置文件

-v /docker/mysql/data:/var/lib/mysql：映射数据目录 





作者：码农code之路 
来源：CSDN 
原文：https://blog.csdn.net/woniu211111/article/details/80968154 
版权声明：本文为博主原创文章，转载请附上博文链接！











----



# mysql 修改  user	/root


```bash
mysql>use mysql;
mysql>update  user set host = '%' where user= 'root';
mysql>selecthost, userfromuser;
```
> (docker-[compose](https://docs.docker.com/compose/reference/start/)，也是这样)。  [Docker Compose](https://docs.docker.com/compose/) 
> 这时想到，我是通过挂载数据卷的方式启动的mysql镜像。那么在镜像中的数据库就是使用我本地的数据库文件，也就是说在启动的时候我没有必要指定root用户密码， 
>  docker之mysql镜像使用 
> https://blog.csdn.net/u010846177/article/details/54356670



```
ALTER USER root@localhost IDENTIFIED WITH mysql_native_password BY 'root';

ALTER USER root@localhost IDENTIFIED WITH mysql_native_password BY 'wsx1001';

```

# Mysql8.0安装步骤

# [在Dockerfile中配置了ROOT密码为什么还是显示密码为空](https://segmentfault.com/q/1010000011389249)

`docker run -e MYSQL_ROOT_PASSWORD=123456`





```
docker run --name mysql -e MYSQL_ROOT_PASSWORD=123456 -p 3307:3306 -d mysql
```



docker run --name mysql58 -e MYSQL_ROOT_PASSWORD=123456 -p 3306:3306 -d mysql

docker pull  mysql/mysql-server : 8.0.15 

docker run --name mysql58 -d -p 3306:3306 
mysql/mysql-server:8.0.15  -- character-set-server=utf8mb4 
-- collation-server=utf8mb4_general_ci

docker exec -it mysql58 bash









---

#   [文件挂载和MySQL字符集设置](https://www.cnblogs.com/trydoit/p/7129039.html)



**docker  run -p 3306:3306 --name mysql -v  /usr/local/mysql/my.cnf:/etc/mysql/my.cnf -v /usr/local/mysql/logs:/logs  -v /usr/local/mysql/data:/mysql_data -e MYSQL_ROOT_PASSWORD=root -d  mysql:5.6**

- **-p 3306:3306：**将容器的3306端口映射到主机的3306端口
- **-v $PWD/conf/my.cnf:/etc/mysql/my.cnf：**将主机当前目录下的conf/my.cnf挂载到容器的/etc/mysql/my.cnf
- **-v $PWD/logs:/logs：**将主机当前目录下的logs目录挂载到容器的/logs
- **-v $PWD/data:/mysql_data：**将主机当前目录下的data目录挂载到容器的/mysql_data
- **-e MYSQL_ROOT_PASSWORD=123456：**初始化root用户的密码

 

 

**show variables like'character%';  # 查看MySQL的字符集**

+--------------------------+----------------------------+
| Variable_name            | Value                      |
+--------------------------+----------------------------+
| character_set_client     | latin1                     |
| character_set_connection | latin1                     |
| character_set_database   | utf8                       |
| character_set_filesystem | binary                     |
| character_set_results    | latin1                     |
| character_set_server     | utf8                       |
| character_set_system     | utf8                       |
| character_sets_dir       | /usr/share/mysql/charsets/ |
+--------------------------+----------------------------+

 

**修改字MySQL字符集    vim /etc/my.cnf**

```
[client]
default-character-set=utf8
```

 

```
[mysql]
default-character-set=utf8
```

 

```
[mysqld]
init_connect='SET collation_connection = utf8_unicode_ci'
init_connect='SET NAMES utf8'
character-set-server=utf8
collation-server=utf8_unicode_ci
skip-character-set-client-handshake
```



---





# [安装mysql 并将文件挂载到本地](https://www.cnblogs.com/chongyao/p/9112119.html)







首先准备好挂载的文件路径

执行mysql创建以及挂载的命令（这里还可以使用-e环境变量来创建新用户MYSQL_USER，MYSQL_PASSWORD）

```
docker run -d -p 3306:3306 --restart always -e MYSQL_ROOT-PASSWORD="root12345" --name db-mysql -v /docker/mysql/config/my.cnf:/etc/my.cnf -v /docker/mysql/data:/var/lib/mysql mysql/mysql-server  
```

 

执行上面的语句可能会发现以下问题（1注意检查挂载的路径和文件是否创建2.注意检查挂载的文件是否为文件格式而不是目录）

过程碰到的问题报错如下：大致意思是您挂载的文件是个cnf目录文件而您挂载的目标文件是个cnf文件。 

[![复制代码](assets/copycode-1561204667465.gif)](javascript:void(0);)

```
docker: Error response from daemon: OCI runtime create failed: container_linux.go:348: starting container process caused "process_linux.go:402: container init caused \"rootfs_linux.go:58: mounting \\\"/docker/mysql/config/my.cnf\\\" to rootfs \\\"/var/lib/docker/overlay2/a745e213e304a1aa8d7b237b6193b95f8b376472196bf3a0646922491aef141b/merged\\\" at \\\"/var/lib/docker/overlay2/a745e213e304a1aa8d7b237b6193b95f8b376472196bf3a0646922491aef141b/merged/etc/my.cnf\\\" caused \\\"not a directory\\\"\"": unknown: Are you trying to mount a directory onto a file (or vice-versa)? Check if the specified host path exists and is the expected type.
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

解决方案 

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
rm -rf my.cnf/
vim my.cnf
#insert 
[mysqld]
user=mysql  
然后运行步骤二语句
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 修改字符集

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
修改my.cnf 文件的内容达到修改字符集的目的
vim my.cnf
#insert
character-set-server=utf8
[client]
default-character-set=utf8
[mysql]
default-character-set=utf8
#退出restart mysql
docker restart db-mysql
#查看修改的字符集
#首先进入bash，这里需要注意如果进入bash的时候root密码没有生效则需要去docker logs db-mysql 中查看创建的默认密码
然后凭借默认密码进入到mysql环境。进入环境第一件事就是修改root用户的密码：ALTER USER 'root'@'localhost' IDENTIFIED BY '123456';
（docker exec -it db-mysql mysql -uroot -proot12345）
docker exec -it db-mysql bash
mysql -uroot -p
#输入用户名密码
use mysql
#查看用户权限
select user,host from user      
#查看字符集                    
show variables like '%char%'
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

创建新用户 

```
mysql> CREATE USER 'cyao'@'localhost' IDENTIFIED BY 'pwd123456';
mysql> GRANT ALL PRIVILEGES ON *.* TO 'cyao'@'localhost' WITH GRANT OPTION;
mysql> CREATE USER 'test'@'%' IDENTIFIED BY 'pwd123456';
mysql> GRANT ALL PRIVILEGES ON *.* TO 'test'@'%'
    ->     WITH GRANT OPTION;
```

 关于挂载有一下几个地方需要注意

```
    1.挂载之前my.cnf 必须要配置一个默认用户[mysqld] user=mysql
    2.挂载之前本地/docker/mysql/data这个路径下不能有其他文件或者文件夹
```



----

# windows docker进行数据库卷挂载



```
docker run -d --name mysql57 --net=backend -p 3306:3306 -e MYSQL_ROOT_PASSWORD=root -v /f/docker_volumn/mysql57/data:/var/lib/mysql -v /f/docker_volumn/mysql57/conf:/etc/mysql/conf.d org.pzy/mysql5.7:1.0
```

盘符的书写方式应该为: `/f/...`, `/d/...`

需要在docker中将指定盘符设置为共享(设置时会要求输入当前的用户密码,如果当前用户未使用密码,则需要先设置windows的登录密码)



![img](https:////upload-images.jianshu.io/upload_images/1890600-635aa61f5d7d327f.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1000)

image.png

小礼物走一走，来简书关注我

作者：码农梦醒

链接：https://www.jianshu.com/p/5d554c09f1dc

来源：简书

简书著作权归作者所有，任何形式的转载都请联系作者获得授权并注明出处。





#	EFCore

Pomelo.EntityFrameworkCore.MySql	和 
Pomelo.EntityFrameworkCore.MySql.Design		两个包，以便支持MySql

Install-Package Microsoft.EntityFrameworkCore.Tools

utf8_general_ci。虽然Pomelo建议选择utf8mb4

“Server=localhost;database=simplecmswithabp;uid=root;pwd=abcd-1234;charset=UTF8;”



# end



