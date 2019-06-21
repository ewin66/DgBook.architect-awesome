



---

# docker安装[mysql遇到的问题](https://blog.csdn.net/zhaokejin521/article/details/80468908)







mysql                             MySQL is a widely used, open-source relation…   8289                [OK]
mariadb                           MariaDB is a community-developed fork of MyS…   2834                [OK]
mysql/mysql-server                Optimized MySQL Server Docker images. Create…   619                                     [OK]
centurylink/mysql

​	docker pull mysql

 docker run -d -P mysql

 docker run -d -P mariadb

```
docker ps -a
CONTAINER ID        IMAGE                 COMMAND                  CREATED             STATUS                      PORTS               NAMES
f6aa9080e011        mysql                 "docker-entrypoint.s…"   11 seconds ago      Exited (1) 8 seconds ago                        elated_heisenberg
fe19135fd984        mariadb:10.2.16       "docker-entrypoint.s…"   24 hours ago        Exited (1) 23 hours ago                         mariadb
3dd3f2636fae        jenkins:2.60.3        "/bin/tini -- /usr/l…"   5 days ago          Exited (143) 5 days ago                         jenkins
03f00b234f26        elasticsearch:7.1.1   "/usr/local/bin/dock…"   5 days ago          Exited (78) 5 days ago                          elasticsearch
076d21ab539a        redis:latest          "docker-entrypoint.s…"   5 days ago          Exited (0) 29 seconds ago                       redis
```

```bash
docker ps -a
```

docker ps -a

CONTAINER ID        IMAGE                 COMMAND                  CREATED             STATUS                      PORTS               NAMES
f6aa9080e011        mysql                 "docker-entrypoint.s…"   11 seconds ago      Exited (1) 8 seconds ago                        elated_heisenberg
fe19135fd984        mariadb:10.2.16       "docker-entrypoint.s…"   24 hours ago        Exited (1) 23 hours ago                         mariadb
3dd3f2636fae        jenkins:2.60.3        "/bin/tini -- /usr/l…"   5 days ago          Exited (143) 5 days ago                         jenkins
03f00b234f26        elasticsearch:7.1.1   "/usr/local/bin/dock…"   5 days ago          Exited (78) 5 days ago                          elasticsearch
076d21ab539a        redis:latest          "docker-entrypoint.s…"   5 days ago          Exited (0) 29 seconds ago                       redis

```bash
 docker run --name mysql  -d -p 3306:3306 -e MYSQL_ROOT_PASSWORD=root mysql
  docker run --name mariadb  -d -p 3307:3307 -e MYSQL_ROOT_PASSWORD=root mariadb
  docker run --name mariadb  -d -p 3306:3306 -e MYSQL_ROOT_PASSWORD=root mariadb
```

 docker run --name mysql  -d -p 3306:3306 -e MYSQL_ROOT_PASSWORD=root mysql    	暂时pwd为root

1689416065b294a25a5b6562bc807c6ce11547a7ffac3c49eea82bb47381ddea

```bash
 docker exec -it mysql01 mysql -uroot -p123456
-- 下方为真的
 docker exec -it mysql mysql -uroot -proot
 ----
//  docker exec -it mysql mysql -uroot -pwsx1001
```

```bash
docker exec -it mysql mysql -uroot -proot
docker exec -it mariadb mariadb -uroot -proot
```

 docker exec -it mysql mysql -uroot -proot



mysql: [Warning] Using a password on the command line interface can be insecure.
Welcome to the MySQL monitor.  Commands end with ; or \g.
Your MySQL connection id is 11
Server version: 8.0.16 MySQL Community Server - GPL

Copyright (c) 2000, 2019, Oracle and/or its affiliates. All rights reserved.

Oracle is a registered trademark of Oracle Corporation and/or its
affiliates. Other names may be trademarks of their respective
owners.

Type 'help;' or '\h' for help. Type '\c' to clear the current input statement.

mysql> mysql -u root -p

```
select host,user,plugin,authentication_string from mysql.user;
```

mysql>  select host,user,plugin,authentication_string from mysql.user
+-----------+------------------+-----------------------+------------------------------------------------------------------------+
| host      | user             | plugin                | authentication_string                                                  |
+-----------+------------------+-----------------------+------------------------------------------------------------------------+
| %         | root             | caching_sha2_password | $A$005$z\V0jM0GxOB/^eNNgkRdCBMnmrybdXeH02Vfq9Rw..hR27FLrJdatP0KW7 |
| localhost | mysql.infoschema | caching_sha2_password | $A$005$THISISACOMBINATIONOFINVALIDSALTANDPASSWORDTHATMUSTNEVERBRBEUSED |
| localhost | mysql.session    | caching_sha2_password | $A$005$THISISACOMBINATIONOFINVALIDSALTANDPASSWORDTHATMUSTNEVERBRBEUSED |
| localhost | mysql.sys        | caching_sha2_password | $A$005$THISISACOMBINATIONOFINVALIDSALTANDPASSWORDTHATMUSTNEVERBRBEUSED |
| localhost | root             | caching_sha2_password | $A$005$dT0|_h?UXK0exg" xQwLSxPzKALDAGMVR9qOflTLYV3U1YQ4cvOwtpgt7Mn5 |
+-----------+------------------+-----------------------+------------------------------------------------------------------------+
5 rows in set (0.00 sec)



```
ALTER USER 'root'@'%' IDENTIFIED WITH mysql_native_password BY 'wsx1001';
```



mysql> ALTER USER 'root'@'%' IDENTIFIED WITH mysql_native_password BY 'wsx1001';
Query OK, 0 rows affected (0.02 sec)





ALTER USER 'root'@'%' IDENTIFIED WITH mysql_native_password BY 'newpassword'; #更新一下用户的密码 root用户密码为newpassword  



```html
ALTER USER 'root'@'%' IDENTIFIED WITH mysql_native_password BY 'wsx1001';

ALTER USER 'root'@'%' IDENTIFIED WITH mysql_native_password BY 'wsx1001';
```

---

---

---



# 	Docker安装[MariaDB](https://www.jianshu.com/p/7028681d7f0f)和Mysql



docker run -v /data/mysql/:/var/lib/mysql -p 3306:3306 -e MYSQL_ROOT_PASSWORD=root123  --privileged=true --restart unless-stopped   --name mariadbs -d mariadb:latest
 docker ps -a 查看运行中的镜像

作者：程序员豪哥

链接：https://www.jianshu.com/p/7028681d7f0f

来源：简书

简书著作权归作者所有，任何形式的转载都请联系作者获得授权并注明出处。





# Docker使用[MariaDB](https://blog.csdn.net/weixin_37272286/article/details/78016176)



# Docker[使用MariaDB](https://www.jianshu.com/p/32542630c2bd)



---



---





--





# -d mysql：需要启动的容器的名称





docker pull docker.io/mysql:8.0.15

2.5：使用 docker run 启动镜像，以Mysql为例，出现下方字符串，启动成功

    #  docker run --name mysql -p 3306:3306 -v /mysql/datadir:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=123456  -d mysql
    787b06122b723e547b1216d499b69d99725a703217cd09f49e9ce548f9ba27dc

--name: 以什么名字启动容器

-p 3306:3306 :将容器端口映射到服务器端口

-v /mysql/datadir:/var/lib/mysql :将mysql的配置路径映射到本地datadir上

-e MYSQL_ROOT_PASSWORD=123456 :设置服务器密码为123456





docker run --name mysql -p 3306:3306   -e MYSQL_ROOT_PASSWORD=wsx1001  -d mysql



787b06122b723e547b1216d499b69d99725a703217cd09f49e9ce548f9ba27dc

-d mysql：需要启动的容器的名称
--------------------- 
作者：mdw5521 
来源：CSDN 
原文：https://blog.csdn.net/mdw5521/article/details/79174094 
版权声明：本文为博主原创文章，转载请附上博文链接！







---

---

---











[docker@xcbeyond mysql]$ 

docker run -p 3306:3306 --name mysql -v $PWD/conf:/etc/mysql/conf.d -v $PWD/logs:/logs -v $PWD/data:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=wsx1001  -d mysql:8.0.11





docker run -p 3306:3306 --name mysql  -e MYSQL_ROOT_PASSWORD=wsx1001  -d mysql:8.0.11



306803948d307424b509abde434a9972b239a02aa0a8ba0945b05b0052613372
[docker@xcbeyond mysql]$ ll
total 12
drwxr-xr-x 2 root root 4096 Aug 31 23:39 conf
drwxr-xr-x 2 polkitd input 4096 Aug 31 23:39 data
drwxr-xr-x 2 root root 4096 Aug 31 23:39 logs



