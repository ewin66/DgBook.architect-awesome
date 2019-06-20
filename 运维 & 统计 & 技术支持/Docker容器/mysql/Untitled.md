# Docker设置Mysql密码&&启动

    使用mysql镜像

 


    runoob@runoob:~/mysql$ docker run -p 3306:3306 --name mymysql -v $PWD/conf:/etc/mysql/conf.d -v $PWD/logs:/logs -v $PWD/data:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=123456 -d mysql:5.6
    21cb89213c93d805c5bacf1028a0da7b5c5852761ba81327e6b99bb3ea89930e
    runoob@runoob:~/mysql$ 

命令说明：

    -p 3306:3306：将容器的 3306 端口映射到主机的 3306 端口。
    
    -v -v $PWD/conf:/etc/mysql/conf.d：将主机当前目录下的 conf/my.cnf 挂载到容器的 /etc/mysql/my.cnf。
    
    -v $PWD/logs:/logs：将主机当前目录下的 logs 目录挂载到容器的 /logs。
    
    -v $PWD/data:/var/lib/mysql ：将主机当前目录下的data目录挂载到容器的 /var/lib/mysql 。
    
    -e MYSQL_ROOT_PASSWORD=123456：初始化 root 用户的密码。

查看容器启动情况

    runoob@runoob:~/mysql$ docker ps 
    CONTAINER ID    IMAGE         COMMAND                  ...  PORTS                    NAMES

---------------------
作者：哟哟哟_蒋大傻 
来源：CSDN 
原文：https://blog.csdn.net/qq_33997121/article/details/83689068 
版权声明：本文为博主原创文章，转载请附上博文链接！



----

# [修改 Docker-MySQL 容器的 默认用户加密规则](https://www.cnblogs.com/atuotuo/p/9402132.html)













---

